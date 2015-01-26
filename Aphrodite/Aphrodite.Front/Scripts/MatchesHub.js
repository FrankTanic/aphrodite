$(function() {

    var matches = $.connection.matchesHub;
    $count = $('#count-notification');
    

    matches.client.addCount = function (count) {

        if (count == 0)
        {
            $count.text("");
        }
        else
        {
            $count.text(count);
        }
    }

    matches.client.addNotification = function (notification) {
        $('.notification').text(notification);
    }

    $.connection.hub.start().done(function () {
        matches.server.sendCountMatches();
    });

});