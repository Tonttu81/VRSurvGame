using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour
{
    public int attachmentPointID;

    AttachableObject parent;

    private void Start()
    {
        // Ottaa attachmentpointin parent objektin ja päivittää parent objektiin attachmentpoint id:n
        
        parent = gameObject.GetComponentInParent<AttachableObject>();
        parent.attachmentPoints[attachmentPointID].attachmentPointID = attachmentPointID;
    }

    private void OnTriggerStay(Collider other)  // Jos attachmentpointin alueella on objekti, päivittää pää-skriptin kohde-objekti arvon
    {
        if (other.tag == "AttachmentPoint")
        {
            parent.attachmentPoints[attachmentPointID].target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)  // Jos attachmentpointin alueella ei ole objektia, päivittää kohde-objektin arvon tyhjäksi
    {
        parent.attachmentPoints[attachmentPointID].target = null;
    }
}
