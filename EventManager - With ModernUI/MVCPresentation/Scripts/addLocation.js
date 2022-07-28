$(document).ready(function () {
    $(document).on('change', '[name="locationDrop"]', function () {
        var checkbox = $(this), // Selected or current checkbox
            value = checkbox.val(); // Value of checkbox

        if (checkbox.is(':checked')) {
            $("#txtZip").val("00000");
            $("#txtZip").addClass("visually-hidden");
            $("#locations").removeClass("visually-hidden");
            $("#name").attr('readonly', true);
            $("#city").attr('readonly', true);
            $("#state").attr('readonly', true);
            $("#address").attr('readonly', true);

        } else {
            $("#locations").addClass("visually-hidden");
            $("#txtZip").removeClass("visually-hidden");
            $("#txtZip").val("");
            $("#name").val("");
            $("#city").val("");
            $("#state").val("");
            $("#address").val("");
            $("#name").attr('readonly', false);
            $("#city").attr('readonly', false);
            $("#state").attr('readonly', false);
            $("#address").attr('readonly', false);
        }
    });
});

function changed() {
    var name = $("#locations :selected").text().match(/(?<=Name: \s*).*?(?=\s*, City:)/gs);
    var city = $("#locations :selected").text().match(/(?<=, City: \s*).*?(?=\s*, State:)/gs);
    var state = $("#locations :selected").text().match(/(?<=, State: \s*).*?(?=\s*, Address:)/gs);
    var address = $("#locations :selected").text().match(/(?<=, Address: \s*).*?(?=\s*[|])/gs);
    $("#name").val(name);
    $("#city").val(city);
    $("#state").val(state);
    $("#address").val(address);
    $("#zip").val(0000);
}