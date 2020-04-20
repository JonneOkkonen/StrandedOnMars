using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    void Start() {
        Destroy(this.gameObject, 2f);
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy") {
            Destroy(this.gameObject);
        }
    }
}
