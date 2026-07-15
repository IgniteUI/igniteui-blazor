This documents provides instruction on building and running Blazor's Playwright Tests.

# Setup

- Instal Blazor **.NET SDK** from this website:
https://dotnet.microsoft.com/learn/aspnet/blazor-tutorial/install

- If you don't have `pwsh`, you will have to [install Powershell](https://docs.microsoft.com/powershell/scripting/install/installing-powershell)

Also run the node & npm setup steps for the repo, see [/README.md](../../README.md#building-and-running-locally)

## 1. Install Playwright browsers

- Open a Powershell terminal in the `./test/IgniteUI.Blazor.Lite.IntegrationTests/bin/Debug/net<version>` folder.

- Run `./playwright.ps1 install chromium` to instal only chromium for Playwright to run. For all browsers just omit the  `chromium` argument.

You can run it from the `./test/IgniteUI.Blazor.Lite.IntegrationTests` folder as well, but you will need to prefix the path to it: `bin/Debug/net<version>/playwright.ps1 install`

For running from **cmd** you will need to use `pwsh ./playwright.ps1 install`.

For other specific browsers refer to `./playwright.ps1 install --help`


## 2. Run tests

### Test settings

The `IgniteUI.Blazor.Lite.IntegrationTests` project once built, will generate all the tests and they should appear in the test explorer in Visual Studio.

Each test once run will start its own instance of the `IgniteUI.Blazor.Lite.TestBed`. That is the test bed app, which renders an instance of each component per test. So no need for any other extra steps than just hitting run/debug on the test.


- Disabling headless run for tests locally - uncomment the headless launch option in `.runsettings` 

- If you don't want each test using in memory browser server, you can disable it in the `.runsettings` by setting `useInMemoryClient` to `false`. In this case you will need to run your own instance of the `IgniteUI.Blazor.Lite.TestBed`, either from VS or using `dotnet run` in the project folder.

### Using Visual Studio

- In the **Test Explorer** you can run all tests or each one individually.

- Debugging from VS - Right click a test and choose debug. You can put breakpoints and go through the execution as normal.

- Debugging with Playwright Inspector - https://playwright.dev/dotnet/docs/debug. Pretty much add `await page.PauseAsync();` to the start of a test or elsewhere and the Playwright inspector will start at that point onward.

### Using `dotnet test`

You can also run the tests via the .NET CLI:
```bash
dotnet test ./tests/IgniteUI.Blazor.Lite.IntegrationTests --settings ./.runsettings
```

## Resources and guidance

- For general guidance related to Playwright tests - https://playwright.dev/dotnet/docs/

- Viewing trace of a failed test group(usually artifact from a build) - https://playwright.dev/dotnet/docs/trace-viewer#opening-the-trace
