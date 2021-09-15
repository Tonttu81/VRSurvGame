using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableObject : MonoBehaviour
{
    [SerializeField] bool inHand;

    public AttachmentPointObject[] attachmentPoints;
    AttachmentPoint[] attachmentPointScripts;

    GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        attachmentPointScripts = new AttachmentPoint[attachmentPoints.Length];

        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            attachmentPointScripts[i] = attachmentPoints[i].gameObject.GetComponent<AttachmentPoint>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inHand) // Jos objekti on kädessä
        {
            if (!attachmentPoints[0].gameObject.activeSelf) 
            {
                SetPointsAsActive();
            }
            else // Ja attachmentpointit on active
            {
                for (int i = 0; i < attachmentPoints.Length; i++) // Käy kaikki attachmentpointit läpi
                {
                    if (attachmentPointScripts[i].objInRadius) // Jos attachmentpointin alueella on objekti, attach variable on true
                    {
                        attachmentPoints[i].attach = true;
                    }
                    else // Jos attachmentpointin alueella ei ole objektia, attach variable false
                    {
                        attachmentPoints[i].attach = false;
                    }
                }
            }
        }
        else
        {
            AttachOrDropObject();
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

    void SetPointsAsActive()
    {
        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            attachmentPoints[i].gameObject.SetActive(true);
        }
    }

    void AttachOrDropObject()
    {
        //Checks all points
        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            //if attach variable is false, disables attachmentpoint
            if (!attachmentPoints[i].attach)
            {
                attachmentPoints[i].gameObject.SetActive(false);
            }
            else // else if attach variable is true, attach object to objinradius
            {

            }
        }
    }

    [System.Serializable]
    public struct AttachmentPointObject
    {
        public GameObject gameObject;
        public int attachmentPointID;
        public bool objInRadius;
        public bool attach;

        public AttachmentPointObject(GameObject _gameObject, int _attachmentPointID, bool _objInRadius, bool _attach)
        {
            gameObject = _gameObject;
            attachmentPointID = _attachmentPointID;
            objInRadius = _objInRadius;
            attach = _attach;
        }
    }
}
