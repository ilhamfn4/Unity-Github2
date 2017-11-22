using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PinchZoomHandler : MonoBehaviour {

    public Text text;
	public bool isAvailable = false;
	public GameObject obj;
	public float scaleSensitivity = 0.0175f;
	public float scaleMinLimit = 0.4f;

	private Vector3 defaultScale;
    private GameObject panelFF;

	void Awake ()
	{
		if (obj == null)
		{
			obj = this.gameObject;
		}
		defaultScale = this.transform.localScale;

	}

	public void InitScale ()
	{
		this.transform.localScale = defaultScale;
	}

	void Update()
	{
		if (isAvailable)
		{
            if (panelFF == null) 
                panelFF = GameObject.Find("Fun Fact(Clone)");
            
			#if UNITY_EDITOR
			if (Input.anyKey) {
				float scaleValue = 0f;
				if (Input.GetKey(KeyCode.LeftArrow)) {
					scaleValue = -scaleSensitivity;
				} else if (Input.GetKey(KeyCode.RightArrow)) {
					scaleValue = scaleSensitivity;
				}
                float scaleValueResult = obj.transform.localScale.x + scaleValue;
                scaleValueResult = Mathf.Clamp(scaleValueResult, scaleMinLimit, scaleValueResult);
                obj.transform.localScale = new Vector3(scaleValueResult, scaleValueResult, scaleValueResult);

                //if (panelFF != null) {
                //    float scaleValueResultPanelFF = panelFF.transform.localScale.x - scaleValue;
                //    scaleValueResultPanelFF = Mathf.Clamp(scaleValueResultPanelFF, scaleMinLimit, scaleValueResultPanelFF);
                //    panelFF.transform.localScale = new Vector3(scaleValueResultPanelFF, scaleValueResult, scaleValueResultPanelFF);
                //}

                text.text = "Parent Scale : x " + obj.transform.localScale.x + " y " + obj.transform.localScale.y + " z " + obj.transform.localScale.z;
            }
#else
			// If there are two touches on the device...
			if (Input.touchCount == 2)
			{
				// Store both touches.
				Touch touchZero = Input.GetTouch (0);
				Touch touchOne = Input.GetTouch (1);

				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) * -1f;

//				// If the camera is orthographic...
//				if (camera.isOrthoGraphic)
//				{
//					// ... change the orthographic size based on the change in distance between the touches.
//					camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
//					// Make sure the orthographic size never drops below zero.
//					camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
//				}
//				else
//				{
//					// Otherwise change the field of view based on the change in distance between the touches.
//					camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
//					// Clamp the field of view to make sure it's between 0 and 180.
//					camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
//				}
				float scaleValue = scaleSensitivity * deltaMagnitudeDiff;
				float scaleValueResult = obj.transform.localScale.x + scaleValue;
				scaleValueResult = Mathf.Clamp (scaleValueResult, scaleMinLimit, scaleValueResult);
				obj.transform.localScale = new Vector3 (scaleValueResult, scaleValueResult, scaleValueResult);
                
                //if (panelFF != null) {
                //    float scaleValueResultPanelFF = panelFF.transform.localScale.x - scaleValue;
                //    scaleValueResultPanelFF = Mathf.Clamp(scaleValueResultPanelFF, scaleMinLimit, scaleValueResultPanelFF);
                //    panelFF.transform.localScale = new Vector3(scaleValueResultPanelFF, scaleValueResult, scaleValueResultPanelFF);
                //}    

                text.text = "Parent Scale : x " + obj.transform.localScale.x + " y " + obj.transform.localScale.y + " z " + obj.transform.localScale.z;
                
			}
#endif
        }
    }
}
