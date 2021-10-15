rd /s /q "sdk-csharp2.0"
del /f "AltaPay_build.zip"
git clone https://github.com/AltaPay/sdk-csharp2.0.git
dotnet build sdk-csharp2.0/AltaPayApi/AltaPayApi.sln
copy sdk-csharp2.0\AltaPayApi\AltaPayApi\bin\Debug\netstandard2.0\AltaPayApi.dll sdk-csharp2.0
powershell "Compress-Archive sdk-csharp2.0 AltaPay_build.zip"
