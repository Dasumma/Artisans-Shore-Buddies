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
	public GameObject weightBar;
    public float playerSpeed;
	public float speedDebuff;
	
	private float actualSpeed;
    private Rigidbody2D rb;
    Vector3 mousePosition;
    Vector2 position = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
		if(photonView.IsMine)
		{
			playerScore = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
			weightBar = GameObject.Find("Bar");
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
            position = Vector2.Lerp(transform.position, mousePosition, actualSpeed); 
			var dir = mousePosition - transform.position;
			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			
			transform.rotation *= Quaternion.Euler(0f, 0f, 90f);
			if(position.x > -2080 && position.x < 2567)
				Camera.main.transform.position = new Vector3(position.x, Camera.main.transform.position.y, -10);
			if(position.y > -70 && position.y < 346)
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
				GetComponent<AudioSource>().Play(0);
				curWeight += itemStats.weight;
				print("Item Colected: " + itemStats.name);
				weightBar.GetComponent<Image>().fillAmount = 0;
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
        }
		else if (collision.CompareTag("Water"))
		{
			actualSpeed = playerSpeed;
		}
		weightBar.GetComponent<Image>().fillAmount = (float)curWeight/(float)weightLimit;
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Beach" && tag != "OceanOnly" && photonView.IsMine)
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			actualSpeed = playerSpeed * speedDebuff;
		}
		if (tag == "NoDebuffs" && photonView.IsMine)
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}
}