1. Vælg et serverside framework

Jeg har valgt én ASP.NET Core-app i `server-c#` med indbygget API. Siden kommer fra `wwwroot`, og API’et kører i samme app.

API’et er Minimal API. Ruterne står i `Program.cs`: `POST /upload`, `GET /files`, `GET /download/{fileName}` og `DELETE /files/{fileName}`. Der er ingen controllere.

2. Case – HTML/CSS/JavaScript-klient til en server app

2.1 Server-appen er `Program.cs`. GUI’en er `wwwroot/index.html`, ikke Razor eller Blazor. `FileStorage` gemmer filer i `uploads`. `loadFiles` henter listen og viser link og Slet-knap.

LINQ sidder i `ListFileNames`: `EnumerateFiles`, `Select`, `Where` og `OrderBy`. Slet kalder `deleteFile`, og `row.remove()` fjerner kun den række.

2.2 Klienten er `client/index.html`, `index.css` og `index.js`, uden framework. Input, Upload og `#fileList` uploader, viser og downloader filerne.

Jeg bruger `fetch` med `async`/`await`, ikke `$.ajax`. Ajax får svaret i en callback. `fetch` returnerer et promise, så samme funktion kan læse JSON eller blob og kun opdatere den del, der ændrede sig.

jQuery lytter i `$(function () { ... })`: klik på `#uploadButton` uploader, klik på `a.file-link` downloader. `#status` sættes med `.text()`. Listen læses med `#fileList .file-link` og opdateres med `$('#fileList').empty().append()`. Efter upload tilføjer `addFileLink` kun den nye fil.

2.3 `loadFiles` bygger listen i et `DocumentFragment` og sætter den ind én gang. `addFileLink` og `setStatus` genbruges. Kaldene er async `fetch`.

Serveren tjekker tom fil, størrelse, filnavn og filtype i `Program.cs`, før den skriver. Tilladte typer er `.txt`, `.pdf`, `.doc`, `.docx` og `.csv`. Klientens tjek er kun til GUI. Navne sættes med jQuery `.text()`. `TryResolveFilePath` holder stien inde i `uploads` og afviser andre filtyper.
