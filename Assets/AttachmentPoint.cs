using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour
{
    public bool objInRadius;
    public GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AttachmentPoint")
        {
            objInRadius = true;
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objInRadius = false;
        target = null;
    }
}
