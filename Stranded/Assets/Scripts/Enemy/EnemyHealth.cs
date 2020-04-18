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
	NavMeshAgent nav; 
    Animator anim;
	PlayerStats playerStats;
	GameObject player;
    bool damaged;              
	public bool isDead;
	bool sink = false;
	EnemyMovement EnemyMovement;

    void Awake ()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent <Animator> ();
		nav = GetComponent <NavMeshAgent> ();
		playerStats = player.GetComponent <PlayerStats>();
		currentHealth = startingHealth;
		EnemyMovement = GetComponent<EnemyMovement>();
    }

    void Update ()
    {
		if(sink)
		{
			// Disable NavMesh
			nav.enabled = false;
			Debug.Log("Enemy is sinking");
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
    }
	
	void OnTriggerEnter(Collider other) {
        if(other.tag == "Bullet"){
			TakeDamage();
			EnemyMovement.AttackingPlayer = true;
        }
    }

    public void TakeDamage ()
    {
		if(!isDead) {
			currentHealth -= amount;
		}
	
		if(currentHealth <= 0)
		{
			Death ();
		}
    }

    void Death ()
    {
		isDead = true;
		anim.SetTrigger("Die");
		playerStats.GetPoints(points);
		Destroy (this.gameObject, 4f);
		sink = true;
    }        
}