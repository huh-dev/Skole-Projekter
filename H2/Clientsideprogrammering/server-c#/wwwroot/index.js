//Show a message as text so it is not parsed as HTML.
//https://stackoverflow.com/a/68198131
function setStatus(message) {
    document.getElementById('status').textContent = message
}

//Create one file row and reuse it for every file in the list.
function createFileRow(fileName) {
    const row = document.createElement('div')

    const link = document.createElement('a')
    link.href = '/download/' + encodeURIComponent(fileName)
    //https://stackoverflow.com/a/68198131
    link.textContent = fileName

    const button = document.createElement('button')
    button.textContent = 'Slet'
    button.addEventListener('click', function () {
        deleteFile(fileName, row, button)
    })

    row.append(link, document.createTextNode(' '), button)
    return row
}

//Build the links in a fragment so the page is not redrawn for every file.
//https://stackoverflow.com/questions/62776700/why-is-that-using-a-document-fragment-can-improve-performance
function renderFileList(fileNames) {
    const fragment = document.createDocumentFragment()
    fileNames.forEach(function (fileName) {
        fragment.append(createFileRow(fileName))
    })
    document.getElementById('fileList').replaceChildren(fragment)
}

//Load the files that are already uploaded.
async function loadFiles() {
    try {
        //Get the file names from the server.
        const response = await fetch('/files')
        if (!response.ok) {
            setStatus('Kunne ikke hente filerne.')
            return
        }

        //Show the list.
        renderFileList(await response.json())
    } catch (error) {
        setStatus('Kunne ikke hente filerne.')
    }
}

//Delete a file by name without reloading the whole list.
async function deleteFile(fileName, row, button) {
    //Disable the button so the same file is not deleted twice.
    button.disabled = true

    try {
        //Send the delete request for this file name.
        //https://stackoverflow.com/a/53127045
        const response = await fetch('/files/' + encodeURIComponent(fileName), {
            method: 'DELETE'
        })

        if (!response.ok) {
            setStatus('Kunne ikke slette filen.')
            button.disabled = false
            return
        }

        //Remove only this row from the page.
        //https://stackoverflow.com/a/27710003
        row.remove()
    } catch (error) {
        setStatus('Kunne ikke slette filen.')
        button.disabled = false
    }
}
