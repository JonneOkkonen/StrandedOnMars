using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAttack : MonoBehaviour 
{
	
	public float timeBetweenMelee = 0.5f;     // The time in seconds between each melee attack.
	public float timeBetweenProjectile = 5f;  //The time in seconds between each projectile attack.
	public int attackDamage = 10;               // The amount of health taken away per attack.
	public int attackRange = 5;
	
	Animator anim;                              // Reference to the animator component.
	GameObject player;                          // Reference to the player GameObject.
	//PlayerHealth playerHealth;                  // Reference to the player's health.
	//EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                				// Timer counting next attack.
	
	
	void Awake () 
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		//playerHealth = player.GetComponent <PlayerHealth> ();
		//enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent <Animator> ();
	}
	
	void Update ()
		{
			// Add the time since Update was last called to the timer.
			timer += Time.deltaTime;
			float dist1 = Vector3.Distance(player.transform.position, transform.position);
			Debug.Log(player.transform.position);
			print("Distance to enemy: " + dist1);
			
			if(dist1 < attackRange)
			{
				playerInRange = true;
			}
			
			if(dist1 > attackRange) 
			{
				playerInRange = false;
			}

			// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
			if(timer >= timeBetweenMelee && playerInRange /*&& enemyHealth.currentHealth > 0*/)
			{
				// ... attack.
				Attack ();
				
			}

			/*// If the player has zero or less health...
			if(playerHealth.currentHealth <= 0)
			{
				// ... tell the animator the player is dead.
				anim.SetTrigger ("PlayerDead");
			}*/
		}


		void Attack ()
		{
			// Reset the timer.
			timer = 0f;
			anim.SetTrigger("Smash Attack");

			/*// If the player has health to lose...
			if(playerHealth.currentHealth > 0)
			{
				// ... damage the player.
				playerHealth.TakeDamage (attackDamage);
			}*/
		}
}