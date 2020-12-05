namespace FarfetchForAll.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using CommandLine;
    using FarfetchForAll.Console;
    using FarfetchForAll.Console.Printer;
    using FarfetchForAll.Simulator.Controllers;
    using FarfetchForAll.Simulator.Scenario;
    using FarfetchForAll.Simulator.Taxes.Pt;
    using MediatR;
    using SimpleInjector;

    public partial class Startup
    {
        private ShareControllers sharesController;
        private FamilyAggregateController familyAggregateController;
        private IMediator mediator;

        public void Run(Container container)
        {
            this.sharesController = container.GetInstance<ShareControllers>();
            this.familyAggregateController = container.GetInstance<FamilyAggregateController>();

            this.mediator = container.GetInstance<IMediator>();

            do
            {
                try
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine("Enter command: --help for instructions");
                    var args = System.Console.ReadLine()?.Split(' ');

                    var r = Parser.Default.ParseArguments<SellOptions, VestOptions, ShowOptions, AggregateOptions, UndoOptions, ResetOptions>(args)
                        .MapResult(
                        (VestOptions opts) => VestOptions(opts),
                        (SellOptions opts) => SellOptions(opts),
                        (ShowOptions opts) => ShowOptions(opts),
                        (AggregateOptions opts) => SetAggregate(opts),
                        (UndoOptions opts) => Undo(),
                        (ResetOptions opts) => Reset(),
                        errs => ShowErrors(errs));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            } while (true);
        }

        private int Reset()
        {
            this.sharesController.Clear();

            ShowOptions(new ShowOptions { Taxes = true });

            return 1;
        }

        private int Undo()
        {
            this.sharesController.Undo();
            ShowOptions(new ShowOptions { Taxes = true });

            return 1;
        }

        private int SetAggregate(AggregateOptions opts)
        {
            this.familyAggregateController.SetAggregate(
                opts.AnualGain,
                opts.FamilyCoeficient,
                opts.TaxDeductions,
                opts.TaxPayed,
                opts.SpecificDeductions);

            return 1;
        }

        private int VestOptions(VestOptions opts)
        {
            this.sharesController.VestShares(opts.Amount, opts.ShareValue, opts.ExerciseCost, opts.Year);

            return 1;
        }

        private int SellOptions(SellOptions opts)
        {
            this.sharesController.Sell(opts.Amount, opts.ShareValue, opts.Year);

            return 1;
        }

        private int ShowOptions(ShowOptions opts)
        {
            var baseScenario = new SimulationScenario(new IRSTaxFileBuilder(), this.mediator);

            var baseResult = baseScenario.Run();

            baseResult.Display();

            return 1;
        }

        private int ShowErrors(IEnumerable<Error> errs)
        {
            return 1;
        }
    }
}