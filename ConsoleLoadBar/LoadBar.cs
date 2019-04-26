using System;

namespace ConsoleLoadBar
{
    /// <summary>
    /// A text-based loading bar for the <see cref="Console"/>
    /// </summary>
    public class LoadBar
    {
        /// <summary>
        /// The starting point - defaults to 0
        /// </summary>
        public double StartValue { get; } = 0;

        /// <summary>
        /// The ending point - defaults to 100
        /// </summary>
        public double FinishValue { get; } = 100;

        private double _currentValue = 0;

        /// <summary>
        /// The value we should be displaying right now
        /// </summary>
        public double CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = Math.Min(Math.Max(value, StartValue), FinishValue);
                if (UpdateOnChange)
                {
                    // TODO: Update()
                }
            }
        }

        /// <summary>
        /// <see cref="CurrentValue"/> represented as a percentage between 0.0 and 1.0
        /// </summary>
        public double CurrentPercentage
        {
            get => CurrentValue / (FinishValue - StartValue);
        }

        /// <summary>
        /// Controls how progress is displayed in the <see cref="Console"/>
        /// </summary>
        public LoadBarStyles Styles { get; set; }

        /// <summary>
        /// Should we update the display whenever the <see cref="CurrentValue"/> changes?
        /// </summary>
        public bool UpdateOnChange { get; set; } = true;

        /// <summary>
        /// Initializes a <see cref="LoadBar"/> with all of its defaults
        /// </summary>
        public LoadBar()
        {
        }

        /// <summary>
        /// Initializes a <see cref="LoadBar"/>, using a <see cref="LoadBarBuilder"/> to specify its properties
        /// </summary>
        /// <param name="builder">Builder to aid in constructing this <see cref="LoadBar"/></param>
        public LoadBar(LoadBarBuilder builder)
        {
            StartValue = builder.StartValue;
            FinishValue = builder.FinishValue;

            if (StartValue >= FinishValue)
            {
                throw new ArgumentException("The start value must be less than finish value",
                    nameof(builder.StartValue));
            }

            CurrentValue = builder.CurrentValue;
            Styles = builder.Styles;
            UpdateOnChange = builder.UpdateOnChange;
        }
    }
}
