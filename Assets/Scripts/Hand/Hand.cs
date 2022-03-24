
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Helpers;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Hand {
    [SerializeField]
    private Transform _handTransform;

    public int Count => _cards.Count;

    private List<CardController> _cards = new List<CardController>();
    public ReadOnlyCollection<CardController> Cards => _cards.AsReadOnly();
    private Vector2 _cardDimensions = new Vector2(100, 180);
    private CardFactory _cardFactory;
    private float _radius = 2;
    public AnimationCurve _curve;
    [SerializeField] private Card _cardPrefab;

    public Hand(GameRules rules, UIRules uiRules, Card cardPrefab, Transform handTransform) {
        _cardPrefab = cardPrefab;
        _cardFactory = new CardFactory(_cardPrefab, rules);
        var numCards = Random.Range(rules.StartCardMin, rules.StartCardMax + 1);
        _radius = uiRules.Radius;
        _handTransform = handTransform;
        _curve = uiRules.Curve;
        CreateHand(numCards);
    }

    public void AddCard(int index, CardController card) {
        _cards.Insert(index, card);
    }

    public void RemoveCard(CardController card) {
        _cards.Remove(card);
        for (int i = 0; i < Count; i++) {
            var card1 = _cards[i].View;
            MoveCardToPosition(card1, i, Count);
        }
    }

    public CardController Select(int index) {
        //We start in reverse
        index = Count - 1 - index;
        if (index < 0 || index >= Count) {
            index = 0;
        }
        return _cards[index];
    }

    public void CreateHand(int numberOfCards) {
        List<Vector3> positions = new List<Vector3>();
        var halfWidth = _cardDimensions.x * (numberOfCards - 1) / 2.0f;
        var endPosition = new Vector3(halfWidth, 0, 0);
        positions.Add(endPosition);
        for (int i = 0; i < numberOfCards; i++) {
            var cardController = _cardFactory.CreateCard();
            cardController.DeadEvent += OnCardDead;
            AddCard(i, cardController);
            var card = cardController.View;
            card.transform.parent = _handTransform;
            card.transform.localPosition = new Vector3((Screen.width + _cardDimensions.x) / 2, 0, 0);
            var (position, rotation) = GetCardPositionForIndex(i, numberOfCards);

            Sequence mySequence = DOTween.Sequence();
            if (positions.Count > 0) {
                mySequence
                .Append(card.transform.DOLocalPath(positions.ToArray(), 1f, PathType.Linear))
                .SetEase(Ease.OutExpo, 1);
            }
            mySequence.Append(card.transform.DOLocalMove(position, 0.5f).SetEase(_curve));
            mySequence.Insert(0, card.transform.DORotateQuaternion(rotation, 1.5f));
            positions.Add(position);
        }
    }

    public void SnapCardBack(CardController card) {
        int index = _cards.IndexOf(card);
        var (position, rotation) = GetCardPositionForIndex(index, _cards.Count);
        card.View.transform.DOLocalMove(position, 0.2f);
    }

    private void OnCardDead(object sender, DeadArgs args) {
        var cc = args.CardController;
        RemoveCard(cc);
        GameObject.Destroy(cc.View.gameObject);
    }

    private void MoveCardToPosition(Card card, int index, int numberOfCards) {
        if (numberOfCards == 1) {
            card.transform.DOLocalMove(Vector3.zero, 0.5f);
            card.transform.DORotateQuaternion(Quaternion.identity, 0.5f);
        }
        else {
            var (position, rotation) = GetCardPositionForIndex(index, numberOfCards);
            card.transform.DOLocalMove(position, 0.5f);
            card.transform.DOLocalRotateQuaternion(rotation, 0.5f);
        }
    }

    private (Vector3, Quaternion) GetCardPositionForIndex(int index, int numberOfCards) {
        if (numberOfCards == 1) {
            return (Vector3.zero, Quaternion.identity);
        }
        var halfWidth = _cardDimensions.x * (numberOfCards - 1) / 2.0f;
        var startPosition = new Vector3(-halfWidth, 0, 0);
        var endPosition = new Vector3(halfWidth, 0, 0);
        var middle = startPosition + (endPosition - startPosition) / 2 + Vector3.up * _radius;
        float t = (float)index / (float)(numberOfCards - 1);
        var position = Bezier.QuadraticBezier(endPosition, middle, startPosition, t);

        float step = 2.5f;
        index++;
        Quaternion rotation = Quaternion.identity;
        if (numberOfCards % 2 == 0 && (index == numberOfCards / 2)) {
            rotation = Quaternion.Euler(0, 0, -step);
        }
        else if (numberOfCards % 2 == 0 && (index - 1 == numberOfCards / 2)) {
            rotation = Quaternion.Euler(0, 0, step);
        }
        else {
            rotation = Quaternion.Euler(0, 0, -step * (numberOfCards + 1) / 2.0f + step * index);
        }
        return (position, rotation);
    }

}