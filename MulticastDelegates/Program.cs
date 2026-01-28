using System;
using System.Collections.Generic;

public delegate void SampleDelegate();
public delegate int SampleDelegateWithReturn(int x);

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Multicast Delegates Comprehensive Demo ===\n");

        // ---------------------------------------
        // SCENARIO 1: BASIC MULTICAST DELEGATE
        // ---------------------------------------
        SampleDelegate del1 = SampleMethodOne;
        SampleDelegate del2 = SampleMethodTwo;
        SampleDelegate del3 = SampleMethodThree;

        // Create multicast delegate using '+'
        SampleDelegate multiDel = del1 + del2 + del3;

        Console.WriteLine("--- Calling multicast delegate using '+' ---");
        multiDel(); // Calls all three methods in order

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 2: ADDING AND REMOVING METHODS
        // ---------------------------------------
        SampleDelegate del = del1;
        del += del2; // add
        del += del3;
        del -= del1; // remove last occurrence of del1

        Console.WriteLine("--- Calling delegate after '+=' and '-=' operations ---");
        del(); // Only del2 and del3 invoked

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 3: MULTICAST DELEGATE WITH RETURN VALUES
        // ---------------------------------------
        SampleDelegateWithReturn delReturn = MethodA;
        delReturn += MethodB;
        delReturn += MethodC;

        Console.WriteLine("--- Multicast delegate with return value ---");
        int result = delReturn(10);
        Console.WriteLine($"Returned value = {result}");
        // Only the LAST method's return is used

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 4: INVOCATION LIST EXPLORATION
        // ---------------------------------------
        Console.WriteLine("--- Exploring invocation list ---");
        foreach (SampleDelegate d in multiDel.GetInvocationList())
        {
            Console.WriteLine($"Method Name: {d.Method.Name}");
            d(); // call each method individually
        }

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 5: EVENTS AS MULTICAST DELEGATES
        // ---------------------------------------
        Button button = new Button();
        button.Click += OnButtonClick;
        button.Click += LogClick;
        Console.WriteLine("--- Invoking event (multicast delegate) ---");
        button.TriggerClick();

        Console.WriteLine();

    }
    // =======================================================
    // METHODS FOR BASIC MULTICAST
    // =======================================================
    public static void SampleMethodOne()
    {
        Console.WriteLine("SampleMethodOne Invoked");
    }

    public static void SampleMethodTwo()
    {
        Console.WriteLine("SampleMethodTwo Invoked");
    }

    public static void SampleMethodThree()
    {
        Console.WriteLine("SampleMethodThree Invoked");
    }

    // =======================================================
    // METHODS FOR RETURN VALUE SCENARIO
    // =======================================================
    public static int MethodA(int x)
    {
        Console.WriteLine($"MethodA called with {x}");
        return x + 1;
    }

    public static int MethodB(int x)
    {
        Console.WriteLine($"MethodB called with {x}");
        return x + 2;
    }

    public static int MethodC(int x)
    {
        Console.WriteLine($"MethodC called with {x}");
        return x + 3;
    }


    // =======================================================
    // EVENT EXAMPLE
    // =======================================================
    class Button
    {
        // Events are multicast delegates internally
        public event SampleDelegate Click;

        public void TriggerClick()
        {
            Click?.Invoke();
        }
    }

    static void OnButtonClick()
    {
        Console.WriteLine("Button clicked handler executed");
    }

    static void LogClick()
    {
        Console.WriteLine("Button click logged");
    }
}
