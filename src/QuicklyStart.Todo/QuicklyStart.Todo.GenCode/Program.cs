
using Masa.BuildingBlocks.Ddd.Domain.Entities;
using System.Reflection;

var entityAssembly = Assembly.Load("QuicklyStart.Todo.WebApi");

var entityTypes = entityAssembly.GetTypes().Where(e => e.IsAbstract == false && typeof(IEntity).IsAssignableFrom(e)).ToList();

foreach (var item in entityTypes)
{
    var saveDir = Path.Combine($"./GenCode/{item.Name}");
    if (Directory.Exists(saveDir) == false)
    {
        Directory.CreateDirectory(saveDir);
    }



}
