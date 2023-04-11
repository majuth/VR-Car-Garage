using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;
using Valve.VR;
public class Grabbing : MonoBehaviour
{
    public SteamVR_Input_Sources handtype;
    public SteamVR_Action_Boolean grabbing;
    public Vector3 objPosition;
    public Vector3 objRotation;
    public Vector3 velocity;
    public GameObject handObject;
    public GameObject inter_obj;
    public Collider handCollider;
    private bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (trigger == false && handObject.transform.childCount > 0)
        {

            foreach (Transform child in handObject.transform)
            {

                child.GetComponent<Rigidbody>().isKinematic = false;
                child.transform.parent = null;


            }

        }



    }

    private void OnTriggerStay(Collider other)
    {
        if (grabbing.GetState(handtype) == true && other.tag == "Touchable")
        {
            other.attachedRigidbody.transform.SetParent(handObject.transform, true);
            other.attachedRigidbody.isKinematic = true;
            trigger = true;
            Debug.Log("Picked up");

        }
        else
        {
            trigger = false;
        }


    }
}
