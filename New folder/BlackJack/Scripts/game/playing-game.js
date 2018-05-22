var model;
var id = parseInt((window.location.search).replace("?id=", ""));
$(window).ready(function () {
    $.get('http://localhost:50220/api/Game/GET')
        .done(function(model) {
            this.model = model;
        });
});