using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
	public GUIStyle guiStyle;
	private float guiButtonsColumnWidthFraction = 0.6f;
	private float guiButtonsColumnLeft = 0;
	private float guiButtonsHeight = 0;
	private int guiButtonsCount = 2;
	private float guiButtonsMargins = 0.1f;
	private float verticalSpaceBetweenButtons = 0;
	// copy of default GUIStyle
	GUIStyle style = null;
	
	// Use this for initialization
	void Start ()
	{
		this.guiButtonsColumnLeft = (Screen.width - Screen.width * guiButtonsColumnWidthFraction) / 2;
		this.guiButtonsHeight = (1 - guiButtonsMargins) / guiButtonsCount * Screen.height;
		this.verticalSpaceBetweenButtons = Screen.height * guiButtonsMargins / (guiButtonsCount + 1);
		//this.style = new GUIStyle (GUI.skin.button);
		//style.fontSize = 24;
		
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (this.guiButtonsColumnLeft, 0,
		                               Screen.width * guiButtonsColumnWidthFraction, Screen.height));
		GUILayout.BeginVertical ();

		GUILayout.Space (this.verticalSpaceBetweenButtons);

		if (GUILayout.Button ("START", guiStyle, GUILayout.Height (this.guiButtonsHeight))) {
			//Application.Quit();
		}

		GUILayout.FlexibleSpace ();

		if (GUILayout.Button ("QUIT", guiStyle, GUILayout.Height (this.guiButtonsHeight))) {
			Application.Quit ();
		}

		GUILayout.Space (this.verticalSpaceBetweenButtons);

		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}
}
