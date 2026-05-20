using CampusLendingSystem;

static void Assert(bool condition, string message)
{
    if (!condition)
    {
        throw new InvalidOperationException(message);
    }
}

var laptop = new Laptop("ThinkPad X1", 1001, "A-204");
var book = new Book("Clean Code", 1002, "Robert C. Martin");

ICampusItem[] items = [laptop, book];

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

Assert(laptop.GetStatusReport() == "Laptop ThinkPad X1 in A-204 (InventoryNumber: 1001) is available.", "Laptop status report changed unexpectedly.");
Assert(book.GetStatusReport() == "Book Clean Code by Robert C. Martin (InventoryNumber: 1002) is available.", "Book status report changed unexpectedly.");
Assert(laptop.IsLessThan(book), "Expected the laptop to have a lower inventory number than the book.");
Assert(book.IsGreaterThan(laptop), "Expected the book to have a higher inventory number than the laptop.");
Assert(laptop.CompareWith(book) < 0, "Expected laptop.CompareWith(book) to be negative.");

Console.WriteLine();
Console.WriteLine("All runtime checks passed.");
