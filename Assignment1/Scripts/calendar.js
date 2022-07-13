var events = [];
$(".events").each(function () {
    var title = $(".title", this).text().trim();
    var start = $(".start", this).text().trim();
    var event = {
        "title": title,
        "start": start
    };
    events.push(event);
});
$("#calendar").fullCalendar({
    locale: 'au',
    events: events,

    dayClick: function (date, allDay, jsEvent, view) {
        var cd = new Date();
        var d = new Date(date);
        var m = moment(d).format("YYYY-MM-DD");
        m = encodeURIComponent(m);
        if (d <= cd) {
            var uri = "/Events/DateExpired"
        }
        else
        {
            var uri = "/Events/Create?date=" + m;
        }
        $(location).attr('href', uri);
    }
});