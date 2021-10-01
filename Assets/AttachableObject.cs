using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AttachableObject : MonoBehaviour
{
    [SerializeField] bool inHand;
    [SerializeField] bool activated;

    [SerializeField] bool attachObject;

    public AttachmentPointObject[] attachmentPoints;

    FixedJoint[] joints;
    
    public GameObject previewObject;

    // Start is called before the first frame update
    void Start()
    {
        /*
        joints = new FixedJoint[attachmentPoints.Length];

        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            joints[i] = gameObject.AddComponent<FixedJoint>();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (attachObject)
        {
            DeActivated();
        }

        if (inHand) // Jos objekti on kädessä
        {
            if (activated)
            {
                if (!attachmentPoints[0].gameObject.activeSelf)
                {
                    SetPointsAsActive();
                }
                else // Ja attachmentpointit on active
                {
                    for (int i = 0; i < attachmentPoints.Length; i++) // Käy kaikki attachmentpointit läpi
                    {
                        if (attachmentPoints[i].target != null) // Jos attachmentpointin alueella on objekti, attach variable on true
                        {
                            attachmentPoints[i].attach = true;
                            UpdatePreview();
                        }
                        else // Jos attachmentpointin alueella ei ole objektia, attach variable false
                        {
                            attachmentPoints[i].attach = false;
                        }
                    }
                }
            }
        }

        if (attachmentPoints.All(obj => obj.target == null))
        {
            previewObject.GetComponent<MeshFilter>().sharedMesh = null;
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

    public void Activated()
    {
        activated = true;
    }

    public void DeActivated()
    {
        activated = false;
        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            if (attachmentPoints[i].attach)
            {
                AttachObject(i);
            }
        }
    }

    void SetPointsAsActive()
    {
        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            attachmentPoints[i].gameObject.SetActive(true);
        }
    }

    void UpdatePreview()
    {
        previewObject.GetComponent<MeshFilter>().sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        previewObject.transform.localScale = transform.localScale;
        previewObject.transform.rotation = transform.rotation;
        

        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            if (attachmentPoints[i].target != null)
            {
                previewObject.transform.position = transform.position;

                previewObject.transform.position -= (attachmentPoints[i].gameObject.transform.position - transform.position) - (attachmentPoints[i].target.transform.position - transform.position);
            }
        }
    }

    void AttachObject(int id)
    {
        if (attachmentPoints[id].attach)
        {
            if (GetComponents<FixedJoint>().Length < 1)
            {
                transform.position -= (attachmentPoints[id].gameObject.transform.position - transform.position) - (attachmentPoints[id].target.transform.position - transform.position);

                FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = attachmentPoints[id].target.GetComponentInParent<Rigidbody>();
            }
        }
        
    }

    [System.Serializable]
    public struct AttachmentPointObject
    {
        public GameObject gameObject;
        public int attachmentPointID;
        public GameObject target;
        public bool attach;

        public AttachmentPointObject(GameObject _gameObject, int _attachmentPointID, GameObject _target, bool _attach)
        {
            gameObject = _gameObject;
            attachmentPointID = _attachmentPointID;
            target = _target;
            attach = _attach;
        }
    }
}
