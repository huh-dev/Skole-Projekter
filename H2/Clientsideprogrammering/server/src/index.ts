import { Elysia } from "elysia";
import { cors } from '@elysia/cors'
import { staticPlugin } from '@elysia/static'
import path from "path"
import { storeFileInUploadFolder } from "./upload"
import { downloadFile, listUploadedFiles } from "./download";
import { deleteUploadedFile } from "./delete";

const app = new Elysia()
  .use(cors({
    origin: '*',
    methods: ['GET', 'POST', 'PUT', 'DELETE', 'OPTIONS'],
    allowedHeaders: ['Content-Type', 'Authorization', 'multipart/form-data'],
  }))
  .post('/upload', async ( body: any ) => await storeFileInUploadFolder(body.body.file))
  .get('/files', () => listUploadedFiles())
  .delete('/files/:fileName', ({ params }: any) => deleteUploadedFile(params.fileName))
  .get('/download/:fileName', async ({ params }: any) => await downloadFile(params.fileName))
  .use(await staticPlugin({
    assets: path.join(import.meta.dir, '..', 'public'),
    prefix: '/',
    indexHTML: true,
    alwaysStatic: true,
  }))
  .listen(3000);

console.log(
  `🦊 Elysia is running at ${app.server?.hostname}:${app.server?.port}`
);
