

dotnet test JobsOpenings.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=coverage.opencover.xml

reportgenerator -reports:coverage.opencover.xml -targetdir:report

start report\index.htm
