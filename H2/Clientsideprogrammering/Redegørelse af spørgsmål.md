- Redegør for væsentlige forskelle mellem JavaScript og et OOP-sprog.

JavaScript i `client/index.js` er funktioner, der tjekkes, når koden kører. C# er klasser som `FileStorage`, hvor typer og `private` tjekkes, når den bygges.

- Vis hvor og hvordan fil-upload og download er implementeret på klienten.

`uploadFile` sender `POST /upload`. `downloadFile` henter `/download/` plus filnavnet og gemmer blob’en. `loadFiles` henter listen fra `/files`.

- Vis hvor endpoints er implementeret, hvordan de virker, og hvorfor appen både er GUI og Web API.

Ruterne står i `Program.cs` med `MapPost`, `MapGet` og `MapDelete`. En request matcher metode og sti, og handleren kalder `FileStorage`. `UseStaticFiles` viser `wwwroot/index.html`. De samme ruter er API’et.

- Forklar forskellen på Controller-based og Minimal API.

Controller-based API samler ruter i en klasse med actions. Minimal API skriver ruten direkte i `Program.cs`. Min app er Minimal API.

- Vis et eksempel på, hvordan appen bruger render tree til at opdatere DOM.

`loadFiles` bygger linkene i et `DocumentFragment` og sætter dem ind én gang med `$('#fileList').empty().append(fragment)`. Efter upload tilføjer `addFileLink` kun det nye link.

- Vis hvor jQuery-implementationerne er, og hvordan de fungerer for kravene i øvelsen.

`$(function () { ... })` kalder `loadFiles`. Klik på `#uploadButton` uploader. Klik på `a.file-link` downloader. `#status` opdateres med `.text()`, og `#fileList .file-link` finder de links, der allerede er vist. Kaldene er `fetch`. `addFileLink` opdaterer kun den nye fil.

- Vis i koden, hvad du har gjort for performance.

Fragmentet tegner ikke siden om per fil. Upload henter ikke listen igen. `addFileLink` og `setStatus` genbruges. `fetch` er async.

- Vis i koden, hvad du har gjort for sikkerhed.

`Program.cs` afviser tom fil, fil over 5 MB, usikkert navn og filtype, før den skriver. Tilladte typer er `.txt`, `.pdf`, `.doc`, `.docx` og `.csv`. Klientens tjek er kun til GUI. Navne sættes med jQuery `.text()`. `TryResolveFilePath` holder stien i `uploads` og afviser andre filtyper.
