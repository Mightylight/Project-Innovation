using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoodooPin : MonoBehaviour
{
    [Tooltip("In what body part should the pin go?")]
    public VoodooPartEnum pinPart;
    private Rigidbody rigidbody;

    public void ToggleKinematicRigid(bool toggle)
    {
        rigidbody.isKinematic = toggle;
    }
}
