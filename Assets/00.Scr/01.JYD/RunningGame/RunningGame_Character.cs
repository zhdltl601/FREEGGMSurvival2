using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGame_Character : MonoBehaviour
{
    [Header("Move info")] 
    public float moveSpeed;
    public float jumpForce;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        _rigidbody2D.velocity = new Vector2(x * moveSpeed , _rigidbody2D.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,jumpForce);
        }
        
    }
}
