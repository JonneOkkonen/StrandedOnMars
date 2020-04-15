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

    void Awake ()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent <Animator> ();
		nav = GetComponent <NavMeshAgent> ();
		playerStats = player.GetComponent <PlayerStats>();
		currentHealth = startingHealth;
    }

    void Update ()
    {
		if(sink)
		{
			Debug.Log("Enemy is sinking");
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
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
		playerStats.GetPoints(points);
		Sinking();
    }        
	
	public void Sinking ()
	{
		nav.enabled = false;
		sink = true;
		Destroy (this.gameObject, 3f);
	}
}