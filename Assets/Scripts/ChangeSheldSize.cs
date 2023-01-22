using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSheldSize : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
}
