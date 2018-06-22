var Hub = null,
    User = null,
    You = null,
    Game = {},
    Gamers = [];

function Gamer(Id) {
    var gamer = null;
    Gamers.forEach(function (v, i) {
        if (v.Id == Id) {
            gamer = v;
            gamer.index = i;
        }
    });
    return gamer;
}

$("document").ready(function () {
    Game.Id = $("#Template").data("id");
    User = $("#Template .Element").clone();
    $("#Template").remove();

    Hub = $.connection.HubIO;

    Hub.client.SetYou = SetYou;
    Hub.client.SetGame = SetGame;
    Hub.client.SetGamers = SetGamers;
    Hub.client.SetReady = SetReady;
    Hub.client.SetAnswers = SetAnswers;

    $.connection.hub.start().done(function () {
        Hub.server.connect(Game.Id);
    });
});

var
    SetYou = function (JsonGamer) {
        You = JsonGamer;
    },
    SetGame = function (JsonGame) {
        Game = JsonGame;
        getPage(Game.Page + "/" + Game.Id);

        if (Game.Level === 1) {
            $("#Gamers").removeClass("Answers");
        } else if (Game.Level === 2) {
            $("#Gamers").addClass("Answers");
        }
    },
    SetGamers = function (JsonGamers) {
        Gamers = JsonGamers;
        $("#Gamers").empty();

        Gamers.forEach(function (v, i) {
            var GamerElement = User.clone();

            GamerElement.find(".Name span").text(v.Login);
            GamerElement.attr("id", "User" + v.Id);
            GamerElement.find(".Gamer").css({ "background-image": "url('" + v.URL + "')" });
            if (v.Ready) GamerElement.addClass("Ready");

            $("#Gamers").append(GamerElement);
        });
    },
    DeleteGamer = function (UserId) {
        var gamer = Gamer(UserId);
        Gamers.splice(gamer.index, 1);
        $(".Gamers #User" + Id).remove();
    },
    SetReady = function (Id, Ready) {
        user = Gamer(Id);
        user.Ready = Ready;
        Gamers[user.index] = user;

        if (Ready) $(".Gamers #User" + Id).addClass("Ready");
        else $(".Gamers #User" + Id).removeClass("Ready");
    },
    SetAnswers = function (Answers) {

    };

function SendReady() {
    You.Ready = !You.Ready;

    if (You.Ready) {
        $(".Header .Ready .Ready").addClass("hidden");
        $(".Header .Ready .Pause").removeClass("hidden");
    } else {
        $(".Header .Ready .Ready").removeClass("hidden");
        $(".Header .Ready .Pause").addClass("hidden");
    }

    Hub.server.setReady(You.Ready);
}