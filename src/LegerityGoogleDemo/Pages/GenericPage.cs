namespace LegerityGoogleDemo.Pages
{
    using System;
    using System.Threading;
    using Legerity.Pages;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    public class GenericPage : BasePage
    {
        protected override By Trait => By.TagName("p");

        public GenericPage ReadPage(double readTime = 2)
        {
            DateTime expectedDateTime = DateTime.UtcNow.AddMinutes(readTime);

            while (DateTime.UtcNow < expectedDateTime)
            {
                var actions = new Actions(this.App);
                actions.SendKeys(Keys.ArrowDown);
                actions.Perform();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }

            return this;
        }
    }
}