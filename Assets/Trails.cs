using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trails : MonoBehaviour
{

List<Item> items = new List<Item>();
List<int> currents = new List<int>();

List<Item> itemsColor = new List<Item>();
List<int> currentsColor = new List<int>();

public Material trailMaterial;

public struct Item
{
        public Item(GameObject trail, List<Vector3> waypoints)
        {
                Trail = trail;
                Waypoints = waypoints;
        }
        public GameObject Trail {
                get; set;
        }
        public List<Vector3> Waypoints {
                get; set;
        }
}

void Start()
{
        int numberOfItems = 20;
        float radius = 3f;
        float theta = (Mathf.PI * 2.0f) / (float) numberOfItems;
        for (int i = 0; i < numberOfItems; i++)
        {
                // position of current item
                Vector3 pos = new Vector3(Mathf.Sin(theta * i) * radius, 30, Mathf.Cos(theta * i) * radius);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                cube.transform.position = transform.TransformPoint(pos);
                cube.transform.parent = this.transform;

                // trail object
                GameObject trail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                trail.transform.position = transform.TransformPoint(new Vector3(0, 10, 0));
                trail.transform.localScale = new Vector3(0, 0, 0);
                trail.AddComponent<TrailRenderer>();

                TrailRenderer aiRenderer = trail.GetComponent<TrailRenderer>();
                aiRenderer.material = trailMaterial;
                aiRenderer.material.SetColor("_EmissionColor", Color.HSVToRGB(i / (float) numberOfItems, 1, 1));
                aiRenderer.endWidth = 0f;
                aiRenderer.startWidth = 0.5f;
                aiRenderer.time = 2f;

                // waypoints
                int sizeOfEnclosing =15;
                List<Vector3> waypoints = new List<Vector3>();
                for(int j = 0; j < 20; j++)
                {
                        Vector3 location = new Vector3(
                                UnityEngine.Random.Range(-sizeOfEnclosing, sizeOfEnclosing),
                                UnityEngine.Random.Range(-sizeOfEnclosing, sizeOfEnclosing),
                                UnityEngine.Random.Range(-sizeOfEnclosing, sizeOfEnclosing));
                        waypoints.Add(cube.transform.TransformPoint(location));
                }
                Item item = new Item(trail, waypoints);
                itemsColor.Add(item);
                currentsColor.Add(0);
        }
}

void Update()
{
        if (MusicVolume.isMusicPlaying)
        {
                for (int i = 0; i < itemsColor.Count; i++)
                {
                        Vector3 toTarget = itemsColor[i].Waypoints[currentsColor[i]] - itemsColor[i].Trail.transform.position;
                        if (toTarget.magnitude < 1)
                                currentsColor[i] = (currentsColor[i] + 1) % itemsColor[i].Waypoints.Count;
                        toTarget.Normalize();

                        TrailRenderer renderer = itemsColor[i].Trail.GetComponent<TrailRenderer>();

                        renderer.startWidth = Mathf.Lerp(renderer.startWidth, (Mathf.Abs(AudioAnalyzer.spectrum[i]) * 100), Time.deltaTime * 100);

                        itemsColor[i].Trail.transform.Translate(toTarget * Time.deltaTime * 200);
                }
        }
}
}
