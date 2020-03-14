namespace FarfetchForAll.Console
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Scenario;

    public static class ScenarioResultPrinter
    {
        public static ScenarioResult Display(this ScenarioResult result)
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"------------------ {result.Name} Scenario ----------------");
            System.Console.WriteLine();

            System.Console.WriteLine("----------- Running scenario ---------------");
            System.Console.WriteLine();

            Display(result.Results);

            System.Console.WriteLine("----------- End of scenario ---------------");
            return result;
        }

        private static void Display(this IEnumerable<YearResult> result)
        {
            foreach (var year in result)
            {
                year.Display();
            }
        }

        private static void Display(this YearResult yearResult)
        {
            System.Console.WriteLine($"             Year: {yearResult.Year}                ");

            System.Console.WriteLine("Tax Parcels:");
            foreach (var item in yearResult.TaxResult.Parcels)
            {
                System.Console.WriteLine($"   {item.Name}: {item.Amount:C}");
            }

            System.Console.WriteLine($"Shares Profit: {yearResult.SharesProfit:C}");
            System.Console.WriteLine("--------------------------------------------");
            System.Console.WriteLine();
        }
    }
}