using System;

namespace UpdateCollection.Contracts
{
    public interface IUsingSource<TDestination, TSource>
    {
        ICompareWith<TDestination, TSource> CompareWith(Func<TDestination, TSource, bool> func);
    }
}