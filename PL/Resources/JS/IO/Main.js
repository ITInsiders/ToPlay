var Hub = null,
    You = null;

function SendReady(element) {
    You.Ready = !You.Ready;
    Hub.server.setReady(You.Ready);
    UpdateGamer(You);
}
function SetSelect(element) {
    if ($("#Gamers").is(".Task")) {
        $(".Gamer").removeClass("Select");
        $(element).closest(".Gamer").addClass("Select");
    }
}
function SendSelect(element) {
    if (element.id == "Send")
        element.id = "Change";
    
    var Gamer = $(".Gamer.Select");

    if (Gamer.is("*") != null) {
        var Id = Gamer.attr("key");
        Hub.server.setAnswer(Id);

        $(".Gamer.SelectSave").removeClass("SelectSave");
        Gamer.addClass("SelectSave");
    } else {
        element.id = "Send";
    }
}
function ClearGamer(Id = null) {
    var Gamer = Id != null ? $(`#Gamer${Id}`) : $(".Gamer");
    $("#Gamers").removeClass(["Waiting", "Task", "Answer", "Result"]);
    Gamer.removeClass(["Select", "SelectSave", "Ready"]);
    Gamer.find(".Answer").remove();
    Gamer.find(".Result").remove();
    return Gamer;
}

function SetYou(JsonYou) {
    You = JsonYou;
    UpdateGamer(You)
}
function SetGame(JsonGame) {
    for (var v in JsonGame) Data.Game[v] = JsonGame[v];
    ReplaceHref(`/IO/${JsonGame.Id}`);
    ClearGamer();
    $("#Gamers").addClass("Waiting");

    $("#Info").empty()
        .append(Data.Div.Waiting.clone());
    $("#Buttons").empty()
        .append(Data.Button.Ready.clone())
        .append(Data.Button.Lose.clone());
}
function SetGamers(JsonGamers) {
    JsonGamers.forEach(function (v, i) {
        var Gamer = $(`#Gamer${v.Id}`);
        if (!Gamer.is("*")) {
            Gamer = Data.Gamer.clone();
            Gamer.attr("id", `Gamer${v.Id}`);
            Gamer.attr("key", v.Id);
            $("#Gamers").append(Gamer);
        }
        UpdateGamer(v);
    });
}
function DeleteGamer(Id) {
    $(`#Gamer${Id}`).remove();
}
function UpdateGamer(JsonGamer) {
    if (You.Id == JsonGamer.Id) {
        You = JsonGamer;
        $("#Coins .Coins").text(JsonGamer.Coins);
        JsonGamer.Ready ? $("#Ready").attr("id", "Pause") : $("#Pause").attr("id", "Ready");
    }

    var Gamer = $(`#Gamer${JsonGamer.Id}`);

    Gamer.find(".Image").css("background-image", `url("${JsonGamer.URL}")`);
    Gamer.find(".Name span").text(JsonGamer.Login);

    JsonGamer.Ready ? Gamer.addClass("Ready") : Gamer.removeClass("Ready");
}
function SetReady(JsonGamer) {
    var Gamer = $("#Gamer" + JsonGamer.Id);
    if (!Gamer.is("*")) SetGamers([JsonGamer]);
    else UpdateGamer(JsonGamer);
}
function SetTask(JsonTask) {
    ClearGamer();
    $("#Gamers").addClass("Task");

    $("#Title span").text(JsonTask.Value);

    $("#Info").empty()
        .append(Data.Div.Timer.clone());
    $("#Buttons").empty()
        .append(Data.Button.Send.clone())
        .append(Data.Button.Lose.clone());
}
function SetAnswers(JsonAnswers) {
    ClearGamer();

    $("#Info").empty()
        .append(Data.Div.Coins.clone())
        .append(Data.Div.Waiting.clone());
    $("#Buttons").empty()
        .append(Data.Button.Ready.clone())
        .append(Data.Button.Lose.clone());

    $("#Gamers").addClass("Answer");

    var hide = JsonAnswers.length < 2;
    console.log(hide);
    JsonAnswers.forEach(function (v, i) {
        var Sender = $(`#Gamer${v.SenderId}`),
            Recipient = $(`#Gamer${v.RecipientId}`),
            RecipientLogin = Recipient.find(".Name span").text(),
            Answer = Sender.find(".Info .Image .Answer");

        Sender.removeClass(["Select", "SelectSave"]);

        Answer = Data.Gamer.Answer.clone();
        Answer.find(".Login").text(RecipientLogin);

        if (hide) Answer.find(".AnswerCoins").addClass("hidden");
        else {
            Answer.find(".AnswerCoins").removeClass("hidden");
            Answer.find(".Coins").text(v.Coins);
        }

        Sender.find(".Info .Image").append(Answer);
    });
}
function SetTimer(Time) {
    $("#Timer .Time").text(Time);
}
function SetResult(Results, Title) {
    console.log(Results);
    ClearGamer();
    $("#Gamers").addClass("Result");

    $("#Info").empty();
    $("#Buttons").empty()
        .append(Data.Button.Lose.clone());

    $("#Lose").attr("id", "Exit");
    $("#Title span").text(Title);

    Results.forEach(function (v, i) {
        var Gamer = $(`#Gamer${v.Id}`),
            Image = Gamer.find(".Info .Image");

        Image.append(Data.Gamer.Characteristic.clone());
        Image.find(`.Characteristic .Text span`).text(v.Feature);
    });
}
function Auth() {
    window.location.href = "/Home/Entry";
}
function Reload() {
    window.location.reload();
}

Hub = $.connection.HubIO;

Hub.client.setYou = SetYou;
Hub.client.setGame = SetGame;
Hub.client.setGamers = SetGamers;
Hub.client.deleteGamer = DeleteGamer;
Hub.client.updateGamer = UpdateGamer;
Hub.client.setReady = SetReady;
Hub.client.setTask = SetTask;
Hub.client.setAnswers = SetAnswers;
Hub.client.setResult = SetResult;
Hub.client.auth = Auth;
Hub.client.reload = Reload;
Hub.client.setTimer = SetTimer;

$.connection.hub.start().done(function () {
    Hub.server.connect(Id);
});