using UnityEngine;
using Random = UnityEngine.Random;

public class CardFactory {
    private Card _cardPrefab;
    private GameRules _gameRules;
    public CardFactory(Card prefab, GameRules gameRules) {
        _cardPrefab = prefab;
        _gameRules = gameRules;
    }

    public CardController CreateCard() {
        var card = GameObject.Instantiate(_cardPrefab);

        var attack = Random.Range(_gameRules.StartCardMin, _gameRules.StartCardMax + 1);
        var health = Random.Range(_gameRules.StartCardMin, _gameRules.StartCardMax + 1);
        var mana = Random.Range(_gameRules.StartCardMin, _gameRules.StartCardMax + 1);

        var cardInfo = new CardInfo();
        cardInfo.Attack.Value = attack;
        cardInfo.Health.Value = health;
        cardInfo.Mana.Value = mana;

        card.StartCoroutine(ArtLoader.DownloadImage(card.GetArt()));
        var cardController = new CardController(card, cardInfo);

        return cardController;
    }
}