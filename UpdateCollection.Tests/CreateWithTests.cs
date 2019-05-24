using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Models;
using UpdateCollection;

namespace Tests
{
    [TestClass]
    public class CreateWithTests
    {
        [TestMethod]
        public void ShouldCreateItemThatDoesNotExist()
        {
            var destination = new List<Item>();
            var source = new List<Item>
            {
                new Item { Id = 1 }
            };

            destination = destination
                .UsingSource(source)
                .CompareWith((d, s) => d.Id == s.Id)
                .CreateWith(s => new Item
                {
                    Id = s.Id
                })
                .Execute()
                .ToList();

            destination.Should().NotBeNull();
            destination.Should().HaveCount(1);
            destination.First().Id.Should().Be(1);
        }

        [TestMethod]
        public void ShouldNotCreateItemThatAlreadyExists()
        {
            var destination = new List<Item> { new Item { Id = 1 } };
            var source = new List<Item> { new Item { Id = 1 } };
            
            destination = destination
                .UsingSource(source)
                .CompareWith((d, s) => d.Id == s.Id)
                .CreateWith(s => new Item { Id = s.Id })
                .Execute()
                .ToList();

            destination.Should().NotBeNull();
            destination.Should().HaveCount(1);
            destination.First().Id.Should().Be(1);
        }
    }
}