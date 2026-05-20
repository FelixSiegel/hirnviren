using CampusLendingSystem;

static void AusgabeLeihstatus(IBorrowable objekt)
{
    var name = (objekt as ICampusItem)?.Name ?? "Objekt";
    Console.WriteLine($"{name} - Verfügbar: {objekt.IsAvailable}");
}

static void Assert(bool condition, string message)
{
    if (!condition)
    {
        throw new InvalidOperationException(message);
    }
}

var laptop = new Laptop("ThinkPad X1", 1001, "A-204");
var book = new Book("Clean Code", 1002, "Robert C. Martin");

ICampusItem[] items = new ICampusItem[] { laptop, book };

Console.WriteLine("Campus lending system demo");
Console.WriteLine("---------------------------");

foreach (var item in items)
{
    Console.WriteLine(item.GetStatusReport());
}

Console.WriteLine();
Console.WriteLine("Comparison checks");
Console.WriteLine($"{laptop.Name} < {book.Name}: {laptop.IsLessThan(book)}");
Console.WriteLine($"{book.Name} > {laptop.Name}: {book.IsGreaterThan(laptop)}");
Console.WriteLine($"Inventory compare result: {laptop.CompareWith(book)}");

// initial availability checks
Assert(laptop.IsAvailable, "Expected the laptop to be available at start.");
Assert(book.IsAvailable, "Expected the book to be available at start.");
Assert(laptop.IsLessThan(book), "Expected the laptop to have a lower inventory number than the book.");
Assert(book.IsGreaterThan(laptop), "Expected the book to have a higher inventory number than the laptop.");
Assert(laptop.CompareWith(book) < 0, "Expected laptop.CompareWith(book) to be negative.");

Console.WriteLine();
Console.WriteLine("All runtime checks passed.");

// Demonstrate borrow/return behavior
Console.WriteLine();
Console.WriteLine("Borrow/Return demo");
AusgabeLeihstatus(laptop);
laptop.Borrow();
AusgabeLeihstatus(laptop);
Assert(!laptop.IsAvailable, "Expected laptop to be not available after Borrow().");
laptop.Return();
AusgabeLeihstatus(laptop);
Assert(laptop.IsAvailable, "Expected laptop to be available after Return().");

Console.WriteLine();
Console.WriteLine("All runtime checks including borrow/return passed.");
