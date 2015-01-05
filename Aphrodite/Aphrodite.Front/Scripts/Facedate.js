//facedate_Image

//facedate_Dislike

//facedate_Like

$(function () {
    $("#facedate_Image").on("swipeleft", swipeleftHandler);
    function swipeleftHandler(event) {
        rate("dislike");
    }

    $("#facedate_Image").on("swiperight", swiperightHandler);
    function swiperightHandler(event) {
        rate("like");
    }
});

function rate(rating){
    var rating = rating;    
    alert(rating);

}