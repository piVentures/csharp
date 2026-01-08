using System;

class Customer{
    string _firstName;
    string _lastName;

    public Customer(string firstName, string lastName){
        this._firstName = firstName;
        this._lastName = lastName; 
    }
    public void PrintFullName(){
        Console.WriteLine("Full-Name {0} {1}", this._firstName, this._lastName);
}
}

class Circle
{   
    static float _PI = 3.14F;
    int _Radius;

    public Circle(int Radius)
    {
        this._Radius = Radius;
    }

    public float CalculateArea(){
        return Circle._PI * this._Radius * this._Radius;
    }

}

class Program{
    public static void Main(){
        // customer
        Customer C1 = new Customer("John", "Doe");
        C1.PrintFullName();
        // circles
        Circle Cir1= new Circle(5);
        float Area1 = Cir1.CalculateArea();
        Console.WriteLine("Area of Circle1: {0}", Area1);

        Circle Cir2= new Circle(6);
        float Area2 = Cir2.CalculateArea();
        Console.WriteLine("Area of Circle2: {0}", Area2);
    }
}