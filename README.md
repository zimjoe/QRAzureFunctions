# FunctionTest
This is just a simple Azure function to test deployment processes from GitHub Actions.  I just jammed all the code in a single project for simplicity sake.  It also seems like Microsoft wants functions to be small instead of big "Clean" multi projects affairs.

## QRCoder
Once I had the function deploying, I figured a simple implementation of the very good QRCoder library would be useful.

## Fluent Validation
Once I had incoming data, I figured that I had better start validating the data.  Found a great article here https://www.tomfaltesek.com/azure-functions-input-validation/ that made it all clear.  This seemed like a nice middle ground to remove a lot of boiler plate validation without using a full "clean" approach like I would in a Web API project.  I feel like adding a lot of DI would ake this more complex than the "spirit" of Azure Functions.  Could be wildly wrong and just not know it yet.

## Testing
I went with xUnit for this.  Extra points for lazy structure!
dotnet new xunit -n Aeveco.FunctionTests -o FunctionTests
dotnet sln Aeveco.AzureFunction.sln add FunctionTests\Aeveco.FunctionTests.csproj

## License
I went with the UnLicense so take what is helpful of my value add stuff and structure.  I borrowed from a great article, you may want to check if he has a license or not.  (It's on the internet, I assume he is helping y'all out)
