namespace FarfetchForAll.Simulator.Domain.Model.Shares
{
    using System;

    public class Entity
    {
        public string Id { get; private set; }

        protected Entity()
        {
            this.Id = Guid.NewGuid().ToString("N");
        }
    }
}