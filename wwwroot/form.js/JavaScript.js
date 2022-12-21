$('#submitRes').hide();
var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the current tab
function showTab(n) {
    // This function will display the specified tab of the form...
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    //... and fix the Previous/Next buttons:
    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {
        var test = $("#regForm").find("input[name=Name]").val();
        var item = $('#summaryName').text(test);
        var test = $("#regForm").find("input[name=Phone]").val();
        var item = $('#summaryPhone').text(test);
        var test = $("#regForm").find("input[name=Email]").val();
        var item = $('#summaryEmail').text(test);
        var test = $("#regForm").find("select[name=StartTime]").val();
        var item = $('#summaryTime').text(test);
        var test = $("#regForm").find("input[name=NumberOfPeople]").val();
        var item = $('#summaryNumberOfPeople').text(test);
        var test = $("#regForm").find("input[name=Notes]").val();
        var item = $('#summaryNotes').text('Notes:' + " " + test);
        var birthdayCheck = $("#regForm").find("input[name=Birthday]").is(":checked");
        if (birthdayCheck) {
            var item = $('#summaryBirthday').text('Birthday');
        }
        var AnniversaryCheck = $("#regForm").find("input[name=Anniversary]").is(":checked");
        if (AnniversaryCheck) {
            var item = $('#summaryAnniversary').text('Anniversary');
        }
        var DisabledAccessCheck = $("#regForm").find("input[name=DisabledAccess]").is(":checked");
        if (DisabledAccessCheck) {
            var item = $('#summaryDisabledAccess').text('Disabled Access');
        }
        var PramCheck = $("#regForm").find("input[name=Pram]").is(":checked");
        if (PramCheck) {
            var item = $('#summaryPram').text('Pram');
        }
        var HighChairCheck = $("#regForm").find("input[name=HighChair]").is(":checked");
        if (HighChairCheck) {
            var item = $('#summaryHighChair').text('High Chair');
        }
        var AllergyCheck = $("#regForm").find("input[name=Allergy]").is(":checked");
        if (AllergyCheck) {
            var item = $('#summaryAllergy').text('Allergy');
        }


        $('#submitRes').show();
        document.getElementById("nextBtn").style.display = "none";
        //document.getElementById("nextBtn").innerHTML = "Submit";
     

    } else {
        document.getElementById("nextBtn").style.display = "inline";
        document.getElementById("nextBtn").innerHTML = "Next";
        $('#submitRes').hide();
    }
   


        //... and run a function that will display the correct step indicator:
        fixStepIndicator(n)
    };

    function nextPrev(n) {
        // This function will figure out which tab to display
        var x = document.getElementsByClassName("tab");
        // Exit the function if any field in the current tab is invalid:
        if (n == 1 && !validateForm()) return false;
        // Hide the current tab:
        x[currentTab].style.display = "none";
        // Increase or decrease the current tab by 1:
        currentTab = currentTab + n;
        // if you have reached the end of the form...
        if (currentTab >= x.length) {
            // ... the form gets submitted:
            document.getElementById("reg-form").submit();
            return false;
 
        }

        // Otherwise, display the correct tab:
        showTab(currentTab);
    }

    function validateForm() {
        // This function deals with validation of the form fields
        var x, y, i, valid = true;
        x = document.getElementsByClassName("tab");
        y = x[currentTab].getElementsByTagName("input");
        // A loop that checks every input field in the current tab:
        for (i = 0; i < y.length; i++) {
            // If a field is empty...
            if (y[i].value == "") {
                // add an "invalid" class to the field:
                y[i].className += " invalid";
                // and set the current valid status to false
                valid = false;
            }
        }
        // If the valid status is true, mark the step as finished and valid:
        if (valid) {
            document.getElementsByClassName("step")[currentTab].className += " finish";
        }
        return valid; // return the valid status
    }

    function fixStepIndicator(n) {
        // This function removes the "active" class of all steps...
        var i, x = document.getElementsByClassName("step");
        for (i = 0; i < x.length; i++) {
            x[i].className = x[i].className.replace(" active", "");
        }
        //... and adds the "active" class on the current step:
        x[n].className += " active";
    }
