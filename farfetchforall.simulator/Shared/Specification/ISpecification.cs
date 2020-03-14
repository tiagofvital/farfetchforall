namespace FarfetchForAll.Simulator.Shared.Specification
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);

        ISpecification<T> And(ISpecification<T> spec);
    }
}
