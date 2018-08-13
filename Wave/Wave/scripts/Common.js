function DisplayMessage(message, isError){
    if (isError) {
        $('#pageError').html(message);
    }
    else {
        $('#pageMessage').html(message);
    }
}

//spinner

$(document)
    .ready(function () {
        $(document)
            .ajaxStart(function () {
                $('#loadingDiv').css("visibility", "visible");
            });

        $(document)
            .ajaxStop(function () {
                $('#loadingDiv').css("visibility", "hidden");
            });        
    });


function CallAjax(url, params, success, error) {
    url = (typeof url === 'string') ? url : "";
    params = params || {};
    success = success || function (xml, status, xhr) { };
    error = error || defaultErrorFunction;

    var respObj = $.ajax({
        type: 'POST',
        url: url,
        data: params,
        dataType: "xml",
        success: success,
        error: error
    });
    return respObj;
}

var defaultErrorFunction = function (XMLHttpRequest, textStatus, errorThrown) {
    console.log("Status: " + textStatus);
    console.log("Error: " + errorThrown);
};
