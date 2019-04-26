using System;

namespace ConsoleLoadBar
{
    /// <summary>
    /// Combinable styles to determine how the <see cref="LoadBar"/> is displayed
    /// </summary>
    [Flags]
    public enum LoadBarStyles
    {
        /// <summary>
        /// No visible load bar
        /// </summary>
        None = 0,

        /// <summary>
        /// A traditional progress bar
        /// </summary>
        Bar = 1,

        /// <summary>
        /// Progress percentage
        /// </summary>
        Percentage = 2,

        /// <summary>
        /// Continuous progress spinner
        /// </summary>
        Spinner = 4,

        /// <summary>
        /// All styles applied
        /// </summary>
        All = 7
    }
}
