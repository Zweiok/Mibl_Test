using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 RandomPointInBounds(this BoxCollider boxCollider)
    {
        Vector3 minPosition = boxCollider.bounds.min;
        Vector3 maxPosition = boxCollider.bounds.max;

        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        float randomZ = Random.Range(minPosition.z, maxPosition.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
