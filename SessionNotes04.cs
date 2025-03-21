using System;

public class Class1
{
	public Class1()
	{
        #region Part 01 Revision and Index

        /*
         
        Architecture Pattern => (3 Tier Architecture)
            - **Separation of Concerns:** The controller, service, and repository layers are decoupled by depending on interfaces.
            - **Flexibility:** You can easily switch between data sources (SQL, MongoDB, or an external API) by changing the DI registration—controlled via configuration.
            - **Testability:** Because classes depend on abstractions, you can substitute mocks when writing unit tests.
            - **End-to-End Flow:** From receiving an HTTP request, through DI-based controller instantiation, down to data access and back as an HTTP response.


        Design Pattern => (Depndency Injection)
        ## Why Is Dependency Injection Important?

            1. **Reduced Coupling:**  
               By depending on abstractions (typically interfaces) rather than concrete classes, your code becomes more flexible. For example, a controller depends on an interface like `IProductRepository` rather than a concrete `ProductRepository`. This makes swapping implementations easier.

            2. **Enhanced Testability:**  
               When classes obtain their dependencies via DI, you can inject mocks during testing. This isolation means you’re testing only the code under test, not its dependencies.

            3. **Improved Maintainability:**  
               Centralizing dependency creation in a DI container means that changes (like refactoring or replacing a service) occur in one place, reducing code duplication and error risk.

            4. **Separation of Concerns:**  
               DI separates the object’s behavior (its logic) from the process of obtaining its required resources. This leads to cleaner, more focused classes.

        
        
        
        In most cases, for each entity, you will create:
        ✅ One interface for the repository (handles data access)
        ✅ One interface for the service (handles business logic)
        ✅ Multiple implementing classes if needed

        But you will not always need multiple implementations. Let’s explore when and why you might need multiple repository or service classes.
        
        
        ## **5. Summary**
            | **Layer** | **Common Structure** | **When to Create Multiple?** |
            |-----------|----------------------|------------------------------|
            | **Repository** | 1 interface + 1 implementation | - Different databases (SQL, NoSQL) <br> - Specialized queries (e.g., `StatisticsRepository`) |
            | **Service** | 1 interface + 1 implementation | - Different business use cases (notifications, approvals) <br> - Different client needs (standard vs. premium services) |

            ---

        ## **Final Thought**
            When starting a project, you can **begin with one repository and one service per entity**.  
            Later, **add more** only if:
            - You need to support **multiple data sources**.
            - The repository/service does **too many things** (separate concerns).
            - Business rules **require different behaviors**.
        
        MVC Project Steps:
        1- Create Solution
        2- Add ASP.NET MVC Project named (ProjectName.PL) PL: Presentation Layer
        3- Add Class Library Named (ProjectName.DAL)  DAL : Data Access Layer
        4- Add Class Library Named (ProjectName.BLL) BLL: Business Logic Layer
        5- Add project Reference From DAL Inside BLL
        6- Add Project Refeerenc From BLL Inside PL
        7- Add GlobalUsing Class Inside Eac Project to use Namespaces globally
        8- Steps in DAL Project:
            8.1- Add Microsoft.EntityFrameworkCore.SqlServer package
            8.2- Add Entities(Models) Folder which has the BasicEntityClass for All Models and Folder for each Model
            8.3- Add Presistence Folder( persistence layer is a way to SAVE and RETRIEVE items that your application uses)  => Data Folder, Repositories Folder
                8.3.1- Data Folder: Add Configurations (Has Folder for each Model), DataSeed, Migrations, ApplicationDbContext class, ApplicationDbContextSeed class
                8.3.2- Repositories Folder:  Has Folder for each Model, and each folder has one or more classe for Implemtnon and one one or more Interface for Abstraction
            8.4-Add Migrations if Completed Cofigurations
        9- Steps in BLL Project:
            9.1- Add project reference for DAL project
            9.2- Add Service Folder:
                9.2.1- For each Model Add folder, Insde each one create Interfaces and the Classes the Implement the Logic 
            9.3- Add DTOs[Data Transfer objects] Folder:
                9.3.1- Each DTO repersent the Data that will be shown to Client based on Action or View or the CRUD operation

        10- Steps In PL Project:
            10.1- Add BLL as Project reference  
            10.2- Add Microsoft.EntityFrameworkCore.Tools package
            10.3- Add Conncection string in appsettings.json
            10.4- Register to Services Inside Main (Dependency Injection)
                10.4.1- ApplicationDbContext with ConnectionStrings
                10.4.2- Models's Repositories
                10.4.3- Models's Services
            10.5- Add Folders for each Controller
            10.6- Add Actions 
            10.7- Add Views
            
            
        Create Controller that has Actions that retrun Views
        We use Controller to send Data from Repo to the View
        we use View to Update Data inside the Repo through the Controller

         
         
         */
        #endregion

        #region 2 - Department Controller - Create

        /*
         
        using ModelState for vaildation in server side(C# App)

         using try and catch for now, and we will use Middleware later
        what are the execption that we wait? for example a bug in migration
        if we went to database and got 0 rows effected, we should show meesage to user and return to Create View.
        if we got Execption we should Log the error in our Database, or Console for development

         
         */
        #endregion

        #region MyRegion

        /*
         
         Always rember to write @model DTO at the top of the view page.
        If you got error you cannot find, check URL? you may have typo in your View

       
         
         */
        #endregion

        #region Edit
        /*
         
         In case Dto reuren properties you don't need? the Clean Solution is to Add ViewModel.
        ViewModel is a class that has only the properties you want to show from the DTO.
         
         */
        #endregion
    }
}
