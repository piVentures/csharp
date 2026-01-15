using System;
namespace Generics
{
   
    public class MainClass
    {
        private static void Main()
        {

            // Calling the generic method AreEqual with int type parameters
            bool Equal = Calculator.AreEqual<int>(10, 20);

            if (Equal)
            {
                Console.WriteLine("Values are equal");
            }
            else
            {
                Console.WriteLine("Values are not equal");
            }
        }
    
    public class Calculator
        {
            // Generic method to compare two values of the same type created using a type parameter T/any thing to represent the type of the values being compared. it makes your code type independent and reusable for different data types.
            public static bool AreEqual<T>(T Value1, T Value2)
            {
                return Value1.Equals(Value2);   
            }
        }
    
    }
}

// this way we made the method generic along the same lines it is also possible to create generic classes, interfaces and Delegates using type parameters.

// not using system.object as it requires boxing and unboxing for value types which is less efficient. Generics provide type safety at compile time and better performance.