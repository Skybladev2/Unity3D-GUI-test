using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Vector2 initialTouchPosition;
	private Vector3 initialCameraPosition;
	private Vector3 delta;
	private Vector3 cameraScreenCoords;
	private Vector3 initialMidPointScreen;
	private Vector3 initialMidPointWorld;


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

	void ZoomToPoint(float cameraOldSize, float scale, Vector3 screenCoords)
	{
		Vector3 zoomPointOldWorldCoords = Camera.main.ScreenToWorldPoint(screenCoords);
		Camera.main.orthographicSize = cameraOldSize / scale;

		Vector3 zoomPointNewWorldCoords = Camera.main.ScreenToWorldPoint(screenCoords);
		Vector3 delta = zoomPointNewWorldCoords-zoomPointOldWorldCoords;
		transform.Translate(-delta.x, -delta.y,0);
	}

	void ZoomToPoint(float cameraOldSize, Vector3 cameraOldPosition, float scale, Vector3 screenCoords)
	{
		Vector3 zoomPointOldWorldCoords = Camera.main.ScreenToWorldPoint(screenCoords);
		Camera.main.orthographicSize = cameraOldSize / scale;
		
		Vector3 zoomPointNewWorldCoords = Camera.main.ScreenToWorldPoint(screenCoords);
		Vector3 delta = zoomPointNewWorldCoords-zoomPointOldWorldCoords;

		Vector3 newPosition = cameraOldPosition;
		newPosition.x -= delta.x;
		newPosition.y -= delta.y;

		transform.position = newPosition;
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
			ZoomToPoint(Camera.main.orthographicSize, 1.01f, Input.mousePosition);
		}

		// отдаляем
		//
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			ZoomToPoint(Camera.main.orthographicSize, 0.99f, Input.mousePosition);
		}
		#endif

//#if UNITY_ANDROID
		if(Input.touchCount == 1)
		{
			zoom = false;
			Touch touch0 = Input.GetTouch(0);

			if(IsTouching (touch0))
			{
				if (!drag)
				{
					initialTouchPosition = touch0.position;
					initialCameraPosition = this.transform.position;

					drag = true;
					touchLabel.text = "Touched at " + touch0.position.ToString();
				}
				else
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
			}

			if(!IsTouching(touch0))
			{
				drag = false;
			}
	   	}
		else
		{
			drag = false;
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
				initialCameraPosition = this.transform.position;
				initialOrthographicSize = Camera.main.orthographicSize;
				initialMidPointScreen = (touch0.position + touch1.position) / 2;
				initialMidPointWorld = camera.ScreenToWorldPoint((touch0.position + touch1.position) / 2);

				zoom = true;
			}
			else
			{
				this.transform.position = initialCameraPosition;
				camera.orthographicSize = initialOrthographicSize;

				float scaleFactor = GetScaleFactor(touch0.position, 
				                                   touch1.position, 
				                                   initialTouch0Position, 
				                                   initialTouch1Position);

				Vector2 currentMidPoint = (touch0.position + touch1.position) / 2;

				Vector3 initialPointWorldBeforeZoom = camera.ScreenToWorldPoint(initialMidPointScreen);

				Camera.main.orthographicSize = initialOrthographicSize / scaleFactor;

				Vector3 initialPointWorldAfterZoom = camera.ScreenToWorldPoint(initialMidPointScreen);

				Vector2 initialPointDelta = initialPointWorldBeforeZoom - initialPointWorldAfterZoom;

				Vector2 oldAndNewPointDelta = 
					camera.ScreenToWorldPoint(currentMidPoint)  - 
					camera.ScreenToWorldPoint(initialMidPointScreen);
				
				Vector3 newPos = initialCameraPosition;
				newPos.x -= oldAndNewPointDelta.x - initialPointDelta.x;
				newPos.y -=  oldAndNewPointDelta.y - initialPointDelta.y;
				this.transform.position = newPos;
				transformLabel.text  = "Transform at " + this.transform.position.ToString();

				this.pinchScaleLabel.text = scaleFactor.ToString();
			}
		}
		else
		{
			zoom = false;
		}
//#endif


	}

	static bool IsTouching (Touch touch)
	{
		return touch.phase == TouchPhase.Began || 
				touch.phase == TouchPhase.Moved || 
				touch.phase == TouchPhase.Stationary;
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
