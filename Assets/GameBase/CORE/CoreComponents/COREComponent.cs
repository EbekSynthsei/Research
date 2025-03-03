using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COREComponent : MonoBehaviour
{
    protected CORE Core;
    protected virtual void Awake()
    {
        Core = transform.parent.GetComponent<CORE>();
        if(Core == null)
        {
            Debug.LogError("No Core On Parent", this);
        }

    }
}
