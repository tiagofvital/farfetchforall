using System.Collections.Generic;
using System.Linq;
using FarfetchForAll.Simulator.Shares;

namespace FarfetchForAll.Simulator.Repositories
{
    public class ShareMovementsRepository
    {
        private List<ShareMvt> shareMvts = new List<ShareMvt>();

        public void Add(ShareMvt mvt)
        {
            this.shareMvts.Add(mvt);
        }

        public IEnumerable<ShareMvt> Get()
        {
            return this.shareMvts.Select(i => i);
        }
    }
}