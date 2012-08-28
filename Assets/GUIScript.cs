using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    public GUIStyle guiStyle;
	private float guiButtonsColumnWidth = 0.6f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnGUI()
    {
		GUILayout.BeginArea (new Rect ((Screen.width - Screen.width * guiButtonsColumnWidth) / 2, 0,
		                               Screen.width * guiButtonsColumnWidth, Screen.height));
		GUILayout.BeginVertical();

		if (GUILayout.Button("START", guiStyle))
        {
            //Application.Quit();
        }

        if (GUILayout.Button("QUIT", guiStyle))
        {
            Application.Quit();
        }

		GUILayout.EndVertical();
		GUILayout.EndArea();
    }
}
