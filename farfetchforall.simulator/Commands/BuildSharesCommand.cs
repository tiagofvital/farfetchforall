namespace FarfetchForAll.Simulator.Commands
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Shares;
    using MediatR;

    public class BuildSharesCommand : IRequest
    {
        public IEnumerable<ShareMvt> Movements { get; set; }
    }
}