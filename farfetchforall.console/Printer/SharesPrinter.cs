using System.Collections.Generic;
using System.Linq;
using FarfetchForAll.Simulator.Shares;

namespace FarfetchForAll.Console.Printer
{
    public static class SharesMvtPrinter
    {
        public static void Display(this IEnumerable<ShareMvt> shareMvts)
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"---------   Shares Activity  --------------");
            System.Console.WriteLine();

            System.Console.WriteLine("-- Movements --");
            System.Console.WriteLine();

            var mvtGroups = shareMvts.GroupBy(i => new
            {
                Year = i.MovementYear,
                Type = i.MovementType,
                Value = i.ShareValue,
                Cost = i.ShareCost
            });

            foreach (var mvtGroup in mvtGroups)
            {
                var firstMvt = mvtGroup.First();

                var mvtStr = $"   {firstMvt.MovementType}: {mvtGroup.Count()} | ShareValue: {firstMvt.ShareValue:C}| " +
                  $"Cost: {firstMvt.ShareCost:C} | Income: {(firstMvt.Income * mvtGroup.Count()):C}";

                System.Console.WriteLine(mvtStr);
            }

            System.Console.WriteLine("-------------------------------------------");
        }
    }
}