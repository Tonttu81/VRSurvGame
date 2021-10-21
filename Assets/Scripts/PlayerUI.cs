using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    float hunger;
    float thrist;

    [SerializeField]Slider hungerBar;
    [SerializeField]Slider thirstBar;

    // Start is called before the first frame update
    void Start()
    {
        thrist = thirstBar.maxValue;
        hunger = hungerBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        hunger -= Time.deltaTime;
        thrist -= Time.deltaTime;

        hungerBar.value = hunger;
        thirstBar.value = thrist;
    }
}
