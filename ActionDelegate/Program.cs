using System;

// action is a predefined delegate in C# that represents a method that takes zero or more parameters and does not return a value.
class Program
{
    static void Main()
    {
        Console.WriteLine("=== ACTION DELEGATE DEMO ===\n");

        // 1️⃣ Action with NO parameters
        Action sayHello = SayHello;
        sayHello();

        Console.WriteLine();

        // 2️⃣ Action with ONE parameter
        Action<string> greet = Greet;
        greet("Kirubel");

        Console.WriteLine();

        // 3️⃣ Action with TWO parameters
        Action<int, int> addAndPrint = Add;
        addAndPrint(5, 7);

        Console.WriteLine();

        // 4️⃣ Action using LAMBDA expression
        Action<string> printMessage = message =>
        {
            Console.WriteLine(message);
        };
        printMessage("This is Action using lambda");

        Console.WriteLine();

        // 5️⃣ Passing Action as a PARAMETER (callback)
        Process(() =>
        {
            Console.WriteLine("Callback executed after processing");
        });

        Console.WriteLine();

        // 6️⃣ Using Action for DEPENDENCY INJECTION
        OrderService orderService = new OrderService(Console.WriteLine);
        orderService.PlaceOrder();

        Console.WriteLine();

        // 7️⃣ Chaining multiple methods to Action
        Action notify = SendEmail;
        notify += SendSms;
        notify();

        Console.WriteLine("\n=== END OF PROGRAM ===");
        Console.ReadKey();
    }

    // ===== Methods used by Action =====

    static void SayHello()
    {
        Console.WriteLine("Hello from Action with no parameters");
    }

    static void Greet(string name)
    {
        Console.WriteLine($"Hello, {name}");
    }

    static void Add(int a, int b)
    {
        Console.WriteLine($"Sum = {a + b}");
    }

    static void Process(Action onCompleted)
    {
        Console.WriteLine("Processing...");
        onCompleted();
    }

    static void SendEmail()
    {
        Console.WriteLine("Email notification sent");
    }

    static void SendSms()
    {
        Console.WriteLine("SMS notification sent");
    }
}

// ===== Simple service using Action (DI example) =====

class OrderService
{
    private readonly Action<string> _logger;

    public OrderService(Action<string> logger)
    {
        _logger = logger;
    }

    public void PlaceOrder()
    {
        _logger("Order placed successfully");
    }
}
