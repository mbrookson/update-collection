# dotnet-update-collection

[![Build Status](https://travis-ci.org/mbrookson/dotnet-update-collection.svg?branch=master)](https://travis-ci.org/mbrookson/dotnet-update-collection)

Update a collection with another collection in .NET

It can be a pain to update a collection using another collection. This library is designed to make this process easier, with a simple fluent API.

**Example usage**
```
  var destination = new List<Item> 
  { 
      new Item { Id = 1, Name = "A" },
      new Item { Id = 2, Name = "B" }
  };
  
  var source = new List<Item>
  {
      new Item { Id = 1, Name = "B" },
      new Item { Id = 3, Name = "C" }
  };

  destination = destination
      .UsingSource(source)
      .CompareWith((d, s) => d.Id == s.Id)
      .CreateWith(s => new Item { Id = s.Id, Name = s.Name, Date = DateTime.Now })
      .UpdateWith((d, s) => { d.Name = s.Name + " updated"; })
      .WithDelete()
      .Execute();
```

The above code would: 
- Update the exiting item where `Id = 1` to have `Name = "B updated"`
- Create a new item where `Id = 3` and `Name = "C updated"`
- Delete item where `Id = 2` which doesn't exist in the source collection
