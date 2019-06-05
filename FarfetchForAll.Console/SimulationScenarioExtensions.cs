namespace FarfetchForAll.Console
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Scenario;

    public static class SimulationScenarioExtensions
    {
        public static ScenarioResult Display(this ScenarioResult result)
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"------------------ {result.Name} Scenario ----------------");
            System.Console.WriteLine();

            result.SharesInfo.Display();

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

            System.Console.WriteLine($"Tax to Pay: {yearResult.TaxResult.TaxToPay}");
            System.Console.WriteLine($"Shares Profit: {yearResult.SharesProfit}");
            System.Console.WriteLine("--------------------------------------------");
            System.Console.WriteLine();
        }

        public static void Display(this SharesInfo sharesInfo)
        {
            System.Console.WriteLine($"Shares:");
            System.Console.WriteLine($"   Amount: {sharesInfo.StockOptions.Amount}");
            System.Console.WriteLine($"Movements:");
            sharesInfo.SharesMovements.Display();

            System.Console.WriteLine($"--");
        }

        public static IEnumerable<ShareMovement> Display(this IEnumerable<ShareMovement> movements)
        {
            foreach (var movement in movements)
            {
                var mvtStr = $"   {movement.Movement}: {movement.Amount} | ShareValue: {movement.ShareValue:C}| " +
                    $"AcquisitionValue: {movement.AcquisitionValue:C} |AcquisitionCost: {movement.AcquisitionCost:C}";

                if (movement.SellIncome.HasValue)
                {
                    mvtStr += $" | Income: {movement.SellIncome}";
                }

                System.Console.WriteLine(mvtStr);
            }

            return movements;
        }
    }
}