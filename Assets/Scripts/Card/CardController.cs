using System;
using UnityEngine;
using System.Collections.Generic;

public class DeadArgs : EventArgs {
    public CardController CardController { get; set; }
}

public class CardController {
    public Card View { get; private set; }

    private CardInfo _model;
    public event EventHandler<DeadArgs> DeadEvent;

    public CardController(Card view, CardInfo cardInfo) {
        View = view;
        _model = cardInfo;
        foreach (var property in _model.GetProperties()) {
            View.SetPropertyValue(property.GetType(), property.Value);
        }
        View.ValuesUpdatedEvent += Update;
    }

    public void SetProperyValue(Type property, int value) {
        _model.SetProperyValue(property, value);
        View.SetPropertyValue(property, value);
    }

    private void Update(object sender, EventArgs args) {
        if (_model.Health.Value <= 0) {
            DeadEvent?.Invoke(this, new DeadArgs() { CardController = this });
        }
    }
}