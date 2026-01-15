using System;

// system.collections.generic is required for using List<T>
using System.Collections.Generic;


// delegate is a type tsafe function pointer that can reference methods with a specific signature
// why we use delegates is to pass methods as arguments to other methods this simplifies code and makes it more flexible and reusable


class Kira
{

    public static void Main()
    {
        // creating a list of employees
        List<Employee> employeeList = new List<Employee>();

        employeeList.Add(new Employee() { Id = 1, Name = "Kira", Salary = 5000, Experience = 6 });
        employeeList.Add(new Employee() { Id = 2, Name = "John", Salary = 4000, Experience = 4 });
        employeeList.Add(new Employee() { Id = 3, Name = "Jane", Salary = 6000, Experience = 7 });
        employeeList.Add(new Employee() { Id = 4, Name = "Doe", Salary = 3000, Experience = 3 });   

    
    
// creating delegate instance pointing to Promote method
       IsPromotable promotable = new IsPromotable(Promote);

// passing delegate as parameter to PromoteEmployee method
       Employee.PromoteEmployee(employeeList,promotable);
    }
   
//   method matching delegate signature to check if employee is eligible for promotion 
    public static bool Promote(Employee emp)
    {
        if (emp.Experience >= 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

// defining a delegate to represent methods that take an Employee object and return a bool
delegate bool IsPromotable(Employee emp);

// Employee class with properties
class Employee
{
    public int Id{get; set; }
    public string Name{get; set; }  

    public int Salary{get; set; }
    public int Experience{get; set; }   

// static method to promote employees based on delegate
    public static void PromoteEmployee(List<Employee> employeeList,IsPromotable IsElligibleToPromote)
    {
        foreach (Employee employee in employeeList)
        {
            if (IsElligibleToPromote(employee))
            {
                Console.WriteLine(employee.Name + " Promoted");
            }
        }
    }
}

// summary of steps to use delegates:

// step one: create employee list
// step two: define delegate and method matching its signature
// step three: pass delegate to static method to promote eligible employees 

// its advantage indepth is that it allows methods to be passed as parameters, enabling flexible and reusable code. This is particularly useful for scenarios like filtering or processing collections based on different criteria without changing the method implementation.