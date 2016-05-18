using UnityEngine;
using System.Collections;

public class MyJoint : MonoBehaviour {

	public Track CurOnTrack {get; set;}
	public MyJoint NextJoint {get; set;}

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Game Oject"; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
