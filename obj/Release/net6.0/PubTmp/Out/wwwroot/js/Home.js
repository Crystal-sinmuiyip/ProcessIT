//const myModal = document.getElementById('modal_form_addappt')
//const myInput = document.getElementById('calendar')
//document.addEventListener("DOMContentLoaded", function () {
//    var calendarUI = document.getElementById("calendar");
//    var calendar = new FullCalendar.Calendar(calendarUI, {});

//    const myModal = document.querySelector("#calendar-modal");
//    myModal.addEventListener("shown.bs.modal", () => {
//        calendar.render();
//    });
//});
//myModal.addEventListener('shown.bs.modal', () => {

//    calendar.render();
//})


//$("#calendar").fullCalendar({
//});


//$('#modal_form_addappt').on('shown.bs.modal', function () {
//       $("#calendar").fullCalendar('render');
//});


//document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {


        header: {
            left: 'prev',
            center: 'title',
            right: 'next',
        },


        initialView: 'dayGridMonth',
        editable: false,
        displayEventTime: false,

        eventClick: function (info, jsEvent, view) {
            var eventObj = info.event;
            if (eventObj.id) {

                window.open('https://localhost:7120/Reservation/Create/' + eventObj.id)

            }
        },
        events: 'https://localhost:7120/Reservation/findallbookings'
    });
    calendar.render();

 
//});


/* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
//window.onclick = function(e) {
//        if (!e.target.matches('.dropbtn')) {
//            var myDropdown = document.getElementById("myDropdown");
//if (myDropdown.classList.contains('show')) {
//    myDropdown.classList.remove('show');
//            }
//        }
$(".dropdown").click(function () {
    if (!e.target.matches('.dropbtn')) {
        var myDropdown = document.getElementById("myDropdown");
        if (myDropdown.classList.contains('show')) {
            myDropdown.classList.remove('show');
        }
    }
})



