using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour
{
    public int attachmentPointID;

    public bool objInRadius;
    public GameObject target;

    AttachableObject parent;

    private void Start()
    {
        parent = gameObject.GetComponentInParent<AttachableObject>();
        parent.attachmentPoints[attachmentPointID].attachmentPointID = attachmentPointID;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AttachmentPoint")
        {
            objInRadius = true;

            parent.attachmentPoints[attachmentPointID].target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objInRadius = false;
        target = null;
    }
}
