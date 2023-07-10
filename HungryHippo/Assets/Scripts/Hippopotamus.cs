using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hippopotamus : MonoBehaviour
{
    // Передвижение бегемота по клавишам A D.
    [SerializeField] private float speed;

    //[SerializeField] private Rigidbody2D _rigidbody;

    private float moveInput;
    private Vector2 direction;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
         direction.x = Input.GetAxisRaw("Horizontal");
    }

    public void OnLeftClick()
    {
        direction.x = -1;
    }

    public void OnRightClick()
    {
        direction.x = 1;
    }

    private void FixedUpdate()
    {
        //moveInput = Input.GetAxis("Horizontal");
        /*
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            Debug.Log("A");
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            Debug.Log("D");
        } */
        
        // Способ движения менее резкий
        
        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
