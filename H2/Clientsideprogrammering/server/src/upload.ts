import path from "path"
import fs from "fs"

export async function storeFileInUploadFolder(file: File) {

    console.log("File: ", file)
    const uploadFolder = path.join(__dirname, 'uploads')
    console.log("Upload folder: ", uploadFolder)
    if (!fs.existsSync(uploadFolder)) {
        fs.mkdirSync(uploadFolder)
    }

    fs.writeFileSync(path.join(uploadFolder, file.name), Buffer.from(await file.arrayBuffer()))

    return { message: 'File uploaded', fileName: file.name }
}
