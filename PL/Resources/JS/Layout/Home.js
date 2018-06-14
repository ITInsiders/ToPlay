$(window).ready(function () {
    ObjCanvas();
}).resize(function () {
    ObjCanvas();
})

function ObjCanvas() {
    var sww = $(window).width(), swh = $(window).height();
    var sdw = $(document).width(), sdh = $(document).height();
    var Arc = {
        star: {
            color: 'rgba(254, 109, 57, 1)',
            width: 2
        },
        line: {
            color: 'rgba(254, 109, 57, 1)',
            width: 0.8
        },
        canvasposition: {
            top: 9,
            left: 0
        },
        width: sww,
        height: 140,
    }
    Canvas("HeaderStars", Arc);
    Arc.height = 82;
    Arc.canvasposition = {
        top: $(document).height() - 152,
        left: 0
    }
    Canvas("FooterStars", Arc);
}

function Canvas(NameI, Arc)
{
    var canvas = document.getElementById(NameI);
    canvas.width = Arc.width;
    canvas.height = Arc.height;
    var c = new Constellation(canvas);
    c.addconfig(Arc);
    c.init();
}

var FlagFrame = false;
function Frame(src)
{
    if (src != null)
    {
        $(".BlackFrame .Frame").attr("src", src);
        if (src == "/Entry") $(".BlackFrame .Frame").css({ "width": "250px", "height": "250px" });
        else if (src == "/Registration") $(".BlackFrame .Frame").css({ "width": "500px", "height": "300px" });
    }
    if (FlagFrame) $(".BlackFrame").css("display", "none");
    else $(".BlackFrame").css("display", "inline-block");
    FlagFrame = !FlagFrame;
}