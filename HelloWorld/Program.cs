using System;

class Program
{
    static void Main1()
    {
        Console.WriteLine("lets know each other, your first name: ");
        string FirstName = Console.ReadLine();

        Console.WriteLine("lets know each other, your last name: ");
        string LastName = Console.ReadLine();
        Console.WriteLine("Hello there! {0}, {1}", FirstName, LastName);
    }
    static void Main2()
    {
    int [] Numbers = new int [3];
    Numbers[0] = 1; 
    Numbers[1] = 2;
    Numbers[2] = 3; 
    int i = 0;
// for each loop
    foreach (int num in Numbers)
    {
        Console.WriteLine(num);
    }
// for loop 
    for (int j = 0; j < Numbers.Length; j++)
    {
        Console.WriteLine(Numbers[j]);
    }
// while loop
    while (i < Numbers.Length)
    {
        Console.WriteLine(Numbers[i]);
        i++;      

    }
    }
public void EvenNumbers(){
    int Start = 0;
    while (Start <= 20)
    {
        if (Start % 2 == 0)
        {
            Console.WriteLine(Start);
        }
        Start++;
    }
}
public static void NextEvenNumers(){
    int Start = 20;
    while (Start <= 50)
    {
        if (Start % 2 == 0)
        {
            Console.WriteLine(Start);
        }
        Start++;
    }
}
  
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World");
        Main1();
        Main2();
        Program p = new Program();
        p.EvenNumbers();    
       Program.NextEvenNumers();
       ProjectA.TeamA .ClassA.Print();
       ProjectA.TeamB .ClassA.Print();
}
}



namespace ProjectA{
    namespace TeamA{
        class ClassA{
            public static void Print(){
                Console.WriteLine("Team A print Method");
            }
        }
    }
}


namespace ProjectA{
    namespace TeamB{
        class ClassA{
            public static void Print(){
                Console.WriteLine("Team B print Method");
            }
        }
    }
}