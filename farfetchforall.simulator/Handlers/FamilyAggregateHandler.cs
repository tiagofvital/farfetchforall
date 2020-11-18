namespace FarfetchForAll.Simulator.Handlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FarfetchForAll.Simulator.Commands;
    using FarfetchForAll.Simulator.Queries;
    using FarfetchForAll.Simulator.Repositories;
    using FarfetchForAll.Simulator.Scenario;
    using MediatR;

    public class FamilyAggregateHandler : IRequestHandler<CreateFamilyAggregateCommand, FamilyAggregate>, IRequestHandler<GetFamilyAggregateInfo, FamilyAggregate>
    {
        private readonly FamilyAggregateRepository repository;

        public FamilyAggregateHandler(FamilyAggregateRepository repository)
        {
            this.repository = repository;
        }

        public async Task<FamilyAggregate> Handle(CreateFamilyAggregateCommand request, CancellationToken cancellationToken)
        {
            var familyAggregate = new FamilyAggregate
            {
                Id = request.Id,
                AnualGain = request.AnualGain,
                FamilyCoeficient = request.FamilyCoeficient,
                SpecificDeductions = request.SpecificDeductions,
                TaxDeductions = request.TaxDeductions,
                TaxPayed = request.TaxPayed
            };

            this.repository.Add(familyAggregate);

            return await Task.FromResult(familyAggregate);
        }

        public async Task<FamilyAggregate> Handle(GetFamilyAggregateInfo request, CancellationToken cancellationToken)
        {
            var familyAggregate = this.repository.Get()
                .LastOrDefault();

            return await Task.FromResult(familyAggregate);
        }
    }
}