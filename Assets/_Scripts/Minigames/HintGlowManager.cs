using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


/// <summary>
/// This is the manager for the glowObject. 
/// It will spawn in 
/// </summary>
public class HintGlowManager : MonoBehaviour
{
    [SerializeField] float timeAlive;
    void Start()
    {
        StartCoroutine(DieAfterTime());
    }

    IEnumerator DieAfterTime()
    {
        //Potentially we could do some animation in here and put this in a while loop
        yield return new WaitForSeconds(timeAlive);
        Destroy(this.gameObject);
    }
}
