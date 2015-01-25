$(function() {

    var matches = $.connection.matchesHub;
    $count = $('#count-notification');
    

    matches.client.addCount = function (count) {
        $count.text(count);
    }

    $.connection.hub.start().done(function () {
        matches.server.sendCountMatches();
    });

});