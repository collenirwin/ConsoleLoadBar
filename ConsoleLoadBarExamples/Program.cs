using ConsoleLoadBar;
using System;
using System.Threading;

namespace ConsoleLoadBarExamples
{
    class Program
    {
        private static readonly Random _random = new Random();

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome.\r\nHere's the default load bar.");

            var loadBar = new LoadBar();
            FakeLoad(loadBar);

            var builder = LoadBarBuilder.Create()
                .AddStyle(LoadBarStyles.Percentage);

            Console.WriteLine("This load bar only displays the progress percentage.");
            loadBar = new LoadBar(builder);
            FakeLoad(loadBar);


            builder = builder
                .AddStyle(LoadBarStyles.All)
                .SetClearWhenFinished(true);

            Console.WriteLine("This load bar has all styles applied, and clears when it is finished.");
            loadBar = new LoadBar(builder);
            FakeLoad(loadBar);

            builder = LoadBarBuilder.Create()
                .SetStyles(LoadBarStyles.Bar | LoadBarStyles.Percentage)
                .SetBarWidth(80)
                .SetBarMaterial('-');

            Console.WriteLine("This bar has a custom width and material.");
            loadBar = new LoadBar(builder);
            FakeLoad(loadBar);

            builder = LoadBarBuilder.Create()
                .SetBarMaterial('|')
                .SetBarWidth(50)
                .SetBracketStyle(LoadBarBracketStyle.Angle)
                .SetClearWhenFinished(true)
                .SetStartValue(100)
                .SetFinishValue(300)
                .SetCurrentValue(125)
                .SetStyles(LoadBarStyles.All);

            Console.WriteLine("This bar is all sorts of customized.");
            loadBar = new LoadBar(builder);
            FakeLoad(loadBar);

            // TODO: Fix issue with min/max set, add styles to show actual value (maybe out of finish value)

            Console.Read();
        }

        private static void FakeLoad(LoadBar loadBar)
        {
            while (!loadBar.IsFinished)
            {
                loadBar.CurrentValue++;
                Thread.Sleep(_random.Next(10, 100));
            }

            Console.WriteLine("Done loading!");
        }
    }
}
