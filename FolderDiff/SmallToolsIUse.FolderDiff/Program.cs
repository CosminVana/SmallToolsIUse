namespace SmallToolsIUse;

public class FolderDiffConsole
{
    static void Main(string[] args)
    {
        Console.WriteLine("Comparing Folders:");
        Console.WriteLine($"Source: {args[0]}");
        Console.WriteLine($"Target: {args[1]}");
        Console.WriteLine();

        var folderDiff = new FolderDiff(args[0], args[1]);
        var result = folderDiff.Compare();

        if (result.FilesOnlyInSource.Count > 0)
        {
            Console.WriteLine($"Files only in {args[0]}");
            foreach (var item in result.FilesOnlyInSource)
            {
                Console.WriteLine(item);
            }
        }

        if(result.FilesOnlyInTarget.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine($"Files only in {args[1]}");
            foreach (var item in result.FilesOnlyInTarget)
            {
                Console.WriteLine(item);
            }
        }

        if (result.FilesInBothButDifferent.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine($"Files in both, but different size:");
            foreach (var item in result.FilesInBothButDifferent)
            {
                Console.WriteLine(item);
            }
        }

        if (result.FoldersOnlyInSource.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine($"Folders only in source:");
            foreach (var item in result.FoldersOnlyInSource)
            {
                Console.WriteLine(item);
            }
        }

        if (result.FoldersOnlyInTarget.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine($"Folders only in target:");
            foreach (var item in result.FoldersOnlyInTarget)
            {
                Console.WriteLine(item);
            }
        }

        if (result.FoldersAreIdentical())
        {
            Console.WriteLine("The two folders are identical");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Summary");
            Console.WriteLine($"Files only in source: {result.FilesOnlyInSource.Count()}");
            Console.WriteLine($"Files only in target: {result.FilesOnlyInTarget.Count()}");
            Console.WriteLine($"Files in both, but different size: {result.FilesInBothButDifferent.Count()}");
            Console.WriteLine($"Folders only in source: {result.FoldersOnlyInSource.Count()}");
            Console.WriteLine($"Folders only in target: {result.FoldersOnlyInTarget.Count()}");
        }
    }
}
