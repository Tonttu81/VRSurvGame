using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachableObject : MonoBehaviour
{
    [SerializeField] bool inHand;

    public GameObject[] attachmentPoints;

    XRGrabInteractable grabScript;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < attachmentPoints.Length; i++)
        {

        }
    }

    public void objGrabbed()
    {
        inHand = true;
    }

    public void objDropped()
    {
        inHand = false;
    }
}
