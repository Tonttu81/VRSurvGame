using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachableObject : MonoBehaviour
{
    [SerializeField] int objectId;

    [SerializeField] bool inHand;
    [SerializeField] bool activated;

    [SerializeField] bool attachObject;

    public AttachmentPointObject[] attachmentPoints;

    CraftingSystem craftingSystem;

    public XRGrabInteractable xrGrabInteractable;

    GameObject previewObject;
    public GameObject previewObjectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
        craftingSystem = FindObjectOfType<CraftingSystem>().GetComponent<CraftingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attachObject) 
        {
            DeActivated(); 
        }
        else
        {
            DetachObject();
        }

        if (inHand) // Jos objekti on kädessä
        {
            if (previewObject == null)
            {
                previewObject = Instantiate(previewObjectPrefab, transform.position, Quaternion.identity);
            }

            SetPointsAsActive(); // Laita attachmentpointit päälle

            if (activated) // Jos pelaaja painaa trigger nappia
            {
                if (!GetComponent<FixedJoint>())
                {
                    for (int i = 0; i < attachmentPoints.Length; i++) // Käy kaikki attachmentpointit läpi
                    {
                        if (attachmentPoints[i].target != null) // Jos attachmentpointin alueella on objekti
                        {
                            attachmentPoints[i].attach = true;
                            UpdatePreview();  // Päivittää previewobjektin ominaisuudet
                        }
                        else
                        {
                            attachmentPoints[i].attach = false;
                        }
                    }
                }
                else
                {
                    DetachObject();
                }
            }
        }
        else
        {
            SetPointsAsInactive(); // Jos objekti ei ole kädessä, attachmentpointit menevät pois päältä
        }

        if (previewObject != null)
        {
            if (attachmentPoints.All(obj => obj.target == null) || attachObject)
            {
                previewObject.GetComponent<MeshFilter>().sharedMesh = null;
            }
        }
    }

    // Näitä käytetään jotta voi ottaa ohjaimista input
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

    void SetPointsAsInactive()
    {
        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            attachmentPoints[i].gameObject.SetActive(false);
        }
    }

    void UpdatePreview()
    {
        previewObject.GetComponent<MeshFilter>().sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh; // Vaihtaa previewobjektin meshin tämän objektin meshiksi.
        previewObject.transform.localScale = transform.localScale; // Vaihtaa skaalan ja rotaation myös samaksi.
        previewObject.transform.rotation = transform.rotation;
        

        for (int i = 0; i < attachmentPoints.Length; i++)
        {
            if (attachmentPoints[i].target != null)  // Jos attachmentpointilla on target
            {
                previewObject.transform.position = transform.position; // Päivittää previewobjektin sijainnin

                // Jonka jälkeen antaa sijainnille offsetin jotta objekti olisi oikeassa kohdassa
                previewObject.transform.position -= (attachmentPoints[i].gameObject.transform.position - transform.position) - (attachmentPoints[i].target.transform.position - transform.position);
            }
        }
    }

    void AttachObject(int id)
    {
        AttachmentPointObject aPoint = attachmentPoints[id]; // Ottaa oikean attachmentpointin

        if (aPoint.attach) // Tarkistaa vielä että pitääkö objekti yhdistää
        {
            GameObject crafting = craftingSystem.CheckForRecipes(objectId, aPoint.target.GetComponentInParent<AttachableObject>().objectId);  // Tarkistaa, löytyykö crafting recipeä yhdistetyille objekteille

            //https://github.com/Unity-Technologies/XR-Interaction-Toolkit-Examples/issues/29
            xrGrabInteractable.CustomForceDrop(xrGrabInteractable.selectingInteractor);
            xrGrabInteractable.CustomForceDrop(attachmentPoints[id].target.GetComponentInParent<XRGrabInteractable>().selectingInteractor);

            if (crafting) // Jos on crafting recipe, luo tuloksen recipestä ja poistaa source objektit
            {
                Instantiate(crafting, transform.position, transform.rotation);

                //vvvvvv tässä ongelma, en tiiä mikä
                //Destroy(aPoint.target.transform.parent.gameObject); // Poistaa ensin target objektin
                //Destroy(gameObject); 
                //^^^^^^

            }
            else if (!GetComponent<FixedJoint>()) // Jos objekteille ei ole crafting recipeä ja objekti ei ole jo yhdistetty mihinkään
            {
                // Siirtää ensin objektin oikeaan kohtaan
                transform.position -= (aPoint.gameObject.transform.position - transform.position) - (aPoint.target.transform.position - transform.position);

                // Jonka jälkeen luo jointin kahden objektin välille
                FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = aPoint.target.GetComponentInParent<Rigidbody>();
            }
        }
    }

    void DetachObject()
    {
        if (GetComponent<FixedJoint>())
        {
            Destroy(GetComponent<FixedJoint>());
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
