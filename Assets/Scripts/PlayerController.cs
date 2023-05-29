using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnDeath();

    public event OnDeath onDeath;

    private CharacterController _characterController;
    private PlayerSettings _settings;
    private Joystick _joystick_Movement;
    private Joystick _joystick_Rotation;

    private int _health = 0;

    private void OnEnable()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void SetupSettings(PlayerSettings settings)
    {
        _settings = settings;
        _health = settings.maxHealth;
    }

    public void SetJoysticks(Joystick joystick_Movement, Joystick joystick_Rotation)
    {
        _joystick_Movement = joystick_Movement;
        _joystick_Rotation = joystick_Rotation;
    }

    private void Update()
    {
        if (_joystick_Movement != null && _joystick_Movement.Direction != Vector2.zero)
        {
            Vector3 moveDir = new Vector3(_joystick_Movement.Direction.x, 0, _joystick_Movement.Direction.y);

            _characterController.Move(transform.TransformDirection(moveDir) * _settings.movementSpeed * Time.deltaTime);
        }

        if (_joystick_Rotation != null && _joystick_Rotation.Direction != Vector2.zero)
        {
            Vector3 rotateDir = new Vector3(0, _joystick_Rotation.Direction.x, 0);

            transform.Rotate(rotateDir * _settings.rotationSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Destroy(gameObject);
            onDeath.Invoke();
        }
    }
}
