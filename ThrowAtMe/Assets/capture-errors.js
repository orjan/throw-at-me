var captureErrors = (function (window, $) {
    var url;
    
    var sendLogMessage = function (logType, errorMessage, errorUrl, lineNumber) {
        var log = {
            "ErrorMessage": errorMessage,
            "Url": errorUrl,
            "LineNumber": lineNumber,
            "LogType": logType
        };

        $.ajax({
            url: url,
            type: "post",
            data: log,
            dataType: "json"
        });
    };

    window.onerror = function (errorMessage, errorUrl, lineNumber) {
        sendLogMessage("window.onerror", errorMessage, errorUrl, lineNumber);

        return false;
    };

    var captureConsole = function () {
        if (Function.prototype.bind && window.console && typeof console.log == "object") {
            [
              "log", "info", "warn", "error", "assert", "dir", "clear", "profile", "profileEnd"
            ].forEach(function (method) {
                console[method] = this.bind(console[method], console);
            }, Function.prototype.call);
        }
        
        var warn = console.warn;
        var error = console.error;

        console.warn = function () {
            sendLogMessage("console.warn", Array.prototype.slice.call(arguments, 0).join(', '), window.location.href, 0);
            warn.apply(this, arguments);
        };

        console.error = function () {
            sendLogMessage("console.error", Array.prototype.slice.call(arguments, 0).join(', '), window.location.href, 0);
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
        init: init,
        log: sendLogMessage
    };

})(window, jQuery);