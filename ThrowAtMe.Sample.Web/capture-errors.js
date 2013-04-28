var captureErrors = (function (window, $) {
    var url;
    var warn = console.warn;
    var error = console.error;

    var reportError = function (logType, errorMessage, errorUrl, lineNumber) {
        var log = {
            "ErrorMessage": errorMessage,
            "Url": errorUrl,
            "LineNumber": lineNumber,
            "LogType": logType
        };

        var request = $.ajax({
            url: url,
            type: "post",
            data: log,
            dataType: "json"
        });

        request.fail(function (jqXhr, textStatus, errorThrown) {
            //error("Couldn't store log", textStatus, errorThrown);
        });
    };

    window.onerror = function (errorMessage, errorUrl, lineNumber) {
        console.log("error...");
        reportError("window.onerror", errorMessage, errorUrl, lineNumber);

        return false;
    };

    var captureConsole = function () {
        console.warn = function () {
            reportError("console.warn", Array.prototype.slice.call(arguments, 0).join(', '), window.location.href, 0);
            warn.apply(this, arguments);
        };

        console.error = function () {
            reportError("console.error", Array.prototype.slice.call(arguments, 0).join(', '), window.location.href, 0);
            error.apply(this, arguments);
        };
    };


    var init = function (loggingServer, shouldLogWarnings) {
        url = loggingServer;
        if (shouldLogWarnings) {
            captureConsole();
        }
    };

    return {
        init: init
    };

})(window, jQuery);