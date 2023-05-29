using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public struct GameProcessSettings 
{
    [InfoBox("Value in seconds", EInfoBoxType.Normal)]
    [Range(1, 3600)]
    public int timeToMaxDifficulty;
    public AnimationCurve difficultyCurve;
}
