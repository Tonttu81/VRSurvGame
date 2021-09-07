using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController; 
    public InputDeviceCharacteristics controllerCharacteristics;

    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject SpawnedController;
    private GameObject SpawnedHandModel;

    private Animator HandAnimator;

    private void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                SpawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find corresponding controller");
                SpawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            SpawnedHandModel = Instantiate(handModelPrefab, transform);
            HandAnimator = SpawnedHandModel.GetComponent<Animator>();

        }
    }

    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            HandAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            HandAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float gripValue))
        {
            HandAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            HandAnimator.SetFloat("Grip", 0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                SpawnedHandModel.SetActive(false);
                SpawnedController.SetActive(true);
            }
            else
            {
                SpawnedHandModel.SetActive(true);
                SpawnedHandModel.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }
}
