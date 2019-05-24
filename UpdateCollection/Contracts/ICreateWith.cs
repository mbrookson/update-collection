using System;
using System.Collections.Generic;

namespace UpdateCollection.Contracts
{
    public interface ICreateWith<TDestination, TSource>
    {
        IWithDelete<TDestination> WithDelete();
        
        IUpdateWith<TDestination, TSource> UpdateWith(Action<TDestination, TSource> action);
        
        IEnumerable<TDestination> Execute();
    }
}