using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	public MyJoint JointH;
	public MyJoint JointL;

	private Game _game;
	// Use this for initialization
	void Start () {
		_game = GameObject.Find ("GameObject").GetComponent<Game>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) )
		{
			
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (this.GetComponent<PolygonCollider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				//your code
				_game.RemoveLink(this);
			}
		}
	}
}
