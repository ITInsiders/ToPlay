/*
$(window).ready(function () {
    ObjCanvas();
}).resize(function () {
    ObjCanvas();
})

function ObjCanvas() {
    var sww = $(window).width(), swh = $(window).height(),
        sdw = $(document).width(), sdh = $(document).height(),
        HeaderSpaceSky = $("#HeaderStarSky"),
        FooterSpaceSky = $("#FooterStarSky");

    var HeaderArc = {
        star: {
            color: '#ffffff',
            width: 0.5
        },
        line: {
            color: '#ffffff',
            width: 0.3
        },
        canvasposition: {
            top: HeaderSpaceSky.offset().top,
            left: HeaderSpaceSky.offset().left
        },
        width: sww,
        height: HeaderSpaceSky.height(),
    }
    Canvas("HeaderStarSky", HeaderArc);

    var FooterArc = {
        star: {
            color: '#2e2d3e',
            width: 2
        },
        line: {
            color: '#2e2d3e',
            width: 0.8
        },
        canvasposition: {
            top: FooterSpaceSky.offset().top,
            left: FooterSpaceSky.offset().left
        },
        width: sww,
        height: FooterSpaceSky.height(),
    }
    Canvas("FooterStarSky", FooterArc);
}

function Canvas(NameI, Arc) {
    var canvas = document.getElementById(NameI);
    canvas.width = Arc.width;
    canvas.height = Arc.height;

    if (canvas.starsky == null) {
        canvas.starsky = new Constellation(canvas);
        canvas.starsky.addconfig(Arc);
        canvas.starsky.init();
    }
    else {
        canvas.starsky.addconfig(Arc);
        canvas.starsky.setContext();
    }
}

var FlagFrame = false;
function Frame(src) {
    if (src != null) {
        $(".BlackFrame .Frame").attr("src", src);
        if (src == "/Entry") $(".BlackFrame .Frame").css({ "width": "250px", "height": "250px" });
        else if (src == "/Registration") $(".BlackFrame .Frame").css({ "width": "500px", "height": "300px" });
    }
    if (FlagFrame) $(".BlackFrame").css("display", "none");
    else $(".BlackFrame").css("display", "inline-block");
    FlagFrame = !FlagFrame;
}
*/