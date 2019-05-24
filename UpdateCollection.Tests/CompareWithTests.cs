using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Models;
using UpdateCollection;
using UpdateCollection.Exceptions;

namespace Tests
{
    [TestClass]
    public class CompareWithTests
    {
        [TestMethod]
        public void ShouldThrowIfCompareWithNotCalled()
        {
            Func<IEnumerable<Item>> func = () =>
            {
                var destination = new List<Item>();
                var source = new List<Item>();

                return new CollectionUpdater<Item, Item>(destination, source).Execute();
            };

            func
                .Should()
                .Throw<MissingCompareFunctionException>();
        }
        
        [TestMethod]
        public void ShouldNotThrowIfCompareWithCalled()
        {
            Func<IEnumerable<Item>> func = () =>
            {
                var sut = new CollectionUpdater<Item, Item>(new List<Item>(), new List<Item>());

                return sut
                    .CompareWith((d, s) => d.Id == s.Id)
                    .UpdateWith((d, s) => {})
                    .Execute();
            };

            func
                .Should()
                .NotThrow<MissingCompareFunctionException>();
        }
    }
}