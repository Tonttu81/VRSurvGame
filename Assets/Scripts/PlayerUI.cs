using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public float hunger;
    public float thirst;

    [SerializeField]Slider hungerBar;
    [SerializeField]Slider thirstBar;

    // Start is called before the first frame update
    void Start()
    {
        thirst = thirstBar.maxValue;
        hunger = hungerBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        hunger -= Time.deltaTime;
        thirst -= Time.deltaTime;

        hungerBar.value = hunger;
        thirstBar.value = thirst;

        if (hunger > 100)
        {
            hunger = 100f;
        }
        if (thirst > 100)
        {
            thirst = 100f;
        }
    }
}
