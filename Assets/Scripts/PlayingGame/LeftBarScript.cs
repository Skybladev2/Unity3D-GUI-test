using UnityEngine;
using System.Collections;

public class LeftBarScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Texture2D leftBarTexture = new Texture2D (50, Screen.height);
		DrawBorders (leftBarTexture);
		Rect newInset = new Rect(0,0, 50, Screen.height);
		guiTexture.pixelInset = newInset;
		guiTexture.texture = leftBarTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void DrawBorders (Texture2D leftBarTexture)
	{
		Color borderColor = new Color32(255,0, 0, 255);
		
		for (int x = 0; x < leftBarTexture.width; x++)
		{
			leftBarTexture.SetPixel(x, 0, borderColor);
			leftBarTexture.SetPixel(x, 1, borderColor);
			leftBarTexture.SetPixel(x, 2, borderColor);
			
			leftBarTexture.SetPixel(x, leftBarTexture.height - 1, borderColor);
			leftBarTexture.SetPixel(x, leftBarTexture.height - 2, borderColor);
			leftBarTexture.SetPixel(x, leftBarTexture.height - 3, borderColor);
		}
		
		for (int y = 3; y < leftBarTexture.height - 3; y++)
		{
			leftBarTexture.SetPixel(0, y, borderColor);
			leftBarTexture.SetPixel(1, y, borderColor);
			leftBarTexture.SetPixel(2, y, borderColor);
			
			leftBarTexture.SetPixel(leftBarTexture.width - 1, y, borderColor);
			leftBarTexture.SetPixel(leftBarTexture.width - 2, y, borderColor);
			leftBarTexture.SetPixel(leftBarTexture.width - 3, y, borderColor);
		}

		leftBarTexture.Apply();
	}

}
