using UnityEngine;

[CreateAssetMenu(fileName = "UIRules", menuName = "UIRules")]
public class UIRules : ScriptableObject {
    public float ValuesAnimationTime = 0.5f;
    public float ValuesScale = 1.5f;

    public float Radius = 2;
    public AnimationCurve Curve;
}
