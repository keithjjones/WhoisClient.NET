$(function () {
    if (typeof (history.pushState) != "function") return;
    var progressContent = '<span class="x-progress">Querying...</span>';
    var errorContent = '<span class="error">Something wrong.</span>';
    var $res = $(".response");

    var getResponseOfQuery = function (data) {
        $res.html(progressContent)
            .load("/ResponseOfQuery", data)
            .ajaxError(function () { $res.html(errorContent) });
    };

    $("form").submit(function () {
        if ($(this).valid() == false) return;
        var data = {
            Query: $("[name=Query]").val(),
            Server: $("[name=Server]").val(),
            Encoding: $("[name=Encoding]").val()
        };

        getResponseOfQuery(data);

        history.pushState(data, document.title,
            "/?Query=" + encodeURI(data.Query) +
            "&Server=" + encodeURI(data.Server) +
            "&Encoding=" + encodeURI(data.Encoding));
        return false;
    });

    $(window).bind("popstate", function (ev) {
        var data = ev.originalEvent.state;
        if (data == null) return;

        getResponseOfQuery(data);

        $("[name=Query]").val(data.Query);
        $("[name=Server]")  .val(data.Server);
        $("[name=Encoding]").val(data.Encoding);
    });
});