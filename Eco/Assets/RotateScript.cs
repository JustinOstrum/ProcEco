using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Rotate(Vector3.up, 90 * Time.deltaTime /2);
    }
}
