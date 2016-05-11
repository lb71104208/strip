using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	
	public Ball ball;

	bool isContructingLink = false;
	List<Vector3> jointPos = new List<Vector3>();

	// Use this for initialization
	void Start () {
		GameObject prefab = Resources.Load("track") as GameObject;
		GameObject track = GameObject.Instantiate(prefab) as GameObject;
		track.transform.parent = transform;

		ball.SetCurOnTrack (track.GetComponent<Track>());

		prefab = Resources.Load("track") as GameObject;
		track = GameObject.Instantiate(prefab) as GameObject;
		track.transform.parent = transform;
		track.transform.localPosition = new Vector3 (0, 0);

	}
	
	// Update is called once per frame
	void Update () {
	

	}

	public void CreateJoint(Vector3 pos)
	{
		jointPos.Add (pos);
		if (isContructingLink) {
			CreateLink();
			jointPos.Clear();
		}
		isContructingLink = !isContructingLink;

	}

	void CreateLink()
	{
		GameObject link = new GameObject ();
		link.AddComponent<MeshFilter>();
		link.AddComponent<MeshRenderer>();
		Mesh mesh = link.GetComponent<MeshFilter>().mesh;
		mesh.Clear();

		Vector3 pos0 = jointPos [0];
		Vector3 pos1 = jointPos [1];

		if (pos1.y > pos0.y) {
			mesh.vertices = new Vector3[] { new Vector3(pos0.x+1, pos0.y-1, 0), 
				new Vector3(pos0.x-1, pos0.y+1, 0), 
				new Vector3(pos1.x-1, pos1.y+1, 0),
				new Vector3(pos1.x+1, pos1.y-1, 0)};

		}

		else if(pos1.y < pos0.y) {
			mesh.vertices = new Vector3[] { new Vector3(pos0.x-0.1f, pos0.y-0.1f, 0), 
				new Vector3(pos0.x+0.1f, pos0.y+0.1f, 0), 
				new Vector3(pos1.x+0.1f, pos1.y+0.1f, 0),
				new Vector3(pos1.x-0.1f, pos1.y-0.1f, 0)};
			
		}


		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0)};
		mesh.triangles = new int[] {0, 1, 2,2,3,0};
	}
	
}
