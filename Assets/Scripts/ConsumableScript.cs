using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableScript : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] float hunger;
    [SerializeField] float thirst;

    public Vector3 Consume()
    {
        return new Vector3(hp, hunger, thirst);
    }
}
