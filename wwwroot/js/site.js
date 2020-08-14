// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

//for dropdown menu





window.addEventListener('load', onVrViewLoad);
function onVrViewLoad() {
    // Selector '#vrview' finds element with id 'vrview'.

    var vrView = new VRView.Player('#vrview', {
        width: '100%',
        height: 300,
        image: 'https://storage.googleapis.com/vrview/examples/coral.jpg',
        is_stereo: true,
        is_autopan_off: true
    });


}

//for use with those three little fading arrows that let you scroll up and down.
$(document).ready(function () {
    // Add smooth scrolling to all links
    $("a").on('click', function (event) {

        // Make sure this.hash has a value before overriding default behavior
        if (this.hash !== "") {
            // Prevent default anchor click behavior
            event.preventDefault();

            // Store hash
            var hash = this.hash;

            // Using jQuery's animate() method to add smooth page scroll
            // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 800, function () {

                // Add hash (#) to URL when done scrolling (default click behavior)
                window.location.hash = hash;
            });
        } // End if
    });



});



/* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
function myFunction() {

    $('.animated-icon1').toggleClass('open');
    document.getElementById("buttonEvent1").classList.toggle("show");
    
    document.getElementById("titlePage").classList.toggle("hide");
   
    
}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropMenuButton')) {
        if ($('.animated-icon1').hasClass('open')) {
            $('.animated-icon1').removeClass('open');
        }
        else {

        }
        var dropdowns = document.getElementsByClassName("dropMenuContent");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
        if (document.getElementById("titlePage").classList.contains('hide')) {
            document.getElementById("titlePage").classList.remove('hide');
           


           
        }
        
    }
}


