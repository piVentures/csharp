using System;

public class Employee
{
    public string FirstName;
    public string LastName;
    public string Email;
    public void printFullName(){
        Console.WriteLine("Full Name: {0} {1}", FirstName, LastName);
     }

}
// inheritance is important for code reusability
// multiple inheritance is not supported in C#
// multilevel inheritance is supported in C#

public class FullTimeEmployee : Employee {
    float YearlySalary;

}

public class PartTimeEmployee : Employee {
    float HourlyRate;
    // method hiding important when derived class wants to implement
    // same method name as in parent class using new keyword
 public new void printFullName(){
        Console.WriteLine("Full partime employee Name: {0} {1}", FirstName, LastName);

        // to call parent class method use base keyword
        // base.printFullName();


     }

}

public class ParentClass{
    public ParentClass()
    {    Console.WriteLine("I am Parent Class Constructor");
    }
    public ParentClass(string msg)
    {    Console.WriteLine("I am Parent Class Constructor with message: " + msg);
    }
}

public class ChildClass : ParentClass{
    // by default parameter less constructor of parent class is called
    // using base keyword we can call parameterized constructor of parent class
    public ChildClass() : base("Hello from Child Class")
    {       
    Console.WriteLine("I am Child Class Constructor");
    }
}

public class Program{
    public static void Main(string[] args){
        
    FullTimeEmployee fte = new FullTimeEmployee();
    fte.FirstName = "Kira";
    fte.LastName = "23j";
    fte.printFullName();

    PartTimeEmployee pte = new PartTimeEmployee();
    pte.FirstName = "Guest";
    pte.LastName = "User";
    pte.printFullName();    // calling child class method
    ((Employee)pte) .printFullName();  // calling parent class method casted to parent class

// we can also create parent class reference for child class object but not vice versa
    Employee pte2 = new PartTimeEmployee();
    pte2.FirstName = "Test";
    pte2.LastName = "User";
    pte2.printFullName();  // calls parent class method even though object is of child class    

// parent class constructor called first then child class constructor
    ChildClass cc = new ChildClass();
}
}