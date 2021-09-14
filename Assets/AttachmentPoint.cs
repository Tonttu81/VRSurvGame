using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour
{
    public bool objInRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AttachmentPoint")
        {
            objInRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objInRadius = false;
    }
}
