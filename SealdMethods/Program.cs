using System;

class Program
{
    static void Main()
    {
        // ---------------------------------------
        // 1. SEALED CLASS USAGE
        // ---------------------------------------
        SecurityService securityService = new SecurityService();
        securityService.ValidateAccess("admin");

        Console.WriteLine();

        // ---------------------------------------
        // 2. SEALED METHOD USAGE
        // ---------------------------------------
        Logger logger = new Logger();
        logger.Log("Base logger");

        Console.WriteLine();

        FileLogger fileLogger = new FileLogger();
        fileLogger.Log("File logger");

        Console.WriteLine();

        // ---------------------------------------
        // 3. POLYMORPHISM WITH SEALED METHODS
        // ---------------------------------------
        Logger polymorphicLogger = new FileLogger();
        polymorphicLogger.Log("Polymorphic call");

        Console.WriteLine("\n=== End of Demo ===");
    }
}

// =======================================================
// SECTION 1: SEALED CLASS
// =======================================================

// CRITICAL:
// - This class CANNOT be inherited
// - Used when behavior must not be changed
// - Common in security, configuration, utilities
sealed class SecurityService
{
    public void ValidateAccess(string role)
    {
        Console.WriteLine($"Access validated for role: {role}");
    }
}

// ❌ NOT ALLOWED
// Attempting inheritance will fail at compile time
/*
class AdvancedSecurityService : SecurityService
{
}
*/

// =======================================================
// SECTION 2: BASE CLASS WITH VIRTUAL METHOD
// =======================================================

class Logger
{
    // Virtual allows subclasses to override behavior
    public virtual void Log(string message)
    {
        Console.WriteLine($"[Logger] {message}");
    }
}

// =======================================================
// SECTION 3: DERIVED CLASS WITH SEALED METHOD
// =======================================================

class FileLogger : Logger
{
    // CRITICAL:
    // - This method overrides Logger.Log
    // - 'sealed' locks the behavior here
    // - No further subclass can override this method
    public sealed override void Log(string message)
    {
        WriteToFile(message);
    }

    private void WriteToFile(string message)
    {
        Console.WriteLine($"[FileLogger] {message}");
    }
}

// =======================================================
// SECTION 4: SEALED METHOD PREVENTS FURTHER OVERRIDING
// =======================================================

// ❌ NOT ALLOWED
// Uncommenting this will cause a compiler error
/*
class AdvancedFileLogger : FileLogger
{
    public override void Log(string message)
    {
        Console.WriteLine("[AdvancedFileLogger] " + message);
    }
}
*/

// =======================================================
// SECTION 5: IMPORTANT EDGE CASES
// =======================================================

class DatabaseLogger : Logger
{
    // This method is override-able again
    // because it is NOT sealed here
    public override void Log(string message)
    {
        Console.WriteLine($"[DatabaseLogger] {message}");
    }
}
