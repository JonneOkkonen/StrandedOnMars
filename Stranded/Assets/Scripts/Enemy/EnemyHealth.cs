using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;          
	public int currentHealth;                       
	public int amount = 20;
	public float sinkSpeed = 0.5f;
	public int points = 10;
	public float sinkDelay;
	float Timer;
	NavMeshAgent nav; 
    Animator anim;
	PlayerStats playerStats;
	GameObject player;
    bool damaged;              
	public bool isDead;
	bool sink = false;
	EnemyMovement EnemyMovement;
	SlopeHandler SlopeHandler;
	public GameObject Bloodspat;

    void Awake ()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent <Animator> ();
		nav = GetComponent <NavMeshAgent> ();
		playerStats = player.GetComponent <PlayerStats>();
		currentHealth = startingHealth;
		EnemyMovement = GetComponent<EnemyMovement>();
		SlopeHandler = GetComponentInChildren(typeof(SlopeHandler), true) as SlopeHandler;
    }

    void Update ()
    {
		if(sink)
		{
			Timer += Time.deltaTime;
			if(Timer >= sinkDelay) {
				transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
			}
		}
    }

	// Get Collision Point
	void OnCollisionEnter(Collision collision){
		// Count as damage if magnitude is more than 20
		if(collision.relativeVelocity.magnitude > 20) {
			// Take Damage
			TakeDamage();
			// Attack Player on hit
			EnemyMovement.AttackingPlayer = true;
			// Get collision Point
			ContactPoint contact = collision.GetContact(0);
			// Instantiate Bloodspat
			GameObject blood = GameObject.Instantiate(Bloodspat, contact.point, new Quaternion(0,0,0,0));
			// Destroy it after 1.5s
			Destroy(blood.gameObject, 1.5f);
		}
	}

    public void TakeDamage ()
    {
		if(!isDead) {
			currentHealth -= amount;
		}
	
		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
    }

    void Death ()
    {
		// Disable Slope Handler
		SlopeHandler.enabled = false;
		// Set isDead to true
		isDead = true;
		// Disable Nav
		nav.enabled = false;
		// Play Die Animation
		anim.SetTrigger("Die");
		// Give Player points
		playerStats.GetPoints(points);
		// Destroy Gameobject with delay
		Destroy (this.gameObject, 4f);
		// Start sinking
		sink = true;
    }        
}