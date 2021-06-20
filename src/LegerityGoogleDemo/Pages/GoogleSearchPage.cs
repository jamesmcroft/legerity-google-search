namespace LegerityGoogleDemo.Pages
{
    using System;
    using System.Linq;
    using Exceptions;
    using Legerity;
    using Legerity.Extensions;
    using Legerity.Pages;
    using Legerity.Web.Elements.Core;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class GoogleSearchPage : BasePage
    {
        /// <summary>
        /// Gets a given trait of the page to verify that the page is in view.
        /// </summary>
        protected override By Trait => By.Id("result-stats");

        /// <summary>
        /// Gets the current page of the Google search.
        /// </summary>
        public int CurrentPage => int.TryParse(
            this.SearchPageNavigation
                .FindWebElements(By.TagName("td"))
                .FirstOrDefault(e => e.GetAttribute("class").Contains("YyVfkd"))?
                .Text, out int currentPage)
            ? currentPage
            : 0;
        
        /// <summary>
        /// Gets the button that is shown in the search policy agreement popup when performing a search for the first time.
        /// </summary>
        public Button PolicyAgreementButton => this.App.FindWebElement(ByExtras.Text("I agree"));

        /// <summary>
        /// Gets the element that defines the search result area of the Google search page.
        /// </summary>
        public RemoteWebElement SearchResultArea => this.App.FindWebElement(By.Id("rcnt"));

        /// <summary>
        /// Gets the element that defines the search page options navigation at the bottom of the Google search page.
        /// </summary>
        public RemoteWebElement SearchPageNavigation => this.SearchResultArea.FindWebElement(By.XPath(".//*[@role='navigation']"));

        /// <summary>
        /// Gets the button that navigates the page forward one count.
        /// </summary>
        public Button NextPageButton => this.SearchPageNavigation.FindWebElement(By.Id("pnnext"));

        /// <summary>
        /// Gets the button that navigates the page back one count.
        /// </summary>
        public Button PreviousPageButton => this.SearchPageNavigation.FindWebElement(By.Id("pnprev"));

        public TPage FindAndViewSearchResultByDomain<TPage>(string domain, int maxPageNumber) where TPage : BasePage
        {
            try
            {
                this.PolicyAgreementButton.Click();
            }
            catch (NoSuchElementException)
            {
                // Ignored. Agreement already clicked.
            }

            try
            {
                Button searchResult = this.App.FindWebElement(By.PartialLinkText(domain));
                searchResult.Click();

                return Activator.CreateInstance<TPage>();
            }
            catch (Exception ex)
            {
                if (maxPageNumber == this.CurrentPage)
                {
                    throw new GoogleSearchNotFoundException(domain, ex);
                }

                return this.GoToNextPage().FindAndViewSearchResultByDomain<TPage>(domain, maxPageNumber);
            }
        }

        public GoogleSearchPage GoToNextPage()
        {
            this.NextPageButton.Click();
            return this;
        }
        
        public GoogleSearchPage GoToPreviousPage()
        {
            this.PreviousPageButton.Click();
            return this;
        }
    }
}
