using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
bool teleportHappened = false;

string walkedThroughWall = null;

private void OnTriggerEnter(Collider other)
{
        if (walkedThroughWall != other.gameObject.tag)
        {
                GameObject oppositeWall = other.gameObject.transform.parent.GetChild((other.gameObject.transform.GetSiblingIndex() + 1) % 2).gameObject;
                Vector3 distance = transform.position - other.gameObject.transform.position;

                if (teleportHappened == false)
                {
                        CharacterController cc = this.GetComponent<CharacterController>();

                        cc.enabled = false;
                        transform.position = oppositeWall.transform.position + distance;
                        cc.enabled = true;
                        teleportHappened = true;
                } else {
                        teleportHappened = false;
                }

                walkedThroughWall = other.gameObject.tag;
        } else
        {
                if (other.gameObject.tag == walkedThroughWall)
                {
                        MusicVolume.playMusic = true;

                        GameObject oppositeWall = GameObject.FindGameObjectsWithTag("finalWall")[0];
                        Vector3 distance = transform.position - other.gameObject.transform.position;

                        float rotation = 0f;
                        switch (other.gameObject.tag)
                        {
                        case "north":
                                rotation = 0f;
                                break;
                        case "south":

                                rotation = 180f;
                                break;
                        case "west":
                                rotation = 90f;
                                break;
                        case "east":
                                rotation = 270f;
                                break;
                        }

                        distance = Quaternion.Euler(0, rotation, 0) * distance;

                        CharacterController cc = this.GetComponent<CharacterController>();

                        cc.enabled = false;
                        transform.position = oppositeWall.transform.position + distance;
                        transform.Rotate(0, rotation, 0);
                        cc.enabled = true;
                        teleportHappened = true;
                }
        }
}
}
