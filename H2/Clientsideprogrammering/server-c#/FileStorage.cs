namespace Server;

public sealed class FileStorage
{
    public const long MaxFileBytes = 5 * 1024 * 1024;
    public const int MaxFileNameLength = 255;

    private readonly string _uploadsPath;

    //Constructor for the file storage.
    public FileStorage(string uploadsPath)
    {
        _uploadsPath = Path.GetFullPath(uploadsPath);
        Directory.CreateDirectory(_uploadsPath);
    }

    // Store function for the file, to store it asyncronously.
    public async Task<UploadResult> StoreAsync(Stream content, string fileName)
    {
        if (!TryResolveFilePath(fileName, out string filePath))
        {
            throw new ArgumentException("File name is not allowed.", nameof(fileName));
        }

        await using FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        await content.CopyToAsync(stream);

        return new UploadResult("File uploaded", fileName);
    }

    //List function for the files in the storage.
    public IReadOnlyList<string> ListFileNames()
    {
        return Directory.EnumerateFiles(_uploadsPath)
            .Select(Path.GetFileName)
            .OfType<string>()
            .Where(IsSafeFileName)
            .OrderBy(fileName => fileName, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    //Delete function for the files in the storage.
    public bool Delete(string fileName)
    {
        if (!TryResolveExistingFile(fileName, out string filePath))
        {
            return false;
        }

        File.Delete(filePath);
        return true;
    }

    //Try to resolve the existing file in the storage.
    public bool TryResolveExistingFile(string fileName, out string filePath)
    {
        if (!TryResolveFilePath(fileName, out filePath))
        {
            return false;
        }

        return File.Exists(filePath);
    }

    public static bool IsSafeFileName(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName) || fileName.Length > MaxFileNameLength || fileName != fileName.Trim())
        {
            return false;
        }

        if (fileName != Path.GetFileName(fileName) || fileName is "." or "..")
        {
            return false;
        }

        foreach (char character in fileName)
        {
            if (character is '/' or '\\' or '<' or '>' or '"' or '|' or ':' or '*' or '?' || char.IsControl(character))
            {
                return false;
            }
        }

        return true;
    }

    private bool TryResolveFilePath(string fileName, out string filePath)
    {
        filePath = string.Empty;

        if (!IsSafeFileName(fileName))
        {
            return false;
        }

        string candidate = Path.GetFullPath(Path.Combine(_uploadsPath, fileName));
        string root = _uploadsPath.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

        if (!candidate.StartsWith(root, StringComparison.Ordinal))
        {
            return false;
        }

        filePath = candidate;
        return true;
    }
}

public sealed record UploadResult(string Message, string FileName);
