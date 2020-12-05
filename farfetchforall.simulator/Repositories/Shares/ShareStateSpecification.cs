namespace FarfetchForAll.Simulator.RequestHandlers
{
    using FarfetchForAll.Simulator.Shared.Specification;
    using FarfetchForAll.Simulator.Shares;

    internal class ShareStateSpecification : CompositeSpecification<Share>
    {
        private readonly Share.ShareState shareState;

        public ShareStateSpecification(Share.ShareState shareState)
        {
            this.shareState = shareState;
        }

        public override bool IsSatisfiedBy(Share entity)
        {
            return entity.State == shareState;
        }
    }
}