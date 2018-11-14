using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + 90, 0, transform.eulerAngles.z);
    }

}
