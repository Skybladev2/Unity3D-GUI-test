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
#if UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			Debug.Log("Touch");
			touch0 = Input.GetTouch(0);

			switch (touch0.phase) 
			{
				case TouchPhase.Began:
					OnMouseDown(touch0.position);
					break;

			case TouchPhase.Canceled:
			case TouchPhase.Ended:
				OnMouseUp();
				break;

			case TouchPhase.Moved:
				OnMouseDrag(touch0.position);
				break;

			default:
						break;
			}
		}
#endif
	}	

#if UNITY_ANDROID
	void OnMouseDown(Vector2 position)
	{
		initialTouchPosition = Camera.main.ScreenToWorldPoint(position);
		delta = this.transform.position - initialTouchPosition;
		drag = true;
	}

	void OnMouseDrag(Vector2 mousePos)
	{		
		this.transform.position = delta + Camera.main.ScreenToWorldPoint(mousePos);
	}
#endif

	void OnMouseUp()
	{
		drag = false;
	}

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
	void OnMouseDown()
	{
		initialTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		delta = this.transform.position - initialTouchPosition;
		drag = true;
	}

	void OnMouseDrag()
	{		
		Vector3 mousePos = Input.mousePosition;		
		this.transform.position = delta + Camera.main.ScreenToWorldPoint(mousePos);
	}
#endif

}
