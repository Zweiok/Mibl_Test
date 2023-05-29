using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private const string DETONATE_TRIGGERNAME = "Detonate";
    private const string DETONATETIME_TRIGGERNAME = "DetonationTime";

    public delegate void OnMineDetonated(Mine mine);

    public event OnMineDetonated onMineDetonated;

    [SerializeField] private Animator animator;
    [SerializeField] private RangeController rangePrefab;
    [SerializeField] private LayerMask damageMask;

    private float _timeToDetonate;
    private int _damage;
    private float _range;

    public void SetupData(float timeToDetonate, int damage, float range)
    {
        _timeToDetonate = timeToDetonate;
        _damage = damage;
        _range = range;
        animator.SetFloat(DETONATETIME_TRIGGERNAME, 1 / timeToDetonate);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController;
        if (other.transform.TryGetComponent(out playerController))
        {
            RangeController range = Instantiate(rangePrefab);
            range.transform.position = transform.position;
            range.InitRange(_damage, _range, _timeToDetonate, damageMask);
            animator.SetTrigger(DETONATE_TRIGGERNAME);
        }
    }

    public void Detonate()
    {
        onMineDetonated.Invoke(this);
        Destroy(gameObject);
    }
}
