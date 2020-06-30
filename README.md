# ConveyR
=======
[![NuGet](https://img.shields.io/nuget/dt/ConveyR.svg)](https://www.nuget.org/packages/ConveyR/) 
[![NuGet](https://img.shields.io/nuget/vpre/ConveyR.svg)](https://www.nuget.org/packages/ConveyR/)

Simple implementation of conveyor handling  in .NET.
The library takes code reuse to a new level.
In-process object handling with no dependencies.

Define process handlers with your business objects in C#. Synchronous and async with intelligent dispatching via C# generic variance.

### Installing ConveyR

You should install [ConveyR with NuGet](https://www.nuget.org/packages/ConveyR):

    Install-Package ConveyR
    
Or via the .NET Core command line interface:

    dotnet add package ConveyR

Use this library to define handlers once and use them to handle different objects with shared interfaces or shared base classes.
Lib contains:
* `IConveyor` interface to use as entry point to handle
* `IProcessHandler` and `AbstractProcessHandler` to define handlers
* delegate `ServiceFactory` for connecting with any DI containers. [Extensions for Microsoft.DependencyInjection](https://www.nuget.org/packages/ConveyR.Extensions.Microsoft.DependencyInjection/)

### Common usage (Example):

### Define your domain classes and interfaces:

```cs 
public class EntityClass1: IEntity, IHasNaming, IHasDescription
{
    public string Id {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}
}

public class EntityClass2: IEntity, IHasNaming
{
    public string Id {get; set;}
    public string Name {get; set;}
}

```

```cs

interface IEntity
{
    string Id {get; set;}
}

interface IHasNaming
{
    string Name {get; set;}
}

interface IHasDescription
{
    string Description {get; set;}
}
```

### Define DTO domain model classes:

```cs

interface IHasNamingPayload
{
    string Name {get; set;}
}

interface IHasDescriptionPayload
{
    string Description {get; set;}
}

class EditEntityModel1: IHasNamingPayload,IHasDescriptionPayload
{
    public string Name {get; set;}
    public string Description {get; set;}
}

```

### Define Handlers from ``AbstractProcessHandler<,>`` or ``IProcessHandler<,>``:

```cs

// IEntity handler without payload
class GenerateIdHandler:AbstractProcessHandler<MyStore, IEntity>
{
    protected override Task Process(TestEntitiesStore context, IEntity entity, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(entity.Id))
        {
            entity.Id = Guid.NewGuid().ToString("N");
            // You have access to context object
            context._someUnitOfWork.SomeMethod(entity);
        }

        

        return Task.CompletedTask;
    }
}

// IHasNaming handler with payload
class ChangeNameHandler:AbstractProcessHandler<MyStore, IHasNaming,IHasNamingPayload>
{
    protected override Task Process(TestEntitiesStore context, IHasNaming entity, IHasNamingPayload payload,CancellationToken cancellationToken = default)
    {
        if(payload.Name==null)
            throw new ArgumentNullException("Name","Entity name must be named");
        entity.Name = payload.Name;
        return Task.CompletedTask;
    }
}

// IHasDescription handler with payload
class ChangeDescriptionHandler: AbstractProcessHandler<MyStore, IHasDescription,IHasDescriptionPayload>
{
    protected override Task Process(TestEntitiesStore context, IHasDescription entity, IHasDescriptionPayload payload, CancellationToken cancellationToken = default)
    {
        entity.Description = payload.Description;
        return Task.CompletedTask;
    }
}


```
### Start processing in your store or controller:

``MyStore `` is some class, where may be started handling.


```cs 
class MyStore
{
    private readonly IConveyor _conveyor;
    public readonly ISomeUnitOfWork _someUnitOfWork;
    public MyStore(IConveyor conveyor)
    {
        _conveyor = conveyor;
        _someUnitOfWork = new SomeUnitOfWork();
    }

    public async Task AddOrSaveEntity(IEntity entity,object payload = null)
    {
        await _conveyor.Process(this,entity,payload); // the conveyor will find all required handlers and apply them
    }
}

//...

var store = new MyStore(_conveyor);
var entity = new EntityClass1();
var entity2 = new EntityClass1();
var payload = new EditEntityModel1()
{
    Name = "Name 1",
    Description = "Description 1",
}

store.AddOrSaveEntity(entity,payload);
store.AddOrSaveEntity(entity2,payload);

```

To register handlers you can use DI container:

```cs

var services = new ServiceCollection();
services.AddConveyR();
var provider = services.BuildServiceProvider();

```
...

```cs 

var conveyor = _serviceProvider.GetService<IConveyor>();
// ...

```
or implement some simple ``ServiceFactory``

```cs
public class SimpleServiceFactory
{
    public SimpleServiceFactory(params Type[] handlerTypes)
    {
        ServiceFactoryExtensions.SetHandlerTypes(handlerTypes);
    }
    public IEnumerable<object> GetServices(Type contextType, Type entityType, Type payloadType = null, string processCase=null)
    {
        var types = ServiceFactoryExtensions.GetProcessServiceTypes(contextType, entityType, payloadType, processCase);

        if(!types.Any())
            yield break;

        foreach (var type in types)
        {
            var h = Activator.CreateInstance(type);
            yield return h;
        }
    }
}

var factory = new SimpleServiceFactory(typeof(ChangeNameHandler), typeof(ChangeDescriptionHandler),typeof(GenerateIdHandler));

var conveyor = new Conveyor(factory.GetServices);

var myStore = new MyStore(conveyor);

```
With DI Microsoft.DependencyInjection:



#### You can lable handlers to split them into groups and manage ordering of handling:

```cs

[ProcessConfig(Order=1,Group="aftercommit")]
class WriteToLogHandler:AbstractProcessHandler<MyStore, IEntity>
{
    protected override Task Process(TestEntitiesStore context, IEntity entity, CancellationToken cancellationToken = default)
    {
        Console.Writeline($"{entity.Id} stored");
        return Task.CompletedTask;
    }
}

//...

class MyStore
{
    private readonly IConveyor _conveyor;
    public readonly ISomeUnitOfWork _someUnitOfWork;
    public MyStore(IConveyor conveyor)
    {
        _conveyor = conveyor;
        _someUnitOfWork = new SomeUnitOfWork();
    }

    public async Task AddOrSaveEntity(IEntity entity,object payload = null)
    {
        await _conveyor.Process(this,entity,payload); // the conveyor will find all required handlers and apply them
    }

    public async Task AfterSaveEntity(IEntity entity,object payload = null)
    {
        await _conveyor.Process(this,entity,payload,"aftercommit");
    }
    
}

```
