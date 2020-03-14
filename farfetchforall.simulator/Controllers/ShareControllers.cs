namespace FarfetchForAll.Simulator.Controllers
{
    using FarfetchForAll.Simulator.Events;
    using MediatR;

    public class ShareControllers
    {
        private readonly IMediator mediator;

        public ShareControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void VestShares(int amount, float value, float exerciseCost, int year)
        {
            this.mediator.Send(new VestShareCommand()
            {
                Amount = amount,
                ShareValue = value,
                ExerciseCost = exerciseCost,
                Year = year
            });
        }

        public void Sell(int amount, float value, int year)
        {
            this.mediator.Send(new SellShareCommand()
            {
                Amount = amount,
                ShareValue = value,
                Year = year
            });
        }
    }
}