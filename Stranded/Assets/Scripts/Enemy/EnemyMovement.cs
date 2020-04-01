using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public Transform player;         // Reference to the player's position.
	//public Transform enemy;
	NavMeshAgent nav;               // Reference to the nav mesh agent.	
	float aggroRange = 10;
	float collisionRange = 5;
	//float collideRange = 6;
	Animator moveAnim;
	
	
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
		//player = GameObject.FindGameObjectWithTag ("Enemy").transform;
        nav = GetComponent <NavMeshAgent> ();
		moveAnim = GetComponent <Animator> ();
    }
	
    void Update ()
    {
		//Check if player is in range of destionation
		//Later add a check if player shoots the Enemy GameObject
		if(Vector3.Distance(this.transform.position, player.transform.position) <= aggroRange) 
		{
			//Debug.Log("Player in range");
			nav.SetDestination(player.position);
			WalkForward();	
		}
		
		if(Vector3.Distance(this.transform.position, player.transform.position) >= aggroRange) 
		{
			//Debug.Log("Player in range");
			ReturnIdle();	
		}
		
		
		
		/*if(player.shoots) 
		{
			nav.SetDestination(play.position);
		}*/
    }
	
	void WalkForward () 
	{
		moveAnim.SetBool("Walk Forward", true);
	}
	
	void ReturnIdle () 
	{
		moveAnim.SetBool("Walk Forward", false);
	}
}
