$outputDir = "$PSScriptRoot/src/cslox";

function DefineAst{ param([string]$baseName, [object[]]$types)
    $path = "$outputDir/$baseName.cs";

    Write-Host "Generating $path";

    "// AUTO GENERATED FILE" | Out-File $path -Encoding utf8 -Force;
    "" | Out-File $path -Encoding utf8 -Append;
    "namespace cslox;" | Out-File $path -Encoding utf8 -Append;
    "" | Out-File $path -Encoding utf8 -Append;
    "public abstract class $baseName" | Out-File $path -Encoding utf8 -Append;
    "{" | Out-File $path -Encoding utf8 -Append;

    foreach ($type in $types) {
        $typeParts = $type -split ":";
        $className = $typeParts[0].Trim();
        $fields = $typeParts[1].Trim();

        DefineType -path $path -baseName $baseName -className $className -fieldList $fields;
    }

    "}" | Out-File $path -Encoding utf8 -Append;
}

function DefineType{ param ([string]$path, [string]$baseName, [string]$className, [object[]]$fieldList)
    "" | Out-File $path -Encoding utf8 -Append;
    "    public class $className : $baseName" | Out-File $path -Encoding utf8 -Append;
    "    {" | Out-File $path -Encoding utf8 -Append;

    $fields = $fieldList -split ", ";

    "        public $classname(" | Out-File $path -Encoding utf8 -Append -NoNewline;
    $isFirst = $true;
    foreach ($field in $fields) {
        $fieldParts = $field -split " ";
        $type = $fieldParts[0].Trim();
        $name = $fieldParts[1].Trim();
        $argName = $name.ToLowerInvariant();
        if ($isFirst) {
            $isFirst = $false;
        } else {
            ", " | Out-File $path -Encoding utf8 -Append -NoNewline;
        }
        "$type $argName" | Out-File $path -Encoding utf8 -Append -NoNewline;
    }
    ")" | Out-File $path -Encoding utf8 -Append;

    "        {" | Out-File $path -Encoding utf8 -Append;
    foreach ($field in $fields) {
        $fieldParts = $field -split " ";
        $propertyName = $fieldParts[1].Trim();
        $argName = $propertyName.ToLowerInvariant();
        "            $propertyName = $argName;" | Out-File $path -Encoding utf8 -Append;
    }
    "        }" | Out-File $path -Encoding utf8 -Append;

    foreach ($field in $fields) {
        $fieldParts = $field -split " ";
        $type = $fieldParts[0].Trim();
        $name = $fieldParts[1].Trim();

        "" | Out-File $path -Encoding utf8 -Append;
        "        public $type $name { get; }" | Out-File $path -Encoding utf8 -Append;
    }

    "    }" | Out-File $path -Encoding utf8 -Append;
}

Clear-Host
DefineAst -baseName "Expr" -types @(
    "Binary   : Expr Left, Token Operator, Expr Right",
    "Grouping : Expr Expression",
    "Literal  : object Value",
    "Unary    : Token Operator, Expr Right"
    );


