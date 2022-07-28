
$(document).ready(function () {
    $("#btnUnkown").removeAttr("checked");
    $("#btnAddDate").click(function () {
        AnotherEventDate();
    });

});

$("#btnSaveEvent").click(function () {
    SaveEventRecord();
});
function RemoveDate(index) {
    $("#r" + index).remove();
}
function RemoveAllDates() {
    $("#tableEventDates").find("tr:gt(0)").remove();
}

function AnotherEventDate() {
    var index = $("#tableEventDates tr").length;
    var EventDates = "<tr id=" + "r" + index + "><td>" + $("#txtDate").val() + "</td><td>" + $("#txtStart").val() + "</td><td>" + $("#txtEnd").val() + "</td><td><input type='button' onClick='RemoveDate(" + index + ")'class ='btn-sm btn-danger' value='remove' /></td></tr>";
    var date = $("#txtDate").val().replace(/-0+/g, '-');;
    var d = new Date();
    var today = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();
    var start = $("#txtStart").val();
    var end = $("#txtEnd").val();
    if ($("#txtDate").val().length > 0 && $("#txtStart").val().length > 0 && $("#txtEnd").val().length > 0) {
        if (start < end) {
            if (today < date) {
                $("#tableEventDates").last().append(EventDates);
                $("#txtDate").val("");
                $("#txtStart").val("");
                $("#txtEnd").val("");
            } else {
                alert("Date cannot be set in the past.");
            }
        }
        else {
            alert("End time cannot come before the start time.");
        }
    }
    else {
        alert("All fields are required.");
    }
}
function NoDates() {

    RemoveAllDates();
    SaveEventRecord(1);

}