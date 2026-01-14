using System;
public class Employee{
    public string FirstName = "kira";
    public string LastName = "23j";

// we use virtual keyword to allow overriding in derived classes
    public virtual void printFullName(){
        Console.WriteLine(FirstName + " " + LastName);
    }   
}

public class PartTimeEmployee : Employee{
    // override keyword to override base class method
    public override void printFullName(){
        Console.WriteLine(FirstName + " " + LastName + " - Part Time");
    }   
}

public class FullTimeEmployee : Employee{
    public override void printFullName(){
        Console.WriteLine(FirstName + " " + LastName + " - Full Time");
    }   
}   

public class TemporaryEmployee : Employee{
    public override void printFullName(){
        Console.WriteLine(FirstName + " " + LastName + " - Temporary");
    }   
}   

public class Program{
    public static void Main(string[] args){

        // polymorphism enables you to treat derived class objects as base class objects at runtime where baseclass use virtual methods and derived class override them
        // if there is no override, base class method is called
         
       Employee[] employees = new Employee[4];

       employees[0] = new Employee();
       employees[1] = new PartTimeEmployee();
       employees[2] = new FullTimeEmployee();
       employees[3] = new TemporaryEmployee();  

       foreach(Employee emp in employees){
           emp.printFullName();
       }

    }   
}