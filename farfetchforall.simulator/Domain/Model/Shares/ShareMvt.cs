namespace FarfetchForAll.Simulator.Shares
{
    public class ShareMvt
    {
        public ShareMovementType MovementType { get; set; }

        public int MovementYear { get; set; }

        public float ShareValue { get; set; }

        public float ShareCost { get; set; }

        public float ExerciseCost { get; set; }

        public float ValueCostDiff { get; set; }

        public float Income { get; set; }
    }
}