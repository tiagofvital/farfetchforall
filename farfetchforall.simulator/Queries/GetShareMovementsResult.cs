namespace FarfetchForAll.Simulator.Queries
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Shares;
    public class GetShareMovementsResult
    {
        public IEnumerable<ShareMvt> Movements { get; set; }
    }
}
