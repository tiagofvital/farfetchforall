namespace FarfetchForAll.Simulator.RequestHandlers
{
    using FarfetchForAll.Simulator.Domain.Model.Shares;
    using FarfetchForAll.Simulator.Shared.Specification;
    using FarfetchForAll.Simulator.Shares;

    internal class VestedSharesSpecification : CompositeSpecification<Share>
    {
        public override bool IsSatisfiedBy(Share entity)
        {
            return entity.State == Share.ShareState.Vested;
        }
    }
}