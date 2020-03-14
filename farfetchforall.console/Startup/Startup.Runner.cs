namespace FarfetchForAll.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandLine;
    using FarfetchForAll.Console;
    using FarfetchForAll.Console.Printer;
    using FarfetchForAll.Simulator.Controllers;
    using FarfetchForAll.Simulator.Queries;
    using FarfetchForAll.Simulator.Scenario;
    using FarfetchForAll.Simulator.Taxes.Pt;
    using MediatR;
    using SimpleInjector;

    public partial class Startup
    {
        private ShareControllers controller;
        private IMediator mediator;
        private TaxPayer aggregateInfo;

        public Startup ConfigureAggregateInfo()
        {
            this.aggregateInfo = new TaxPayerBuilder()
                .WithAnualGain(45430.98F)
                .WithFamilyCoeficient(2)
                .WithDeductionsToTax(2113.14F)
                .WithSpecificDeductions(4388.58F)
                .WithTaxPayed(198 + 7943)
                .Build();

            return this;
        }

        public void Run(Container container)
        {
            this.controller = container.GetInstance<ShareControllers>();
            this.mediator = container.GetInstance<IMediator>();

            do
            {
                try
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine("Enter command: --help for instructions");
                    var args = System.Console.ReadLine()?.Split(' ');

                    var r = Parser.Default.ParseArguments<SellOptions, VestOptions, ShowOptions>(args)
                        .MapResult(
                        (VestOptions opts) => VestOptions(opts),
                        (SellOptions opts) => SellOptions(opts),
                        (ShowOptions opts) => ShowOptions(opts),
                        errs => ShowErrors(errs));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            } while (true);
        }

        private int SimulationOptions(SimulationOptions opts)
        {
            ShowOptions(new ShowOptions { Taxes = true });

            return 1;
        }

        private int VestOptions(VestOptions opts)
        {
            this.controller.VestShares(opts.Amount, opts.ShareValue, opts.ExerciseCost, opts.Year);

            ShowOptions(new ShowOptions { Taxes = true });

            return 1;
        }

        private int SellOptions(SellOptions opts)
        {
            this.controller.Sell(opts.Amount, opts.ShareValue, opts.Year);

            ShowOptions(new ShowOptions { Taxes = true });

            return 1;
        }

        private int ShowOptions(ShowOptions opts)
        {
            var shareMvts = this.mediator.Send(new GetShareMovements())
                .GetAwaiter()
                .GetResult();

            shareMvts.Movements.Display();

            if (opts.Taxes)
            {
                var baseScenario = new SimulationScenario(new IRSCalculator());

                var baseResult = baseScenario.Run(aggregateInfo, shareMvts.Movements.ToList());

                baseResult.Display();
            }

            return 1;
        }

        private int ShowErrors(IEnumerable<Error> errs)
        {
            return 1;
        }
    }
}