namespace FarfetchForAll.Simulator.Controllers
{
    using System;
    using FarfetchForAll.Simulator.Commands;
    using MediatR;

    public class FamilyAggregateController
    {
        private readonly IMediator mediator;

        public FamilyAggregateController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void SetAggregate(float anualGain, int familyCoeficient, float taxDeductions, float taxPayed, float specificDeductions)
        {
            var aggregateInfoCmd = new CreateFamilyAggregateCommand()
            {
                Id = Guid.NewGuid().ToString(),
                AnualGain = anualGain,
                FamilyCoeficient = familyCoeficient,
                TaxDeductions = taxDeductions,
                TaxPayed = taxPayed,
                SpecificDeductions = specificDeductions,
            };

            this.mediator.Send(aggregateInfoCmd);
        }
    }
}