using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Models;
using UpdateCollection;

namespace Tests
{
    [TestClass]
    public class UpdateWithTests
    {
        [TestMethod]
        public void ShouldUpdateItemsThatAlreadyExist()
        {
            var destination = new List<Item> { new Item { Id = 1, Name = "A" }};
            var source = new List<Item>
            {
                new Item { Id = 1, Name = "B" },
                new Item { Id = 2, Name = "C" }
            };

            destination = destination
                .UsingSource(source)
                .CompareWith((d, s) => d.Id == s.Id)
                .UpdateWith((d, s) => { d.Name = s.Name; })
                .Execute()
                .ToList();

            destination.Should().NotBeNull();
            destination.Should().HaveCount(1);
            destination.First().Id.Should().Be(1);
            destination.First().Name.Should().Be("B");
        }
    }
}