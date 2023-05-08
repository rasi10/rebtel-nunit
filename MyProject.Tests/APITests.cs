namespace MyProject.Tests;

using NUnit.Allure.Core;
using System.IO;

[TestFixture]
[AllureNUnit]
public class Tests
{
    private UniversitiesHelper uh = new UniversitiesHelper();
    private const string ApiBaseUrl = "http://universities.hipolabs.com";
    private const string ApiBaseUrlSearch = "http://universities.hipolabs.com/search?";
    private static IEnumerable<TestCaseData> TestDataProviderValidNames(string filePath)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                yield return new TestCaseData(line).SetName($"TEST-TEMPLATE || FILE:\'{filePath}\'|| INPUT:\'{line}\'");
            }
        }
    }

    [SetUp]
    public void Setup()
    {
        uh = new UniversitiesHelper();
    }

    [Test]
    public async Task TestBaseURL()
    {
        await uh.CheckBaseUrl();
    }

    [Test, TestCaseSource(nameof(TestDataProviderValidNames), new object[] { "../../../test-data/valid-names.txt" })]
    public async Task TestValidNameSearch(string input)
    {
        Console.WriteLine($"TestValidNameSearch: Searching for name '{input}'");
        string endpoint = $"{ApiBaseUrlSearch}name={input}";
        await uh.CheckValidEndpoint(endpoint);
    }


    [Test, TestCaseSource(nameof(TestDataProviderValidNames), new object[] { "../../../test-data/invalid-names.txt" })]
    public async Task TestInvalidNameSearch(string input)
    {
        Console.WriteLine($"TestInvalidNameSearch: Searching for name '{input}'");
        string endpoint = $"{ApiBaseUrlSearch}name={input}";
        await uh.CheckInvalidEndpoint(endpoint);
    }

    [Test, TestCaseSource(nameof(TestDataProviderValidNames), new object[] { "../../../test-data/valid-countries.txt" })]
    public async Task TestValidCoutrySearch(string input)
    {
        Console.WriteLine($"TestValidCoutrySearch: Searching for name '{input}'");
        string endpoint = $"{ApiBaseUrlSearch}country={input}";
        await uh.CheckValidEndpoint(endpoint);
    }

    [Test, TestCaseSource(nameof(TestDataProviderValidNames), new object[] { "../../../test-data/invalid-countries.txt" })]
    public async Task TestInvalidCoutrySearch(string input)
    {
        Console.WriteLine($"TestInvalidCoutrySearch: Searching for country '{input}'");
        string endpoint = $"{ApiBaseUrlSearch}country={input}";
        await uh.CheckInvalidEndpoint(endpoint);
    }

    [Test]
    public async Task TestValidNameAndCountrySearch()
    {
        Console.WriteLine("TestInvalidCoutrySearch: Searching for name \"Middle\" and country \"Turkey\"");
        string endpoint = $"{ApiBaseUrlSearch}name=Middle&country=Turkey";
        await uh.CheckValidEndpoint(endpoint);
    }

    [Test]
    public async Task TestInvalidNameAndCountrySearch()
    {
        Console.WriteLine("TestInvalidNameAndCountrySearch: Searching for name \"asdasdasda\" and country \"asdasdasdas\"");
        string endpoint = $"{ApiBaseUrlSearch}name=asdasdasda&country=asdasdasdas";
        await uh.CheckInvalidEndpoint(endpoint);
    }

}