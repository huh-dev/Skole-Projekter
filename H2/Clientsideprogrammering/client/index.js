$(function () {
    $('#uploadButton').on('click', uploadFile)
    loadFiles()
})

const MAX_FILE_BYTES = 5 * 1024 * 1024
const ALLOWED_EXTENSIONS = ['.txt', '.pdf', '.doc', '.docx', '.csv']

function isSafeFileName(fileName) {
    return !!fileName
        && fileName === fileName.trim()
        && fileName.length <= 255
        && fileName !== '.'
        && fileName !== '..'
        && !/[\\/\u0000-\u001F\u007F<>"|:*?]/.test(fileName)
}

function isAllowedFileType(fileName) {
    const dot = fileName.lastIndexOf('.')
    return dot >= 0 && ALLOWED_EXTENSIONS.includes(fileName.slice(dot).toLowerCase())
}

//Show a message as text so it is not parsed as HTML.
//https://stackoverflow.com/a/68198131
function setStatus(message) {
    $('#status').text(message)
}

function addFileLink(parent, fileName) {
    const link = $('<a>', { href: '#', class: 'file-link' })
    //https://stackoverflow.com/a/68198131
    link.text(fileName)
    link.on('click', function (event) {
        event.preventDefault()
        downloadFile(fileName)
    })

    $(parent).append(link, $('<br>'))
}

async function uploadFile() {
    const fileInput = $('#fileInput')
    const file = fileInput.prop('files')[0]

    if (!file) {
        setStatus('Vælg en fil først.')
        return
    }

    if (file.size === 0) {
        setStatus('Filen er tom.')
        return
    }

    if (file.size > MAX_FILE_BYTES) {
        setStatus('Filen er for stor. Maks. 5 MB.')
        return
    }

    if (!isSafeFileName(file.name)) {
        setStatus('Filnavnet er ikke tilladt.')
        return
    }

    if (!isAllowedFileType(file.name)) {
        setStatus('Filtypen er ikke tilladt.')
        return
    }

    //Build the form data and post the file without setting Content-Type.
    //https://stackoverflow.com/a/40826943
    const formData = new FormData()
    formData.append('file', file)

    const uploadButton = $('#uploadButton')
    uploadButton.prop('disabled', true)

    try {
        const response = await fetch('http://localhost:3000/upload', {
            method: 'POST',
            body: formData
        })

        if (!response.ok) {
            setStatus('Upload mislykkedes.')
            return
        }

        const data = await response.json()
        const fileList = $('#fileList')
        let alreadyListed = false

        fileList.find('a.file-link').each(function () {
            if ($(this).text() === data.fileName) {
                alreadyListed = true
            }
        })

        if (!alreadyListed) {
            addFileLink(fileList, data.fileName)
        }

        fileInput.val('')
        setStatus('Filen er uploadet.')
    } catch (error) {
        console.error('Error:', error)
        setStatus('Upload mislykkedes.')
    } finally {
        uploadButton.prop('disabled', false)
    }
}

async function loadFiles() {
    try {
        const response = await fetch('http://localhost:3000/files')
        if (!response.ok) {
            setStatus('Kunne ikke hente filerne.')
            return
        }

        //Build the links in a fragment so the page is not redrawn for every file.
        //https://stackoverflow.com/questions/62776700/why-is-that-using-a-document-fragment-can-improve-performance
        const fileNames = await response.json()
        const fileList = $('#fileList')
        const fragment = document.createDocumentFragment()

        fileNames.forEach(fileName => {
            addFileLink(fragment, fileName)
        })

        fileList.empty().append(fragment)
    } catch (error) {
        console.error('Error:', error)
        setStatus('Kunne ikke hente filerne.')
    }
}

async function downloadFile(fileName) {
    try {
        const response = await fetch('http://localhost:3000/download/' + encodeURIComponent(fileName))

        if (!response.ok) {
            setStatus('Download mislykkedes.')
            return
        }

        //Save the blob through a temporary link, then remove the link.
        //https://stackoverflow.com/a/73787076
        const blob = await response.blob()
        const url = URL.createObjectURL(blob)
        const link = $('<a>')
        link.attr('href', url)
        link.attr('download', fileName)
        $('body').append(link)
        link.get(0).click()
        URL.revokeObjectURL(url)
        link.remove()
    } catch (error) {
        console.error('Error:', error)
        setStatus('Download mislykkedes.')
    }
}
