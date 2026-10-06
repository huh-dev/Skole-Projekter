function loadFiles() {
    fetch('/files')
    .then(response => response.json())
    .then(fileNames => {
        const fileList = document.getElementById('fileList')
        fileList.innerHTML = ''

        fileNames.forEach(fileName => {
            const link = document.createElement('a')
            link.href = '/download/' + encodeURIComponent(fileName)
            link.textContent = fileName

            const button = document.createElement('button')
            button.textContent = 'Slet'
            button.onclick = () => deleteFile(fileName)

            fileList.appendChild(link)
            fileList.appendChild(document.createTextNode(' '))
            fileList.appendChild(button)
            fileList.appendChild(document.createElement('br'))
        })
    })
    .catch(error => console.error('Error:', error))
}

function deleteFile(fileName) {
    fetch('/files/' + encodeURIComponent(fileName), {
        method: 'DELETE'
    })
    .then(response => response.json())
    .then(data => {
        console.log(data)
        loadFiles()
    })
    .catch(error => console.error('Error:', error))
}
