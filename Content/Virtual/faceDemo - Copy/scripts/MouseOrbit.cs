using UnityEngine;
using System.Collections;

//[AddComponentMenu("Podium/CameraOrbit")]
//[RequireComponent(typeof(Camera))]
public class MouseOrbit : MonoBehaviour {
	public Transform target;
	public int mouseButton = 0;
	public float distance = 3.0f;
	public float zoomSpeed = 1.0f;
	public float distanceMin = 0.2f;
	public float distanceMax = 10f;

	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = 0f;
	public float yMaxLimit = 90f;
	
	private float x = 0.0f;
	private float y = 0.0f;
	private Vector3 center = Vector3.zero;

	void Start () {
	    if (!target)
			return;
		
	    var angles = transform.eulerAngles;
	    x = angles.y;
	    y = angles.x;
	
		// Make the rigid body not change rotation
	   	if (rigidbody)
			rigidbody.freezeRotation = true;
		
		if (target.renderer) {
			center = target.renderer.bounds.center;
//			Debug.Log("target has renderer");
		}
		
		UpdateRotation();
	}
	
	void Update () {
	
	}


	void LateUpdate () {
	    if (!target)
			return;
		
		if (Input.GetMouseButton(mouseButton)) {
	        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
	        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			
			UpdateRotation();
		}
		
		float wheelDelta = Input.GetAxis("Mouse ScrollWheel");
		if (wheelDelta != 0f) {
			
			distance = Mathf.Clamp(distance * (1.0f - wheelDelta * zoomSpeed), distanceMin, distanceMax);
			UpdateRotation();
		}
	}
	
	void UpdateRotation() {
	    if (!target)
			return;

 		y = ClampAngle(y, yMinLimit, yMaxLimit);
		
        Quaternion rotation = Quaternion.Euler(y, x, 0f);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + center;
        
        transform.rotation = rotation;
        transform.position = position;
		
	}

	static float ClampAngle(float angle, float min, float max) {
		if (angle < -360f)
			angle += 360f;
		if (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp(angle, min, max);
	}
}
