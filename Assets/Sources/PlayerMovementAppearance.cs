using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementAppearance : MonoBehaviour {

	[Range(0f, 30f)]
	public float angularSpeed = 1f;
	[Range(0f, 30f)]
	public float speed = 1f;
	[Range(0f, 5f)]
	public float startDelay = 1f;
	private float targetAngle;
	public Vector3 targetPosition;
	private List<float> targetRotations = new List<float>();
	public direction currentDirection;
	private HashIDs hash;
	private Animator anim;

    public delegate void FinishMoving();

    public static event FinishMoving finished;

    public GameObject player;
	public enum direction{
		up, down, left, right
	}

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
		targetRotations.Add(0);
		targetRotations.Add(90f);
		targetRotations.Add(180f);
		targetRotations.Add(270);
		targetPosition = player.transform.position;
		switchDirection(currentDirection);
		hash = GetComponent<HashIDs>();
	}
	
	// Update is called once per frame
	void Update () {
		smoothToTarget();
		nearTarget();
	}
	private void nearTarget(){
	    if (Vector3.Distance(targetPosition, player.transform.position) < 0.1f)
	    {
	        anim.SetBool(hash.walkBool, false);
	        finished();
	    }
	}
	private IEnumerator setTargetAndWalk(Vector3 newPos){
		yield return new WaitForSeconds(startDelay);
		targetPosition = newPos;
		anim.SetBool(hash.walkBool, true);
	}
	private void smoothToTarget(){
        float desiredAngle = Mathf.LerpAngle(player.transform.rotation.eulerAngles.y, targetAngle, Time.deltaTime * angularSpeed);
        player.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, desiredAngle, player.transform.rotation.eulerAngles.z);
        player.transform.position = Vector3.Slerp(player.transform.position, targetPosition, Time.deltaTime * speed);
	}
	private void switchDirection(direction TargetLookDirection){
		currentDirection = TargetLookDirection;

		switch(TargetLookDirection)
		{
		case direction.up:
			targetAngle = targetRotations[0];
			break;
		case direction.down:
			targetAngle = targetRotations[2];
			break;
		case direction.left:
			targetAngle = targetRotations[3];
			break;
		case direction.right:
			targetAngle = targetRotations[1];
			break;
		}
	}
	public void goTo(Vector3 newTargetPos, direction newDesiredDirection){
		switchDirection(newDesiredDirection);
		StartCoroutine(setTargetAndWalk(newTargetPos));
	}
	public void shout(){
		anim.SetTrigger(hash.shout);
	}
	public void die(){
		anim.SetTrigger(hash.dead);
	}
	public void win(){
		anim.SetTrigger(hash.win);
	}

}
