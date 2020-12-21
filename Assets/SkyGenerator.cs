using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyGenerator : MonoBehaviour
{

List<GameObject> stars = new List<GameObject>();
public Material starMaterial;
public GameObject platform;

Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
{
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;
        return point;
}

void Start()
{
        for (int i = 0; i < 1000; i++)
        {

                GameObject wallUp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                wallUp.transform.position = new Vector3(UnityEngine.Random.Range(-1000.0f, 1000.0f), UnityEngine.Random.Range(400, 800), UnityEngine.Random.Range(-1000.0f, 1000.0f));
                float size = UnityEngine.Random.Range(2f, 10f);
                wallUp.transform.localScale =new Vector3(size, size, size);
                wallUp.GetComponent<Renderer>().material = starMaterial;

                if (i < 400)
                        stars.Add(wallUp);
        }
}

void Update()
{
        if (MusicVolume.isMusicPlaying)
        {
                foreach (GameObject star in stars)
                {
                        Vector3 lscale = star.transform.localScale;
                        float size = Mathf.Lerp(lscale.y, 1 + (Mathf.Abs((int)Mathf.RoundToInt(UnityEngine.Random.Range(0, 2))) * 30), Time.deltaTime * 20);
                        star.transform.localScale = new Vector3(size, size, size);
                }
                foreach (GameObject i in stars)
                {
                        if (AudioAnalyzer.bands[1]*100 > 3)
                                i.transform.RotateAround(platform.transform.position, Vector3.up, 200 * Time.deltaTime);
                }
        }
}
}
