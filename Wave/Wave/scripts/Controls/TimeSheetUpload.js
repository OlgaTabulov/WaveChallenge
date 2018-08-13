
function handleSubmit() {
    if (!FormData) {
        alert('Sorry, your browser doesn\'t support the File API => falling back to normal form submit');
        return true;
    }

    var fd = new FormData();
    var file = document.getElementById('reportFile');
    for (var i = 0; i < file.files.length; i++) {
        fd.append('reportFile', file.files[i]);
    }

    var xhr = new XMLHttpRequest();
    xhr.open('POST', Wave.WebServicesPath + 'UploadTimesheetFile', true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            DisplayMessage(xhr.responseText, false);
            if (GetReport) {
                GetReport();
            }
        }
    };
    xhr.send(fd);

    return false;
}