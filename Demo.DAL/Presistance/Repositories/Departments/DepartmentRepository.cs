﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance;
using Demo.DAL.Presistance.Data;

namespace Demo.DAL.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
    {

        /*
        //We need to use DbContext in order to get elements from DB.

        //ApplicationDbContext context = new ApplicationDbContext(); //this is not Good, we must use Dependency Inversion
        //Context Life time is depend on the Life time of the object from type DepartmentRepository.
        //the Second problem that you will use Context constructor that take options as a paramter, then the options now depend again on the object.
        //What is the Sol?
         there is a common solution called Constructor Injection


         
         */
        private readonly ApplicationDbContext _Context;

        public DepartmentRepository(ApplicationDbContext context) //Injection
        {
            _Context = context;
          
        }
        public IEnumerable<Department> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return _Context.Departments.AsNoTracking().ToList(); //detached

            return _Context.Departments.ToList(); //unchanged
        }

        //Get
        public Department? GetById(int Id)
        {
            //return _Context.Departments.Local.FirstOrDefault(D => D.Id == Id);
            return _Context.Departments.Find(Id); //Search Locally , In case Found  => Return , else => search database.

        }
        public int AddDeparment(Department department)
        {
            _Context.Departments.Add(department); //Save Locally
            int RowAffected = _Context.SaveChanges(); //Apply Remotely
            return RowAffected;
        }
        public int UpdateDeparment(Department department)
        {
            _Context.Departments.Update(department); //Modified
            int RowAffected = _Context.SaveChanges(); //Unchanged
            return RowAffected;
        }

        public int DeleteDeparment(Department department)
        {
            _Context.Departments.Remove(department); //Deleted
            return _Context.SaveChanges(); //untrack
        }

        public IQueryable<Department> GetAllQueryable()
        {
            return _Context.Departments;
        }




        //Get ALL
        //Create
        //Update
        //Delete



    }
    //Repository<TEntity>

    //using generic Repository could help you when Entites shares same operation Implementation.


}
