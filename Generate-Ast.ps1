$outputDir = "$PSScriptRoot/src/cslox/AbstractSyntaxTree";
$namespace = "cslox.AbstractSyntaxTree";

$keywords = [System.Collections.Immutable.ImmutableHashSet]::Create("if", "operator", "return", "var", "while");

function DefineAst{ param([string]$baseName, [object[]]$types)
    $path = "$outputDir/$baseName.cs";

    FileHeader -path $path;
    "public abstract class $baseName" | Out-File $path -Encoding utf8 -Append;
    "{" | Out-File $path -Encoding utf8 -Append;
    "    public abstract TResult Accept<TResult>(I$($baseName)Visitor<TResult> visitor);" | Out-File $path -Encoding utf8 -Append;
    "}" | Out-File $path -Encoding utf8 -Append;
    UpdateHeaderWithMetadata -path $path;

    DefineVisitor -path "$outputDir/I$($baseName)Visitor.cs" -baseName $baseName -types $types;

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

    $content = "";
    if (Test-Path $path) {
        $content = Get-Content -Path $path -Encoding utf8;
        $indexOfFirstBlankLine = $content.IndexOf("");
        if ($indexOfFirstBlankLine -gt 0) {
            $content = $content[0..($indexOfFirstBlankLine)] -join "`n"
        }
    }

    "$($content)namespace $namespace;" | Out-File $path -Encoding utf8 -Force;
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
    "    public override TResult Accept<TResult>(I$($baseName)Visitor<TResult> visitor)" | Out-File $path -Encoding utf8 -Append;
    "        => visitor.Visit$className$baseName(this);" | Out-File $path -Encoding utf8 -Append;

    "}" | Out-File $path -Encoding utf8 -Append;
    UpdateHeaderWithMetadata -path $path;
}

function DefineVisitor{ param ([string]$path, [string]$baseName, [object[]]$types)
    FileHeader -path $path;

    "public interface I$($baseName)Visitor<TResult>" | Out-File $path -Encoding utf8 -Append;
    "{" | Out-File $path -Encoding utf8 -Append;

    foreach ($type in $types) {
        $typeParts = $type -split ":";
        $className = $typeParts[0].Trim();
        $argName = AsIdentifier -name $className
        "        TResult Visit$className$baseName($className $argName);" | Out-File $path -Encoding utf8 -Append;
    }

    "}" | Out-File $path -Encoding utf8 -Append;

    UpdateHeaderWithMetadata -path $path;
}

function AsIdentifier([string]$name) {
    $name = $name.Trim();
    $name = $name[0].ToString().ToLowerInvariant() + $name.Substring(1);
    if ($keywords.Contains($name)) {
        return "@" + $name;
    }
    return $name;
}

function UpdateHeaderWithMetadata([string]$path){
    $content = Get-Content -Path $path -Encoding utf8
    $indexOfFirstBlankLine = $content.IndexOf("");
    $header = "";
    if ($indexOfFirstBlankLine -gt 0) {
        $header = $content[0..($indexOfFirstBlankLine)]
        $content = $content[($indexOfFirstBlankLine - 1)..($content.Length - 1)] -join "`n"
    }

    $existingHashLine = $header | Select-String -Pattern "^// Hash: (.*)$"
    $existingHash = ""
    if ($existingHashLine) {
        $existingHash = $existingHashLine.Matches.Groups[1].Value
    }

    $dateCreatedLine = $header | Select-String -Pattern "^// Date Created: (.*)$"
    $dateCreated = (Get-Date -AsUTC).ToString("yyyy-MM-dd HH:mm:ss") + " UTC";
    if ($dateCreatedLine) {
        $dateCreated = $dateCreatedLine.Matches.Groups[1].Value
    }

    $lastUpdatedLine = $header | Select-String -Pattern "^// Last Updated: (.*)$"
    $lastUpdated = (Get-Date -AsUTC).ToString("yyyy-MM-dd HH:mm:ss") + " UTC";
    if ($lastUpdatedLine) {
        $lastUpdated = $lastUpdatedLine.Matches.Groups[1].Value
    }

    $contentBytes = [System.Text.Encoding]::UTF8.GetBytes($content);
    $hashBytes = [System.Security.Cryptography.SHA256]::Create().ComputeHash($contentBytes);
    $hash = [System.BitConverter]::ToString($hashBytes).Replace("-", "").ToLowerInvariant();

    if ($existingHash -ne $hash) {
        $lastUpdated = (Get-Date -AsUTC).ToString("yyyy-MM-dd HH:mm:ss") + " UTC";
    }

    "// *****************************************" | Out-File $path -Encoding utf8 -Force;
    "// ** AUTO GENERATED FILE - DO NOT MODIFY **" | Out-File $path -Encoding utf8 -Append;
    "// *****************************************" | Out-File $path -Encoding utf8 -Append;
    "//" | Out-File $path -Encoding utf8 -Append;
    "// Hash: $hash" | Out-File $path -Encoding utf8 -Append;
    "// Date Created: $dateCreated" | Out-File $path -Encoding utf8 -Append;
    "// Last Updated: $lastUpdated" | Out-File $path -Encoding utf8 -Append;
    "// --------------------------------------------------------------------------------" | Out-File $path -Encoding utf8 -Append;
    "" | Out-File $path -Encoding utf8 -Append;
    $content | Out-File $path -Encoding utf8 -Append;
}

Clear-Host
if (-not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir;
}

DefineAst -baseName "Expr" -types @(
    "Assign   : Token Name, Expr Value",
    "Binary   : Expr Left, Token Operator, Expr Right",
    "Call     : Expr Callee, Token Paren, List<Expr> Arguments",
    "Grouping : Expr Expression",
    "Literal  : object? Value",
    "Logical  : Expr Left, Token Operator, Expr Right",
    "Unary    : Token Operator, Expr Right",
    "Variable : Token Name"
    );

DefineAst -baseName "Stmt" -types @(
    "Block      : List<Stmt> Statements",
    "Expression : Expr InnerExpression",
    "Function   : Token Name, List<Token> Parameters, List<Stmt> Body",
    "If         : Expr Condition, Stmt ThenBranch, Stmt? ElseBranch",
    "Print      : Expr Expression",
    "Return     : Token keyword, Expr? Value",
    "Var        : Token Name, Expr? Initializer",
    "While      : Expr Condition, Stmt Body"
    );
