using System;

namespace ConsoleLoadBar
{
    /// <summary>
    /// A text-based loading bar for the <see cref="Console"/>.
    /// </summary>
    public class LoadBar
    {
        /// <summary>
        /// All possible states of the progress spinner, in order.
        /// </summary>
        private static readonly string[] _spinnerStates = new string[] { "|", "/", "-", "\\" };

        private int _spinnerStateIndex = 0;

        /// <summary>
        /// The starting point. Defaults to 0.
        /// </summary>
        public double StartValue { get; } = 0;

        /// <summary>
        /// The ending point. Defaults to 100.
        /// </summary>
        public double FinishValue { get; } = 100;

        private double _currentValue = 0;

        /// <summary>
        /// The value we should be displaying right now.
        /// </summary>
        /// <remarks>
        /// Values will be kept between <see cref="StartValue"/> and <see cref="FinishValue"/>.
        /// </remarks>
        public double CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = Math.Min(Math.Max(value, StartValue), FinishValue);

                if (UpdateOnChange)
                {
                    Draw();

                    if (IsFinished)
                    {
                        if (ClearWhenFinished)
                        {
                            Clear();
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <see cref="CurrentValue"/> represented as a percentage between 0.0 and 1.0.
        /// </summary>
        public double CurrentPercentage => (CurrentValue - StartValue) / (FinishValue - StartValue);

        /// <summary>
        /// Is the <see cref="CurrentValue"/> equal to the <see cref="FinishValue"/>?
        /// </summary>
        public bool IsFinished => CurrentValue == FinishValue;

        private int _barWidth = 20;

        /// <summary>
        /// The width (in characters) of the loading bar. Defaults to 20.
        /// </summary>
        /// <remarks>Values under 4 will be set to 4</remarks>
        public int BarWitdth
        {
            get => _barWidth;
            set
            {
                _barWidth = Math.Max(value, 4);
            }
        }

        /// <summary>
        /// The character used to render the progress bar - defaults to '■'.
        /// </summary>
        public char BarMaterial { get; set; } = '■';

        /// <summary>
        /// Controls how progress is displayed in the <see cref="Console"/>.
        /// Defaults to <see cref="LoadBarStyles.Bar"/>.
        /// </summary>
        public LoadBarStyles Styles { get; set; } = LoadBarStyles.Bar;

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
        /// <remarks>
        /// This will only take effect if <see cref="UpdateOnChange"/> is set to true.
        /// </remarks>
        public bool ClearWhenFinished { get; set; } = false;

        /// <summary>
        /// Initializes a <see cref="LoadBar"/> with all of its defaults.
        /// </summary>
        public LoadBar()
        {
        }

        /// <summary>
        /// Initializes a <see cref="LoadBar"/>, using a <see cref="LoadBarBuilder"/> to specify its properties.
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
            BarWitdth = builder.BarWidth;
            BarMaterial = builder.BarMaterial;
            Styles = builder.Styles;
            BracketStyle = builder.BracketStyle;
            UpdateOnChange = builder.UpdateOnChange;
            ClearWhenFinished = builder.ClearWhenFinished;
        }

        /// <summary>
        /// Writes the specified text over the current line in the <see cref="Console"/>.
        /// </summary>
        /// <param name="text">Text to write</param>
        private void WriteToCurrentLine(string text)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(text.PadRight(Console.BufferWidth - Environment.NewLine.Length, ' '));
        }

        /// <summary>
        /// Draw the loading bar to the <see cref="Console"/>.
        /// </summary>
        public void Draw()
        {
            WriteToCurrentLine(GetBarText());
        }

        /// <summary>
        /// Generates the loading bar text, following all set property constraints.
        /// </summary>
        private string GetBarText()
        {
            string barText = "";

            if (Styles.HasFlag(LoadBarStyles.Bar))
            {
                int progress = (int)((BarWitdth - 2) * CurrentPercentage);
                string leftBracket = "";
                string rightBracket = "";

                if (BracketStyle == LoadBarBracketStyle.Angle)
                {
                    leftBracket = "<";
                    rightBracket = ">";
                }
                else if (BracketStyle == LoadBarBracketStyle.Bar)
                {
                    leftBracket = "|";
                    rightBracket = "|";
                }
                else if (BracketStyle == LoadBarBracketStyle.Curly)
                {
                    leftBracket = "{";
                    rightBracket = "}";
                }
                else if (BracketStyle == LoadBarBracketStyle.Parentheses)
                {
                    leftBracket = "(";
                    rightBracket = ")";
                }
                else if (BracketStyle == LoadBarBracketStyle.Square)
                {
                    leftBracket = "[";
                    rightBracket = "]";
                }

                // create enough spaces to fill the bar width, including the width of the brackets
                string blankSpace = new string(' ', BarWitdth - progress - leftBracket.Length - rightBracket.Length);

                // put it all together, ex: [■■  ]
                barText = leftBracket + new string(BarMaterial, progress) + blankSpace + rightBracket;
            }

            if (Styles.HasFlag(LoadBarStyles.CurrentOfFinish))
            {
                barText += $" ({CurrentValue} / {FinishValue})";
            }

            if (Styles.HasFlag(LoadBarStyles.Percentage))
            {
                barText += $" {(CurrentPercentage * 100).ToString("n0")}%";
            }

            if (Styles.HasFlag(LoadBarStyles.Spinner))
            {
                barText += " " + _spinnerStates[_spinnerStateIndex];

                // advance the spinner state index or wrap it around to 0
                _spinnerStateIndex = _spinnerStateIndex == _spinnerStates.Length - 1
                    ? 0
                    : _spinnerStateIndex + 1;
            }

            return barText.TrimStart();
        }

        /// <summary>
        /// Clears the line that the progress bar is rendered on.
        /// </summary>
        public void Clear()
        {
            WriteToCurrentLine("");
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
