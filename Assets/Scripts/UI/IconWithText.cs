using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class IconWithText : MonoBehaviour {
    public event EventHandler AnimationEndEvenet;

    [SerializeField]
    private UIRules _uiRules;

    [SerializeField]
    private Text _text;

    private int _value;
    private Coroutine _coroutine;
    private Vector3 _initialScale;

    public int Value {
        get => _value;
        set {
            ResetCoroutine();
            _coroutine = StartCoroutine(ValueIncrement(value));
            _value = value;
        }
    }

    private void Awake() {
        _initialScale = transform.localScale;
    }

    private void ResetCoroutine() {
        transform.localScale = _initialScale;
        if (_coroutine != null) {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator ValueIncrement(int target) {
        int current = Value;
        float time = 0;
        float changeTime = _uiRules.ValuesAnimationTime;
        var maxScale = _initialScale * _uiRules.ValuesScale;
        while (time <= changeTime) {
            time += Time.deltaTime;
            float percentage = (time / changeTime);
            int newValue = current + (int)((target - current) * percentage);
            transform.localScale = Vector3.Lerp(_initialScale, maxScale, Mathf.Sin(percentage * Mathf.PI));
            _text.text = newValue.ToString();
            yield return null;
        }
        transform.localScale = _initialScale;
        AnimationEndEvenet?.Invoke(this, EventArgs.Empty);
    }

}
