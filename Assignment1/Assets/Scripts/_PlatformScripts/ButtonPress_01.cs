﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress_01 : MonoBehaviour
{

	public Animator animator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			animator.SetBool("buttonPressed", true);
		}
	}
}
