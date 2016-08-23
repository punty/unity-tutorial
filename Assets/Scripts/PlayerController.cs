using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rbody2d;
	public float speed;
	private int count;
	public Text countText;


	float AccelerometerUpdateInterval  = 1.0f / 60.0f;
	float LowPassKernelWidthInSeconds  = 1.0f;

	private float LowPassFilterFactor;// tweakable
	private Vector3 lowPassValue = Vector3.zero;


	Vector3 LowPassFilterAccelerometer() {
		Vector3 acc = Vector3.zero;
		acc.x = -Input.acceleration.y;
		acc.z = Input.acceleration.x;
		lowPassValue = Vector3.Lerp(lowPassValue, acc, LowPassFilterFactor);
		return lowPassValue;
	}

	// Use this for initialization
	void Start () {
		LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; 
		lowPassValue = Input.acceleration;
		rbody2d = GetComponent<Rigidbody2D> ();
		count = 0;
		setCountText ();
	}

	void setCountText() {
		countText.text = count.ToString();
	}

	// Update is called once per frame
	void Update () {
		/*float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rbody2d.AddForce (movement * speed);*/

		float moveHorizontal = Input.acceleration.x;
		float moveVertical = Input.acceleration.y;
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rbody2d.AddForce (movement * speed);


 		

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count += 1;
			setCountText ();
		}
	}
}
