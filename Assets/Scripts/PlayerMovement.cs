using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public VariableJoystick _movementJoystick;
    public GameObject _playerCharacter;
    private Player _playerScript;

    private void Start()
    {
        _playerScript = _playerCharacter.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.up * _movementJoystick.Vertical + Vector2.right * _movementJoystick.Horizontal;
        transform.Translate(direction * _playerScript.GetSpeed());
    }
}
