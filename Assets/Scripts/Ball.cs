using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	float JOINT_DIAMETER = 0.3f;
	Track _curOnTrack;
	MyJoint _targetJoint;
	float _moveSpeed  = 0.1f;
	float _moveSpeedX = 0;
	float _moveSpeedY = 0;

	enum BallState {Ready,FreeDrop, MoveToOtherTrack};

	BallState _curState;
	// Use this for initialization
	void Start () {
		_curState = BallState.Ready;
		GetComponent<Rigidbody2D> ().gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		float thisY = transform.localPosition.y;
		if (_curState == BallState.FreeDrop) {
			foreach (MyJoint joint in _curOnTrack.getJoints()) {
				float jointY = _curOnTrack.transform.TransformPoint (joint.transform.localPosition).y;
				
				if (Mathf.Abs (thisY - jointY) < JOINT_DIAMETER / 2) {

					Debug.Log ("hit joint");
					_curState = BallState.MoveToOtherTrack;

					_targetJoint = joint.NextJoint;
					_curOnTrack = _targetJoint.CurOnTrack;
					MoveToNextTrack ();

				}
				
			}
		} else if (_curState == BallState.MoveToOtherTrack) {
			//transform.localPosition += new Vector3(_moveSpeedX,_moveSpeedY);

		}

	}

	public void SetCurOnTrack(Track track)
	{
		_curOnTrack = track;
	}

	void MoveToNextTrack()
	{
		GetComponent<Rigidbody2D> ().gravityScale = 0;

		Vector3 targetWorldPosition = _curOnTrack.transform.TransformPoint (_targetJoint.transform.localPosition);

//		float angle = (targetWorldPosition.y - transform.localPosition.y) / (targetWorldPosition.x - transform.localPosition.x);
//		_moveSpeedX = Mathf.Sqrt (_moveSpeed / (1 + Mathf.Pow (angle, 2)));
//		_moveSpeedY = angle * _moveSpeedX;

		Debug.Log ("this " + transform.localPosition);
		Debug.Log ("target " + targetWorldPosition);

		float angle = Mathf.Atan ((targetWorldPosition.y - transform.localPosition.y) / (targetWorldPosition.x - transform.localPosition.x));
		_moveSpeedX = _moveSpeed * Mathf.Cos (angle);
		_moveSpeedY = _moveSpeed * Mathf.Sin (angle);

		Debug.Log ("move speed x y " + _moveSpeedX + " " + _moveSpeedY);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (_moveSpeedX, _moveSpeedY);
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		Debug.Log ("collision");
		this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
	}

	public void StartDrop()
	{
		_curState = BallState.FreeDrop;
		GetComponent<Rigidbody2D> ().gravityScale = 1f;
	}

}
