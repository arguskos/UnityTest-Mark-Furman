using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardView : MonoBehaviour {
    private RectTransform _rectTransform;

    private void Awake() {
        _rectTransform = (RectTransform)transform;
    }

    private Rect GetGlobalPosition() {
        Vector3[] corners = new Vector3[4];
        _rectTransform.GetWorldCorners(corners);
        return new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
    }

    public bool IsMouseOverUI() {
        Rect position = GetGlobalPosition();
        return position.Contains(Input.mousePosition);
    }

    public void PositionCardView(Card card, int index) {
        card.transform.parent = transform;
        Vector3[] corners = new Vector3[4];
        _rectTransform.GetWorldCorners(corners);
        var boardStart = -(corners[2].x - corners[0].x) / 4;
        var xShift = boardStart + card.CardDimensions.x * index;
        var position = new Vector3(xShift, 0, 0);
        card.transform.DOLocalMove(position, 0.5f);
        card.transform.DORotateQuaternion(Quaternion.identity, 0.5f);
    }
}

