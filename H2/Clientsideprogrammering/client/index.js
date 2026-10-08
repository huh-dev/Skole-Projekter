$(function () {
    $('#uploadButton').on('click', uploadFile)
    loadFiles()
})

async function uploadFile() {
    const file = document.getElementById('fileInput').files[0]
    const formData = new FormData()
    formData.append('file', file)

    await fetch('http://localhost:3000/upload', {
        method: 'POST',
        body: formData
    })
    .then(response => response.json())
    .then(data => {
        console.log(data)
        loadFiles()
    })
    .catch(error => console.error('Error:', error))
}

function loadFiles() {
    fetch('http://localhost:3000/files')
    .then(response => response.json())
    .then(fileNames => {
        const fileList = document.getElementById('fileList')
        fileList.innerHTML = ''

        fileNames.forEach(fileName => {
            const link = document.createElement('a')
            link.href = '#'
            link.textContent = fileName
            $(link).on('click', function (event) {
                event.preventDefault()
                downloadFile(fileName)
            })
            fileList.appendChild(link)
            fileList.appendChild(document.createElement('br'))
        })
    })
    .catch(error => console.error('Error:', error))
}

async function downloadFile(fileName) {
    await fetch('http://localhost:3000/download/' + encodeURIComponent(fileName))
    .then(response => response.blob())
    .then(blob => {
        const url = URL.createObjectURL(blob)
        const link = document.createElement('a')
        link.href = url
        link.download = fileName
        link.click()
        URL.revokeObjectURL(url)
    })
    .catch(error => console.error('Error:', error))
}
