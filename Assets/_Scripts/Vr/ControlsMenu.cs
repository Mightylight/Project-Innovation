using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private InputActionProperty _onAButtonPressed;
    [SerializeField] private List<GameObject> _controllerUIList;

    private void Update()
    {
        if (!_onAButtonPressed.action.WasPressedThisFrame()) return;
        foreach (GameObject pObject in _controllerUIList)
        {
            pObject.SetActive(!pObject.activeSelf);
        }
    }
}
