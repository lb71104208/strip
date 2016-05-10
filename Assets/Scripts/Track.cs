using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {

	public SpriteRenderer end;
	// Use this for initialization
	private Game _game;

	void Start () {
		_game = GameObject.Find ("GameObject").GetComponent<Game>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) )
		{

			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (this.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				//your code
				Debug.Log("touch track");
				this.createJoint(touchPos);
			}
		}
	}

	void createJoint(Vector2 pos)
	{

		Sprite joint = Resources.Load<Sprite>("Images/PowerUp");
		// create gameobject
		GameObject jointObj = new GameObject();
		jointObj.AddComponent<SpriteRenderer>();

		SpriteRenderer SR = jointObj.GetComponent<SpriteRenderer>();
		SR.sprite = joint;
		jointObj.transform.parent = transform;
		jointObj.transform.localPosition = transform.InverseTransformPoint (pos);

		_game.CreateJoint (pos);
	}
}
