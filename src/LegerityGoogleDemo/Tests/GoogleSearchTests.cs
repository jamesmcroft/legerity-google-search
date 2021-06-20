namespace LegerityGoogleDemo.Tests
{
    using Legerity;
    using NUnit.Framework;
    using Pages;

    [TestFixture]
    public class GoogleSearchTests : BaseTestClass
    {
        [TestCase("Best chocolate brownie recipe", "bbcgoodfood.com")]
        [TestCase("How to build NuGet packages with GitHub Actions", "jamescroft.co.uk")]
        [TestCase("The Summer House At Roundhay Park", "getlost.blog")]
        [TestCase("The Razor Sprint", "razor.co.uk")]
        public void ShouldFindAndViewSearchResult(string searchTerm, string domain)
        {
            // Arrange
            string formattedSearchTerm = string.Join('+', searchTerm.Split(' '));
            AppManager.StartApp(GetEdgeOptions($"https://www.google.co.uk/search?q={formattedSearchTerm}"));

            // Act
            var googleSearchPage = new GoogleSearchPage();
            GenericPage searchResultPage = googleSearchPage.FindAndViewSearchResultByDomain<GenericPage>(domain, 5);

            // Assert
            searchResultPage.ReadPage();
        }
    }
}
