using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	public Transform target;

	public Vector3 targetPos;

	public float x;
	public float y;
	public float z;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		targetPos = target.position + target.forward * x + Vector3.up * y;

		transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * z);

		transform.LookAt (target);
	}
}
