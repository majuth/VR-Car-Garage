using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoodAttach : MonoBehaviour
{
    public GameObject hoodSocket;
    public GameObject ornament;
    public GameObject handL;
    public GameObject handR; 
    private Vector3 conPosition;
    private int count;
    private bool trigger; 
    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        trigger = false;
        ornament = null;
        hoodSocket = this.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if(trigger == true)
        {
            ornament.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
            ornament.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        else
        {

        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ornament" && count == 0)
        {
            ornament = other.gameObject;

            ornament.transform.SetParent(hoodSocket.transform);
            ornament.GetComponent<Rigidbody>().isKinematic = true;
            trigger = true;
            count++;
            Debug.Log("Child is born " + count);
        }

        if (other.gameObject.tag == "Left Hand" && count > 0)
        {
            conPosition = handL.transform.position;
            ornament.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            ornament.gameObject.transform.SetParent(null);
            trigger = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
            ornament.gameObject.transform.position = conPosition;
            yield return new WaitForSeconds(2);
            this.gameObject.GetComponent<Collider>().enabled = true;
            count--;
            Debug.Log("Child is aborted " + count);
        }
        if (other.gameObject.tag == "Right Hand" && count > 0)
        {
            conPosition = handR.transform.position;
            ornament.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            ornament.gameObject.transform.SetParent(null);
            trigger = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
            ornament.gameObject.transform.position = conPosition;
            yield return new WaitForSeconds(2);
            this.gameObject.GetComponent<Collider>().enabled = true;
            count--;
            Debug.Log("Child is aborted " + count);
        }


    }
}
