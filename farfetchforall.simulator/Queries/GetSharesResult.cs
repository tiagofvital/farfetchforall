namespace FarfetchForAll.Simulator.Queries
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Shares;

    public class GetSharesResult
    {
        public IEnumerable<Share> Shares { get; set; }
    }
}