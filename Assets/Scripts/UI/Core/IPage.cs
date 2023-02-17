namespace UI.Core
{
    /// <summary>
    /// The core functionality needed to operate UI pages properly.
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// Opens the page, triggers entry animations.
        /// </summary>
        public void Open();
        
        /// <summary>
        /// Closes the page, triggers exit animations.
        /// </summary>
        public void Close();
        
        /// <summary>
        /// Closes the current page, and when all animations are done;
        /// opens the next page.
        /// </summary>
        /// <param name="page">The next page to be opened after this one closes.</param>
        public void TriggerNextPage(IPage page);
    }
}
