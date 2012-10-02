using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Vector2 initialTouchPosition;
	private Vector3 initialCameraPosition;
	private Vector3 delta;
	private Vector3 cameraScreenCoords;

	private Vector3 initialMousePos;

	private bool drag = false;
	private bool zoom = false;

	private Vector2 initialTouch0Position;
	private Vector2 initialTouch1Position;
	private float initialOrthographicSize = 1;

	public GUIText touchLabel;
	public GUIText transformLabel;
	public GUIText positionLabel;

	public GUIText pinchScaleLabel;
	public GUIText pinchDeltaLabel;

	// Use this for initialization
	void Start ()
	{

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

//#if UNITY_ANDROID
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

		if(Input.touchCount == 2)
		{
			drag = false;

			Touch touch0 = Input.GetTouch(0);			
			Touch touch1 = Input.GetTouch(1);

			if(!zoom)
			{
				initialTouch0Position = touch0.position;
				initialTouch1Position = touch1.position;
				initialOrthographicSize = Camera.main.orthographicSize;

				zoom = true;
			}
			else
			{
				float delta0 = Vector2.Distance(touch0.position, initialTouch0Position);
				float delta1 = Vector2.Distance(touch1.position, initialTouch1Position);
				float scaleFactor = GetScaleFactor(touch0.position, 
				                                   touch1.position, 
				                                   initialTouch0Position, 
				                                   initialTouch1Position);
				//Vector2 translationDelta = GetTranslationDelta(touch0.position, touch0.position,
				//                                               touch0.deltaPosition, touch1.deltaPosition,
				//                                               this.transform.position, scaleFactor);


				this.pinchScaleLabel.text = scaleFactor.ToString();
				//this.pinchDeltaLabel.text = translationDelta.ToString();
				Camera.main.orthographicSize = initialOrthographicSize / scaleFactor;
			}
		}
//#endif


	}

	public static float GetScaleFactor(Vector2 position1, Vector2 position2, Vector2 oldPosition1, Vector2 oldPosition2)
	{
		float distance = Vector2.Distance(position1, position2);
		float oldDistance = Vector2.Distance(oldPosition1, oldPosition2);
		
		if(oldDistance == 0 || distance == 0)
		{
			return 1.0f;
		}
		
		return distance / oldDistance;
	}

	public static Vector2 GetTranslationDelta(Vector2 position1, Vector2 position2, Vector2 delta1, Vector2 delta2,
	                                          Vector2 objectPos, float scaleFactor)
	{
		Vector2 oldPosition1 = position1 - delta1;
		Vector2 oldPosition2 = position2 - delta2;
		
		Vector2 newPos1 = position1 + (objectPos - oldPosition1) * scaleFactor;
		Vector2 newPos2 = position2 + (objectPos - oldPosition2) * scaleFactor;
		Vector2 newPos = (newPos1 + newPos2) / 2;
		
		return newPos - objectPos;
	}
}
