using System;

// =======================================================
// ENTRY POINT
// =======================================================
class Program
{
    static void Main()
    {
        // ---------------------------------------
        // SCENARIO 1: ABSTRACT CLASS POLYMORPHISM
        // ---------------------------------------
        PaymentProcessor card = new CardPaymentProcessor();
        card.Process(1000);

        Console.WriteLine();

        PaymentProcessor paypal = new PayPalPaymentProcessor();
        paypal.Process(750);

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 2: ABSTRACT METHOD ENFORCEMENT
        // ---------------------------------------
        Shape rectangle = new Rectangle(5, 10);
        Shape circle = new Circle(7);

        Console.WriteLine($"Rectangle area: {rectangle.GetArea()}");
        Console.WriteLine($"Circle area: {circle.GetArea()}");

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 3: TEMPLATE METHOD PATTERN
        // ---------------------------------------
        DataExporter exporter = new CsvExporter();
        exporter.Export();

        Console.WriteLine("\n=== End of Demo ===");
    }
}

// =======================================================
// SCENARIO 1: ABSTRACT BASE CLASS WITH SHARED LOGIC
// =======================================================

abstract class PaymentProcessor
{
    // Concrete method (shared behavior)
    public void Process(decimal amount)
    {
        Validate();
        Pay(amount);   // Abstract step
        Log();
    }

    protected void Validate()
    {
        Console.WriteLine("Validating payment...");
    }

    // CRITICAL:
    // Must be implemented by subclasses
    protected abstract void Pay(decimal amount);

    protected void Log()
    {
        Console.WriteLine("Payment logged.");
    }
}

class CardPaymentProcessor : PaymentProcessor
{
    protected override void Pay(decimal amount)
    {
        Console.WriteLine($"Processing card payment: {amount}");
    }
}

class PayPalPaymentProcessor : PaymentProcessor
{
    protected override void Pay(decimal amount)
    {
        Console.WriteLine($"Processing PayPal payment: {amount}");
    }
}

// =======================================================
// SCENARIO 2: ABSTRACT CLASS AS DOMAIN MODEL
// =======================================================

abstract class Shape
{
    // Abstract method forces implementation
    public abstract double GetArea();
}

class Rectangle : Shape
{
    private double width;
    private double height;

    public Rectangle(double w, double h)
    {
        width = w;
        height = h;
    }

    public override double GetArea()
    {
        return width * height;
    }
}

class Circle : Shape
{
    private double radius;

    public Circle(double r)
    {
        radius = r;
    }

    public override double GetArea()
    {
        return Math.PI * radius * radius;
    }
}

// =======================================================
// SCENARIO 3: TEMPLATE METHOD PATTERN
// =======================================================

abstract class DataExporter
{
    // CRITICAL:
    // This is the TEMPLATE METHOD
    // Defines algorithm structure
    public void Export()
    {
        Open();
        WriteData(); // abstract step
        Close();
    }

    protected void Open()
    {
        Console.WriteLine("Opening file...");
    }

    protected abstract void WriteData();

    protected void Close()
    {
        Console.WriteLine("Closing file...");
    }
}

class CsvExporter : DataExporter
{
    protected override void WriteData()
    {
        Console.WriteLine("Writing CSV data...");
    }
}
