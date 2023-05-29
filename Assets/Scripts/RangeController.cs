using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class RangeController : MonoBehaviour
{
    private int _damage;
    private float _radius;
    private float _growDuration;
    private LayerMask _mask;
    public void InitRange(int damage, float radius, float growDuration, LayerMask mask)
    {
        _damage = damage;
        _radius = radius;
        _growDuration = growDuration;
        _mask = mask;
        transform.DOScale(.2f * radius, growDuration);
        Activate();
    }

    private void Activate()
    {
        Invoke(nameof(DamagePlayerInRange), _growDuration);
    }

    private void DamagePlayerInRange()
    {
        Collider hitcollider = Physics.OverlapSphere(transform.position, _radius, _mask)?.FirstOrDefault();
        PlayerController player;
        if(hitcollider != null && hitcollider.TryGetComponent(out player))
        {
            player.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
