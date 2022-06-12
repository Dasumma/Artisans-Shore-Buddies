using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float speed = 3;  // units per second; consider making this a public property for easy tweaking
        float dy = v * speed * Time.deltaTime;
        float dx = h * speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + dx, transform.position.y + dy);
    }
}
