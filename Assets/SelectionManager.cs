using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

private Transform _selection;
public GameObject player;
public Camera playerCamera;

bool beforeTeleport = true;

string selectableTag = "button";

void OnGUI()
{
        if (beforeTeleport)
                GUI.Label(new Rect(10, 10, 150, 20), "Press M for map");
}

void Start()
{
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("finalFloor"))
                obj.SetActive(false);
}

void Update()
{
        if (_selection != null)
                _selection = null;


        if (Input.GetMouseButtonDown(0))
        {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                        var selection = hit.transform;
                        Debug.Log(selection.tag);

                        if (selection.CompareTag(selectableTag))
                        {

                                CharacterController cc = player.GetComponent<CharacterController>();

                                cc.enabled = false;
                                player.transform.position = new Vector3(0f, -3.37f, 0f);
                                cc.enabled = true;

                                beforeTeleport = false;

                                foreach(GameObject obj in GameObject.FindGameObjectsWithTag("finalFloor"))
                                        obj.SetActive(true);

                                playerCamera.GetComponent<CameraMapLook>().enabled = false;
                        }
                        _selection = selection;
                }
        }
}
}
