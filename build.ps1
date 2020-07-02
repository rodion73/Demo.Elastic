[CmdletBinding()]
param (
    [Parameter(Position = 0)]
    [ValidateSet('build', 'test', 'clean', 'publish')]
    [string]
    $Target = 'build',
    [Parameter()]
    [ValidateSet('Debug', 'Release')]
    $Config = 'Debug'
)

if($Target -eq 'clean') {
    git clean -dfx -e .vs -e .vscode
    return
}

if($Target -eq 'build' -or $Target -eq 'test') {
    dotnet restore
    dotnet build --no-restore -c $Config

    if($Target -eq 'test') {
        dotnet test --no-build -c $Config
        dotnet tool restore

        dotnet tool run reportgenerator `
            -reports:**\coverage.cobertura.xml `
            -targetdir:.coverage

        Start-Process '.coverage\index.htm'
    }

    return
}
