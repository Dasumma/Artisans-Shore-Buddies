using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;


public class PlayerMovement : MonoBehaviourPun

{
    public List<string> items;
    public int score;
    public MovementJoystick movementJoystick;
    public TextMeshProUGUI playerScore;
    public float playerSpeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            
            if (movementJoystick.joystickVec.y != 0)
            {
                rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            string itemType = collision.gameObject.GetComponent<collectableScript>().itemType;
            print("Item Collected: " + itemType);

            //Each Objects Score Value
            switch (itemType)
            {
                case ("carton"):
                    score += 10;
                    break;
                case ("battery"):
                    score += 20;
                    break;
                case ("can"):
                    score += 10;
                    break;
                case ("ring"):
                    score += 5;
                    break;
                case ("flipflop"):
                    score += 10;
                    break;
            }
            playerScore.text = "Score: " + score.ToString();
            items.Add(itemType);
            Destroy(collision.gameObject);
        }
    }
}