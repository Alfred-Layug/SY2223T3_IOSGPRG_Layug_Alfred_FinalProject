using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingJoystick : MonoBehaviour
{
    public Joystick rotationJoystick;
    public Vector2 move;

    private void FixedUpdate()
    {
        if (rotationJoystick.Horizontal != 0 || rotationJoystick.Vertical != 0)
        {
            move.x = rotationJoystick.Horizontal;
            move.y = rotationJoystick.Vertical;

            float xRotation = move.x;
            float yRotation = move.y;
            float zRotation = Mathf.Atan2(xRotation, yRotation) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, -zRotation);
        }
    }
}
