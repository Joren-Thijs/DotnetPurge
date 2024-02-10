# Dotnet Purge
dotnet purge is a command-line tool designed to clean up bin and obj folders within C# solution or project directories. It ensures that your build artifacts are removed, ensuring a clean rebuild.

## Usage
1. Installation:
   - Make sure you have the .NET SDK installed.
   - Install globally using `dotnet tool install --global DotnetPurge`
   
2. Run the Tool:
   - Open a terminal or command prompt.
   - Navigate to your C# solution or project directory.
   - Execute the following command: `dotnet purge`

3. Behavior:
   - The tool will search for bin and obj folders recursively within the current directory and its subdirectories.
   - If found, it will delete these folders.
   - If no such folders are found, it will display a message indicating that no cleanup is necessary.
   - For safety the tool can only run inside a solution or project (sub)directory.
     - If the tool is run outside a solution or project (sub)directory it will display an error message

4. Uninstall
    - Uninstall globally using `dotnet tool uninstall --global DotnetPurge`

## Example Output
```text
Found 2 directories
Removed Directory
C:\MyProject\bin\Debug
C:\MyProject\obj\Debug
Removed 2 directories
```

## Supported Platforms
This tool is built using .Net Core and is fully cross-platform. Happy cleaning! 🧹🚀