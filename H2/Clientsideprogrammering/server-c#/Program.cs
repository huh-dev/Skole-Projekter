using Microsoft.AspNetCore.StaticFiles;
using Server;

// We start by creating the builder object.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Get the uploads path
string uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "uploads");

// Use this in a singleton service to avoid creating a new instance for each request.
builder.Services.AddSingleton(new FileStorage(uploadsPath));

// Add CORS to allow requests from any origin.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Build the application.
WebApplication app = builder.Build();

// Use CORS.
app.UseCors();

// Use default and static files.
app.UseDefaultFiles();
app.UseStaticFiles();

FileExtensionContentTypeProvider contentTypes = new FileExtensionContentTypeProvider();

//Create the new upload endpoint.
app.MapPost("/upload", async (IFormFile? file, FileStorage storage) =>
{
    //Check if the file is null, empty, or not a safe file name.
    if (file is null || file.Length == 0 || !FileStorage.IsSafeFileName(file.FileName))
    {
        return Results.BadRequest(new { message = "No file uploaded" });
    }

    //Open the file and read the content.
    await using Stream content = file.OpenReadStream();

    //Store the file in the storage.
    UploadResult result = await storage.StoreAsync(content, file.FileName);

    //Return the result.
    return Results.Ok(result);

}).DisableAntiforgery();

//Simple endpoint for getting the files already uploaded
app.MapGet("/files", (FileStorage storage) => storage.ListFileNames());

//Delete a file by name, we use a dynamic route to get the file name from the url/request.
app.MapDelete("/files/{fileName}", (string fileName, FileStorage storage) =>
{
    return Results.Ok(storage.Delete(fileName));
});

//Download a file by name, we use again the same dynamic route method as earlier.
app.MapGet("/download/{fileName}", (string fileName, FileStorage storage) =>
{
    //Check if the file exists.
    if (!storage.TryResolveExistingFile(fileName, out string filePath))
    {
        return Results.NotFound();
    }

    //Get the content type of the file.
    if (!contentTypes.TryGetContentType(filePath, out string? contentType))
    {
        contentType = "application/octet-stream";
    }

    //Return the file.
    return Results.File(filePath, contentType, fileName);
});

//The URL the server will run on.
app.Run($"http://localhost:3000");
