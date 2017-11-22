using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _DragRotationHandler : MonoBehaviour
{
	public bool isAvailable = false;
	public GameObject obj;
	public bool isReverse = true;
    public Vector2 sensitivity = new Vector2(2.25f, 2.25f);
	public Vector2 xRotationLimit = new Vector2 (-89f, 89f);

    // Assign this if there's a parent object controlling motion, such as a Character Controller.
    // Yaw rotation will affect this object instead of the camera if set.

	private Vector3 firstpoint; //change type on Vector3
	private Vector3 secondpoint;
	private Vector3 previousPoint;
	private float xAngle = 0.0f; //angle for axes x for rotation
	private float yAngle = 0.0f;
	private float xAngTemp = 0.0f; //temp variable for angle
	private float yAngTemp = 0.0f;
	private bool isActivate = true;
	private Vector3 defaultLocalEulerAngles;

    void Awake()
    {
		//Initialization our angles of camera
		defaultLocalEulerAngles = this.transform.localEulerAngles;
		InitRotation ();
    }

	public void InitRotation ()
	{
		xAngle = 0f;
		yAngle = 0f;
		//this.transform.rotation = Quaternion.Euler (yAngle, xAngle, 0f);
		this.transform.localEulerAngles = defaultLocalEulerAngles;
	}

    void Update()
	{
		if (isAvailable)
		{
			#if UNITY_EDITOR
			if (isActivate)
			{
				if (Input.GetMouseButtonDown (0))
				{
					firstpoint = Input.mousePosition;
					//--
					previousPoint = Input.mousePosition;
					//--
					xAngle = this.transform.localEulerAngles.y;
					yAngle = this.transform.localEulerAngles.x;

					xAngTemp = xAngle;
					yAngTemp = yAngle;
				}
				else if (Input.GetMouseButton (0))
				{
					secondpoint = Input.mousePosition;
					//Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
					float xDiff = sensitivity.x * (secondpoint.x - firstpoint.x) * 180f / Screen.width;
					float yDiff = sensitivity.y * (secondpoint.y - firstpoint.y) * 90f / Screen.height;
					float nextX;
					float nextY;
					if (!isReverse)
					{
						nextX = (xAngTemp + xDiff) % 360f;
						nextY = (yAngTemp - yDiff) % 360f;
					}
					else
					{
						nextX = (xAngTemp - xDiff) % 360f;
						nextY = (yAngTemp + yDiff) % 360f;
					}

					xAngle = nextX;
					yAngle = nextY;

					if (yAngle > 180f)
						yAngle -= 360f;

					float tempClamp = Mathf.Clamp (yAngle, xRotationLimit.x, xRotationLimit.y);
					yAngle = tempClamp;
					this.transform.rotation = Quaternion.Euler (yAngle, xAngle, 0f);

					//-------------------------------------------------------------------------------

//					float xDiff2 = sensitivity.x * (secondpoint.x - previousPoint.x) * 180f / Screen.width;
//					float yDiff2 = sensitivity.y * (secondpoint.y - previousPoint.y) * 90f / Screen.height;
////					if (Math.Abs(xDiff2) < 1f)
////						xDiff2 = 0f;
////					if (Math.Abs(yDiff2) < 1f)
////						yDiff2 = 0f;
//					Vector3 tempVector  = new Vector3(yDiff2, -xDiff2, 0f);
//					this.transform.Rotate (tempVector, Space.World);
////					this.transform.Rotate (tempVector, Space.Self);
//					previousPoint = Input.mousePosition;
				}
				else if (Input.GetMouseButtonUp (0))
				{
					isActivate = true;
				}
			}
			#else
			//Check count touches
			if (Input.touchCount == 1)
			{
				if (isActivate)
				{
					// Touch began, save position
					if(Input.GetTouch(0).phase == TouchPhase.Began)
					{
						firstpoint = Input.GetTouch(0).position;
						xAngle = this.transform.localEulerAngles.y;
						yAngle = this.transform.localEulerAngles.x;

						xAngTemp = xAngle;
						yAngTemp = yAngle;
					}
					// Move finger by screen
					else if (Input.GetTouch(0).phase == TouchPhase.Moved)
					{
						secondpoint = Input.GetTouch(0).position;
						//Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree

						float xDiff = sensitivity.x * (secondpoint.x - firstpoint.x) * 180f / Screen.width;
						float yDiff = sensitivity.y * (secondpoint.y - firstpoint.y) * 90f / Screen.height;
						float nextX;
						float nextY;
						if (!isReverse)
						{
							nextX = (xAngTemp + xDiff) % 360f;
							nextY = (yAngTemp - yDiff) % 360f;
						}
						else
						{
							nextX = (xAngTemp - xDiff) % 360f;
							nextY = (yAngTemp + yDiff) % 360f;
						}

						xAngle = nextX;
						yAngle = nextY;

						if (yAngle > 180f)
							yAngle -= 360f;

						float tempClamp = Mathf.Clamp (yAngle, xRotationLimit.x, xRotationLimit.y);
						yAngle = tempClamp;
						this.transform.rotation = Quaternion.Euler (yAngle, xAngle, 0f);
					}
					else if (Input.GetTouch (0).phase == TouchPhase.Ended)
					{
						isActivate = true;
					}
				}
			}
			else if (Input.touchCount > 1)
			{
				isActivate = false;
			}
			else if (Input.touchCount == 0)
			{
				isActivate = true;
			}
			#endif
		}
	}
}
