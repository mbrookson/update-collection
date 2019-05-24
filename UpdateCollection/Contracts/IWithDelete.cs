using System.Collections.Generic;

namespace UpdateCollection.Contracts
{
    public interface IWithDelete<out TDestination>
    {
        IEnumerable<TDestination> Execute();
    }
}