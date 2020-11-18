namespace FarfetchForAll.Simulator.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Scenario;

    public class FamilyAggregateRepository
    {
        private List<FamilyAggregate> familyAggregate = new List<FamilyAggregate>();

        public void Add(FamilyAggregate familyAggregate)
        {
            this.familyAggregate.Add(familyAggregate);
        }

        public IEnumerable<FamilyAggregate> Get()
        {
            return this.familyAggregate.Select(i => i);
        }

        public IEnumerable<FamilyAggregate> Get(string Id)
        {
            return this.familyAggregate
                .Where(i => i.Id == Id)
                .Select(i => i);
        }
    }
}