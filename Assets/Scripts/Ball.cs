using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Track _curOnTrack;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		Debug.Log ("collision");
		this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
	}
	

}
