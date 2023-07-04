using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJoystick : MonoBehaviour
{
    public VariableJoystick _movementJoystick;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.up * _movementJoystick.Vertical + Vector2.right * _movementJoystick.Horizontal;
        transform.Translate(direction * _speed);
    }
}
