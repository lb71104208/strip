using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyBouns : MonoBehaviour {

	public int Point;
	Game _game;

	List<Color> _scoreColor = new List<Color> {Color.black, Color.white, Color.green, Color.blue, Color.cyan, Color.yellow};
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
		if (point == 0)
			point = -1;

		Point = point;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log ("hit point " + Point);
		_game.GetScore (Point);
	}
}
