using System;

public class Class1
{
	public Class1()
	{
        #region Routing

        //Need to master MVC, Need to master any web App, Routing is the Solution to all your problems
        //Ask yourself Three questions:
        //1.Where was I?
        //2.Where do I wanna go?
        //3.Where could I go?

        #endregion
        #region 1 - Employee Entity - Configs - Migration

        /*
        1- Inside Entiteies Add Employee Folder, then Add Employee class that inherite from Base
        2- Create Common Folder to Add Common Types like Enums
        3- Add EmployeeConfiguration Class Inside Configuration Folder
        4- Apply Configuration Inide the DbContext and Employees property as DbSet
        5- Add Migration through PL(Who has the Connection string) but inisde DAL(Who has teh Migrations Folder)
        6- Update Database

         
         
         */
        #endregion

        #region 2 - Employee Repository AND GenericRepository

        /*
        -Create your IRepo and Repo fro Employee
        
        you will notice that you you can use IGeneric Type of that to help you with All types 
        - Create Interface IGenericRepository<T> where T is a BaseEntity, and it has All the Signatures
        - Create Class GenericRepository<T> where T is a BaseEntity, and it Has All the Implemntion of IGenericRepository
        - Every IEntityRepository Implement IGenericRepository
        - Every EntityRepository Inherite from GenericRepository and Implement IEntityRepository and Chain on GenericRepository Constructor

         
         */
        #endregion


        #region 3 - Employee Service


        /*
         
        1- Create DTOs With Vaildations
        2- Create IEntityService , Create EntityService and use IEntityRepository not the Generic type.
        3- Regesiter To Allow Dependency Injection Add Service Inside the Main Function
        4- Implemen Methods Inside the  EntityService with the requiered Manual Mapping
        5- We need to Update the Delete operation in GenericRepository to Update the State of the object notto delete it entirly, in case we need to recover the record to the user.
        6- We need to Update the Get operation in order to retrive only what is not deleted from the database. 
        7- Update EntityService with NOT IsDeleted filter

         
         */
        #endregion

        #region 4 - Employee Controller - Index - Create

        /*
        Inside PL Layer
        

        The Story: Request : Has Controller and Action and Paramters => Controller: Create instance of Service => Service => Repository => DbContext => Database
                   Response: Has View Binded with Model in a Razor Page (C# with Html)
        1- Create EntityController
        2- Create Action
        3- Each Action has a return View (may be it is RedirecttoAction)[May be return to the same view with the model holding its current values]
        4- Each Model sent to the View From Service is DTO Model
        5- open or Add View for Each Method, and Add the Model you are trying to show.
        6- In View Import inside the Pl layer don't forget to add the namespace of the DTO Folder
        7- In Layout Add anchor to the new Controller in order to navigate Easily.
        8- Add ViewModel only if you need to show Different Data that the Dto class
        9- remember why Edit post take Id and EmployeUpdate?
        10- In order to dispaly Data in any kind, you should Add ''

         
         */
        #endregion

    }
}
