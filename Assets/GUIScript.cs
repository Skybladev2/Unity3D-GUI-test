using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    public GUIStyle guiStyle;
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
        if (GUILayout.Button("QUIT", guiStyle))
        {
            //Application.Quit();
        }
    }
}
