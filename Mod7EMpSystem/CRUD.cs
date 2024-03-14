using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod7EMpSystem
{
    interface ICRUD
    {
        int GetMaxId();
        void AddRecord(Employee emp);
        void DeleteRecord(Employee emp);
        ICollection<Employee> GetAll();
        Employee FindEmployee(int id);
        void UpdateRecord(int id, Employee emp);
        ICollection<Department> GetDepartments();
    }

    class CRUD : ICRUD
    {
        PCAD10EmployeesEntities1 entities; // representing db
        public CRUD()
        {
            entities = new PCAD10EmployeesEntities1();
            // foraech below allows us to add department name in the employees table
            foreach(var emp in entities.Employees)
            {
                foreach(var d in entities.Departments)
                {
                    if(emp.DeptId == d.DeptId)
                    {
                        emp.DeptName = d.DeptName;
                    }
                }
            }
        }


        public void AddRecord(Employee emp)
        {
            entities.Employees.Add(emp);
            entities.SaveChanges(); // inserts a new record in the database tabel by firing insert query
        }

        public void DeleteRecord(Employee emp)
        {
            entities.Employees.Remove(emp);
            entities.SaveChanges();
        }

        public Employee FindEmployee(int id)
        {
            return entities.Employees.Find(id);
        }

        public ICollection<Employee> GetAll()
        {
            return entities.Employees.ToList();
        }

        public ICollection<Department> GetDepartments()
        {
            return entities.Departments.ToList();
        }

        public int GetMaxId()
        {
            return entities.Employees.Max(e => e.EmpId);
        }

        public void UpdateRecord(int id, Employee emp)
        {
            var empToUpdate = entities.Employees.Find(id);
            empToUpdate.EmpId = emp.EmpId;
            empToUpdate.EmpName = emp.EmpName;
            empToUpdate.EmpSalary = emp.EmpSalary;
            empToUpdate.DeptId = emp.DeptId;
            entities.SaveChanges();
        }
    }
}
