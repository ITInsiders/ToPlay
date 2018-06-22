$(document).ready(Ready);

var Data = {};

function Ready() {
    initHashChange();
    CalcData();
}

function initHashChange() {
    $("a").click(function () {
        var link = $(this),
            href = link.attr("href");

        getPage(href);
    });
}

function getPage(href) {
    if (href.substr(0, 4) == "/LF/") {
        if (getNameBrouser() == "gecko") {
            window.history.replaceState(null, null, href);
            getPageByHash(href);
        } else {
            window.location.hash = href;
        }
    } else {
        window.location.href = href;
    }
}

function getPageByHash(href) {
    if (typeof (href) != "undefined" && href != "") {
        $.ajax({
            type: "POST",
            dataType: "html",
            cache: false,
            async: false,
            url: href,
            success: function (data) {
                setPage(data, href);
            }
        });
    }
}

var setPage = function (data, href) {
    $("#Page").html(data);
    CalcData();
    loadCSS();
    loadJS();
}

var loadJS = function () {
    $.ajax({
        type: "GET",
        url: "/Resources/JS/" + Data.PageInfo.Controller + "/" + Data.PageInfo.View + ".js",
        dataType: "script",
        cache: true,
        async: false
    });
}

var loadCSS = function () {
    $("#TempLink").remove();
    $("<link/>", {
        rel: "stylesheet",
        type: "text/css",
        href: "/Resources/CSS/" + Data.PageInfo.Controller + "/" + Data.PageInfo.View + ".min.css",
    }).attr("id", "TempLink").appendTo("head");
}

window.addEventListener("popstate", function (e) {
    getPage(location.pathname);
});

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

function CalcData() {
    var PageData = $("#Page .Data"),
        HeaderTempChildren = PageData.find(".HeaderTemp").children();

    HeaderTempChildren.each(function (i, e) {
        var self = $(e),
            parentSelf = $(".Header ." + self.attr("class"));
        parentSelf.html(self.html());
    });

    Data.PageInfo = {};
    Data.PageInfo.View = PageData.data("view");
    Data.PageInfo.Controller = PageData.data("controller");

    PageData.remove();
}