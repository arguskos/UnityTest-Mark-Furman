using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shine : MonoBehaviour
{
    [SerializeField] private Image _image;
    public float ShineSpeed = 1;
    public float ShineDelay = 0.5f;
    private void Awake() {
        _image.enabled = false;
    }

    public void StartShine(float height) {
        _image.enabled = true;
        gameObject.SetActive(true);
        StartCoroutine(_StartShine(height));

    }

    public void StopShine() {
        _image.enabled = false;
        StopAllCoroutines();
    }

    private IEnumerator _StartShine(float height) {
        var startPos = new Vector3(0, -height / 2 - 20);
        var pos = startPos;
        transform.localPosition = pos;
        while(true) {
            pos += Vector3.up * ShineSpeed;
            transform.localPosition = pos;
            if (transform.localPosition.y > height / 2) {
                _image.enabled = false;
                yield return new WaitForSeconds(ShineDelay);
                _image.enabled = true;
                pos = startPos;
                transform.localPosition = startPos;

            }
            yield return true;
        }
    }
}
