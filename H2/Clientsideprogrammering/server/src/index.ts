import { Elysia } from "elysia";
import { cors } from '@elysia/cors'
import { storeFileInUploadFolder } from "./upload"
import { downloadFile, listUploadedFiles } from "./download";

const app = new Elysia()
  .use(cors({
    origin: '*',
    methods: ['GET', 'POST', 'PUT', 'DELETE', 'OPTIONS'],
    allowedHeaders: ['Content-Type', 'Authorization', 'multipart/form-data'],
  }))  
  .get("/", () => "Hello Elysia")
  .post('/upload', async ( body: any ) => await storeFileInUploadFolder(body.body.file))
  .get('/files', () => listUploadedFiles())
  .get('/download/:fileName', async ({ params }: any) => await downloadFile(params.fileName))
  .listen(3000);

console.log(
  `🦊 Elysia is running at ${app.server?.hostname}:${app.server?.port}`
);
