using System;
using System.IO;

namespace ExceptionHandlingDeepDive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# Exception Handling – Complete Guide ===\n");

            // 1️⃣ BASIC TRY-CATCH-FINALLY
            BasicExceptionHandling();

            // 2️⃣ MULTIPLE CATCH BLOCKS (Specific → General)
            MultipleCatchExample();

            // 3️⃣ THROWING EXCEPTIONS MANUALLY
            ThrowingExceptions();

            // 4️⃣ CUSTOM EXCEPTION
            CustomExceptionExample();

            // 5️⃣ NESTED EXCEPTIONS & INNER EXCEPTION
            InnerExceptionExample();

            // 6️⃣ USING STATEMENT (RESOURCE SAFETY)
            UsingStatementExample();

            Console.WriteLine("\nProgram finished safely.");
        }

        // ------------------------------------------------------------
        // 1️⃣ BASIC TRY / CATCH / FINALLY
        // ------------------------------------------------------------
        static void BasicExceptionHandling()
        {
            Console.WriteLine("1) Basic try-catch-finally");

            try
            {
                int a = 10;
                int b = 0;

                // ❌ This will crash if not handled
                int result = a / b;

                Console.WriteLine($"Result: {result}");
            }
            catch (DivideByZeroException ex)
            {
                // 🎯 Catch ONLY what you expect
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // ✅ Always runs (success or failure)
                Console.WriteLine("Cleanup done (finally block)\n");
            }
        }

        // ------------------------------------------------------------
        // 2️⃣ MULTIPLE CATCH BLOCKS
        // ------------------------------------------------------------
        static void MultipleCatchExample()
        {
            Console.WriteLine("2) Multiple catch blocks");

            try
            {
                int[] numbers = { 1, 2, 3 };

                // ❌ Out of range
                Console.WriteLine(numbers[10]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index was outside the array bounds.");
            }
            catch (Exception ex)
            {
                // ⚠️ Fallback (should be LAST)
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            Console.WriteLine();
        }

        // ------------------------------------------------------------
        // 3️⃣ THROWING EXCEPTIONS (VALIDATION LOGIC)
        // ------------------------------------------------------------
        static void ThrowingExceptions()
        {
            Console.WriteLine("3) Throwing exceptions manually");

            try
            {
                ValidateAge(-5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }

        static void ValidateAge(int age)
        {
            if (age < 0)
            {
                // 🎯 Use meaningful exceptions
                throw new ArgumentException("Age cannot be negative.");
            }
        }

        // ------------------------------------------------------------
        // 4️⃣ CUSTOM EXCEPTION
        // ------------------------------------------------------------
        static void CustomExceptionExample()
        {
            Console.WriteLine("4) Custom exception");

            try
            {
                Withdraw(100, 500);
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }

        static void Withdraw(decimal balance, decimal amount)
        {
            if (amount > balance)
            {
                throw new InsufficientBalanceException(
                    $"Withdrawal failed. Balance: {balance}, Requested: {amount}"
                );
            }
        }

        // ------------------------------------------------------------
        // 5️⃣ INNER EXCEPTION (EXCEPTION CHAINING)
        // ------------------------------------------------------------
        static void InnerExceptionExample()
        {
            Console.WriteLine("5) Inner exception example");

            try
            {
                ReadFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine("High-level error occurred.");
                Console.WriteLine($"Message: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Root cause: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine();
        }

        static void ReadFile()
        {
            try
            {
                // ❌ File does not exist
                File.ReadAllText("non_existing_file.txt");
            }
            catch (IOException ex)
            {
                // ✅ Wrap low-level exception
                throw new ApplicationException("Failed to read configuration file.", ex);
            }
        }

        // ------------------------------------------------------------
        // 6️⃣ USING STATEMENT (RESOURCE MANAGEMENT)
        // ------------------------------------------------------------
        static void UsingStatementExample()
        {
            Console.WriteLine("6) Using statement");

            try
            {
                // ✅ Automatically disposes resource
                using (var reader = new StreamReader("another_missing_file.txt"))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found (resource safely released).");
            }

            Console.WriteLine();
        }
    }

    // ------------------------------------------------------------
    // 🧩 CUSTOM EXCEPTION CLASS
    // ------------------------------------------------------------
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message)
            : base(message)
        {
        }
    }
}
