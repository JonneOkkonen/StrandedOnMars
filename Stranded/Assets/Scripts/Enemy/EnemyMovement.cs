using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{	
	public static bool GroupAttack = false;
	public int aggroRange;
	public int StoppingDistance;
	Animator animator;
	GameObject player;       // Reference to the player's position.
	NavMeshAgent nav;		// Reference to the nav mesh agent.
	Vector3 StartLocation;
	public float AnimationSpeed;
	public bool AttackingPlayer = false;
	
	void Awake ()
    {
		// Set StartLocation
		StartLocation = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		//shot = GetComponent<PlayerShoot>();
		// Set Animator Speed
		animator.speed = AnimationSpeed;
    }
	
    void Update ()
    {
		if(nav.enabled) {
			//Check if player is in range of destionation
			//Later add a check if player shoots the Enemy GameObject
			if(Vector3.Distance(this.transform.position, player.transform.position) <= aggroRange || AttackingPlayer || GroupAttack) 
			{
				nav.SetDestination(player.transform.position);
			}else {
				nav.SetDestination(StartLocation);
			}

			// Start/Stop Walking animation based remainingDistance
			if(nav.remainingDistance <= StoppingDistance) {
				animator.SetBool("Walk Forward", false);
			}else {
				animator.SetBool("Walk Forward", true);
			}
		}
    }
}
