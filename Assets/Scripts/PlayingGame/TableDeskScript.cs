using UnityEngine;
using System.Collections;

public class TableDeskScript : MonoBehaviour {
	private bool drag = false;
	private Vector3 initialTouchPosition;
	private Vector3 delta;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}	
	
	void OnMouseDown()
	{
		initialTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		delta = this.transform.position - initialTouchPosition;
		drag = true;
	}
	
	void OnMouseUp()
	{
		drag = false;
	}
	
	void OnMouseDrag()
	{		
		Vector3 mousePos = Input.mousePosition;		
		this.transform.position = delta + Camera.main.ScreenToWorldPoint(mousePos);
	}
}
