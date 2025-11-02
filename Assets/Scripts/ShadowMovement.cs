using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class ShadowMovement : MonoBehaviour
{
	public Transform player;
	NavMeshAgent agent;
	MeshRenderer MR;
	
	void Start()
	{
		MR = GetComponent<MeshRenderer>();
		agent = GetComponent<NavMeshAgent>();
		
		MR.enabled = false;
	}
	void Update()
	{
		if (Vector3.Distance(transform.position, player.transform.position) < 7)
		{
			MR.enabled = true;
			agent.speed = 1;
		}
		else if (Vector3.Distance(transform.position, player.transform.position) > 7 && Vector3.Distance(transform.position, player.transform.position) < 15)
		{
			MR.enabled = false;
			agent.speed = 0.2f;
		}
		else
		{
			MR.enabled = false;
			agent.speed = 0;
		}
		agent.SetDestination(player.position);
	}
}
