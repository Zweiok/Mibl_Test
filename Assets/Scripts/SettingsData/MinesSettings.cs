using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public struct MinesSettings
{
    [ShowAssetPreview(64, 64)]
    public GameObject prefab;
    [Range(0, 5)]
    public int minSpawnDelay;
    [Range(1, 25)]
    public int maxSpawnDelay;
    public AnimationCurve spawnDelayCurve;
    [Range(.1f, 2f)]
    public float timeToDetonate;
    [Range(.1f, 15f)]
    public float detonateRange;
    [Range(0, 100)] 
    public int damage;
}