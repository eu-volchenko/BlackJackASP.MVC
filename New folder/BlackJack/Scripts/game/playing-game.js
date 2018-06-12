$(window).ready(function() {
    var gameId = getParameterByName('id');
    $('#menu').hide();
    var idRound;
    var playersCards;
    var dealersCards;
    var botsCards = [];

    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }

    var gameModel;
    var url = 'http://localhost:50220/api/Game/Get';
    $.ajax({
        dataType: "json",
        url: url,
        data: "id=" + gameId
    }).done(function(model) {
        gameModel = JSON.parse(model);
        var dealerName = document.createElement('h2');
        dealerName.innerHTML = gameModel.DealerName;
        $('#dealer-place').append(dealerName);
        var playerName = document.createElement('h2');
        playerName.innerHTML = gameModel.PlayerName;
        $('#player-place').append(playerName);
        for (var i = 0; i < gameModel.NameOfBots.length; i++) {

            var botName = document.createElement('h2');
            botName.innerHTML = gameModel.NameOfBots[i];
            $('#bot-place-' + i).append(botName);
        }
        $.ajax({
            dataType: "json",
            url: 'http://localhost:50220/api/Game/CreateRound',
            data: "gameId=" + gameId
        }).done(function(roundJson) {
            var round = JSON.parse(roundJson);
            idRound = round.Id;
            playersCards = getCardsForPlayers(gameModel.PlayerName, gameId, $('#player-place'), idRound);
            dealersCards = getCardsForPlayers(gameModel.DealerName, gameId, $('#dealer-place'), idRound);
            for (var j = 0; j < gameModel.NameOfBots.length; j++) {
                var placeForUser = $('#bot-place-' + j);
                botsCards[j] = getCardsForPlayers(gameModel.NameOfBots[j], gameId, placeForUser, idRound);
            }
        });
    });

    function getCardsForPlayers(userName, gameId, placeForUser, idRound) {
        var cardsForPlayer;
        $.ajax({
            dataType: "json",
            url: 'http://localhost:50220/api/Game/GetCards',
            data: "idGame=" + gameId + "&nameUser=" + userName + "&roundId=" + idRound
        }).done(function(cardsForPlayerJson) {
            cardsForPlayer = JSON.parse(cardsForPlayerJson);
            var divForCards = document.createElement('div');
            divForCards.classList.add('cardList');
            for (var i = 0; i < cardsForPlayer.UserCardsId.length; i++) {
                var imgForCard = document.createElement('img');
                if (userName === gameModel.PlayerName) {
                    imgForCard.setAttribute("src", "../Content/Images/" + cardsForPlayer.UserCardsId[i] + ".png");
                } else {
                    imgForCard.setAttribute("src", "../Content/Images/back.png");
                }
                imgForCard.classList.add('cards');
                divForCards.appendChild(imgForCard);
                placeForUser.append(divForCards);

            }

        });
        return cardsForPlayer;
    }

    $('#one-more').click(function() {
        $.ajax({
            dataType: "json",
            url: 'http://localhost:50220/api/Game/OneMore',
            data: "gameId=" + gameId + "&userName=" + gameModel.PlayerName + "&roundId=" + idRound
        }).done(function(idCard) {
            cardId = JSON.parse(idCard);
            var imgForCard = document.createElement('img');
            imgForCard.setAttribute("src", "../Content/Images/" + cardId + ".png");
            imgForCard.classList.add('cards');
            $('#player-place').find('.cardList').append(imgForCard);
        });
    });

    $('#finish-round').click(function() {
        gameModel.RoundId = idRound;
        gameModel.Id = gameId;
        $.ajax({
            dataType: "json",
            url: 'http://localhost:50220/api/Game/OneMoreBots',
            data: "gameId=" + gameId + "&userName=" + gameModel.DealerName + "&roundId=" + idRound,
            type: "GET",
            contentType: 'application/json; charset=utf-8'
        });
        for (var j = 0; j < gameModel.NumberOfBots; j++) {
            $.ajax({
                dataType: "json",
                url: 'http://localhost:50220/api/Game/OneMoreBots',
                data: "gameId=" + gameId + "&userName=" + gameModel.NameOfBots[j] + "&roundId=" + idRound,
                contentType: 'application/json; charset=utf-8'
            });
        }

        $('#menu').fadeIn('fast');
        $.ajax({
            dataType: "json",
            url: 'http://localhost:50220/api/Game/FinishRound',
            data: JSON.stringify(gameModel),
            type: "POST",
            contentType: 'application/json; charset=utf-8'
        }).done(function(winnerJson) {
            winner = JSON.parse(winnerJson);
            var menu = $('#menu');
            var divForCards = document.createElement('div');
            divForCards.classList.add('cardListEndRound');
            var text = document.createElement('h2');
            text.textContent = "Winner is - " + winner.Name + "!!\n With Cards:";
            menu.append(text);
            for (var i = 0; i < winner.Cards.length; i++) {
                var imgForCard = document.createElement('img');
                imgForCard.setAttribute("src", "../Content/Images/" + winner.Cards[i] + ".png");
                imgForCard.classList.add('cardsEndRound');
                divForCards.appendChild(imgForCard);
                menu.append(divForCards);
            }
            menu.css('z-index', 10);
            var divHide = document.createElement('div');
            divHide.classList.add('hide-window');
            document.body.appendChild(divHide);
            $('#menu').show();
        });
    });

    $('#one-more-round').click(function() {
        window.location.replace("http://localhost:50220//Game/Game?id=" + gameId);
    });

    $('#finish-game').click(function() {
        window.location.replace("http://localhost:50220/Home/CreateGame/");
    });

    $('#menu').hide();
});
