using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;


public class PlayerMovement : MonoBehaviourPun

{
	private struct itemInfo
	{
		public string name;
		public int value;
		public int weight;
	}
    private List<itemInfo> itemList;
    public int score;
	public int curWeight;
	public int weightLimit;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI playerWeight;
    public float playerSpeed;
    private Rigidbody2D rb;
    Vector3 mousePosition;
    Vector2 position = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
		if(photonView.IsMine)
		{
			playerScore = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
			playerWeight = GameObject.Find("Weight").GetComponent<TextMeshProUGUI>();
		}
		itemList = new List<itemInfo>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            position = Vector2.Lerp(transform.position, mousePosition, playerSpeed);
			if(position.x > -2080 && position.x < 2567)
				Camera.main.transform.position = new Vector3(position.x, Camera.main.transform.position.y, -10);
			if(position.y > 175 && position.y < 593)
				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, position.y, -10);
			
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
			itemInfo itemStats = new itemInfo();
			
            itemStats.name = collision.gameObject.GetComponent<collectableScript>().itemType;
			itemStats.value = collision.gameObject.GetComponent<collectableScript>().itemValue;
			itemStats.weight = collision.gameObject.GetComponent<collectableScript>().itemWeight;
			
			if(itemStats.weight + curWeight <= weightLimit)
			{
				curWeight += itemStats.weight;
				print("Item Colected: " + itemStats.name);
				playerWeight.text = "Weight: " + curWeight.ToString() + "/" + weightLimit.ToString();
				itemList.Add(itemStats);
				collision.gameObject.GetComponent<collectableScript>().CallToDelete();
			} 
			else
			{
				print(itemStats.name + " is too heavy!: " + itemStats.weight.ToString() + "lb");
			}
		}
		else if (collision.CompareTag("RecyclingBin"))
		{
			print("Depositing Current Trash");
			for(int i = itemList.Count - 1; i >= 0; i--)
			{
				score += itemList[i].value;
				itemList.RemoveAt(i);
				playerScore.text = "Score: " + score.ToString();
			}
			curWeight = 0;
			playerWeight.text = "Weight: " + curWeight.ToString() + "/" + weightLimit.ToString();
        }
    }
}