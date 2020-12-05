namespace FarfetchForAll.Simulator.API.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.API.Models;
    using FarfetchForAll.Simulator.Controllers;
    using FarfetchForAll.Simulator.Scenario;
    using FarfetchForAll.Simulator.Shares;
    using FarfetchForAll.Simulator.Taxes.Pt;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/simulations")]
    [ApiController]
    public class SimulationsController : ControllerBase
    {
        private readonly ShareControllers sharesController;
        private readonly FamilyAggregateController familyAggregateController;
        private readonly IMediator mediator;

        public SimulationsController(
            ShareControllers sharesController,
            FamilyAggregateController familyAggregateController,
            IMediator mediator)
        {
            this.sharesController = sharesController;
            this.familyAggregateController = familyAggregateController;
            this.mediator = mediator;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SimulationContextModel model)
        {
            this.familyAggregateController.SetAggregate(
                model.AnnualGain,
                model.FamilyCoeficient,
                model.TaxDeductions,
                model.TaxPayed,
                model.SpecificDeductions);

            return this.Ok();
        }

        // POST api/simulation/id/vest
        [Route("{id}/vest")]
        [HttpPost]
        public IActionResult Vesting([FromBody] VestingModel vestModel)
        {
            this.sharesController.VestShares(
                vestModel.Amount,
                vestModel.ShareValue,
                vestModel.ExerciseCost,
                vestModel.Year);

            var mvts = this.sharesController.GetMovements();

            var result = Adapt(mvts);

            return this.Ok(result);
        }

        // POST api/simulation/id/sell
        [Route("{id}/sell")]
        [HttpPost]
        public IActionResult Sell([FromBody] SellModel sellModel)
        {
            this.sharesController.Sell(
                sellModel.Amount,
                sellModel.ShareValue,
                sellModel.Year);

            var mvts = this.sharesController.GetMovements();

            var result = Adapt(mvts);

            return this.Ok(result);
        }

        [Route("{id}/run")]
        [HttpPost]
        public IActionResult Post([FromRoute] string id)
        {
            var simulationScenario = new SimulationScenario(new IRSTaxFileBuilder(), this.mediator);

            var result = simulationScenario.Run();

            var yearResults = result.Results.
                Select(i => new YearResultModel
                {
                    Movements = Adapt(i.Movements),
                    Year = i.Year,
                    SharesProfit = i.SharesProfit,
                    TaxResult = i.TaxResult
                });

            var shares = new ShareModel
            {
                TotalCount = result.Shares.Count()
            };

            var model = new SimulationResultsModel
            {
                SimulationId = id,
                YearResults = yearResults,
                Shares = shares,
                TotalGains = result.TotalProfit,
                SharesIncome = result.Results.Sum(i => i.SharesProfit),
                TotalTaxes = result.Results.Sum(i => i.TaxResult.TaxSettling)
            };

            return this.Ok(model);
        }

        // POST api/simulation/id/vest
        [Route("{id}/clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            this.sharesController.Clear();

            return this.Ok();
        }

        [Route("{id}/undo")]
        [HttpPost]
        public IActionResult Undo()
        {
            this.sharesController.Undo();

            return this.Ok();
        }

        private static IEnumerable<MovementsModel> Adapt(IEnumerable<ShareMvt> mvts)
        {
            var mvtsGroups = mvts.GroupBy(i => new
            {
                Year = i.MovementYear,
                Type = i.MovementType,
                Value = i.ShareValue,
                Cost = i.ShareCost
            });

            return mvtsGroups.Select(mvt =>
                new MovementsModel
                {
                    Amount = mvt.Count(),
                    Year = mvt.First().MovementYear,
                    ShareCost = mvt.First().ShareCost,
                    ShareValue = mvt.First().ShareValue,
                    Type = mvt.First().MovementType == Shares.ShareMovementType.Acquisition
                        ? "Vested"
                        : "Sold"
                });
        }
    }
}