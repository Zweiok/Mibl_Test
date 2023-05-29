using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public struct EnemiesSettings
{
    [ShowAssetPreview(64, 64)]
    public GameObject prefab;
    [Header("Speed settings")]
    [Range(.1f, 15f)]
    public float minSpeed;
    [Range(.1f, 25f)]
    public float maxSpeed;
    public AnimationCurve speedCurve;
    [Header("Spawn settings")]
    [Range(.1f, 5f)]
    public float minSpawnDelay;
    [Range(.1f, 5f)]
    public float maxSpawnDelay;
    public AnimationCurve spawnDelayCurve;
    [Header("Damage settings")]
    [Range(0, 100)]
    public int damage;
}
