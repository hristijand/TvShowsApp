// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function rent(id) {
    $.post("/RentedMovies/Rent",
        {
            UserId: document.getElementById("selected_item").value,
            TvShowId: id
        });

    var rentId = "#rent_" + id;
    $(rentId).hide();
    var retId = "#ret_" + id;
    $(retId).show();
}

function ret(id) {
    $.post("/RentedMovies/Return",
        {
            UserId: document.getElementById("selected_item").value,
            TvShowId: id
        });

    var rentId = "#rent_" + id;
    $(rentId).show();
    var retId = "#ret_" + id;
    $(retId).hide();
}

    function myFunction() {
  // Declare variables
        let input, filter, i;
        input = document.getElementById("myInput");
        filter = input.value.toLowerCase();
        let myRowTv = document.querySelectorAll('.myRowTv');
        let title = document.querySelectorAll('.title');
  
    // Loop through all table rows, and hide those who don't match the search query
        for (i = 0; i < myRowTv.length; i++) {
            let titleText = title[i].innerText;
            if (titleText.toLowerCase().indexOf(filter) > -1) {
                myRowTv[i].style.display = "";
            }
            else {
                myRowTv[i].style.display = "none";
            }
        
}
}

