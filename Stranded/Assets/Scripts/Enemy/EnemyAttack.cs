using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAttack : MonoBehaviour 
{
	
	public float timeBetweenMelee = 5f;     // The time in seconds between each melee attack.
	public float timeBetweenProjectile = 5f;  //The time in seconds between each projectile attack.
	public int meleeDamage = 10;               // The amount of health taken away per melee attack.
	public int shootDamage = 20;				//The amount of health taken away per projectile attack.
	public int meleeRange = 5;					//The range that the enemy GameObject can melee attack.
	public int shootRange = 20;					//The range that the enemy GameObject can shoot attack.
	public bool attackAbility; 					//Allows setting which attack is available for what enemy type.
	public float enemyAttackDamage = 10f;
	PlayerStats playerStats;
	Animator attackAnim;                              // Reference to the animator component.
	GameObject player;                          // Reference to the player GameObject.
	//PlayerHealth playerHealth;                  // Reference to the player's health.
	//EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	bool playerInMeleeRange;                         // Whether player is within the trigger collider and can be attacked.
	bool playerInShootRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                				// Timer counting next attack.
	string[] melee = {"Stab Attack", "Smash Attack"}; //Create array to randomize between 2 attack animations.
	EnemyHealth Health;

	void Awake () 
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		attackAnim = GetComponent <Animator> ();
		playerStats = player.GetComponent <PlayerStats>();
		Health = GetComponent<EnemyHealth>();
	}
	
	void Update ()
		{
			// Add the time since Update was last called to the timer.
			timer += Time.deltaTime;
			float dist = Vector3.Distance(player.transform.position, transform.position);
			//Debug.Log(player.transform.position);
			//print("Distance to enemy: " + dist1);
			
			//Check distance and determine if to engage in melee attack.
			if(dist < meleeRange)
			{
				playerInMeleeRange = true;
			}
			
			if(dist > meleeRange) 
			{
				playerInMeleeRange = false;
			}
			
			if(attackAbility) 
			{
				//Check distance and determine if to engage in shoot attack.
				if(dist < shootRange) 
				{
					playerInShootRange = true;
				}
			
				if(dist > shootRange) 
				{
					playerInShootRange = false;
				}
			}
			
			// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
			if(timer >= timeBetweenMelee && playerInMeleeRange && playerStats.currentHealth > 0 && !Health.isDead)
			{
				//Attack if in melee range
				Attack ();
				
			}
			
			if(timer >= timeBetweenProjectile && playerInShootRange && playerStats.currentHealth > 0)
			{
				//Attack if in shooting range.
				//Attack ();	
			}
		}


		void Attack ()
		{
			// Reset the timer.
			timer = 0f;
			
			//Debug.Log("Returns value of melee: " + (melee[Random.Range(0, 2)]));
			attackAnim.SetTrigger(melee[Random.Range(0, 2)]);
			
			attackAnim.SetTrigger("Cast Spell");
			
			playerStats.TakeDamage(meleeDamage);
		}
}