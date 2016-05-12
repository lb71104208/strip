using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track : MonoBehaviour {

	public SpriteRenderer end;
	// Use this for initialization
	private Game _game;
	private List<MyJoint> _joints = new List<MyJoint>();
	private bool _isDest; 

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

	public void SetIsDest(bool dest)
	{
		_isDest = dest;
		if (dest) {
			end.color = Color.green;
		} else {
			end.color = Color.white;
		}
	}

	public bool GetIsDest()
	{
		return _isDest;
	}

	void createJoint(Vector2 pos) //pos is WorldPosition
	{

		GameObject prefab = Resources.Load("joint") as GameObject;
		GameObject joint = GameObject.Instantiate(prefab) as GameObject;
		joint.transform.parent = transform;

		Vector3 localPos = transform.InverseTransformPoint (pos);
		joint.transform.localPosition = new Vector3 (0, localPos.y);


		_game.CreateJoint (pos, joint.GetComponent<MyJoint>());
		_joints.Add (joint.GetComponent<MyJoint>());

		joint.GetComponent<MyJoint> ().CurOnTrack = this;
	}

	public List<MyJoint> getJoints()
	{
		return _joints;
	}
}
