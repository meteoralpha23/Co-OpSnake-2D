using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    float vertical,horizontal;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBat();  
    }

    private void MoveBat()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2((horizontal*moveSpeed), (vertical*moveSpeed));   
    }
}
