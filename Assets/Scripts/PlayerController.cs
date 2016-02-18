using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	public GUIText countText;
	public GUIText winText;
	public GUIText timeText;
	public GameObject ground;
	private int count;
	private int numberOfGameObjects;

	void Start()
	{
		count = 0;
		SetCountText();
		winText.text = "";
		numberOfGameObjects = GameObject.FindGameObjectsWithTag("PickUp").Length;
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		float moveNormal = 0.0f;
		if (Input.GetKeyDown (KeyCode.Space))
		{
			//winText.text = "JUMP";
			moveNormal = 10.0f;
		}
		
		Vector3 movement = new Vector3(moveHorizontal, moveNormal, moveVertical);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
		timeText.text = "TIME: " + Time.time;
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "PickUp")
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
			if(count >= numberOfGameObjects)
			{
				ground.SetActive (false);
			}
		}
	}		

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Wall") {
			Color c = other.gameObject.GetComponent<Renderer> ().material.color;

			if (c.Equals(Color.white)) {
				c = Color.red;
			} else if (c.Equals(Color.red)) {				
				c = Color.green;
			} else if (c.Equals(Color.green)) {				
				c = Color.black;
			} else if (c.Equals(Color.black)) {		
				c = Color.white;
			}

			other.gameObject.GetComponent<Renderer> ().material.color = c;
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString();
		if(count >= numberOfGameObjects)
		{
			winText.text = "YOU WIN!";
		}
	}
}
