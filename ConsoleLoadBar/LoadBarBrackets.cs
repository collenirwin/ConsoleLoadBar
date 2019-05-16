namespace ConsoleLoadBar
{
    /// <summary>
    /// Determines which style of brackets should be rendered on either side of a loading bar.
    /// </summary>
    public enum LoadBarBracketStyle
    {
        /// <summary>
        /// No brackets.
        /// </summary>
        None,

        /// <summary>
        /// [ ]
        /// </summary>
        Square,

        /// <summary>
        /// { }
        /// </summary>
        Curly,

        /// <summary>
        /// < >
        /// </summary>
        Angle,

        /// <summary>
        /// ( )
        /// </summary>
        Parentheses,

        /// <summary>
        /// | |
        /// </summary>
        Bar
    }
}
