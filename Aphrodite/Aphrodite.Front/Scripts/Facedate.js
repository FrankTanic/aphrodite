//facedate_Image

//facedate_Dislike

//facedate_Like

$(function () {

    $('a#facedate_Dislike').click(function (e) { e.preventDefault(); });
    $('a#facedate_Like').click(function (e) { e.preventDefault(); });

  //// $("#facedate_Image").on("swipeleft", swipeleftHandler);
  //  function swipeleftHandler(event) {
  //      rate("dislike");
  //  }

  //  $("#facedate_Image").on("swiperight", swiperightHandler);
  //  function swiperightHandler(event) {
  //      rate("like");
  //  }

  //  $('#facedate_Dislike').click(function () { rate() });
  //  $('#facedate_Like').click(function () { rate() });
});


function rate(location) {
    console.log(location);
    var location = location;
    var xmlhttp;
    if (window.XMLHttpRequest) { xmlhttp = new XMLHttpRequest(); } else { xmlhttp = new ActiveXObject("Microsoft.XMLHTTP"); };
    xmlhttp.onreadystatechange = function () {
        response = xmlhttp.responseText;
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            response = xmlhttp.responseText;
            $("#profile-box-wrapper").hide(500);
            $("#profile-box").replaceWith(response);
            $("#profile-box-wrapper").show(500);



        }
        else {
           console.log("ERROR!!:" + response); 
        }
    }
    xmlhttp.open("GET", location, true);
    xmlhttp.send();

    console.log(location);
}