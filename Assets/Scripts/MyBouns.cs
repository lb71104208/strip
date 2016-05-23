using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyBouns : MonoBehaviour {

	public int Point;
	Game _game;

	List<Color> _scoreColor = new List<Color> {Color.white, Color.yellow};
	// Use this for initialization
	void Start () {
		_game = GameObject.Find ("GameObject").GetComponent<Game>();
	}


	// Update is called once per frame
	void Update () {
	
	}

	public void SetPoint(int point)
	{
		gameObject.GetComponent<SpriteRenderer> ().color = _scoreColor [point];

		Point = point+1;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log ("hit point " + Point);
		_game.GetScore (Point);
	}
}
