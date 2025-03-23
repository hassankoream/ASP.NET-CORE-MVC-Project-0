using System;

public class Class1
{
	public Class1()
	{
        /*
		 Cannot instantiate implementation type 'Demo.DAL.Presistance.Repositories.Employees.IEmployeeRepository' 
		for service type 'Demo.DAL.Presistance.Repositories.Employees.IEmployeeRepository'.'

		 problem:  builder.Services.AddScoped<IEmployeeRepository, IEmployeeRepository>();
		 Solution:  builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
		 
		 */

		/*
		 if Id not Added to UpdateEntity during the mapping, you will Add new record every time  
		 
		 */
    }
}
