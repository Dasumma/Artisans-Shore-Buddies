using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class PlayerMovement : MonoBehaviour

{
    private struct itemInfo
    {
        public string name;
        public int value;
        public int weight;
    }

    private List<itemInfo> itemList;
    public int weightLimit;
    private int score;
    private int curWeight;
    public MovementJoystick movementJoystick;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI playerWeight;
    public float playerSpeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        UpdateWeightText(0);
        itemList = new List<itemInfo>();
        rb = GetComponent<Rigidbody2D>();
    }
	
	void Awake()
	{
		/*movementJoystick = GameObject.Find("MovementJoystick").GetComponent<MovementJoystick>();
		playerScore = GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
		playerWeight = GameObject.Find("PlayerWeight").GetComponent<TextMeshProUGUI>();*/
	}
 

    // Update is called once per frame
    void FixedUpdate()
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

    //Item Collection and Other 2D collision triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable") )
        {
            GameObject item = collision.gameObject;
            itemInfo itemStats = new itemInfo();
            itemStats.name = item.GetComponent<collectableScript>().itemType;
            itemStats.value = item.GetComponent<collectableScript>().value;
            itemStats.weight = item.GetComponent<collectableScript>().weight;
            if(itemStats.weight + curWeight <= weightLimit)
            {
                curWeight += itemStats.weight;
                print("Item Collected: " + itemStats.name);
                UpdateWeightText(curWeight);
                itemList.Add(itemStats);
                Destroy(item);
            }
            else
            {
                print(itemStats.name + " is too heavy!: " + itemStats.weight.ToString() + "lb");
            }
        }   else if (collision.CompareTag("RecyclingBin"))
        {
            print("Depositing Current Trash");
            for(int i = itemList.Count - 1; i >= 0; i--)
            {
                score += itemList[i].value;
                itemList.RemoveAt(i);
                playerScore.text = "Score: " + score.ToString();
            }
            curWeight = 0;
            UpdateWeightText(0);
        }
    }

    private void UpdateWeightText(int curWeight)
    {
        playerWeight.text = "Weight: " + curWeight.ToString() + "/" + weightLimit.ToString();
    }
}