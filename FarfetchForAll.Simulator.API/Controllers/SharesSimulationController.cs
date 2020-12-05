namespace FarfetchForAll.Simulator.API.Controllers
{
    using System.Linq;
    using FarfetchForAll.Simulator.API.Models;
    using FarfetchForAll.Simulator.Controllers;
    using FarfetchForAll.Simulator.Scenario;
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
        public SimulationContextModel Post([FromBody] SimulationContextModel model)
        {
            this.familyAggregateController.SetAggregate(
                model.AnnualGain,
                model.FamilyCoeficient,
                model.TaxDeductions,
                model.TaxPayed,
                model.SpecificDeductions);

            model.Id = "1";

            return model;
        }

        // POST api/simulations/id/vest
        [Route("{id}/vestings")]
        [HttpPost]
        public VestModel Post([FromBody] VestModel vestModel)
        {
            this.sharesController.VestShares(
                vestModel.Amount,
                vestModel.ShareValue,
                vestModel.ExerciseCost,
                vestModel.Year);

            return vestModel;
        }

        // POST api/simulation/id/sellings
        [Route("{id}/sellings")]
        [HttpPost]
        public SellModel Sell([FromBody] SellModel sellModel)
        {
            this.sharesController.Sell(
                sellModel.Amount,
                sellModel.SharesValue,
                sellModel.Year);

            return sellModel;
        }

        [Route("{id}/runs")]
        [HttpPost]
        public SimulationRunModel Post([FromRoute] string id)
        {
            var simulationScenario = new SimulationScenario(new IRSTaxFileBuilder(), this.mediator);

            var result = simulationScenario.Run();

            var yearResults = result.Results.
                Select(i => new YearResultModel
                {
                    Movements = i.Movements.Select(mvt => new MovementsModel { ExerciseCost = mvt.ExerciseCost, ShareCost = mvt.ShareCost, ShareValue = mvt.ShareValue, Type = mvt.MovementType == Shares.ShareMovementType.Acquisition ? MovementType.Vesting : MovementType.Selling }),
                    Year = i.Year,
                    SharesProfit = i.SharesProfit,
                    TaxResult = i.TaxResult
                });

            var shares = new ShareModel
            {
                TotalCount = result.Shares.Count()
            };

            var model = new SimulationRunModel
            {
                SimulationId = id,
                FamilyAggregate = result.Aggregate,
                YearResults = yearResults,
                Shares = shares,
                TotalGains = result.TotalProfit
            };

            return model;
        }
    }
}