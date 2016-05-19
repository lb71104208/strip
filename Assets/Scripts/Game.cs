using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game : MonoBehaviour {
	
	public Ball ball;
	public int Score;
	public int LinksAvaiable;

	public Text TextScore;
	public Text TextLinksAvailable;
	public Text TextScoreRequired;

	int _scoreRequired = 10;

	MyJoint _firstJoint;

	bool isContructingLink = false;
	List<Vector3> jointPos = new List<Vector3>();

	enum GameState {Editing,Playing};

	List<Track> _tracks = new List<Track> ();
	List<GameObject> _links = new List<GameObject> ();

	GameState _gameState;

	// Use this for initialization
	void Start () {
		GameObject prefab = Resources.Load("track") as GameObject;
		GameObject track = GameObject.Instantiate(prefab) as GameObject;
		track.transform.parent = transform;
		track.transform.localPosition = new Vector3 (-1.0f, 0);
		track.GetComponent<Track> ().SetIsDest (false);
		_tracks.Add (track.GetComponent<Track> ());

		ball.SetCurOnTrack (track.GetComponent<Track>());

		prefab = Resources.Load("track") as GameObject;
		track = GameObject.Instantiate(prefab) as GameObject;
		track.transform.parent = transform;
		track.transform.localPosition = new Vector3 (1.0f, 0);
		track.GetComponent<Track> ().SetIsDest (true);
		_tracks.Add (track.GetComponent<Track> ());

		_gameState = GameState.Editing;

		CreateUI ();
	}

	void CreateUI()
	{
		TextScore.text = "Score " + Score.ToString();
		TextLinksAvailable.text = LinksAvaiable.ToString();
		TextScoreRequired.text = "Score Required  " + _scoreRequired;
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	public void CreateJoint(Vector3 pos, MyJoint joint)
	{
		jointPos.Add (pos);
		if (isContructingLink) {

				CreateLink ();
				jointPos.Clear ();
				_firstJoint.NextJoint = joint;
				joint.NextJoint = _firstJoint;
				_firstJoint = null;

			LinksAvaiable--;
			TextLinksAvailable.text =  LinksAvaiable.ToString();

		} else {
			_firstJoint = joint;
		}
		isContructingLink = !isContructingLink;

	}

	public void StartGame()
	{
		if (_gameState == GameState.Editing) {
			ball.StartDrop ();
			_gameState = GameState.Playing;
		}
	}

	public void ResetGame()
	{
		ball.Reset ();
		_gameState = GameState.Editing;
		ball.SetCurOnTrack (_tracks [0]);

		foreach (Track track in _tracks) {
			track.Reset();
		}

		foreach (GameObject link in _links) {
			Destroy(link);
		}
		_links.Clear ();
		Score = 0;
		LinksAvaiable = 1;

		CreateUI ();
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

		if (pos1.x > pos0.x) {
			if (pos1.y > pos0.y) {
				mesh.vertices = new Vector3[] { new Vector3 (pos0.x + 0.1f, pos0.y - 0.1f, 0), 
					new Vector3 (pos0.x - 0.1f, pos0.y + 0.1f, 0), 
					new Vector3 (pos1.x - 0.1f, pos1.y + 0.1f, 0),
					new Vector3 (pos1.x + 0.1f, pos1.y - 0.1f, 0)};
				
			} else if (pos1.y < pos0.y) {
				mesh.vertices = new Vector3[] { new Vector3 (pos0.x - 0.1f, pos0.y - 0.1f, 0), 
					new Vector3 (pos0.x + 0.1f, pos0.y + 0.1f, 0), 
					new Vector3 (pos1.x + 0.1f, pos1.y + 0.1f, 0),
					new Vector3 (pos1.x - 0.1f, pos1.y - 0.1f, 0)};
				
			}
		} else {

			if (pos1.y > pos0.y) {
				mesh.vertices = new Vector3[] {
					new Vector3 (pos1.x - 0.1f, pos1.y - 0.1f, 0),
					new Vector3 (pos1.x + 0.1f, pos1.y + 0.1f, 0),
					new Vector3 (pos0.x + 0.1f, pos0.y + 0.1f, 0), 
					new Vector3 (pos0.x - 0.1f, pos0.y - 0.1f, 0) 

					};
				
			} else if (pos1.y < pos0.y) {
				mesh.vertices = new Vector3[] { 
					new Vector3 (pos1.x + 0.1f, pos1.y - 0.1f, 0),
					new Vector3 (pos1.x - 0.1f, pos1.y + 0.1f, 0),
					new Vector3 (pos0.x - 0.1f, pos0.y + 0.1f, 0), 
					new Vector3 (pos0.x + 0.1f, pos0.y - 0.1f, 0) 
					};
				
			}
		}


		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0)};
		mesh.triangles = new int[] {0, 1, 2,2,3,0};

		link.GetComponent<MeshRenderer>().material = Resources.Load("Materials/link", typeof(Material)) as Material;

		PolygonCollider2D polygonCollider = link.AddComponent<PolygonCollider2D>();
		polygonCollider.points = new Vector2[] {
			new Vector2 (pos1.x - 0.1f, pos1.y - 0.1f),
			new Vector2 (pos1.x + 0.1f, pos1.y + 0.1f),
			new Vector2 (pos0.x + 0.1f, pos0.y + 0.1f), 
			new Vector2 (pos0.x - 0.1f, pos0.y - 0.1f) 
		};

		polygonCollider.SetPath(0,  polygonCollider.points);

		_links.Add (link);
	}

	public MyJoint GetFirstJoint()
	{
		return _firstJoint;
	}

	public void GetScore(int score)
	{
		Score += score;
		TextScore.text = "Score " + Score.ToString ();
	}

	public void CheckSuccess()
	{
		if (Score >= _scoreRequired) {
			Debug.Log ("Success !");
		} else {
			Debug.Log("Fail Not enough score!");
		}
	}
	
}
