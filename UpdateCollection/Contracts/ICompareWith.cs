using System;

namespace UpdateCollection.Contracts
{
    public interface ICompareWith<TDestination, TSource>
    {
        /// <summary>
        /// Expression is called once for every item that is created.
        /// To be used for initialisation.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        ICreateWith<TDestination, TSource> CreateWith(Func<TSource, TDestination> func);

        /// <summary>
        /// Expression is called for every item.
        /// To be used for updating values.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IUpdateWith<TDestination, TSource> UpdateWith(Action<TDestination, TSource> action);
    }
}