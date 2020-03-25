using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramRotate : MonoBehaviour
{
    public int Speed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * Speed, 0);
    }
}
