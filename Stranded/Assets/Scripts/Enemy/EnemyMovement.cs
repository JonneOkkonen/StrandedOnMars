using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public Transform player;         // Reference to the player's position.
	Transform enemy;
	NavMeshAgent nav;               // Reference to the nav mesh agent.	
	float AggroRange = 10;
	float collideRange = 6;
	
	
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        nav = GetComponent <NavMeshAgent> ();
    }
	
    void Update ()
    {
		//Check if player is in range of destionation
		//Later add a check if player shoots the Enemy GameObject
		if(Vector3.Distance(this.transform.position, player.transform.position) <= AggroRange) 
		{
			Debug.Log("Player in range");
			nav.SetDestination(player.position);
		}
    }
}
