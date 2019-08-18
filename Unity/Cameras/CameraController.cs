using UnityEngine;

public class CameraController : MonoBehaviour {
    public float cameraSpanvelocity = 60f;
    public float panborderThickness = 10f;
    public Vector2 panLimits;

    public float scrollSpeed = 20f;
    public Vector2 cameraZoomLimits;
	
	// Update is called once per frame
	void Update () {
        //guardo mi posicion actual.
        Vector3 currentPos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panborderThickness)
        {
            currentPos.z += cameraSpanvelocity * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panborderThickness)
        {
            currentPos.z -= cameraSpanvelocity * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.height - panborderThickness)
        {
            currentPos.x += cameraSpanvelocity * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panborderThickness)
        {
            currentPos.x -= cameraSpanvelocity * Time.deltaTime;
        }

        currentPos.x = Mathf.Clamp(currentPos.x, -panLimits.x, panLimits.x);
        currentPos.z = Mathf.Clamp(currentPos.z, -panLimits.y, panLimits.y);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        print(scroll);
        currentPos.y = Mathf.Clamp(currentPos.y, cameraZoomLimits.x, cameraZoomLimits.y);
        currentPos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        transform.position = currentPos;
		
	}
}
