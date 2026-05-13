using System.Runtime.InteropServices;

public static class MemoryVisualizer
{
    public static void VisualizeMemory(params object[] objects)
    {
        Console.WriteLine("Memory Visualization:");
        foreach (var obj in objects)
        {
            Console.WriteLine($"Object Type: {obj.GetType().Name}");
            Console.WriteLine($"Hash Code: {obj.GetHashCode()}");
            Console.WriteLine($"String Representation: {obj}");
            Console.WriteLine(new string('-', 40));
        }
    }

    public static unsafe void ShowMemoryContentUnsafe(params object[] objects)
    {
        Console.WriteLine("Unsafe Memory Visualization:");
        foreach (var obj in objects)
        {
            int size = Marshal.SizeOf(obj.GetType());
            IntPtr ptr = Marshal.AllocHGlobal(size);

            try
            {
                Marshal.StructureToPtr(obj, ptr, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(ptr, bytes, 0, size);
                Console.WriteLine($"Object Type: {obj.GetType().Name}");
                Console.WriteLine($"Memory Content (Hex): {BitConverter.ToString(bytes)}");
                Console.WriteLine(new string('-', 40));
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}