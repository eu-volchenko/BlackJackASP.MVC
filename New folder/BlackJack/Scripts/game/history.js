$(document).ready(function () {
    $('.details-container').hide();
    $('.dark').hide();
    var products;
    $.ajax({
        dataType: "json",
        url: 'http://localhost:59384/api/History/GetGames/'
    }).done(function (jsonList) {
        products = JSON.parse(jsonList);
        $("#grid").kendoGrid({
            dataSource: {
                data: products,
                schema: {
                    model: {
                        fields: {
                            CountOfBots: { type: "int" },
                            DateTimeGame: { type: "DateTime" },
                            Id: { type: "int" }
                        }
                    }
                },
                pageSize: 20
            },
            height: 550,
            scrollable: true,
            sortable: true,
            filterable: true,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
                {
                    field: "DateTimeGame",
                    title: "Date Time"
                },
                {
                    field: "CountOfBots",
                    title: "Count of bots"
                },
                {
                    field: "Id",
                    title: "Id"
                },
                {
                    template: "<button onclick = \"ShowInfo(#=Id#)\">Info</button>"
                }
            ]
        });


    });

});
function ShowInfo(id) {
    $('#info-round').empty();
    var detailsConteiner = $('.details-container');
    detailsConteiner.fadeIn("fast");
    detailsConteiner.css('z-index', 10);
    detailsConteiner.show();
    $('.dark').fadeIn("fast");
    $('.dark').show();
    var rounds;
    $.ajax({
        dataType: "json",
        url: 'http://localhost:59384/api/History/GetRounds',
        data: "gameId=" + id
    }).done(function (jsonRounds) {
        rounds = JSON.parse(jsonRounds);
        $('#rounds').kendoGrid({
            dataSource: {
                data: rounds,
                schema: {
                    model: {
                        fields: {
                            RoundInGame: { type: "int" },
                            WinnerName: { type: "string" }
                        }
                    }
                },
                pageSize: 20
            },
            height: 700,
            scrollable: true,
            sortable: true,
            filterable: true,
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
                {
                    field: "RoundInGame",
                    title: "Round in Game"
                },
                {
                    field: "WinnerName",
                    title: "Winner Name"
                },
                {
                    template: "<button onclick = \"GameInfo(#=Id#," + id + ")\">Info</button>"
                }
            ]
        });
    });
}


function GameInfo(roundId, gameId) {
    $('#info-round').empty();
    $.ajax({
        dataType: "json",
        url: 'http://localhost:59384/api/History/GetPlayers',
        data: "gameId=" + gameId
    }).done(function (playersIdJson) {
        var playersId = JSON.parse(playersIdJson);
        var infoCards = document.createElement("div");
        infoCards.classList.add("cards-history");
        for (var i = 0; i < playersId.PlayersId.length; i++) {
            $.ajax(
                {
                    dataType: "json",
                    url: 'http://localhost:59384/api/History/GetPlayers',
                    data: "roundId=" + roundId + "&userId=" + playersId.PlayersId[i]
                }).done(function (playerCardsIdJson) {
                    var playersCardId = JSON.parse(playerCardsIdJson);
                    var redString = document.createElement('p');
                    var name = document.createElement('h3');

                    name.textContent = "Player " + playersCardId.PlayerName + " had cards:";
                    redString.appendChild(name);
                    infoCards.append(redString);
                    var divForCards = document.createElement('div');
                    divForCards.classList.add('cardList');
                    for (var i = 0; i < playersCardId.CardsId.length; i++) {
                        var imgForCard = document.createElement('img');
                        imgForCard.setAttribute("src", "../Content/Images/" + playersCardId.CardsId[i] + ".png");
                        imgForCard.classList.add('cards');
                        divForCards.appendChild(imgForCard);
                        infoCards.append(divForCards);

                    }
                    $('#info-round').append(infoCards);
                });
        }
    });
}

function CloseDetailsWindow() {
    var detailsConteiner = $('.details-container');
    var dark = $('.dark');
    detailsConteiner.fadeOut("fast");
    dark.fadeOut("fast");
}

