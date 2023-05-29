using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public struct PlayerSettings
{
    [ShowAssetPreview(64, 64)]
    public GameObject prefab;
    [Range(.1f, 10f)]
    public float movementSpeed;
    [Range(.1f, 180f)]
    public float rotationSpeed;
    [Range(1, 100)]
    public int maxHealth;
}
