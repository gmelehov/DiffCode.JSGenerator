$module = 'DiffCode.JSGenerator';

dotnet restore
dotnet pack -c Release -o $module\bin\Release\publish
dotnet nuget push $module\bin\Release\publish\$module.*.* --source https://proget.gmel.pro:8625/nuget/NuGetPrivate/ --api-key $ENV:ProGetApiKey --skip-duplicate