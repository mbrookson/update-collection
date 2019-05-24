using System.Collections.Generic;

namespace UpdateCollection.Contracts
{
    public interface IUpdateWith<out TDestination, TSource>
    {
        IWithDelete<TDestination> WithDelete();

        IEnumerable<TDestination> Execute();
    }
}