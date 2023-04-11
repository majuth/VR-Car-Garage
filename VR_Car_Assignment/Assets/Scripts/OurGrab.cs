using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;
using Valve.VR;
public class OurGrab : MonoBehaviour
{
    public SteamVR_Input_Sources handtype;
    public SteamVR_Action_Boolean grabbing;
    public SteamVR_Behaviour_Pose controllerPose;
    private GameObject grabbedObject;
    private GameObject nearObject;
    private FixedJoint joint;
    public bool trigger;
    public int limit;
    // Start is called before the first frame update
 
    void Start()
    {
        nearObject = null;
        trigger = false;
        limit = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (grabbing.GetLastStateDown(handtype))
        {
            if (nearObject && nearObject.GetComponent<Rigidbody>().mass < 10)
            {
                GrabObject();
                trigger = true;

            }
        }
        if (grabbing.GetLastStateUp(handtype))
        {
            if (grabbedObject)
            {
                ReleasedObject();

                trigger = false;

            }
        }


    }

    private void setNearby(Collider other)
    {
        
        if (!other.attachedRigidbody)
        {
            return;
        }
        
        
            nearObject = other.gameObject;
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        setNearby(other);


    }
    private void OnTriggerStay(Collider other)
    {

        setNearby(other);
       

    }
    private void OnTriggerExit(Collider other)
    {

        if (!nearObject)
        {
            return;
        }
        nearObject = null;
       
    }

    private void GrabObject()
    {
        grabbedObject = nearObject;
        nearObject = null;
        joint = AddFixedJoint();
        joint.connectedBody = grabbedObject.GetComponent<Rigidbody>();
        
       
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleasedObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            grabbedObject.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
        }
        grabbedObject = null;
        
    }
}
