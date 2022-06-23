using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;


public class MoveTest : MonoBehaviour

{
    
    Vector3 mousePosition;
    public float playerSpeed;
    private Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            position = Vector2.Lerp(transform.position, mousePosition, playerSpeed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
 
}