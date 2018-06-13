$(window).ready(function () {

});


$("#currency").kendoNumericTextBox({
    format: "n",
    decimals: null,
    restrictDecimals: 1
});

$('#currency').change(function () {
    var botsValue = $('#currency').val();
    $('.nameBots').remove();

    var i = 0;
    while (botsValue * 2 !== $('.nameBots').length) {
        var labelForInput = document.createElement('label');
        labelForInput.innerText = "Name of bot #" + (i + 1);
        labelForInput.setAttribute('for', 'botName' + i);
        labelForInput.classList.add('nameBots');
        labelForInput.classList.add('label-control');
        document.getElementById('bots').appendChild(labelForInput);
        var inputBotName = document.createElement('input');
        inputBotName.setAttribute('id', 'botName' + i);
        inputBotName.classList.add('form-control');
        inputBotName.classList.add('nameBots');
        document.getElementById('bots').appendChild(inputBotName);
        i++;
    }
});

function getBotNames () {
    //var fieldsWithName = $("[id ^= 'botName']").length;
    //var names = [];
    //for (var i = 0; i < fieldsWithName; i++) {
    //    names[i] = fieldsWithName[i].val();
    //}
    //return names;

    var names = [];
    var index = 0;
    $("[id ^= 'botName']").each(function(index) {
        names[index] = $(this).val();
        index++;
    });
    return names;
}

$(window).ready(function() {
    $('#sendData').click(function() {
        "use strict";
        var model = {
            PlayerName: $('#playerName').val(),
            NumberOfBots: $('#currency').val(),
            DealerName: $('#dealerName').val()
        };
        model.NameOfBots = getBotNames();
        $.ajax({
            url: 'http://localhost:59384/api/CreateGame/Create',
            type: "POST",
            data: JSON.stringify(model),
            contentType: "application/json"
        }).done(function(id) {
            window.location.replace("http://localhost:50220//Game/Game?id=" + id);
        });

    });
});

