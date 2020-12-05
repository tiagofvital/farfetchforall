using System.Collections.Generic;

namespace FarfetchForAll.Simulator.API.Models
{
    public class SellModel
    {
        public int Amount { get; set; }

        public float ShareValue { get; set; }

        public int Year { get; set; }

        public IEnumerable<MovementsModel> Movements { get; set; }
    }
}