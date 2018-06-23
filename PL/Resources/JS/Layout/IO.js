var Data = {};

$(document).ready(Ready);

function Ready() {

    Data.Game = {};
    Data.Game.Id = $("#Data").data("id");
    Data.Game.View = $("#Data").data("view");
    Data.Game.Controller = $("#Data").data("controller");

    Data.Button = {};
    Data.Div = {};

    $("#Data").children().each(function (i, v) {
        v = $(v);
        if (v.attr("type") == "button") Data.Button[v.attr("id")] = v.clone();
        else Data.Div[v.attr("id")] = v.clone();
    });

    Data.Gamer = $("#Template .Gamer").clone();

    $("#Data").remove();
    $("#Template").remove();
}

function ReplaceHref(href) {
    if (getNameBrouser() == "gecko") {
        window.history.replaceState(null, null, href);
    } else {
        window.location.hash = href;
    }
}
function getNameBrouser() {
    var userAgent = navigator.userAgent.toLowerCase();
    // Определим Internet Explorer
    if (userAgent.indexOf("msie") != -1 && userAgent.indexOf("opera") == -1 && userAgent.indexOf("webtv") == -1) {
        return "msie";
    }
    // Opera
    if (userAgent.indexOf("opera") != -1) {
        return "opera";
    }
    // Gecko = Mozilla + Firefox + Netscape
    if (userAgent.indexOf("gecko") != -1) {
        return "gecko";
    }
    // Safari, используется в MAC OS
    if (userAgent.indexOf("safari") != -1) {
        return "safari";
    }
    // Konqueror, используется в UNIX-системах
    if (userAgent.indexOf("konqueror") != -1) {
        return "konqueror";
    }

    return "unknown";
}