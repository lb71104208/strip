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
		if (_game.GetFirstJoint () == null || _game.GetFirstJoint ().CurOnTrack != this) {
			GameObject prefab = Resources.Load("joint") as GameObject;
			GameObject joint = GameObject.Instantiate(prefab) as GameObject;
			joint.transform.parent = transform;
			
			Vector3 localPos = transform.InverseTransformPoint (pos);
			joint.transform.localPosition = new Vector3 (0, localPos.y);
			
			joint.GetComponent<MyJoint> ().CurOnTrack = this;
			
			_game.CreateJoint (pos, joint.GetComponent<MyJoint>());
			_joints.Add (joint.GetComponent<MyJoint>());
		}
		else
		{
			Debug.Log("cannot create link on same track");
		}
	

	}

	public List<MyJoint> getJoints()
	{
		return _joints;
	}

	public void Reset()
	{
		foreach (MyJoint joint in _joints) {
			Destroy(joint.gameObject);
		}

		_joints.Clear ();
	}
}
