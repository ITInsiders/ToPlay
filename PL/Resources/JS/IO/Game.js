$(document).ready(Ready);

function Ready() {
    $(".Gamer").click(SelectGamer);
}

function SelectGamer() {
    var self = $(this);
    $(".Gamer").removeClass("Select");
    self.addClass("Select");
}