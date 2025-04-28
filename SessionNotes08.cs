using System;

public class Class1
{
	public Class1()
	{
        #region 1 - Asynchronous Vs Synchronous

        /*
         
        ++Keywords:
        
        -Task  => Task Keyword wrap the return and the method to Identify it as Asynchronous
        -async => Used in the signature of the Method
        -await => Used before the Calling the Method to identify it as Asynchronous


        ++Entity Framework methods
        There are a lot methods ends with async that indicate that methods use Asynchronous 



        +++Refactoring

        ++Start DAL: 
        1.Go to IGeneric and Generic repository
        IEnumerable<T> GetAll(bool AsNoTracking = true); Re-factor => Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true);
    
        2.IUnitOfWork & IUnitOfWork

        ++BL:
        Services => Rename, Task, Async, Await

        ++PL:
        Controller
        Views

       



         
         
         */
        #endregion

        #region 2 - BackEnd Security Overview

        /*
         
        We have 3 Steps that we should follow to secure our web app using Microsoft Package
        
        1-Identification(Registration)
        2-Authentication
            -Local(SQL)
            -Active Directory(Make users manual)
            -External Server(Google, Facebook)
            -Federated server(Souq => Amazon)


        3-Authorization
         
         
         */
        #endregion

        #region 3 - Microsoft Identity Package

        /*
         
        It is recommended to create 2 databases one for business[Employee, Department] and One for Security[Role, User]
        1- Install Microsoft Identity Package in DAL Dependencies.
        2- Add IdentityUser and IdentityRole DbSets to ApplicationDbContext.
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        3- Make DbContext inherit from IdentityDbContext
        4-Add this to onModelCreating in the DbContext:  base.OnModelCreating(modelBuilder);
        5- AddMigration
        6- UpdateDatabase



         
         
         */
        #endregion

        #region 4 - Account Controller 


        /*
         
        Go Create Account Controller that has These 3 Actions:
        Register, Login, SignOut 


        Add _AuthLayout as a razor layout for not-users different from the home layout for users 

        look how we added new features for the user and how we modified the ApplicationDbContext


         
         */
        #endregion

        #region 5 - Account Controller - Register

        /*
         
        Add register logic to the controller
        first add the view of the register get request to get the data, validate it, and then send it again to the database.
        Add AuthLayout to the get view of the register
        Add Add new class "RegisterViewModel" used to represent data we will get from the user.
        Add the Post Action to the controller to Add the new user to the database
        Add a new Entity called Identity "ApplicationUser" that inherit from IdentityUser that come form using Microsoft.AspNetCore.Identity; 
        Add this to DbContext: IdentityDbContext<ApplicationUser>

        Go to MAin and Add this:
           builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>

            {
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 5;
            }

            )
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication();

        Go back and complete the Action Post
        then Add migration
        then Update Database
        then run




         
         
         */
        #endregion

        #region 6 - Account Controller - Login

        /*
         
        Add GetHttp Login Action inside the controller
        Add view for Login Action same to register
        Add new ViewModel to Extract user data
        Add new Action for the post and check if the ViewModel sending the correct user to login
        Add

         
         
         */
        #endregion

        #region 7 - Account Controller - SignOut

        /*
         
        In Sign Out we will delete the cookies and token and redirect the user to the login page
        Add new Action for the sign out
        Add new button in the layout to go to the method or the action
        
         
         */
        #endregion
    }
}
