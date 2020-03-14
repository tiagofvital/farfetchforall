﻿using System;
using System.Collections.Generic;
using System.Linq;
using FarfetchForAll.Simulator.Events;
using FarfetchForAll.Simulator.RequestHandlers;
using FarfetchForAll.Simulator.Shared.Specification;
using FarfetchForAll.Simulator.Shares;

namespace FarfetchForAll.Simulator.Repositories
{
    public class SharesRepository
    {
        private Queue<Share> shares = new Queue<Share>();
        public IEnumerable<Share> Get(ISpecification<Share> spec)
        {
            return this.shares.Where(i => spec.IsSatisfiedBy(i));
        }
        public void Add(Share share)
        {
            this.shares.Enqueue(share);
        }
    }
}