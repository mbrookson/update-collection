using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Models;
using UpdateCollection;

namespace Tests
{
    [TestClass]
    public class WithDeleteTests
    {
        [TestMethod]
        public void ShouldNotDeleteIfWithDeleteNotCalled()
        {
            var destination = new List<Item>
            {
                new Item { Id = 1 },
                new Item { Id = 2 }
            };
            
            var source = new List<Item> { new Item { Id = 1 }};

            destination = destination
                .UsingSource(source)
                .CompareWith((d, s) => d.Id == s.Id)
                .CreateWith(s => new Item { Id = 1 })
                .Execute()
                .ToList();

            destination.Should().NotBeNull();
            destination.Should().HaveCount(2);
            destination[0].Id.Should().Be(1);
            destination[1].Id.Should().Be(2);
        }
        
        [TestMethod]
        public void ShouldDeleteIfWithDeleteCalled()
        {
            var destination = new List<Item>
            {
                new Item { Id = 1 },
                new Item { Id = 2 }
            };
            
            var source = new List<Item> { new Item { Id = 1 }};

            destination = destination
                .UsingSource(source)
                .CompareWith((d, s) => d.Id == s.Id)
                .CreateWith(s => new Item { Id = 1 })
                .WithDelete()
                .Execute()
                .ToList();

            destination.Should().NotBeNull();
            destination.Should().HaveCount(1);
            destination[0].Id.Should().Be(1);
        }
    }
}