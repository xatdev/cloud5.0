#Remove obj folders, because there are conflicts if built with Visual Studio before
Remove-Item .\obj\** -Recurse -Verbose

dotnet restore
#Builds the *.sln file in the same directory
dotnet build -c Release
#nuget pack .\RobCat\RobCat.csproj -Tool -OutputDirectory .\RobCat\bin\Release -Prop Platform=AnyCpu -Prop Configuration=Release
#nuget pack .\RobCatResultCleaner\RobCatResultCleaner.csproj -Tool -OutputDirectory .\RobCatResultCleaner\bin\Release -Prop Platform=AnyCpu -Prop Configuration=Release
#Shutdown dotnet build servers, to omit problems with kept references to nuget package Nerdbank.GitVersioning
dotnet build-server shutdown
