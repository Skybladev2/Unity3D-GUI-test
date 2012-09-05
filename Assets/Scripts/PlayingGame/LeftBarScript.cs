using UnityEngine;
using System.Collections;

public class LeftBarScript : MonoBehaviour {
	
	private Texture2D leftBarTexture = null;
	
	// Use this for initialization
	void Start () {
		leftBarTexture = new Texture2D (50, Screen.height);
		DrawBorders (leftBarTexture);
		Rect newInset = new Rect(0,0, 50, Screen.height);
		guiTexture.pixelInset = newInset;
		//guiTexture.pixelInset.width = 50;
		//guiTexture.pixelInset.height = Screen.height;
		guiTexture.texture = leftBarTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void DrawBorders (Texture2D leftBarTexture)
	{
		Color borderColor = new Color32(255,255, 255, 255);
		
		for (int x = 0; x < leftBarTexture.width - 1; x++)
		{
			leftBarTexture.SetPixel(x, 0, borderColor);
			leftBarTexture.SetPixel(x, 1, borderColor);
			leftBarTexture.SetPixel(x, 2, borderColor);
			
			leftBarTexture.SetPixel(x, leftBarTexture.height - 1, borderColor);
			leftBarTexture.SetPixel(x, leftBarTexture.height - 2, borderColor);
			leftBarTexture.SetPixel(x, leftBarTexture.height - 3, borderColor);
		}
		
		for (int y = 0; y < leftBarTexture.height - 1; y++)
		{
			leftBarTexture.SetPixel(0, y, borderColor);
			leftBarTexture.SetPixel(1, y, borderColor);
			leftBarTexture.SetPixel(2, y, borderColor);
			
			leftBarTexture.SetPixel(leftBarTexture.width - 1, y, borderColor);
			leftBarTexture.SetPixel(leftBarTexture.width - 2, y, borderColor);
			leftBarTexture.SetPixel(leftBarTexture.width - 3, y, borderColor);
		}
		
		//leftBarTexture.SetPixel(1,1,Color.white);
		leftBarTexture.Apply();
	}

}
