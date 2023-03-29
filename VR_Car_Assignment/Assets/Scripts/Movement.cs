using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Movement : MonoBehaviour
{
    private Boolean canTeleport;
    private Vector3 teleportPoint;

    public SteamVR_Input_Sources handtype;
    public SteamVR_Action_Boolean moveForward;
    public SteamVR_Action_Boolean teleportation;
    public Vector3 conPosition;
    public Vector3 conDirection;
    public GameObject selectorLine;
    public GameObject teleportCircle;
    public GameObject cameraRig;
    public Collider floorCollider;
    public Vector3 positionChange;
    // Start is called before the first frame update
    void Start()
    {
        positionChange = new Vector3(0f, 0f, 0f);
        teleportation.AddOnStateDownListener(teleportButton, handtype);
    }

    // Update is called once per frame
    void Update()
    {
        conPosition = transform.position;
        conDirection = transform.forward;
        LineRenderer navLine = selectorLine.GetComponent<LineRenderer>(); 
        navLine.SetPosition(0, conPosition + positionChange); 
        if(moveForward.GetState(handtype) == true)
        {
            /*RaycastHit hit;
            bool hitObject = Physics.Raycast(conPosition, conDirection, out hit, 20f);
            navLine.SetPosition(1, hit.point);
            navLine.enabled = true;*/

            RaycastHit hit;
            bool hitObject = Physics.Raycast(conPosition, conDirection, out hit, 20f);
            navLine.SetPosition(1, conPosition + positionChange + conDirection *20);
            navLine.enabled = true;

            if (hit.collider == floorCollider)
            {
                Transform arrowTransform = teleportCircle.GetComponent<Transform>();
                arrowTransform.position = hit.point + new Vector3(0.0f, 0.0f, 0.0f);
                teleportCircle.SetActive(true);
                teleportPoint = hit.point + new Vector3(0f, 0.5f, 0f);
                canTeleport = true;
            }
            else
            {
                teleportCircle.SetActive(false);
                canTeleport = false;
            }
        }

        else
        {
            navLine.enabled = false;
            teleportCircle.SetActive(false);
        }

    }

    private void teleportButton(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if(canTeleport = true)
        {
            Transform cameraTransform = cameraRig.GetComponent<Transform>();
            cameraTransform.position = teleportPoint;
            print("moved");
            LineRenderer navLine = selectorLine.GetComponent<LineRenderer>();
            navLine.SetPosition(0, transform.position + positionChange);
        }
    }
}
