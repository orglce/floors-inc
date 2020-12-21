using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMapLook : MonoBehaviour
{

public GameObject firstPersonController;

bool isInMapView = false;
bool wasinMapView = false;
bool moveCamera = false;

Vector3 startPosition;
Vector3 endPosition;
Vector3 initialPlayerPosition;

Quaternion startAngle;
Quaternion endAngle;
Quaternion initialPlayerAngle;

float lerpDuration = 0.5f;
float timePassed = 0;

void Update()
{
        if (moveCamera == false && Input.GetKeyDown(KeyCode.M)) {
                if (isInMapView)
                {
                        wasinMapView = true;
                        isInMapView = false;

                        startPosition = new Vector3(0, 80f, 0);
                        endPosition = initialPlayerPosition;

                        startAngle = Quaternion.Euler(90, 0, 0);
                        endAngle = initialPlayerAngle;

                        moveCamera = true;
                        timePassed = 0;
                }
                else
                {
                        this.GetComponent<MouseLook>().enabled = false;
                        firstPersonController.GetComponent<PlayerMovement>().enabled = false;
                        isInMapView = true;

                        startPosition = transform.position;
                        endPosition = new Vector3(0, 80f, 0);
                        initialPlayerPosition = startPosition;

                        startAngle = transform.rotation;
                        endAngle = Quaternion.Euler(90, 0, 0);
                        initialPlayerAngle = startAngle;

                        moveCamera = true;
                        timePassed = 0;
                }
        }

        if (moveCamera)
        {
                timePassed += Time.deltaTime;
                float p = timePassed/lerpDuration;
                if (p <= 1.2)
                {
                        transform.rotation = Quaternion.Slerp(startAngle, endAngle, p);
                        transform.position = Vector3.Lerp(startPosition, endPosition, p);
                }
                else
                {
                        if (wasinMapView)
                        {
                                this.GetComponent<MouseLook>().enabled = true;
                                firstPersonController.GetComponent<PlayerMovement>().enabled = true;
                                wasinMapView = false;
                        }
                        moveCamera = false;
                }
        }
}
}
