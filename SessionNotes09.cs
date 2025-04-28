using System;

public class Class1
{
	public Class1()
	{
        #region 1 - Forget Password

        /*
         
        
        Add new Action for th forget password
        Add View for that get Action to display the form
        
        
        Go and Add the new Action for post to receive the email from the user
        Build a new Entity to send the Email(to, subject, body)

         
         */
        #endregion

        #region 2 - Send Email

        /*
         
        Add EmailSettings service to BL
        Add Interface and class
        Allow Dependency Injection for IEmailSetting


        Implementation of SendEmail will be different based on the host(gmail, yahoo, and so on)

         Add IEmailSetting to th contractor of the Account controller to use it to send the email
         
         */
        #endregion

        #region 3 - Reset Password

        /*
         
        We need create the reset password action [post] and [get]

        Need to use TempData to send email and token from action to action without getting hacked 

         
         
         */
        #endregion

        #region 4 - UserController - Index

        /*
         
        

        Add Action to get all thee users from database and display them in the view
        Add new View model to display only the data you need
        
         




        in appsettings.json file add this to make SQL know how to execute more than one query at the same time at the connection.
        MultipleActiveResultSets = true

         
         */

        #endregion

        #region 5 - UserController - Details - Edit - Delete

        /*
         
        Create User is already done in Register


         
         */
        #endregion

        #region 6 - RoleController - Index - Create - Edit - Details - Delete

        /*
        

        Start by creating the controller then Actions then views and don't forget to add new viewModel for the client
        in database there is a Microsoft service for the roles called roleManager like the UserManager 
        
        Update Layout if needed
        Update ViewImports
         

         
         
         
         */
        #endregion

        #region 7 - Assign_Remove Roles To_From Users

        /*
         
        will start this from roles not from users, Adding new view model for the UserRoleViewModel

        Adding users to the RoleViewModel

        getting users from database using UserManager 
         
         
         
         */
        #endregion
    }
}
