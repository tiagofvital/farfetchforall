namespace FarfetchForAll.API.Controllers
{
    using FarfetchForAll.Simulator.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Presentation.API.Model;

    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        private readonly ShareControllers sharesController;

        public SharesController()
        {
            //this.sharesController = new FarfetchForAll.Simulator.Controllers.ShareControllers();
        }

        // POST api/shares/vest
        [Route("vest")]
        [HttpPost]
        public void Post([FromBody] VestModel vestModel)
        {
        }

        // PUT api/values/5
        [HttpPost("sell")]
        public void Sell([FromBody] SellModel sellModel)
        {
        }
    }
}