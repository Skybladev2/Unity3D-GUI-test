using UnityEngine;
using System.Collections;

public class btnHostGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUpAsButton()
	{
		Application.LoadLevel("PlayingGame");
	}
}
