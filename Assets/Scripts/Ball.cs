using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Track _curOnTrack;
	enum BallState {FreeDrop, MoveToOtherTrack};

	BallState _curState;
	// Use this for initialization
	void Start () {
		_curState = BallState.FreeDrop;

	}
	
	// Update is called once per frame
	void Update () {
		float thisY = transform.localPosition.y;
		foreach (Vector3 jointPos in _curOnTrack.getJointPositions()) {
			if(Mathf.Abs(thisY-jointPos.y) < float.Epsilon )
			{

			}

		}
	}

	public void SetCurOnTrack(Track track)
	{
		_curOnTrack = track;
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		Debug.Log ("collision");
		this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
	}
	

}
