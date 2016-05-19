using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	float JOINT_DIAMETER = 0.3f;

	Track _curOnTrack;
	MyJoint _targetJoint;
	MyJoint _curLandingJoint;

	float _moveSpeed  = 2f;
	float _moveSpeedX = 0;
	float _moveSpeedY = 0;
	Vector3 _originPosition;

	public Game TheGame;

	enum BallState {Ready,FreeDrop, MoveToOtherTrack};

	BallState _curState;
	// Use this for initialization
	void Start () {
		_curState = BallState.Ready;
		GetComponent<Rigidbody2D> ().gravityScale = 0;
		_originPosition = transform.localPosition;
	}

	public void Reset()
	{
		_curState = BallState.Ready;
		GetComponent<Rigidbody2D> ().gravityScale = 0;
		transform.localPosition = _originPosition;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		if (_curState == BallState.FreeDrop) {
		
				
			if (ReachAJoint()!= null && ReachAJoint()!= _curLandingJoint) {
				Debug.Log ("hit joint");
				MyJoint joint = ReachAJoint();
				_targetJoint = joint.NextJoint;

				if(_targetJoint != null && _targetJoint.transform.localPosition.y < joint.transform.localPosition.y)
				{
					transform.localPosition =  _curOnTrack.transform.TransformPoint (joint.transform.localPosition);
					_curState = BallState.MoveToOtherTrack;
					_curOnTrack = _targetJoint.CurOnTrack;
					MoveToNextTrack ();
				}

			}
		} else if (_curState == BallState.MoveToOtherTrack) {
			//transform.localPosition += new Vector3(_moveSpeedX,_moveSpeedY);
			if (ReachAJoint()!= null) {
				Debug.Log("reach another track");
				transform.localPosition =  _curOnTrack.transform.TransformPoint (ReachAJoint().transform.localPosition);
				_curLandingJoint = ReachAJoint();
				StartDrop();
			}

		}

	}

	MyJoint ReachAJoint()
	{
		Vector3 thisPos = transform.localPosition;
		foreach (MyJoint joint in _curOnTrack.getJoints()) {
			Vector3 targetPos = _curOnTrack.transform.TransformPoint (joint.transform.localPosition);
			float dis = Mathf.Sqrt(Mathf.Pow(thisPos.x-targetPos.x,2) + Mathf.Pow(thisPos.y-targetPos.y,2) ) ;
			
			if (dis < JOINT_DIAMETER / 2)
			{
				return joint;
			}
		}

		return null;
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

		if (targetWorldPosition.x < transform.localPosition.x) {
			_moveSpeedX = -_moveSpeed * Mathf.Cos (angle);
			_moveSpeedY = -_moveSpeed * Mathf.Sin (angle);
		}

		Debug.Log ("move speed x y " + _moveSpeedX + " " + _moveSpeedY);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (_moveSpeedX, _moveSpeedY);
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{

		GetComponent<Rigidbody2D> ().gravityScale = 0;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

		if (_curOnTrack.GetIsDest () == true) {
			TheGame.CheckSuccess();

		} else {
			Debug.Log("Fail Wrong End !");
		}
	}

	public void StartDrop()
	{
		_curState = BallState.FreeDrop;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D> ().gravityScale = 1f;
	}

}
