namespace ChineseCheckers.Tests;

[TestClass]
public class HelloWorldTTest
{
    [TestMethod]
    public async Task HelloWorld()
    {
        // Given
        // api : /helloworld
        var client = new TestServer().CreateClient();

        // When 
        // GET /helloworld
        var response = await client.GetAsync("/helloworld");


        // Then
        // Response : "hello world"
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("hello world", responseString);
    }
}