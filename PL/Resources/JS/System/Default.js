var Data = {};

$(document).ready(Ready).click(CDocument);
$(window).resize(Resize);

function Ready() {
    Data.Temp = {};

    CustomSelect();

    $("button").focus(function () { $(this).blur(); });
}

function Resize() {

}

function CDocument(e) {
    var element = null;
    
    if (element = CheckElement($(".CustomSelect"), e))
        element.removeClass("Show");
}

function CheckElement(E, e) {
    return (!E.is(e.target) && !E.has(e.target).length) ? E : null;
}

function CustomSelect() {
    var CustomSelect = $(".CustomSelect");

    var Temp;
    Temp = CustomSelect.find(".List [data-value = '" + CustomSelect.find(".Value").data("value") + "']");
    CustomSelect.find(".Value").html(Temp.html());

    CustomSelect.find(".Value")
        .click(function () {
            if (CustomSelect.hasClass("Show")) CustomSelect.removeClass("Show");
            else CustomSelect.addClass("Show");
        });

    CustomSelect.find(".Option").click(CustomSelectOption)
    CustomSelect.click(SendToServer);

    Data.Temp.Form = $("<form />");
    Data.Temp.Form.css("display", "none");
    $("body").append(Data.Temp.Form);
}

function CustomSelectOption() {
    var self = $(this),
        select = self.closest(".CustomSelect"),
        value = select.find(".Value");

    value.html(self.html()).data("value", self.data("value"));
}

function SendToServer() {
    var self = $(this),
        action = self.data("action"),
        method = self.data("method"),
        type = self.data("type"),
        name = self.data("name"),
        value = self.find(".Value").data("value");

    if (action != null && action != "") {
        var form = Data.Temp.Form,
            input = $("<input />");

        form.append(input);
        form.attr({ "action": action, "method": method });
        input.attr({ type: "text", "name": name, "value": value });
        form.submit();
    }
}