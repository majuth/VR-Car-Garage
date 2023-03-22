using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR; 

public class SprayCan : MonoBehaviour
{
    public ParticleSystem spraycolor;
    public List<ParticleCollisionEvent> collisionEvents;
    public Material sprayMat;
    public SteamVR_Input_Sources handtype;
    public SteamVR_Action_Boolean spray; 
    // Start is called before the first frame update
    void Start()
    {
        //Get the Particle System 
        spraycolor = GetComponent<ParticleSystem>();
        //List of Collisions 
        collisionEvents = new List<ParticleCollisionEvent>();
        //Material of Particle System (Can be changed in the renderer tab for the particle system) 
        sprayMat = GetComponent<ParticleSystemRenderer>().material;
        
    }
    void Update()
    {

        if (spray.GetState(handtype) == true)
        {
            //Emit spray for 1 second
            spraycolor.Emit(1);
        }
    }

    /* There are several things that need to be done if you want to create
     * another particle system that interacts with an object like this.
     * Make sure in the particle system inspector that Collision tab is checked off 
     * And set the "Type" to world so that it can interact with anything with a collider
     * Also make sure triggers tab is checked. 
     */

    void OnParticleCollision(GameObject other)
    {
        //The amount of collisions 
        //Note: Collisions always read one collision at a time
        //If there are multiple collisions it's still just gonna be 1. 
        int numCollisionEvents = spraycolor.GetCollisionEvents(other, collisionEvents);

        //Get the rigidbody of the object that it collided with and its material
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Material mat = other.GetComponent<Renderer>().material;

        int i = 0;
        //While the particles collide with anything with the tag paintable 
        while (i < numCollisionEvents && other.tag == "Paintable")
        {
            if (rb)
            {
                //Get the intersection of the particles and object 
                Vector3 pos = collisionEvents[i].intersection;
                //Change color to spray material 
                mat.color = sprayMat.color;
                Debug.Log("thisworks");
            }
            i++;
        }

    } 


}
