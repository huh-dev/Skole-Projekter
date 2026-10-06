import Enumerable from "linq"
import path from "path"
import fs from "fs"

export async function downloadFile(fileName: string) {
    const uploadFolder = path.join(__dirname, 'uploads')
    const filePath = path.join(uploadFolder, fileName)
    const file = Bun.file(filePath)

    return new Response(file, {
        headers: {
            'Content-Type': file.type || 'application/octet-stream',
            'Content-Disposition': `attachment; filename="${fileName}"`
        }
    })
}

export function listUploadedFiles() {
    const uploadFolder = path.join(__dirname, 'uploads')
    const fileNames = fs.readdirSync(uploadFolder)

    return Enumerable.from(fileNames)
        .orderBy(fileName => fileName)
        .toArray()
}
