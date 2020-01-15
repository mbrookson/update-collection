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
        public void ShouldOnlyUpdateItemsThatAlreadyExist()
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
                .CreateWith(s => new Item { Id = s.Id, Name = s.Name })
                .UpdateWith((d, s) => { d.Name = s.Name + "-updated"; })
                .Execute()
                .ToList();

            destination.Should().NotBeNull();
            destination.Should().HaveCount(2);
            
            destination[0].Id.Should().Be(1);
            destination[0].Name.Should().Be("B-updated");
            
            destination[1].Id.Should().Be(2);
            destination[1].Name.Should().Be("C");
        }
    }
}