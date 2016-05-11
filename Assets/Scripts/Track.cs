using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track : MonoBehaviour {

	public SpriteRenderer end;
	// Use this for initialization
	private Game _game;
	private List<MyJoint> _joints = new List<MyJoint>();
	//private List<GameObject> _joints = new List<GameObject>();

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

	void createJoint(Vector2 pos) //pos is WorldPosition
	{

//		Sprite joint = Resources.Load<Sprite>("Images/PowerUp");
//		// create gameobject
//		GameObject jointObj = new GameObject();
//		jointObj.AddComponent<SpriteRenderer>();
//
//		SpriteRenderer SR = jointObj.GetComponent<SpriteRenderer>();
//		SR.sprite = joint;
//		jointObj.transform.parent = transform;
//		jointObj.transform.localPosition = transform.InverseTransformPoint (pos);

		GameObject prefab = Resources.Load("joint") as GameObject;
		GameObject joint = GameObject.Instantiate(prefab) as GameObject;
		joint.transform.parent = transform;
		joint.transform.localPosition = transform.InverseTransformPoint (pos);


		_game.CreateJoint (pos, joint.GetComponent<MyJoint>());
		_joints.Add (joint.GetComponent<MyJoint>());

		joint.GetComponent<MyJoint> ().CurOnTrack = this;
	}

	public List<MyJoint> getJoints()
	{
		return _joints;
	}
}
