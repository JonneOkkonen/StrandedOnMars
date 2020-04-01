using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{	
	public int aggroRange;
	public int collisionRange;
	public int StoppingDistance;
	Animator animator;
	GameObject player;       // Reference to the player's position.
	NavMeshAgent nav;		// Reference to the nav mesh agent.
	
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
    }
	
    void Update ()
    {
		//Check if player is in range of destionation
		//Later add a check if player shoots the Enemy GameObject
		if(Vector3.Distance(this.transform.position, player.transform.position) <= aggroRange) 
		{
			//Debug.Log("Player in range");
			nav.SetDestination(player.transform.position);
			if(nav.remainingDistance <= StoppingDistance) {
				animator.SetBool("Walk Forward", false);
			}else {
				animator.SetBool("Walk Forward", true);
			}
		}
    }
}
