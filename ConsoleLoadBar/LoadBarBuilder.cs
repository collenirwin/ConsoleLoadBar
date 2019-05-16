using System;

namespace ConsoleLoadBar
{
    /// <summary>
    /// Builder to aid with constructing <see cref="LoadBar"/> objects
    /// </summary>
    public class LoadBarBuilder
    {
        /// <summary>
        /// The starting point. Defaults to 0.
        /// </summary>
        public double StartValue { get; set; } = 0;

        /// <summary>
        /// The ending point. Defaults to 100.
        /// </summary>
        public double FinishValue { get; set; } = 100;

        /// <summary>
        /// The value we should be displaying right now.
        /// </summary>
        public double CurrentValue { get; set; }

        /// <summary>
        /// The width (in characters) of the loading bar. Defaults to 20.
        /// </summary>
        public int BarWidth { get; set; } = 20;

        /// <summary>
        /// The character used to render the progress bar. Defaults to '■'.
        /// </summary>
        public char BarMaterial { get; set; } = '■';

        /// <summary>
        /// Controls how progress is displayed in the <see cref="Console"/>.
        /// </summary>
        public LoadBarStyles Styles { get; set; }

        /// <summary>
        /// Determines which style of brackets should be rendered on either side of a loading bar.
        /// Defaults to <see cref="LoadBarBracketStyle.Square"/>.
        /// </summary
        public LoadBarBracketStyle BracketStyle { get; set; } = LoadBarBracketStyle.Square;

        /// <summary>
        /// Should we update the display whenever the <see cref="CurrentValue"/> changes?
        /// Defaults to true.
        /// </summary>
        public bool UpdateOnChange { get; set; } = true;

        /// <summary>
        /// Should we call <see cref="Clear"/> when <see cref="CurrentValue"/> hits <see cref="FinishValue"/>?
        /// Defaults to false.
        /// </summary>
        public bool ClearWhenFinished { get; set; } = false;

        /// <summary>
        /// Initializes a <see cref="LoadBarBuilder"/> object.
        /// </summary>
        public static LoadBarBuilder Create()
        {
            return new LoadBarBuilder();
        }

        /// <summary>
        /// Set a starting point.
        /// </summary>
        public LoadBarBuilder SetStartValue(double value)
        {
            StartValue = value;
            return this;
        }

        /// <summary>
        /// Set an ending point.
        /// </summary>
        public LoadBarBuilder SetFinishValue(double value)
        {
            FinishValue = value;
            return this;
        }

        /// <summary>
        /// Set the current progress value.
        /// </summary>
        public LoadBarBuilder SetCurrentValue(double value)
        {
            CurrentValue = value;
            return this;
        }

        /// <summary>
        /// Set the loading bar width.
        /// </summary>
        public LoadBarBuilder SetBarWidth(int value)
        {
            BarWidth = value;
            return this;
        }

        /// <summary>
        /// Set the character that will be rendered to create the progress bar.
        /// </summary>
        public LoadBarBuilder SetBarMaterial(char value)
        {
            BarMaterial = value;
            return this;
        }

        /// <summary>
        /// Set the display styles.
        /// </summary>
        public LoadBarBuilder SetStyles(LoadBarStyles styles)
        {
            Styles = styles;
            return this;
        }

        /// <summary>
        /// Add a style to the <see cref="Styles"/>.
        /// </summary>
        public LoadBarBuilder AddStyle(LoadBarStyles style)
        {
            Styles |= style;
            return this;
        }

        /// <summary>
        /// Sets the style of brackets that will be rendered on either side of a loading bar.
        /// </summary
        public LoadBarBuilder SetBracketStyle(LoadBarBracketStyle style)
        {
            BracketStyle = style;
            return this;
        }

        /// <summary>
        /// Should we update the display whenever the <see cref="CurrentValue"/> changes?
        /// </summary>
        public LoadBarBuilder SetUpdateOnChange(bool shouldUpdate)
        {
            UpdateOnChange = shouldUpdate;
            return this;
        }

        /// <summary>
        /// Should we call <see cref="Clear"/> when <see cref="CurrentValue"/> hits <see cref="FinishValue"/>?
        /// </summary>
        public LoadBarBuilder SetClearWhenFinished(bool shouldClear)
        {
            ClearWhenFinished = shouldClear;
            return this;
        }
    }
}
