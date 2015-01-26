//facedate_Image

//facedate_Dislike

//facedate_Like

$(function () {

    $('a#facedate_Dislike').click(function (e) { e.preventDefault(); });
    $('a#facedate_Like').click(function (e) { e.preventDefault(); });
    position_picture()
  //// $("#facedate_Image").on("swipeleft", swipeleftHandler);
  //  function swipeleftHandler(event) {
  //      rate("dislike");
  //  }

  //  $("#facedate_Image").on("swiperight", swiperightHandler);
  //  function swiperightHandler(event) {
  //      rate("like");
  //  }
});


function rate(location) {
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

function position_picture(){
    var width = 320;    //Display width
    var height = 320;   //Display height
    var left;
    var top;
    var img = document.getElementById('profile-image');
    var img_width = img.clientWidth;
    var img_height = img.clientHeight;

    //position central
    left = img_width - width;
    left = Math.round(left / 2);
    left = left * (-1);
    top = img_height - height;
    top = Math.round(top / 2);
    top = top * (-1);     

    $("#profile-image").css('left', left);
    $("#profile-image").css('top', top);
    console.log(left);
    console.log(top);

}