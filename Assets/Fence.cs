using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
void Start()
{
        float radius = GetComponent<CapsuleCollider>().radius;
        float theta = Mathf.PI * 2.0f / (float) 40;
        for(int i = 0; i < 40; i++)
        {
                float angle = theta*i;
                if (angle > Mathf.PI + 0.3 || angle < Mathf.PI - 0.3)
                {
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.SetParent(transform, false);
                        cube.transform.localPosition = new Vector3(Mathf.Sin(theta * i) * radius, 1.5f, Mathf.Cos(theta * i) * radius);
                        cube.transform.localScale = new Vector3(0.009f, 3f, 0.009f);

                        GameObject rail = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        rail.transform.SetParent(transform, false);
                        rail.transform.localPosition = new Vector3(Mathf.Sin(theta * i) * radius, 3f, Mathf.Cos(theta * i) * radius);
                        rail.transform.localScale = new Vector3(0.09f, 0.1f, 0.015f);
                        rail.transform.Rotate(0.0f, Mathf.Rad2Deg * angle, 0.0f, Space.Self);
                }
        }
}
}
