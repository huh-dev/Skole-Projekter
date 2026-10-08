namespace Server;

public sealed class FileStorage
{
    public const long MaxFileBytes = 5 * 1024 * 1024;
    public const int MaxFileNameLength = 255;

    private readonly string _uploadsPath;

    public FileStorage(string uploadsPath)
    {
        _uploadsPath = Path.GetFullPath(uploadsPath);
        Directory.CreateDirectory(_uploadsPath);
    }

    public async Task<UploadResult> StoreAsync(Stream content, string fileName)
    {
        if (!TryResolveFilePath(fileName, out string filePath))
        {
            throw new ArgumentException("File name is not allowed.", nameof(fileName));
        }

        //Copy into a new file and await it, so the stream is not closed before the copy finishes.
        //https://stackoverflow.com/questions/39322085/how-to-save-iformfile-to-disk
        await using FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        await content.CopyToAsync(stream);

        return new UploadResult("File uploaded", fileName);
    }

    public IReadOnlyList<string> ListFileNames()
    {
        return Directory.EnumerateFiles(_uploadsPath)
            .Select(Path.GetFileName)
            .OfType<string>()
            .Where(IsSafeFileName)
            .OrderBy(fileName => fileName, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    public bool Delete(string fileName)
    {
        if (!TryResolveExistingFile(fileName, out string filePath))
        {
            return false;
        }

        File.Delete(filePath);
        return true;
    }

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

        //Reject a name that still contains a directory, so it cannot leave the uploads folder.
        //https://stackoverflow.com/questions/14144933/checking-file-path-when-deleting-a-file
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

        //Require the uploads folder plus a separator, so "uploads" does not match "uploads-other".
        //https://stackoverflow.com/questions/22671180/how-to-check-effectively-if-one-path-is-a-child-of-another-path-in-c
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
