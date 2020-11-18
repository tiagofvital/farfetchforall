namespace FarfetchForAll.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandLine;
    using FarfetchForAll.Console;
    using FarfetchForAll.Console.Printer;
    using FarfetchForAll.Simulator.Commands;
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
            var clearMovementsCommand = new ClearMovementsCommand();
            this.mediator.Send(clearMovementsCommand);

            ShowOptions(new ShowOptions { Taxes = true });

            return 1;
        }

        private int Undo()
        {
            var shareMvts = this.mediator.Send(new GetShareMovements())
                .GetAwaiter()
                .GetResult();

            var lastMvt = shareMvts.Movements.Last();

            if (lastMvt != null)
            {
                var clearMovementsCommand = new ClearMovementsCommand()
                {
                    TransactionId = lastMvt.TransactionId
                };

                this.mediator.Send(clearMovementsCommand);

                ShowOptions(new ShowOptions { Taxes = true });
            }

            return 1;
        }

        private int SetAggregate(AggregateOptions opts)
        {
            var aggregateInfoCmd = new CreateFamilyAggregateCommand()
            {
                Id = Guid.NewGuid().ToString(),
                AnualGain = opts.AnualGain,
                FamilyCoeficient = opts.FamilyCoeficient,
                TaxDeductions = opts.TaxDeductions,
                TaxPayed = opts.TaxPayed,
                SpecificDeductions = opts.SpecificDeductions,
            };

            this.mediator.Send(aggregateInfoCmd);

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
                var baseScenario = new SimulationScenario(new IRSTaxFileBuilder());

                var request = new GetFamilyAggregateInfo();

                var aggregateInfo = this.mediator.Send(request)
                .GetAwaiter()
                .GetResult();

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