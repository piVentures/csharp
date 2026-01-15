using System;
using System.Threading.Tasks;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("starting both methods");

        var task1 = Method1();
        Method2();  
        await task1;
        Console.WriteLine("both methods completed");    
        Console.ReadKey();

    }
// as shown above Method1 is awaited meaning the control will return to Main method only after Method1 is completed. Meanwhile Method2 runs synchronously.

    public static async Task Method1()
    {
       
            for(int i=0; i<100; i++)
            {
                Console.WriteLine($"Method1 is iteration {i} ");
                await Task.Delay(100);
            };
    }
    public static async Task Method2()
    {
        for(int i=0; i<25; i++)
        {
            Console.WriteLine($"Method2 is iteration {i} ");
            await Task.Delay(100);
        }
    }
}

// Main starts
// ├─ Program starts
// │├─ "starting both methods" printed  
// ├─ Method1 starts (background)
// ├─ Method2 starts (background)
// ├─ Main waits for Method1
// ├─ Method1 finishes
// ├─ Main continues
// ├─ "both methods completed" printed
// └─ Program ends

// In this example, Method1 runs asynchronously, allowing Main to continue executing while waiting for Method1 to complete. Method2 runs synchronously after Method1 is started but before Main awaits its completion. 