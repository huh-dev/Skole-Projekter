import path from "path"
import fs from "fs"

export function deleteUploadedFile(fileName: string) {
    const uploadFolder = path.resolve(__dirname, "uploads")
    const filePath = path.resolve(uploadFolder, fileName)

    if (!filePath.startsWith(uploadFolder + path.sep)) {
        return false
    }

    if (!fs.existsSync(filePath) || !fs.statSync(filePath).isFile()) {
        return false
    }

    fs.unlinkSync(filePath)
    return true
}
