using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class TireAttach_L : MonoBehaviour
{

    public GameObject socket;
    public GameObject wheel;
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
            wheel.transform.localPosition = new Vector3(0.1f, 0f, 0f);
        }
        else
        {

        }

    }

    private void OnTriggerEnter(Collider other)
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

        if (other.gameObject.tag == "Left Hand" && count > 0 || other.gameObject.tag == "Right Hand" && count > 0)
        {
            wheel.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            wheel.gameObject.transform.localPosition = new Vector3(1f, 0f, 0f);
            wheel.gameObject.transform.SetParent(null);
            trigger = false;
            count--;
            Debug.Log("Child is aborted " + count);
        }


    }
}