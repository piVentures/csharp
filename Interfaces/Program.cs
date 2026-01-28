using System;

// =======================================================
// ENTRY POINT
// =======================================================
class Program
{
    static void Main()
    {

        // ---------------------------------------
        // SCENARIO 1: BASIC INTERFACE IMPLEMENTATION
        // ---------------------------------------
        IPaymentService paymentService = new CardPaymentService();
        paymentService.Pay(1000);

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 2: POLYMORPHISM USING INTERFACES
        // ---------------------------------------
        IPaymentService mobilePayment = new MobilePaymentService();
        mobilePayment.Pay(500);

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 3: MULTIPLE INTERFACE IMPLEMENTATION
        // ---------------------------------------
        SmartPrinter printer = new SmartPrinter();
        printer.Print("Invoice");
        printer.Scan("Invoice");

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 4: INTERFACE AS CONTRACT
        // ---------------------------------------
        ProcessPayment(paymentService);

        Console.WriteLine();

        // ---------------------------------------
        // SCENARIO 5: INTERFACE VS CLASS
        // ---------------------------------------
        // You cannot instantiate an interface
        // IPaymentService service = new IPaymentService(); ❌

        
    }

    static void ProcessPayment(IPaymentService service)
    {
        // CRITICAL:
        // Any class implementing IPaymentService can be passed here
        service.Pay(300);
    }
}

// =======================================================
// SCENARIO 1: INTERFACE DEFINITION
// =======================================================

// CRITICAL:
// - Interfaces define WHAT, not HOW
// - No implementation (traditionally)
interface IPaymentService
{
    void Pay(decimal amount);
}

// =======================================================
// SCENARIO 1 IMPLEMENTATION
// =======================================================

class CardPaymentService : IPaymentService
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Card payment of {amount} processed");
    }
}

class MobilePaymentService : IPaymentService
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Mobile payment of {amount} processed");
    }
}

// =======================================================
// SCENARIO 3: MULTIPLE INTERFACES
// =======================================================

interface IPrinter
{
    void Print(string document);
}

interface IScanner
{
    void Scan(string document);
}

// CRITICAL:
// Classes CAN implement multiple interfaces
class SmartPrinter : IPrinter, IScanner
{
    public void Print(string document)
    {
        Console.WriteLine($"Printing: {document}");
    }

    public void Scan(string document)
    {
        Console.WriteLine($"Scanning: {document}");
    }
}
