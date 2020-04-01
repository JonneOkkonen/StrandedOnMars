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
	//Animator moveAnim;
	//public bool moveForward = false;
	
	
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
		//player = GameObject.FindGameObjectWithTag ("Enemy").transform;
        nav = GetComponent <NavMeshAgent> ();
		//moveAnim = GetComponent <Animator> ();
    }
	
    void Update ()
    {
		//Check if player is in range of destionation
		//Later add a check if player shoots the Enemy GameObject
		if(Vector3.Distance(this.transform.position, player.transform.position) <= aggroRange) 
		{
			//Debug.Log("Player in range");
			nav.SetDestination(player.position);
			//Animate();	
		}
		
		
		
		/*if(Vector3.Distance(this.transform.position, enemy.transform.position) <= collisionRange) 
		{
			Debug.Log("Enemy in range");
			Vector3 createDist = transform.position;
			createDist.x = 10.0f;
			transform.position = createDist;
		}*/
		
		/*if(player.shoots) 
		{
			nav.SetDestination(play.position);
		}*/
    }
	
	/*void Animate () 
	{
		moveAnim = true;
	}*/
}
