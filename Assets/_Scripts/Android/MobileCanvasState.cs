using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.SerializableAttribute]
public class MobileCanvasState : MonoBehaviour
{
    [SerializeField] public MobileState state;
    [SerializeField] List<GameObject> stateObjects;

    public void OnStateEnter()
    {
        foreach (GameObject obj in stateObjects)
        {
            obj.SetActive(true);
        }
    }

    public void OnStateExit()
    {
        foreach(GameObject obj in stateObjects)
        {
            obj.SetActive(false);
        }
    }
}
[System.Serializable] public enum MobileState { MAIN_MENU, LOGIN, JOINING, TUTORIAL, PLAYING, DISCONNECTED, WON, LOST }
