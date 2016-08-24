using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rbody2d;
	public float speed;
	private int count;
	public Text countText;

	public void Awake() {
		Input.gyro.enabled = true;
	}
		
	// Use this for initialization
	public void Start () {
		rbody2d = GetComponent<Rigidbody2D> ();
		count = 0;
		setCountText ();
	}

	void setCountText() {
		countText.text = count.ToString();
	}

	// Update is called once per frame
	public void Update () {
		Vector3 rotation = Input.acceleration;
		float minTilt = 0.1f;
		if(Mathf.Abs(rotation.x) < minTilt) rotation.x = 0;
		if(Mathf.Abs(rotation.y) < minTilt) rotation.y = 0;
		if(Mathf.Abs(rotation.z) < minTilt) rotation.z = 0;
		Vector2 movement = new Vector2 (rotation.x, rotation.y);
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
