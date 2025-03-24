using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Entities.Employees;

namespace Demo.DAL.Presistance.Data
{

    //Why we need Repo to talk to DbContext? Seperation of concern , Connection to Database
    /*
     if not using Dependency Injection we will open Connection with Database every time we make object???????

    Repo talks to ApplicationDbContext
    Employee talks to EmployeeRepo to open new Connection
    Deparment talks to DempartmentRepo to Open new Connection


     
     
     */
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //Old Approch without options and Dependency Injection
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.; Database=MVCProject01; Trusted_Connection=true; TrustedServerCertificate=true");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.ApplyConfiguration(new DepartmentConfigurations());


            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }


        /*
         
        That line of code is not referring to **Assembly Language**; it's using **Reflection** in C# to dynamically apply Entity Framework configurations from an assembly. Let me break it down for you step by step:

---

### **Understanding the Code**
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}
```

### **Step-by-Step Explanation**
1. **Method Override:**  
   - `OnModelCreating` is a method in `DbContext` that allows you to configure your database model when using **Entity Framework Core**.
   - You override this method to define custom configurations for your entity models.

2. **Applying Configurations:**  
   - `modelBuilder.ApplyConfigurationsFromAssembly(...)` automatically applies all configurations found in a given assembly.

3. **Assembly Reference:**  
   - `typeof(ApplicationDbContext).Assembly` gets the **assembly** where the `ApplicationDbContext` class is defined.
   - This means Entity Framework will search for all **entity configurations** (classes implementing `IEntityTypeConfiguration<T>`) in the same assembly as `ApplicationDbContext` and apply them automatically.

---

### **What This Code Does**
Instead of manually adding configurations like:
```csharp
modelBuilder.ApplyConfiguration(new UserConfiguration());
modelBuilder.ApplyConfiguration(new ProductConfiguration());
```
It automatically loads all configurations in the assembly.

---

### **Why Use This?**
✅ **Less manual work** – You don't need to register each entity configuration separately.  
✅ **More maintainable** – If you add new configurations, they are applied automatically.  
✅ **Better separation of concerns** – Keeps `OnModelCreating` cleaner.

---

### **Example of Configuration Class**
If you have a class like this:
```csharp
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
    }
}
```
Entity Framework will **automatically** apply this configuration because of the `ApplyConfigurationsFromAssembly` method.

---

### **Conclusion**
- This is **not Assembly Language**, it's a **C# Reflection** technique.
- It helps Entity Framework **automatically** find and apply configurations.
- It makes your `DbContext` cleaner and more maintainable.

Let me know if you need further clarification! 🚀
         
         
         */
    }
}
