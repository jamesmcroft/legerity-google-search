namespace LegerityGoogleDemo.Exceptions
{
    using System;

    /// <summary>
    /// Defines an exception for when a Google search has not found a result for the search term.
    /// </summary>
    public class GoogleSearchNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleSearchNotFoundException"/> class.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term that resulted in no matches.
        /// </param>
        public GoogleSearchNotFoundException(string searchTerm, Exception innerException = null)
            : base($"Unable to verify on page '{searchTerm}'", innerException)
        {
            this.SearchTerm = searchTerm;
        }

        /// <summary>
        /// Gets the search term that resulted in no matches.
        /// </summary>
        public string SearchTerm { get; }
    }
}