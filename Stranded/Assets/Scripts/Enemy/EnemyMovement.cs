using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public Transform player;         // Reference to the player's position.
	NavMeshAgent nav;               // Reference to the nav mesh agent.	
	
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        nav = GetComponent <NavMeshAgent> ();
    }
	
    void Update ()
    {
		nav.SetDestination(player.position);	
		//nav.SetDestination (player.position);
		//nav.enabled = false;
    }
}
