using UnityEngine;

[CreateAssetMenu(fileName = "GameRules", menuName = "GameRules")]
public class GameRules : ScriptableObject {
    public int StartCardMin = 4;
    public int StartCardMax = 6;
    public int StartCardValueMin = 1;
    public int StartCardValueMax = 9;

    public int RandomValueMin = -2;
    public int RandomValueMax = 9;
}
