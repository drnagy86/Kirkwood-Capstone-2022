/// <summary>
/// Derrick Nagy
/// Created: 2022/04/07
/// 
/// Description:
/// sets a label attribute on all td tags
/// ripped off from https://medium.com/allenhwkim/mobile-friendly-table-b0cb066dbc0e
/// </summary>
window.setMobileTable = function (selector) {
    // if (window.innerWidth > 600) return false;
    const tableEl = document.querySelector(selector);
    const thEls = tableEl.querySelectorAll('thead th');
    const tdLabels = Array.from(thEls).map(el => el.innerText);
    tableEl.querySelectorAll('tbody tr').forEach(tr => {
        Array.from(tr.children).forEach(
            (td, ndx) => td.setAttribute('label', tdLabels[ndx])
        );
    });
}



$(document).ready(function () {

// <summary>
// Derrick Nagy
// Created: 2022/04/28
// 
// Description:
// Checks to see if start time if before end if both have been entered
// </summary>    
    $("#volunteerStartTime").change(function () {
        if ($("#volunteerEndTime").val() !== "") {
            checkTimes();
        }
        
    });// start time change

// <summary>
// Derrick Nagy
// Created: 2022/04/28
// 
// Description:
// Checks to see if start time if before end if both have been entered
// </summary>
    $("#volunteerEndTime").change(function () {
        if ($("#volunteerStartTime").val() !== "") {
            checkTimes();
        }
    });// end time change event

// <summary>
// Derrick Nagy
// Created: 2022/04/28
// 
// Description:
// Checks to see if start time if before end 
// </summary>
    function checkTimes() {
        let startTime = $("#volunteerStartTime").val();
        let endTime = $("#volunteerEndTime").val();

        if (startTime > endTime) {
            // problem(send message)
            $("#volunteerEndTimeValidationMessage").text("Start not before end.");
            $("#applyToVolunteerSubmit").prop("disabled", true);
            console.log("disabled");

        } else {
            // cool(remove message)
            $("#volunteerEndTimeValidationMessage").text("");
            $("#applyToVolunteerSubmit").prop("disabled", false);
            console.log("enabled");
        }
    }
        
    $('input:checkbox').click(function () {
        atLeastOneDaySelected();

    });// end apply to volunteer submit click

// <summary>
// Derrick Nagy
// Created: 2022/04/28
// 
// Description:
// Checks to see if at least one day has been selected
// </summary>
    function atLeastOneDaySelected() {
        
        let checkBoxes = $('input:checkbox');
        let count = 0;

        checkBoxes.each(function () {
            if (!this.checked) {
                count++;
            }
        });

        if (count === 7) {
            $("#volunteerDaysOfWeekValidationMessage").text("Select at least one day");
            $("#applyToVolunteerSubmit").prop("disabled", true);
            console.log("enabled");
        }
        else {
            $("#volunteerDaysOfWeekValidationMessage").text("");
            $("#applyToVolunteerSubmit").prop("disabled", false);
            console.log("disabled");
        }
    };
    console.log("hey");

});