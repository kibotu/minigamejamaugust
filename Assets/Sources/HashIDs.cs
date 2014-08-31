using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {
	public int walkBool;
	public int shout;
	public int dead;
	public int win;


	void Awake()
	{
		walkBool = Animator.StringToHash("Walking");
		shout = Animator.StringToHash("Shout");
		dead = Animator.StringToHash("Dead");
		win = Animator.StringToHash("Win");
	}
}
