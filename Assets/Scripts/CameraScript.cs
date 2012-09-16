using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Vector2 initialTouchPosition;
	private Vector3 initialCameraPosition;
	private Vector3 delta;
	private Vector3 cameraScreenCoords;

	private Vector3 initialMousePos;

	private bool drag = false;
	public GUIText touchLabel;
	public GUIText transformLabel;
	public GUIText positionLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		if(Input.GetMouseButtonDown(0))
		{
			if(!drag)
			{
				drag = true;
				initialMousePos = Input.mousePosition;
				initialCameraPosition = this.transform.position;
			}
		}

		if(Input.GetMouseButtonUp(0))
		{
			drag = false;
		}

		if(drag)
		{
			Vector3 mousePos = Input.mousePosition;
			Vector2 delta = camera.ScreenToWorldPoint(Input.mousePosition) - camera.ScreenToWorldPoint(initialMousePos);
			
			Vector3 newPos = initialCameraPosition;
			newPos.x -= delta.x;
			newPos.y -= delta.y;
			this.transform.position = newPos;
		}

		// приближаем
		//
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			Vector3 zoomPointScreenCoords = Input.mousePosition;
			Vector3 zoomPointOldWorldCoords = Camera.main.ScreenToWorldPoint(zoomPointScreenCoords);

			Camera.main.orthographicSize -= 10;

			Vector3 zoomPointNewWorldCoords = Camera.main.ScreenToWorldPoint(zoomPointScreenCoords);
			Vector3 delta = zoomPointNewWorldCoords-zoomPointOldWorldCoords;
			transform.Translate(-delta.x, -delta.y,0);
		}

		// отдаляем
		//
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			Vector3 zoomPointScreenCoords = Input.mousePosition;
			Vector3 zoomPointOldWorldCoords = Camera.main.ScreenToWorldPoint(zoomPointScreenCoords);
			
			Camera.main.orthographicSize += 10;
			
			Vector3 zoomPointNewWorldCoords = Camera.main.ScreenToWorldPoint(zoomPointScreenCoords);
			Vector3 delta = zoomPointNewWorldCoords-zoomPointOldWorldCoords;
			transform.Translate(-delta.x, -delta.y,0);
		}
		#endif

#if UNITY_ANDROID
		if(Input.touchCount == 1)
		{
			Touch touch0 = Input.GetTouch(0);

			if(touch0.phase == TouchPhase.Began)
			{
				initialTouchPosition = touch0.position;
				initialCameraPosition = this.transform.position;

				//delta =  this.transform.position - initialTouchPosition;
				drag = true;
				touchLabel.text = "Touched at " + touch0.position.ToString();

			}

			if(touch0.phase == TouchPhase.Moved || touch0.phase == TouchPhase.Stationary)
			{
				Vector3 mousePos = touch0.position;
				Vector2 delta = camera.ScreenToWorldPoint(touch0.position) - camera.ScreenToWorldPoint(initialTouchPosition);

				positionLabel.text= "Now at " + touch0.position.ToString();
				Vector3 newPos = initialCameraPosition;
				newPos.x -= delta.x;
				newPos.y -= delta.y;
				this.transform.position = newPos;
				transformLabel.text  = "Transform at " + this.transform.position.ToString();
			}

			if(touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled)
			{
				drag = false;
			}
	   	}
#endif

	}
}
