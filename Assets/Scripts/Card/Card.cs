using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public event EventHandler ValuesUpdatedEvent;
    public event EventHandler PointerDownEvent;
    public event EventHandler PointerUpEvent;

    [SerializeField] private IconWithText Attack;
    [SerializeField] private IconWithText Health;
    [SerializeField] private IconWithText Mana;
    [SerializeField] private RawImage _art;
    [SerializeField] private Text _description;
    [SerializeField] private Text _title;
    [SerializeField] private Shine _shine;

    private Dictionary<Type, IconWithText> _propertyToUI = new Dictionary<Type, IconWithText>();

    public Vector2 CardDimensions => ((RectTransform)transform).rect.size;
    public string Title { get => _title.text; set => _title.text = value; }
    public string Description { get => _description.text; set => _description.text = value; }

    private void Awake() {

        _propertyToUI = new Dictionary<Type, IconWithText>() {
            { typeof(AttackProperty), Attack },
            { typeof(HealthProperty), Health },
            { typeof(ManaProperty), Mana },
        };
        foreach (var propertyUI in _propertyToUI.Values) {
            propertyUI.AnimationEndEvenet += (object sender, EventArgs e) =>
            { ValuesUpdatedEvent?.Invoke(this, EventArgs.Empty); };
        }
    }

    public void SetPropertyValue(Type property, int value) {
        _propertyToUI[property].Value = value;
    }

    public void OnPointerDown(PointerEventData eventData) {
        PointerDownEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerUp(PointerEventData eventData) {
        PointerUpEvent?.Invoke(this, EventArgs.Empty);
    }

    public void ShineState(bool state) {
        if (state) {
            _shine.StartShine(160);
        }
        else {
            _shine.StopShine();
        }
    }

    public RawImage GetArt() {
        return _art;
    }
}
