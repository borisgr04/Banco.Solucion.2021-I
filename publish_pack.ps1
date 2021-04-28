$version="0.1.48"
dotnet build -p:Version=$version
dotnet publish -c release -o publish
Compress-Archive -Path .\publish\* -DestinationPath .\"publish$version".zip
