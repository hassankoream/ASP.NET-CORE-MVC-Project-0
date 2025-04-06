using System;

public class Class1
{
	public Class1()
	{
        #region Part 01 Service Life Time

        /*
         
        Add three Interfaces(IScoped and ITransient and ISingleTone) with three Implementation
        Why using Guid as a private property? to make sure how many objects created every time we call the constructor

        If they have same guid then they are the same object.

         
         
         */

        /*
         
         In C# and .NET, **service lifetime** refers to how long a dependency (service) exists in memory before being disposed of. This concept is crucial in **dependency injection (DI)** because it controls how instances of services are created and managed.  

## **Service Lifetimes in .NET Core and .NET Framework**
.NET provides three main types of service lifetimes:

| Lifetime Type | Behavior | Common Use Cases |
|--------------|---------|------------------|
| **Transient** (`AddTransient`) | A new instance is created every time it is requested. | Short-lived operations like lightweight services, calculations, or request-specific operations. |
| **Scoped** (`AddScoped`) | A single instance is created per **request (or scope)**. | Used for database contexts (DbContext in EF Core) or unit-of-work patterns. |
| **Singleton** (`AddSingleton`) | A single instance is created and shared throughout the application's lifetime. | Caching, logging, or services that must maintain state. |

---

## **How to Understand It Practically?**
Think of it like a **water dispenser**:
1. **Transient**: Every time you press the tap, you get fresh water. Each request is independent.
2. **Scoped**: Each **bottle** you take for a trip has a fixed amount of water. You can reuse the bottle (service instance) while on the trip, but once you're back, you get a new one.
3. **Singleton**: A **large water tank** used by everyone in the house. It stays the same throughout, and all family members use the same source.

---

## **Example Usage in .NET Core**
Let's say we have a simple logging service:

```csharp
public interface IMyService
{
    void DoWork();
}

public class MyService : IMyService
{
    public void DoWork()
    {
        Console.WriteLine($"Service Instance: {GetHashCode()}");
    }
}
```

Now, let's register this service in `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMyService, MyService>();  // Change to AddScoped or AddSingleton to see the difference

var app = builder.Build();

app.MapGet("/", (IMyService service1, IMyService service2) =>
{
    service1.DoWork();
    service2.DoWork();
    
    return "Check Console!";
});

app.Run();
```

### **What Happens When You Change the Lifetime?**
- `AddTransient`: `service1` and `service2` are different instances (different hash codes).
- `AddScoped`: `service1` and `service2` are the same **within a single request**, but a new request gets a new instance.
- `AddSingleton`: `service1` and `service2` always have the **same instance** across all requests.

---

## **When to Use Each Lifetime?**
| Scenario | Best Lifetime |
|----------|--------------|
| Stateless, short-lived services (e.g., utility functions, API clients) | `Transient` |
| Services that must persist within a request (e.g., database context, unit of work) | `Scoped` |
| Global services that must be shared across the app (e.g., caching, logging) | `Singleton` |

---

This should help you grasp **service lifetimes** in .NET. Do you want me to demonstrate with a working .NET project? 🚀
         
         */


        #endregion

        #region Part 02 Employee Department Relationship  & Configurations

        /*
         
        Add Employees collection in department
        Add Department in the Employee with Id as a foreign key
        Add those changes and relationship inside the configuration class of employee or department
         



         


        
         
         
        How to add the relationship inside the DTO?
        inside EmployeeToCreateDto and EmployeeToUpdateDto
        1] Insert data with related data
        2] Insert with FK
        3] Navigational property










        ++Go To EmployeeController =>
        Inject IDpartmentService in the constructor
        Add ViewData in the create View to return all departments from the service to the controller then to the view.
        
        ++Go to the CreateEditView =>
        Add new variable of type SelectList and assign the ViewData[] to it.
        var departments = new SelectList(@ViewData["Department"] as IEnumerable<DepartmentToReturnDto>, nameof(DepartmentToReturnDto.Id), nameof(DepartmentToReturnDto.Name));

        Add the SelectList to asp-items



        ++Inject in View using @inject



   



        Create new SelectList object, which is used to generate drop-down options
        the first parameter is the IEnumerable
        the Second parameter is the Value [Id]
        the Third parameter is the text displayed [Name]

        <option value = "Id">Name</option>




        ++Inject Service only when it is needed in controller
         
            public IActionResult Create([FromServices] IDepartmentService departmentServices)
        {
            ViewData["Department"] = departmentServices.GetAllDeparments(); 
            return View();
        }

        Inject in View when needed
@inject IDepartmentService departmentServices


        ++Add Department into the index and details view
        1.Add what property you need to add in the DTO
        2.map the new property in the service
        3.use lazy loading service package [Microsoft.EntityFrameworkCore.Proxies]
        4.Add uselazyloadingproxies in the main program inside
        5.Add virtual to the entities navigational property
        6. 



        ++


         */
        #endregion

        #region Search

        /*
         
        ++Adding new Feature (Search, Column, Image, Etc...)

        1- Start with Adding it inside the view then go back to the controller
        2- Ask yourself , What do you need in the Action or the Model in order to help you see it in the View
        3- Add what is required in the Model, 
        4- modify the service in order to Implemented the Business requirements for search results


        Search by AJAX is a real time search.







         
         
         
         
         
         
         
         
         */
        #endregion

        #region 3 - Mapping Using AutoMapper

        /*
         
        Install Auto Mapper in PL where you map your entity to something else
        Allow dependency Injection the main function in the program class for the AutoMapper
         
         
         */
        #endregion


        #region 4 - Unit Of Work

        /*
         
        Unit of work is a design pattern used to minimize th number of requests send to database that helps manage transactional operations
        Create Interface that holds other interfaces of the repositories
        The Idea of rollback or Committing changes if there are two repository speaking to each other like if creating order will change the order column in the customer table
        this ensures that order creation is associated with updating the customer table, they must be one or none.

        Changing to the new pattern require Updating the GenericRepository, Services dealing with UnitOfWork now, 

        After using IUnitOfwork as Dependency Injection we need to update the startup or program main function to allow dependency.
        
        Need dispose the connection to close it after you the user opened it, we cannot let it open.




         */

        /*
         
         
         The **Unit of Work (UoW) pattern** is a design pattern that helps manage transactional operations in software development, particularly in systems that interact with a database. It works alongside the **Repository pattern** to ensure that all changes to the data within a particular business transaction are treated as a single unit.

### Key Concepts of the Unit of Work Pattern:
1. **Coordination of Repositories**: The Unit of Work pattern coordinates multiple repositories in a single transaction. It ensures that all changes made through repositories are committed or rolled back together.
2. **Transaction Management**: It manages the lifecycle of a transaction, providing methods to commit or rollback changes. Typically, it opens and closes the transaction scope, ensuring that all repository operations are executed within a single transaction.
3. **Concurrency Management**: It helps track changes made to objects within a single transaction, and ensures that these changes are saved correctly when the transaction is committed.
4. **Efficiency**: By managing transactions at a higher level, it minimizes unnecessary database calls, and only saves changes when the entire transaction is successful.

### How the Unit of Work Pattern Works:
- **Commit**: When the work is done, all changes are committed to the database.
- **Rollback**: If an error occurs, any changes made to the database are rolled back, ensuring that the database remains in a consistent state.
- **Track Changes**: The Unit of Work keeps track of which entities have been modified, inserted, or deleted during the transaction.

### Why Use the Unit of Work Pattern Over the Repository Pattern?
1. **Transactional Integrity**: While the Repository pattern abstracts the database operations for a particular entity, it doesn't provide any mechanism to handle transactions across multiple entities. The Unit of Work pattern ensures that changes to multiple entities, potentially across several repositories, are all handled within a single transactional scope.
   
   Without Unit of Work, you'd have to manually handle transactions across different repositories, potentially leading to inconsistent or incomplete data in case of failure.

2. **Centralized Commit and Rollback**: The Unit of Work pattern centralizes the commit and rollback logic, which can be beneficial when dealing with multiple repositories and changes. This means that instead of committing or rolling back changes on individual repositories, you can handle the entire set of changes at once, ensuring consistency.

3. **Optimized Performance**: It prevents redundant database queries. Without Unit of Work, each repository might execute its own query to persist changes, but with Unit of Work, changes are tracked and sent to the database in a single batch.

4. **Separation of Concerns**: The Unit of Work helps separate transaction management from business logic. You can keep your repositories focused on data access, while the Unit of Work manages how the data is saved or reverted, keeping the codebase cleaner and more maintainable.

### Example of Usage in C#:
Here’s how the Unit of Work pattern might work in combination with the Repository pattern in a C# application:

1. **Repository Interface**:
   ```csharp
   public interface IProductRepository
   {
       void Add(Product product);
       Product Get(int id);
       IEnumerable<Product> GetAll();
   }
   ```

2. **Unit of Work Interface**:
   ```csharp
   public interface IUnitOfWork
   {
       IProductRepository ProductRepository { get; }
       void Commit();
       void Rollback();
   }
   ```

3. **Unit of Work Implementation**:
   ```csharp
   public class UnitOfWork : IUnitOfWork
   {
       private readonly DbContext _context;
       private IProductRepository _productRepository;

       public UnitOfWork(DbContext context)
       {
           _context = context;
       }

       public IProductRepository ProductRepository
       {
           get
           {
               if (_productRepository == null)
               {
                   _productRepository = new ProductRepository(_context);
               }
               return _productRepository;
           }
       }

       public void Commit()
       {
           _context.SaveChanges();
       }

       public void Rollback()
       {
           // Handle rollback logic, if necessary
       }
   }
   ```

In this case, the **Unit of Work** coordinates repository actions, and when you call **Commit()**, it ensures that changes made to all repositories are saved to the database in a single transaction.

### Summary:
- **Repository Pattern** abstracts the data access for specific entities.
- **Unit of Work Pattern** ensures that multiple operations on repositories are handled in a single transaction, providing consistency and transactional integrity.

Use **Unit of Work** over the Repository pattern when you need:
- To handle transactions across multiple repositories.
- To ensure all database changes are committed or rolled back together.
- To manage multiple entities or business operations in a cohesive manner.
         
         */



        //Complete Example
        /*
         
         ### Benefits of Using the **Unit of Work** and **Repository** Patterns

When used together, these two patterns offer several **advantages** in terms of code maintainability, flexibility, and performance. Here’s a breakdown of the key **benefits**:

#### 1. **Consistency and Transaction Management**
   - **Benefit**: The Unit of Work ensures that changes to different entities (through repositories) are either all committed or all rolled back, maintaining transactional integrity. For example, when creating an order and associating it with a customer, if the order creation fails, the changes made to the customer entity (if any) are also rolled back.
   - **How It Helps**: This guarantees that the system is always in a consistent state, preventing partial updates that could lead to data inconsistencies.

#### 2. **Separation of Concerns**
   - **Benefit**: The Unit of Work and Repository patterns help keep different responsibilities separate. The repository focuses on querying and persisting entities, while the Unit of Work manages the coordination of these repositories and the transactional scope.
   - **How It Helps**: By isolating data access from business logic, it promotes cleaner, more maintainable code. Developers can focus on implementing business rules without worrying about transaction management.

#### 3. **Optimized Performance**
   - **Benefit**: The Unit of Work pattern reduces the number of database calls by grouping multiple operations into a single transaction. This is especially beneficial when you need to perform multiple CRUD (Create, Read, Update, Delete) operations across different repositories within the same business transaction.
   - **How It Helps**: By batching operations into a single database transaction, it can significantly reduce overhead and improve application performance.

#### 4. **Testability**
   - **Benefit**: Both patterns improve testability by abstracting database operations and transactions. Repositories can be mocked or stubbed in unit tests, and the Unit of Work makes it easy to simulate commit/rollback behavior during tests.
   - **How It Helps**: This allows developers to isolate units of functionality, ensuring that individual components can be tested without having to rely on an actual database.

#### 5. **Avoiding Redundant Code**
   - **Benefit**: The Unit of Work centralizes the transaction logic, which means you don’t need to repeat the same transaction handling code in every repository or service class.
   - **How It Helps**: It eliminates boilerplate code by handling commit/rollback logic at a central point, making the system more efficient and easier to maintain.

---

### Where You Might Notice the **Difference** (and How to See It)

#### 1. **Without Unit of Work (Traditional Repository Approach)**
   - **Problem**: If you don’t use Unit of Work and only rely on individual repositories for each entity, each repository may open, commit, or rollback its own transaction. If you make several changes across different repositories (e.g., create an order and associate it with a customer), you’ll have to handle transactions manually. This increases the risk of inconsistencies.
   - **Example Scenario**: Let’s say you add a new order and update a customer’s order count. If you don’t use Unit of Work, one repository might commit while the other fails, leaving your data in an inconsistent state.

   **Code Example**:
   ```csharp
   public class OrderService
   {
       private readonly IProductRepository _productRepo;
       private readonly IOrderRepository _orderRepo;
       private readonly ICustomerRepository _customerRepo;

       public OrderService(IProductRepository productRepo, IOrderRepository orderRepo, ICustomerRepository customerRepo)
       {
           _productRepo = productRepo;
           _orderRepo = orderRepo;
           _customerRepo = customerRepo;
       }

       public void CreateOrder(Order order, Customer customer)
       {
           // Add order
           _orderRepo.Add(order);
           _productRepo.Add(new Product { Name = "Product1", Price = 100 });
           // Update customer
           _customerRepo.Update(customer);
           // Transaction management is manual and error-prone
       }
   }
   ```

   **Issues**:
   - There is no single transaction context — each repository could commit or fail independently.
   - The logic for committing and rolling back transactions must be repeated, which is error-prone and less maintainable.

#### 2. **With Unit of Work**
   - **Benefit**: The Unit of Work pattern manages the transaction for you. You can coordinate changes across repositories and commit them all at once in a consistent and safe manner. The commit or rollback happens in a single place, reducing the chances of inconsistencies.
   - **Example Scenario**: If the order creation fails but the customer update is already committed, you might end up with a customer who is associated with a non-existent order. With Unit of Work, if one operation fails, all changes can be rolled back together.

   **Code Example**:
   ```csharp
   public class OrderService
   {
       private readonly IUnitOfWork _unitOfWork;

       public OrderService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }

       public void CreateOrder(Order order, Customer customer)
       {
           try
           {
               // Add order
               _unitOfWork.Orders.Add(order);
               // Update customer
               _unitOfWork.Customers.Add(customer);
               // Commit all changes in one go
               _unitOfWork.Commit();
           }
           catch (Exception)
           {
               // If an error occurs, rollback all changes
               _unitOfWork.Rollback();
               throw;
           }
       }
   }
   ```

   **Benefits**:
   - The `UnitOfWork.Commit()` ensures that all changes are persisted together.
   - If one repository fails, the `Rollback()` method ensures that the database state remains consistent.

---

### How to "See" the Difference

#### 1. **Without Unit of Work**
   - **Testing Issues**: You might need to write custom logic for managing transactions across repositories.
   - **Error Handling**: If a failure occurs after making partial changes to the database (e.g., adding an order but failing to update the customer), you may need complex code to undo or compensate for those changes.
   - **Performance Overhead**: You may execute multiple database transactions, resulting in extra round trips to the database and increased overhead.

#### 2. **With Unit of Work**
   - **Centralized Control**: You can observe the code to see that the `UnitOfWork.Commit()` method consolidates multiple repository operations into a single transaction.
   - **Better Error Handling**: If one part of the transaction fails, the `Rollback()` ensures that the database remains in a consistent state.
   - **Less Boilerplate**: You won’t need to repeat transaction management code in each repository or service.

---

### Practical Example: **E-Commerce Order Processing**

#### Without Unit of Work:
You may have multiple repositories that add products, orders, and update customer data, but each one may commit independently, leading to possible inconsistencies.

#### With Unit of Work:
All repository operations (adding an order, updating a customer, etc.) are handled within a single transactional context. If the order is created successfully, the customer’s data is updated, and everything is committed at once. If any error happens, all operations are rolled back.

### **How to "see" it in Practice**:
- **Before**: You would need to manually manage transaction boundaries across multiple repository calls, which can be error-prone and hard to maintain.
- **After**: You can see the Unit of Work simplifying the transactional flow, improving consistency and maintainability.

---

### Conclusion:
By using **Unit of Work** and **Repository**, you ensure:
- **Consistency** across multiple repositories.
- **Clean separation of concerns** between business logic and data access.
- **Efficiency** in transaction management.
- **Testability**, as both patterns facilitate mocking and unit testing.

         
         */
        #endregion

        #region 5 - Attachment Service

        /*
         
         Add in BLL: Common/Service/AttachmentService/IAttachmentService

        Install package called "Microsoft.AspNetCore.Http"

        Follow the steps to Upload and Delete file in your app


         
         
         */
        #endregion

        #region  6 - ReFactor Create Employee To Upload Image

        /*
        


        from Html View I will get file so I need to create property in the model to save Data in the same type using IFormFile

        
        1]Add Property of string to save filePath in Employee Entity
        2]Add IAtachmentService to the EntityService
        3]Allow Dependency injection in the Main program
        4]Map ImageName string to Image file


        <form method="post" asp-controller="Employee" asp-action="@ViewData["Title"]" class="col-8" enctype="multipart/form-data">
         
         
         */
        #endregion


    }
}
