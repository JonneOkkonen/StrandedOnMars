using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;                            // The amount of health the enemey starts the game with.
    public int currentHealth;                                   // The current health the enemey has.
	public int amount = 20;
	//public float sinkSpeed = 2.5f; 


    Animator anim;                                              // Reference to the Animator component.                           // Reference to the EnemyAttacking script.
    bool damaged;                                               // True when the enemey gets damaged.
	bool isDead;
	//bool isSinking; 


    void Awake ()
    {
		anim = GetComponent <Animator> ();
		currentHealth = startingHealth;
    }


    void Update ()
    {
        /*if(isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}*/
    }
	
	void OnTriggerEnter(Collider other) {
        if(other.tag == "Bullet"){
			Debug.Log("Bullet hits the target");
			TakeDamage();
        }
		
    }

    public void TakeDamage ()
    {
		currentHealth -= amount;
		
		if(isDead)
			return;
	
       if(currentHealth <= 0)
		{
			Death ();
		}
    }


    void Death ()
    {
      isDead = true;
	  anim.SetTrigger("Die");
    }        
	
	/*public void StartSinking ()
	{
		GetComponent <NavMeshAgent> ().enabled = false;

		GetComponent <Rigidbody> ().isKinematic = true;

		isSinking = true;

		Destroy (gameObject, 2f);
	}*/
}