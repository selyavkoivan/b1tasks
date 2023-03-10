$(document).ready(() => {
    
    $('#upload-file, #uploaded-file').click(function() {
        $(this).children('i').toggleClass('fa-caret-down').toggleClass('fa-caret-up')
    })
    
    let dropArea = document.getElementById('drop-area');
    let input = document.getElementById('file');

    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        dropArea.addEventListener(eventName, preventDefaults, false)
    })

    function preventDefaults(e) {
        e.preventDefault()
        e.stopPropagation()
    }

    dropArea.addEventListener('drop', handleDrop, false)
    input.addEventListener('change', () => handleFiles(input.files));
    function handleDrop(e) {
        let dt = e.dataTransfer
        let files = dt.files
        handleFiles(files)
    }

    function handleFiles(files) {
        ([...files]).forEach(uploadFile)
    }



    function uploadFile(file) {
        let url = 'upload'
        let formData = new FormData()
        formData.append('file', file)
        fetch(url, {
            method: 'POST',
            body: formData
        })
            .then(() => {
                location.reload()
            })
            .catch(() => {
                location.reload()
            })
    }
})