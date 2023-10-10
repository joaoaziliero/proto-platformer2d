using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionControl : MonoBehaviour
{
    public Rigidbody2D character;

    [Header("Motion Settings")]
    [SerializeField] public float speed;
    [SerializeField] public float frictionScale;
    [SerializeField] public float jumpScale;

    private void Update()
    {

    }

    private void HandleLateralMotion()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            character.velocity = new Vector2(speed, character.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            character.velocity = new Vector2(-speed, character.velocity.y);
        }

        if (character.velocity.x != 0 && character.velocity.y == 0)
        {
            character.velocity -= character.velocity.normalized * frictionScale;
        }
    }

    private void HandleUpwardMotion()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.velocity = jumpScale * Vector2.up;
        }
    }
}
