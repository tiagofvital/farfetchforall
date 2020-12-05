namespace FarfetchForAll.Simulator.Queries
{
    using MediatR;

    public class GetShares : IRequest<GetSharesResult>
    {
        public Shares.Share.ShareState State { get; set; }
    }
}