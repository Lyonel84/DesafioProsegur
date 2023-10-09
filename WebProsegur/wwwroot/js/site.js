$(document).ready(function () {

    menu();
});

function menu() {
    var idrol = $("#idrol").val();

    if (idrol == 1) {
        $("#item1").show();
        $("#item2").show();
        $("#item3").show();
        $("#item4").show();
        $("#item5").show();
        $("#item6").show();
        $("#item7").show();
    }
    else if (idrol == 2) {
        $("#item1").show();
        $("#item3").show();
        $("#item4").show();
    }
    else if (idrol == 3) {       
        $("#item2").show();
    }
    else {
        $("#item1").show();
    }
}