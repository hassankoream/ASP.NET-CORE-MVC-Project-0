using System;

public class Class1
{
    public Class1()
    {
        #region 1 - IEnumerable Vs IQueryable

        /*
         
        ### **Difference Between `IEnumerable` and `IQueryable` in C#**

Both `IEnumerable` and `IQueryable` are used to work with collections of data, but they have key differences in terms of where and how the data processing occurs.

| Feature        | `IEnumerable<T>` | `IQueryable<T>` |
|---------------|----------------|----------------|
| **Definition** | Represents a forward-only, read-only collection that can be iterated using `foreach`. | Represents a queryable collection that allows LINQ-to-SQL (or Entity Framework) queries to be executed on a database. |
| **Namespace** | `System.Collections.Generic` | `System.Linq` |
| **Execution** | Deferred execution, but processes data in-memory. | Deferred execution and translates queries to SQL for database execution. |
| **Performance** | Less efficient for large datasets because filtering occurs in-memory. | More efficient for large datasets because filtering happens at the database level. |
| **Use Case** | Suitable for in-memory collections like Lists, Arrays, and Collections. | Suitable for querying databases using LINQ-to-SQL or Entity Framework. |
| **Query Execution** | Executes on the client side (pulls all data and then filters). | Executes on the server side (filters before fetching data). |
| **Modification** | Cannot modify the collection during iteration. | Cannot modify data directly, as it represents a queryable source. |

### **Examples**

#### **Using `IEnumerable` (In-Memory Processing)**
```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);  // Filters in-memory

foreach (var num in evenNumbers)
{
    Console.WriteLine(num); // Output: 2, 4, 6
}
```
- The entire list is loaded into memory before filtering.

#### **Using `IQueryable` (Database Processing)**
```csharp
using (var context = new MyDbContext())
{
    IQueryable<User> users = context.Users.Where(u => u.Age > 18); // Query translated to SQL
    
    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }
}
```
- The filtering (`WHERE Age > 18`) is applied at the database level before fetching data.

### **When to Use What?**
- Use **`IEnumerable<T>`** for **in-memory collections** (e.g., `List<T>`, `Array<T>`).
- Use **`IQueryable<T>`** when **querying a database** to leverage **lazy loading** and optimize performance.

Would you like me to explain any part in more detail? 🚀
         
         
         */
        #endregion

        #region 2 - Client-Side Validation

        /*
        
        We use Client-Side Validation to prevent too many Invaild requests to the server.
        This is not include tools like Postman, attackers still have the chance to send many requests.
        
        - open wwwroot/lib/Jquery/: then drag Jqueryvalidations to the create and edit view, in order to make client side vaildation
        - Add it as a section to call it anytime, anywhere you need, not just at @RenderBody().
        - Add it JQuery Vaildation after JQuery scripts.  @await RenderSectionAsync("ValidationScripts", required: false)
        - 
         
         */
        #endregion
    }
}
