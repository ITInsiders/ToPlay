var SearchGame = function () {
    var _this = this,
        LoadObject =
            {
                html: "<div class=\"WindowGame\"><div class=\"Block\"><div class=\"Top\"><div class=\"Logo\"></div></div><div class=\"Bottom\"></div></div></div>",
                one: "<div class=\"Title\">Поиск игры</div><div class=\"Load\" >Ожидание</div><div class=\"Cancel\" onclick=\"SearchGame.Close();\">Отмена</div>",
                two: "<div class=\"Message\">Игра не найдена!<br />Желаете продолжить поиск?</div><div class=\"Answer\" onclick=\"SearchGame.Start();\">Да</div><div class=\"Answer\" onclick=\"SearchGame.Close();\">Нет</div>",
                three: "<div class=\"TitleLoad\">Загрузка</div>"
            },
        Connected =
            {
                GameHub: null,
                Connect: null,
                Search: null,
                Stop: null
            },
        GameID = null,
        SearchID = null;

    function Connection() {
        Connected.GameHub = $.connection.SearchGame;
        Connected.GameHub.client.startGame = StartGame;
        Connected.GameHub.client.stop = function () { Close(); Frame("/Entry"); }
        $.connection.hub.logging = true;

        $.connection.hub.start().done(function () {
            Connected.GameHub.server.connect();
            Connected.Connect = Connected.GameHub.server.connect;
            Connected.Search = Connected.GameHub.server.search;
            Connected.Stop = Connected.GameHub.server.stop;
            Connected.Disconnected = Connected.GameHub.server.disconnected;
        });
    };

    var StartGame = function (URL) {
        $(".WindowGame .Bottom").html(LoadObject.three);
        $(location).attr("href", URL);
    }

    this.Open = function (ID) {
        GameID = ID;
        $('body').append(LoadObject.html);
        this.Start();
    }

    var Close = function () {
        clearInterval(SearchID);
        Connected.Stop();
        $(".WindowGame").remove();
    }
    this.Close = Close;

    this.Start = function () {
        $(".WindowGame .Bottom").html(LoadObject.one);
        SearchID = setInterval(function () { Connected.Search(GameID); }, 3000);
        setTimeout(Ask, 30000);
    }

    var Ask = function () {
        clearInterval(SearchID);
        Connected.Stop();
        $(".WindowGame .Bottom").html(LoadObject.two);
    }

    Connection();
};

$(function () {
    SearchGame = new SearchGame();
});