using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	public MyJoint JointH;
	public MyJoint JointL;

	// Use this for initialization
	void Start () {
	
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
				Debug.Log("click link");
			}
		}
	}
}
