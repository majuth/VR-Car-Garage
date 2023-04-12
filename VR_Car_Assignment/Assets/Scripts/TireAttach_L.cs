using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class TireAttach_L : MonoBehaviour
{

    public GameObject socket;
    public GameObject wheel;
    public GameObject handL;
    public GameObject handR;
    private Vector3 conPosition;
    private int count;
    private bool trigger;
    private AudioSource attach;
    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        trigger = false;
        wheel = null;
        socket = this.gameObject;
        attach = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger == true)
        {
            wheel.transform.localEulerAngles = new Vector3(0f,180f, 0f);
            wheel.transform.localPosition = new Vector3(-0.1f, 0f, 0f);
        }
        else
        {

        }

    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tire" && count == 0)
        {
            wheel = other.gameObject;

            wheel.transform.SetParent(socket.transform);
            wheel.GetComponent<Rigidbody>().isKinematic = true;
            trigger = true;
            attach.Play();
            count++;
            Debug.Log("Child is born " + count);
        }

        if (other.gameObject.tag == "Left Hand" && count > 0)
        {
            conPosition = handL.transform.position;
            wheel.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            wheel.gameObject.transform.SetParent(null);
            trigger = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
            wheel.gameObject.transform.position = conPosition;
            yield return new WaitForSeconds(2);
            this.gameObject.GetComponent<Collider>().enabled = true;
            count--;
            Debug.Log("Child is aborted " + count);
        }
        if (other.gameObject.tag == "Right Hand" && count > 0)
        {
            conPosition = handR.transform.position;
            wheel.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            wheel.gameObject.transform.SetParent(null);
            trigger = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
            wheel.gameObject.transform.position = conPosition;
            yield return new WaitForSeconds(2);
            this.gameObject.GetComponent<Collider>().enabled = true;
            count--;
            Debug.Log("Child is aborted " + count);
        }


    }
}