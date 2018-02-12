using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GuyMovement : MonoBehaviour {

	private Rigidbody rb;
	public GameObject EnemyObject;
	public Transform enemy;
	private bool isFiring;
	public bool isDead;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		isFiring = false;
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isDead){
			transform.LookAt(enemy.transform.position);

			float x = CrossPlatformInputManager.GetAxis ("Horizontal");
			float z = CrossPlatformInputManager.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (x, 0, z);

			rb.velocity = movement * 1f;

			if(!isFiring){
				if (x != 0 && z != 0) {
					transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.Atan2(x, z) * Mathf.Rad2Deg, transform.eulerAngles.z);
				}

				if (x != 0 || z != 0) {
					GetComponentInChildren<Animator> ().Play("Walk");
				}
				else 
				{
					GetComponentInChildren<Animator> ().Play("Idle");
				}
			}
		}
	}

	public void Fire () {
		if(!isFiring && !isDead){
			GetComponentInChildren<Animator> ().Play("Shoot");
			StartCoroutine(EnemyObject.GetComponent<BarbMovement>().SetRunning(1.0f));
			StartCoroutine(SetFiring());
		}
	}

	IEnumerator SetFiring () {
		isFiring = true;
		yield return new WaitForSeconds(1.0f);
		isFiring = false;
	}

	public void Die () {
		GetComponentInChildren<Animator> ().Play("Die");
		isDead = true;
	}
}
