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
    
    var Id = Gamer.attr("key");
    //console.log(Id);
    Hub.server.setAnswer(Id);

    $(".Gamer.SelectSave").removeClass("SelectSave");
    Gamer.addClass("SelectSave");
}

function SetYou(JsonYou) {
    You = JsonYou;
}
function SetGame(JsonGame) {
    for (var v in JsonGame) Data.Game[v] = JsonGame[v];
    ReplaceHref(`/IO/${JsonGame.Id}`);
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
    var Gamer = $(`#Gamer${JsonGamer.Id}`);

    Gamer.find(".Image").css("background-image", `url("${JsonGamer.URL}")`);
    Gamer.find(".Name span").text(JsonGamer.Login);

    JsonGamer.Ready ? Gamer.addClass("Ready") : Gamer.removeClass("Ready");

    if (JsonGamer.Id == You.Id) {
        $("#Coins .Coins").text(JsonGamer.Coins);
        JsonGamer.Ready ? $("#Ready").attr("id", "Pause") : $("#Pause").attr("id", "Ready");
    }
}
function SetReady(JsonGamer) {
    var Gamer = $("#Gamer" + JsonGamer.Id);
    if (!Gamer.is("*")) SetGamers([JsonGamer]);
    else UpdateGamer(JsonGamer);
}
function SetTask(JsonTask) {
    $("#Gamers").removeClass(["Waiting", "Answer"]).addClass("Task");

    $("#Title span").text(JsonTask.Value);

    $(".Gamer").removeClass("Ready");

    $("#Info").empty()
        .append(Data.Div.Timer.clone());
    $("#Buttons").empty()
        .append(Data.Button.Send.clone())
        .append(Data.Button.Lose.clone());
}
function SetAnswers(JsonAnswers) {
    JsonAnswers.forEach(function (v, i) {

    });
    $("Gamers").removeClass("Task").addClass("Answer");
}
function SetResult(Results) {

}
function Auth() {

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

$.connection.hub.start().done(function () {
    Hub.server.connect(Data.Game.Id);
});