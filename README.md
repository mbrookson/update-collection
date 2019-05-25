# .NET - UpdateCollection

[![Build Status](https://travis-ci.org/mbrookson/dotnet-update-collection.svg?branch=master)](https://travis-ci.org/mbrookson/dotnet-update-collection)

Update a collection with another collection in .NET

It can be a pain to update a collection using another collection. This library is designed to make this process easier, with a simple fluent API.

## Example usage ##
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


## Methods ##
### `.UsingSource(IEnumerable<T> source)` ###
- The entry point for UpdateCollection.
- Call this extension  method on an existing IEnumerable<TDestination> instance.
- (param: source) A source IEnumerable<TSource> instance which is used to create/update/delete items in the destination. It does not have to be of the same type as the destination.
  
### `.CompareWith((TDestination, TSource) => bool)` ###
- To be chained to `.UsingSource()`
- (param: func) A delegate that returns a boolean which is called for each item in the original collection as a comparator to use which matches against an item in the source collection.

### `.CreateWith(TSource => TDestination)` ###
- Optional method which can be chained to `.CompareWith()`
- (param: func) A delegate that returns an instance of TDestination which is called for each item in the source collection that needs to be created in the destination. It provides the current source item which can be used to construct a destination item.

### `.UpdateWith((TDestination, TSource) => void)` ###
- Optional method which can be chained to `.CompareWith()` or `.CreateWith()`
- (param: func) A delegate that returns an instance of TDestination which is called for each item in the source collection that needs to be created in the destination. It provides the current source item which can be used to construct a destination item.
- You must create the TDestination item yourself as it is not created automatically.

### `.WithDelete()` ###
- Optional method which can be chained to `.CreateWith()` or `.UpdateWith()`
- Remove any items from the destination which do not exist in the source, based on the delegate in `.CompareWith()`

### `.Execute()` ###
- Required. Must be called last and can be chained to `.CreateWith()`, `.UpdateWith()` or `.WithDelete()`
- Returns the new and updated version of the destination collection
