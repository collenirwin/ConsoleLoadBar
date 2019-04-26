using System;

namespace ConsoleLoadBar
{
    /// <summary>
    /// Builder to aid with constructing <see cref="LoadBar"/> objects
    /// </summary>
    public class LoadBarBuilder
    {
        /// <summary>
        /// The starting point - defaults to 0
        /// </summary>
        public double StartValue { get; set; } = 0;

        /// <summary>
        /// The ending point - defaults to 100
        /// </summary>
        public double FinishValue { get; set; } = 100;

        /// <summary>
        /// The value we should be displaying right now
        /// </summary>
        public double CurrentValue { get; set; }

        /// <summary>
        /// Controls how progress is displayed in the <see cref="Console"/>
        /// </summary>
        public LoadBarStyles Styles { get; set; }

        /// <summary>
        /// Should we update the display whenever the <see cref="CurrentValue"/> changes?
        /// </summary>
        public bool UpdateOnChange { get; set; } = true;

        /// <summary>
        /// Initializes a <see cref="LoadBarBuilder"/> object
        /// </summary>
        public static LoadBarBuilder Create()
        {
            return new LoadBarBuilder();
        }

        /// <summary>
        /// Set a starting point
        /// </summary>
        public LoadBarBuilder SetStartValue(double value)
        {
            StartValue = value;
            return this;
        }

        /// <summary>
        /// Set an ending point
        /// </summary>
        public LoadBarBuilder SetFinishValue(double value)
        {
            FinishValue = value;
            return this;
        }

        /// <summary>
        /// Set the current progress value
        /// </summary>
        public LoadBarBuilder SetCurrentValue(double value)
        {
            CurrentValue = value;
            return this;
        }

        /// <summary>
        /// Set the display styles
        /// </summary>
        public LoadBarBuilder SetStyles(LoadBarStyles styles)
        {
            Styles = styles;
            return this;
        }

        /// <summary>
        /// Add a style to the <see cref="Styles"/>
        /// </summary>
        public LoadBarBuilder AddStyle(LoadBarStyles style)
        {
            Styles |= style;
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
    }
}
