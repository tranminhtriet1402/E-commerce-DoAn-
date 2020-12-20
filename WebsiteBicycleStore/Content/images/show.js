$(document).ready(function () {
    $("p#check").hide();
    $("input[name=ship]").on("click", function () {
        var selectedValue = $("input[name=ship]:checked").val();

        if (selectedValue == "Show") {
            $("p#check").show();

        }
        else if (selectedValue == "Hide") {
            $("p#check").hide();
        }
    });
});