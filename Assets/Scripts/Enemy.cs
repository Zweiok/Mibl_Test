using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    private float _speed;
    private int _damage;
    private Transform _target;

    public void SetupData(int damage, float speed, Transform targetToFollow)
    {
        _damage = damage;
        _target = targetToFollow;
        _speed = speed;
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget() // better way to use navMesh to follow target if some obstacles on the scene
    {
        Vector3 targetPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        controller.Move((targetPos - transform.position).normalized * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController;
        if (other.transform.TryGetComponent(out playerController))
        {
            playerController.TakeDamage(_damage);
        }
    }
}
