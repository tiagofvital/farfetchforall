namespace FarfetchForAll.ConsoleApp
{
    using CommandLine;

    [Verb("sim", HelpText = "Simulation options")]
    public class SimulationOptions
    {
        [Option('r', "reset", Required = true, HelpText = "Resests simulation.")]
        public bool Reset { get; set; }
    }

    [Verb("vest", HelpText = "To vest stock options / RSU's.")]
    public class VestOptions
    {
        [Option('a', "amount", Required = true, HelpText = "Amount of Shares to vest.")]
        public int Amount { get; set; }

        [Option('v', "value", Required = true, HelpText = "Share value")]
        public float ShareValue { get; set; }

        [Option('c', "cost", Required = true, HelpText = "Exercise cost")]
        public float ExerciseCost { get; internal set; }

        [Option('y', "year", Required = true, HelpText = "Year of vesting")]
        public int Year { get; set; }
    }

    [Verb("sell", HelpText = "To sell shares")]
    public class SellOptions
    {
        [Option('a', "amount", Required = true, HelpText = "Amount of shares to sell.")]
        public int Amount { get; set; }

        [Option('v', "value", Required = true, HelpText = "Share value")]
        public float ShareValue { get; set; }

        [Option('y', "year", Required = true, HelpText = "Year of selling")]
        public int Year { get; set; }
    }

    [Verb("show", HelpText = "Shows current status")]
    public class ShowOptions
    {
        [Option('a', "all", Required = false, HelpText = "Includes taxes results.")]
        public bool Taxes { get; set; }
    }

    [Verb("setAggregate", HelpText = "Sets Aggregate Info")]
    public class AggregateOptions
    {
        [Option('a', "Anual Gain", Required = false, HelpText = "")]
        public float AnualGain { get; set; }

        [Option('f', "Family Coeficient", Required = false, HelpText = "")]
        public int FamilyCoeficient { get; set; }

        [Option('d', "Tax Deductions", Required = false, HelpText = "")]
        public float TaxDeductions { get; set; }

        [Option('p', "Tax Payed", Required = false, HelpText = "")]
        public float TaxPayed { get; set; }

        [Option('s', "Specific Deductions", Required = false, HelpText = "")]
        public float SpecificDeductions { get; set; }
    }
}