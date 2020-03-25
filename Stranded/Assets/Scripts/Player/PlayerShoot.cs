using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject BulletSpawn;
    Rigidbody BulletRB;
    public int BulletSpeed;
    public float GunCoolDown;
    float Timer;

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Input.GetButtonDown("Fire1") && Timer >= GunCoolDown) {
            // Spawn Bullet
            GameObject CurrentBullet = GameObject.Instantiate(Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);

            // Get Bullet Rigidbody
            BulletRB = CurrentBullet.GetComponent<Rigidbody>();
            
            // Shoot Bullet
            BulletRB.velocity = -CurrentBullet.transform.up * BulletSpeed;

            // Reset Timer
            Timer = 0;
        }
    }
}
