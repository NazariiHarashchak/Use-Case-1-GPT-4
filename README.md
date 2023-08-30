# Use-Case-1-GPT-4
Application Description:
This .NET Core 6 Web API application provides functionalities related to querying country information sourced from an external API. The core logic, contained within the ValuesController and DataProccessor classes, allows users to filter countries based on different criteria like name, population, sorting order, and pagination. The country data comprises various attributes including official names, common names, currency details, flags, and more.

Running the Application Locally:
To initialize and operate the application locally, you must have .NET SDK 6.0 installed. Start by navigating to the project's root directory in your terminal or command prompt. Execute dotnet restore to restore all necessary packages. Once the restoration process concludes, input dotnet build to compile your application, ensuring it's error-free. Commence the application using dotnet run, which typically starts the API server on ports 5000 (HTTP) and 5001 (HTTPS).

Accessing the Endpoints:
With the application up and running, endpoints can be accessed via a web browser, API testing tools (e.g., Postman), or terminal utilities like curl. Here are 10 examples to interact with various endpoints:

Filter Countries Based on Specific Criteria:
POST to http://localhost:5000/values/filter-countries with form data such as name, sortByOption, population, and pagesCount.

Retrieve a Country by Name:
GET http://localhost:5000/values/by-name?name=Canada

Fetch Countries with Population Less Than a Specified Number:
GET http://localhost:5000/values/by-population?population=5 (This fetches countries with populations less than 5 million).

Sort Countries by Name in Ascending Order:
GET http://localhost:5000/values/sort-by/name-common?sortByOption=ascend

Sort Countries by Name in Descending Order:
GET http://localhost:5000/values/sort-by/name-common?sortByOption=descend

Retrieve a Specified Number of Countries (Pagination):
GET http://localhost:5000/values/pagination?pagesCount=10 (This retrieves the first 10 countries).

For Extended Search combining multiple parameters:
POST to http://localhost:5000/values/filter-countries with form data like name=Canada&sortByOption=ascend&population=40&pagesCount=5

Get Countries in Europe Region (Assuming such functionality exists in the future):
GET http://localhost:5000/values/by-region?region=Europe

Fetch Countries with a Specific Currency (For potential future expansions):
GET http://localhost:5000/values/by-currency?currency=USD

Retrieve Landlocked Countries (Considering such a feature is implemented):
GET http://localhost:5000/values/is-landlocked?status=true

Note: Always ensure the parameters align with the specific API endpoint requirements, and always consult the API documentation or the application's endpoint descriptions for exact specifications.
Running xUnit Tests:
Setup and Requirements:

Make sure you have the xUnit runner installed. If you're using .NET Core, it can be done via the NuGet package manager with the following command: dotnet add package xunit.runner.console.
Ensure that your test project (e.g., Use_Case_1_GPT_4_Tests) references both the xUnit framework and the main project (Use_Case_1_GPT_4).
Running the Tests:

From the Command Line:

Navigate to the root directory of your test project (Use_Case_1_GPT_4_Tests).
Run the command dotnet test. This command builds the project and runs the test suite. You should see output indicating the number of tests passed/failed.
From Visual Studio:

Open the solution containing both your main and test projects in Visual Studio.
Build the solution by selecting Build > Build Solution.
Open the Test Explorer window (View > Test Explorer).
In the Test Explorer, you should see a list of your tests. You can run all tests by clicking the Run All button, or run specific tests by right-clicking on them and choosing Run Selected Tests.
Reviewing Test Results:

After running the tests, the results (pass or fail) will be displayed both in the command line and the Test Explorer in Visual Studio. In the case of failures, error messages or exception details should provide insights into what went wrong.
Remember, unit tests are meant to be run frequently, especially after making changes to your application, to ensure that everything still functions as expected. If a test fails, review the associated code in your application to understand and rectify the issue.
