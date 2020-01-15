using System;
using System.Collections.Generic;
using System.Linq;
using UpdateCollection.Contracts;
using UpdateCollection.Exceptions;

namespace UpdateCollection
{
    public class CollectionUpdater<TDestination, TSource> :
        IUsingSource<TDestination, TSource>, 
        ICompareWith<TDestination, TSource>, 
        ICreateWith<TDestination, TSource>,
        IUpdateWith<TDestination, TSource>,
        IWithDelete<TDestination> 
        where TDestination : class
    {
        private readonly IList<TDestination> _destination;
        private readonly IEnumerable<TSource> _source;
        private Func<TDestination, TSource, bool> _compareFunc;
        private Func<TSource, TDestination> _createFunc;
        private Action<TDestination, TSource> _updateAction;
        private bool _withDelete;

        internal CollectionUpdater(IList<TDestination> destination, IEnumerable<TSource> source)
        {
            _destination = destination ?? throw new ArgumentNullException(nameof(destination));
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }
        
        public ICompareWith<TDestination, TSource> CompareWith(Func<TDestination, TSource, bool> func)
        {
            _compareFunc = func;

            return this;
        }

        public ICreateWith<TDestination, TSource> CreateWith(Func<TSource, TDestination> func)
        {
            _createFunc = func;

            return this;
        }

        public IWithDelete<TDestination> WithDelete()
        {
            _withDelete = true;

            return this;
        }

        public IUpdateWith<TDestination, TSource> UpdateWith(Action<TDestination, TSource> action)
        {
            _updateAction = action;

            return this;
        }
        
        public IEnumerable<TDestination> Execute()
        {
            if (_compareFunc == null)
                throw new MissingCompareFunctionException();
            
            foreach (var sourceItem in _source)
            {
                var destinationItem = _destination.FirstOrDefault(d => _compareFunc(d, sourceItem));

                if (destinationItem != null)
                {
                    _updateAction?.Invoke(destinationItem, sourceItem);
                    continue;
                }
                
                if (_createFunc != null)
                {
                    destinationItem = _createFunc(sourceItem);
                    
                    _destination.Add(destinationItem);
                }
            }

            if (_withDelete)
            {
                for (var i = _destination.Count - 1; i > 0; i--)
                {
                    var sourceItem = _source.FirstOrDefault(s => _compareFunc(_destination[i], s));
                    
                    if (sourceItem == null)
                        _destination.RemoveAt(i);
                }
            }

            return _destination;
        }
    }
}