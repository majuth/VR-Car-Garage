using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;
using TMPro;

public class SprayCanMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean menuButton;
    public SteamVR_Action_Boolean colorPick;
    public SteamVR_Input_Sources handtype;
    public List<GameObject> cans = new List<GameObject>();
    public GameObject CanvasText;
    public TextMeshProUGUI myText;
    public GameObject can;
    private int picked;
    private Vector3 conPosition;
    private Vector3 conDirection;
    private bool spawned; 
    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
        picked = 0;
        CanvasText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        conPosition = transform.position;
        if (menuButton.GetState(handtype) == true && spawned == false )
        {
            can = Instantiate(cans[picked], conPosition, Quaternion.identity);
           
            spawned = true;
            CanvasText.SetActive(false);


        }
        if(colorPick.GetState(handtype) == true && spawned == true)
        {
            Destroy(can);
            picked++;
            CanvasText.SetActive(true);
            if (picked > 5)
            {
                picked = 0;
            }
            switch (picked)
            {
                case 0:
                    myText.text = "Black Paint";
                    break;
                case 1:
                    myText.text = "Blue Paint";
                    break;
                case 2:
                    myText.text = "Green Paint";
                    break;
                case 3:
                    myText.text = "Purple Paint";
                    break;
                case 4:
                    myText.text = "Red Paint";
                    break;
                case 5:
                    myText.text = "White Paint";
                    break;
            }
            spawned = false;
            Debug.Log(picked);
        }
    }


  
}
