using UnityEngine;
using System.Collections;

public class LeftBarScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Texture2D leftBarTexture = new Texture2D (50, Screen.height);
		GUIScript. DrawBorders (leftBarTexture, true);
		Rect newInset = new Rect(0,0, 50, Screen.height);
		guiTexture.pixelInset = newInset;
		guiTexture.texture = leftBarTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	


}
