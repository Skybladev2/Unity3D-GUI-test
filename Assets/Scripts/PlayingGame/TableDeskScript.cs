using UnityEngine;
using System.Collections;

public class TableDeskScript : MonoBehaviour {
	private bool drag = false;
	private Vector3 initialTouchPosition;
	private Vector3 delta;
	private Touch touch0;

	void Awake() {
#if UNITY_EDITOR
		Debug.Log("Unity Editor");
#endif
		
#if UNITY_IPHONE
		Debug.Log("Iphone");
#endif

#if UNITY_STANDALONE_OSX
		Debug.Log("Stand Alone OSX");
#endif
		
#if UNITY_STANDALONE_WIN
		Debug.Log("Stand Alone Windows");
#endif	

#if UNITY_ANDROID
		Debug.Log("Android");
#endif 
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}	

}
