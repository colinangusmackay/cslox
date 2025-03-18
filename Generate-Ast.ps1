$outputDir = "$PSScriptRoot/src/cslox/Expressions";
$namespace = "cslox.Expressions";

function DefineAst{ param([string]$baseName, [object[]]$types)
    $path = "$outputDir/$baseName.cs";

    FileHeader -path $path;
    "public abstract class $baseName" | Out-File $path -Encoding utf8 -Append;
    "{" | Out-File $path -Encoding utf8 -Append;
    "    public abstract TResult Accept<TResult>(IVisitor<TResult> visitor);" | Out-File $path -Encoding utf8 -Append;
    "}" | Out-File $path -Encoding utf8 -Append;

    DefineVisitor -path "$outputDir/IVisitor.cs" -baseName $baseName -types $types;

    foreach ($type in $types) {
        $typeParts = $type -split ":";
        $className = $typeParts[0].Trim();
        $fields = $typeParts[1].Trim();

        $typePath = "$outputDir/$className.cs";
        DefineType -path $typePath -baseName $baseName -className $className -fieldList $fields;
    }
}

function FileHeader([string]$path) {
    Write-Host "Generating $path";
    "// AUTO GENERATED FILE" | Out-File $path -Encoding utf8 -Force;
    "" | Out-File $path -Encoding utf8 -Append;
    "namespace $namespace;" | Out-File $path -Encoding utf8 -Append;
    "" | Out-File $path -Encoding utf8 -Append;
}

function DefineType{ param ([string]$path, [string]$baseName, [string]$className, [object[]]$fieldList)
    FileHeader -path $path;
    "public class $className : $baseName" | Out-File $path -Encoding utf8 -Append;
    "{" | Out-File $path -Encoding utf8 -Append;

    $fields = $fieldList -split ", ";

    "    public $classname(" | Out-File $path -Encoding utf8 -Append -NoNewline;
    $isFirst = $true;
    foreach ($field in $fields) {
        $fieldParts = $field -split " ";
        $type = $fieldParts[0].Trim();
        $name = $fieldParts[1].Trim();
        $argName = AsIdentifier -name $name
        if ($isFirst) {
            $isFirst = $false;
        } else {
            ", " | Out-File $path -Encoding utf8 -Append -NoNewline;
        }
        "$type $argName" | Out-File $path -Encoding utf8 -Append -NoNewline;
    }
    ")" | Out-File $path -Encoding utf8 -Append;

    "    {" | Out-File $path -Encoding utf8 -Append;
    foreach ($field in $fields) {
        $fieldParts = $field -split " ";
        $propertyName = $fieldParts[1].Trim();
        $argName = AsIdentifier -name $propertyName
        "        $propertyName = $argName;" | Out-File $path -Encoding utf8 -Append;
    }
    "    }" | Out-File $path -Encoding utf8 -Append;

    foreach ($field in $fields) {
        $fieldParts = $field -split " ";
        $type = $fieldParts[0].Trim();
        $name = $fieldParts[1].Trim();

        "" | Out-File $path -Encoding utf8 -Append;
        "    public $type $name { get; }" | Out-File $path -Encoding utf8 -Append;
    }

    "" | Out-File $path -Encoding utf8 -Append;
    "    public override TResult Accept<TResult>(IVisitor<TResult> visitor)" | Out-File $path -Encoding utf8 -Append;
    "        => visitor.Visit$className$baseName(this);" | Out-File $path -Encoding utf8 -Append;

    "}" | Out-File $path -Encoding utf8 -Append;
}

function DefineVisitor{ param ([string]$path, [string]$baseName, [object[]]$types)
    FileHeader -path $path;

    "public interface IVisitor<TResult>" | Out-File $path -Encoding utf8 -Append;
    "{" | Out-File $path -Encoding utf8 -Append;

    foreach ($type in $types) {
        $typeParts = $type -split ":";
        $className = $typeParts[0].Trim();
        $argName = $className.ToLowerInvariant();
        "        TResult Visit$className$baseName($className $argName);" | Out-File $path -Encoding utf8 -Append;
    }

    "}" | Out-File $path -Encoding utf8 -Append;
}

function AsIdentifier([string]$name) {
    $name = $name.ToLowerInvariant();
    switch ($name) {
        "operator" { return "@$name"; }
        Default {return $name;}
    }
}

Clear-Host
if (-not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir;
}

DefineAst -baseName "Expr" -types @(
    "Binary   : Expr Left, Token Operator, Expr Right",
    "Grouping : Expr Expression",
    "Literal  : object Value",
    "Unary    : Token Operator, Expr Right"
    );


