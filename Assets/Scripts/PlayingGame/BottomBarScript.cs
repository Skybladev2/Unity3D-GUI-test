using UnityEngine;
using System.Collections;

public class BottomBarScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Texture2D leftBarTexture = new Texture2D (Screen.width - 50, 50);
		GUIScript.DrawBorders (leftBarTexture, false);
		Rect newInset = new Rect(50,0, Screen.width - 50, 50);
		guiTexture.pixelInset = newInset;
		guiTexture.texture = leftBarTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
