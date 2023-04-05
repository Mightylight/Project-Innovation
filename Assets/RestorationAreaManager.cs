using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorationAreaManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        RestorationBackup backup = other.GetComponent<RestorationBackup>();
        if(backup != null)
        {
            backup.LoadBackup();
        }
    }
}
