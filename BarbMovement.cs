using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbMovement : MonoBehaviour {

	public GameObject guy;
	public Transform target;

	private GameObject[] enemies;

	private bool isRunning;

	private bool isDead;

	// Use this for initialization
	void Start () {
		// collide = false;
		isRunning = true;
		GetComponentInChildren<Animator> ().Play("Run");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update(){
		isDead = guy.GetComponent<GuyMovement>().isDead;
		if(!isDead){
			if(isRunning){
				transform.position = Vector3.MoveTowards(transform.position, guy.transform.position, 0.01f);
			}

			transform.LookAt(target.transform.position);
		}
		else
		{
			if(isRunning){
				GetComponentInChildren<Animator> ().Play("Walk");
			}
		}
		
	}

	void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.CompareTag ("Guy")) 
		{
			GetComponentInChildren<Animator> ().Play("RoundKick");
			guy.GetComponent<GuyMovement>().Die();
			StartCoroutine(SetRunning(3.0f));
		}
	}

	public IEnumerator SetRunning (float waitTime) {
		isRunning = false;
		yield return new WaitForSeconds(waitTime);
		isRunning = true;
	}
}
