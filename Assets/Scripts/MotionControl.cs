using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionControl : MonoBehaviour
{
    public Rigidbody2D mainCharacter;

    [Header("Motion Settings")]
    [SerializeField] public float normalSpeed;
    [SerializeField] public float runningSpeed;
    [SerializeField] public float frictionScale;
    [SerializeField] public float jumpMagnitude;


    [Header("Jump Count")]
    public int maximumJumps;

    private float _speedMagnitude;
    private int _jumpCount;
    private bool _notFloating;

    private void Start()
    {
        _speedMagnitude = normalSpeed;
        _jumpCount = 0;
        _notFloating = true;
    }

    private void Update()
    {
        HandleVerticalMotion();
        HandleHorizontalMotion();
        AccountForFriction();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(mainCharacter.velocity.y >= 0)
        {
            _jumpCount = 0;
        }

        _notFloating = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(mainCharacter.velocity.y < 0)
        {
            _jumpCount = maximumJumps;
        }

        _notFloating = false;
    }

    private void HandleHorizontalMotion()
    {
        if(_notFloating && _jumpCount < maximumJumps)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                _speedMagnitude = runningSpeed;
            }
            else
            {
                _speedMagnitude = normalSpeed;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                mainCharacter.velocity = new Vector2(_speedMagnitude, mainCharacter.velocity.y);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                mainCharacter.velocity = new Vector2(-_speedMagnitude, mainCharacter.velocity.y);
            }
        }
    }

    private void HandleVerticalMotion()
    {
        if(_jumpCount < maximumJumps)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mainCharacter.velocity = new Vector2(mainCharacter.velocity.x, jumpMagnitude);
                _jumpCount++;
            }
        }
    }

    private void AccountForFriction()
    {
        if(mainCharacter.velocity.x != 0 && mainCharacter.velocity.y == 0)
        {
            mainCharacter.velocity -= mainCharacter.velocity.normalized * frictionScale;
        }
    }
}
