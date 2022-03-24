using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform _handTransform;
    [SerializeField] private GameRules _gameRules;
    [SerializeField] private UIRules _uiRules;
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private BoardView _boardView;

    private int _currentSelectedCardIndex;
    private Hand _hand;
    private CardController _currentSelectedCard;
    private BoardController _boardController;


    private void Start() {
        _hand = new Hand(_gameRules, _uiRules, _cardPrefab, _handTransform);
        _boardController = new BoardController(_boardView, null);
        foreach (var card in _hand.Cards) {
            card.View.PointerDownEvent += (object sender, EventArgs args) => { OnCardSelected(card); };
            card.View.PointerUpEvent += (object sender, EventArgs args) => { OnCardDeselected(card); };
        }
    }

    private void OnCardSelected(CardController card) {
        if (!_hand.Cards.Contains(card)) {
            return;
        }
        card.View.ShineState(true);
        _currentSelectedCard = card;
    }

    private void OnCardDeselected(CardController card) {
        if (_currentSelectedCard == null) {
            return;
        }
        card.View.ShineState(false);
        _currentSelectedCard = null;
        if (IsCardInBoard(card.View, _boardView)) {
            _hand.RemoveCard(card);
            _boardController.AddCard(card);
        }
        else {
            _hand.SnapCardBack(card);
        }
    }

    private bool IsCardInBoard(Card card, BoardView board) {
        return board.IsMouseOverUI();
    }

    private void Update() {
        if (_currentSelectedCard != null) {
            _currentSelectedCard.View.transform.position = Input.mousePosition;
        }
    }

    public void ButtonAction() {
        if (_hand.Count == 0) {
            return;
        }
        var card = _hand.Select(_currentSelectedCardIndex);
        var property = SelectRandomProperty(card);
        var newValue = Random.Range(_gameRules.RandomValueMin, _gameRules.RandomValueMax + 1);
        card.SetProperyValue(property, newValue);
        _currentSelectedCardIndex++;
        _currentSelectedCardIndex %= _hand.Count;
    }

    private Type SelectRandomProperty(CardController card) {
        int random = UnityEngine.Random.Range(0, 3);
        Type propertyType = random switch
        {
            0 => typeof(HealthProperty),
            1 => typeof(AttackProperty),
            2 => typeof(ManaProperty),
            _ => throw new NotImplementedException(),
        };
        return propertyType;
    }
}
