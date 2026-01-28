using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // ---------------------------------------
        // SCENARIO 1: EXTENDING BUILT-IN TYPES
        // ---------------------------------------
        string email = "kirubel@gmail.com";
        Console.WriteLine($"Is valid email? {email.IsValidEmail()}");

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 2: EXTENDING CUSTOM CLASSES
        // ---------------------------------------
        User user = new User { Name = "Kirubel", Age = 24 };
        Console.WriteLine(user.Introduce());

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 3: EXTENDING COLLECTIONS
        // ---------------------------------------
        List<int> numbers = new List<int> { 10, 20, 30, 40 };
        Console.WriteLine($"Average value: {numbers.AverageValue()}");

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 4: EXTENSION METHODS + LINQ
        // ---------------------------------------
        numbers
            .FilterGreaterThan(20)
            .ForEachPrint();

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 5: EXTENSION METHOD VS REAL METHOD
        // ---------------------------------------
        ConflictingExample example = new ConflictingExample();
        example.Show(); // Real method wins over extension

        Console.WriteLine("\n=== End of Demo ===");
    }
}

// =======================================================
// SCENARIO 2 SUPPORTING CLASS
// =======================================================

class User
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Real method
    public void Show()
    {
        Console.WriteLine("User.Show() – real method");
    }
}

// =======================================================
// SCENARIO 5 SUPPORTING CLASS
// =======================================================

class ConflictingExample
{
    public void Show()
    {
        Console.WriteLine("ConflictingExample.Show() – real method");
    }
}

// =======================================================
// EXTENSION METHODS – STRING
// =======================================================

static class StringExtensions
{
    // CRITICAL RULES:
    // 1. Static class
    // 2. Static method
    // 3. First parameter uses 'this'
    public static bool IsValidEmail(this string value)
    {
        return value.Contains("@") && value.Contains(".");
    }
}

// =======================================================
// EXTENSION METHODS – USER
// =======================================================

static class UserExtensions
{
    public static string Introduce(this User user)
    {
        return $"Hi, I am {user.Name} and I am {user.Age} years old.";
    }
}

// =======================================================
// EXTENSION METHODS – COLLECTIONS
// =======================================================

static class CollectionExtensions
{
    public static double AverageValue(this List<int> numbers)
    {
        double sum = 0;

        foreach (var n in numbers)
        {
            sum += n;
        }

        return sum / numbers.Count;
    }

    // Extension methods can return IEnumerable
    // enabling fluent chaining like LINQ
    public static IEnumerable<int> FilterGreaterThan(
        this IEnumerable<int> source, int value)
    {
        foreach (var n in source)
        {
            if (n > value)
                yield return n;
        }
    }

    public static void ForEachPrint(this IEnumerable<int> source)
    {
        foreach (var n in source)
        {
            Console.WriteLine(n);
        }
    }
}

// =======================================================
// EXTENSION METHOD CONFLICT EXAMPLE
// =======================================================

static class ConflictExtensions
{
    // This method will NEVER be called
    // because the class already has Show()
    public static void Show(this ConflictingExample example)
    {
        Console.WriteLine("Extension Show() – ignored");
    }
}
