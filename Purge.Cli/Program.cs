var currentDirectory = Directory.GetCurrentDirectory();

if (!IsRunningInsideSolutionOrProjectDirectory(currentDirectory))
{
    Console.WriteLine("Please run this tool within a C# solution or project folder.");
    return;
}

var directories = Directory.GetDirectories(currentDirectory, "*", SearchOption.AllDirectories);
var targetDirectories = directories.Where(d => d.EndsWith("bin") || d.EndsWith("obj")).ToList();

if (targetDirectories.Count < 1)
{
    Console.WriteLine("No bin or obj folders found.");
    return;
}

Console.WriteLine($"Found {targetDirectories.Count} directories");

Console.WriteLine("Removed Directory");
var deleted = 0;
foreach (var directory in targetDirectories) 
{
    try
    {
        if (await TryDeleteDirectory(directory))
            deleted++;
        else
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
            Console.WriteLine(directory);
            deleted++;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"An exception happened while trying to delete {directory}");
        Console.WriteLine(e);
    }
}

Console.WriteLine($"Removed {deleted} directories");

static async Task<bool> TryDeleteDirectory(
    string directory,
    int maxRetries = 10,
    int millisecondsDelay = 30)
{
    if (directory == null)
        throw new ArgumentNullException(nameof(directory));
    if (maxRetries < 1)
        throw new ArgumentOutOfRangeException(nameof(maxRetries));
    if (millisecondsDelay < 1)
        throw new ArgumentOutOfRangeException(nameof(millisecondsDelay));

    for (int i = 0; i < maxRetries; ++i)
    {
        try
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
            Console.WriteLine(directory);

            return true;
        }
        catch (IOException)
        {
            await Task.Delay(millisecondsDelay);
        }
        catch (UnauthorizedAccessException)
        {
            await Task.Delay(millisecondsDelay);
        }
    }

    return false;
}

static bool IsRunningInsideSolutionOrProjectDirectory(string currentDirectory)
{
    var solutionOrProjectFound = Directory.GetFiles(currentDirectory, "*.sln").Any()
                                 || Directory.GetFiles(currentDirectory, "*.csproj").Any();

    // Recursively check parent directories
    var parentDirectory = Directory.GetParent(currentDirectory);
    while (!solutionOrProjectFound && parentDirectory != null)
    {
        solutionOrProjectFound = Directory.GetFiles(parentDirectory.FullName, "*.sln").Any()
                                 || Directory.GetFiles(parentDirectory.FullName, "*.csproj").Any();
        parentDirectory = parentDirectory.Parent;
    }

    return solutionOrProjectFound;
}