namespace FarfetchForAll.Simulator.Shared.Specification
{
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);
        public ISpecification<T> And(ISpecification<T> spec)
        {
            return new AndSpecification<T>(this, spec);
        }

    }
}
