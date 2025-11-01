using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class ShadowMovement : MonoBehaviour
{
	public Transform player;
	NavMeshAgent agent;
	
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}
	void Update()
	{
		agent.SetDestination(player.position);
	}
}
