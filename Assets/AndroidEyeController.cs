using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidEyeController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
    }
}
