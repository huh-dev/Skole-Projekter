using Microsoft.AspNetCore.StaticFiles;
using Server;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "uploads");

//Keep one storage instance for every request.
//https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences
builder.Services.AddSingleton(new FileStorage(uploadsPath));

//Allow the separate client page to call this server.
//https://stackoverflow.com/questions/44379560/how-to-enable-cors-in-asp-net-core-webapi
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

WebApplication app = builder.Build();

app.UseCors();

//Rewrite "/" to index.html before the static files middleware serves it.
//https://stackoverflow.com/questions/53988848/why-does-order-between-usestaticfiles-and-usedefaultfiles-matter
app.UseDefaultFiles();
app.UseStaticFiles();

FileExtensionContentTypeProvider contentTypes = new FileExtensionContentTypeProvider();

//Bind the uploaded file, and skip the antiforgery token this endpoint does not receive.
//https://stackoverflow.com/questions/77189996/upload-files-to-a-minimal-api-endpoint-in-net-8
app.MapPost("/upload", async (IFormFile? file, FileStorage storage) =>
{
    if (file is null || file.Length == 0)
    {
        return Results.BadRequest(new { message = "No file uploaded" });
    }

    if (file.Length > FileStorage.MaxFileBytes)
    {
        return Results.BadRequest(new { message = "File is too large" });
    }

    if (!FileStorage.IsSafeFileName(file.FileName))
    {
        return Results.BadRequest(new { message = "File name is not allowed" });
    }

    if (!FileStorage.IsAllowedFileType(file.FileName))
    {
        return Results.BadRequest(new { message = "File type is not allowed" });
    }

    await using Stream content = file.OpenReadStream();
    UploadResult result = await storage.StoreAsync(content, file.FileName);
    return Results.Ok(result);

}).DisableAntiforgery();

app.MapGet("/files", (FileStorage storage) => storage.ListFileNames());

app.MapDelete("/files/{fileName}", (string fileName, FileStorage storage) =>
{
    if (!storage.Delete(fileName))
    {
        return Results.NotFound();
    }

    return Results.Ok(new { message = "File deleted" });
});

app.MapGet("/download/{fileName}", (string fileName, FileStorage storage) =>
{
    if (!storage.TryResolveExistingFile(fileName, out string filePath))
    {
        return Results.NotFound();
    }

    //Look up the type from the extension, and fall back when the extension is unknown.
    //https://stackoverflow.com/questions/34131326/using-mimemapping-in-asp-net-core
    if (!contentTypes.TryGetContentType(filePath, out string? contentType))
    {
        contentType = "application/octet-stream";
    }

    //Send the file with its name so the browser saves it.
    //https://stackoverflow.com/questions/42460198/return-file-in-asp-net-core-web-api
    return Results.File(filePath, contentType, fileName);
});

app.Run($"http://localhost:3000");
