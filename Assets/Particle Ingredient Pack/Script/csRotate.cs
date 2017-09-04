using UnityEngine;
using System.Collections;

public class csRotate : MonoBehaviour 
{
	public float XRotateSpeed;
	public float YRotateSpeed;
	public float ZRotateSpeed;

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (XRotateSpeed, YRotateSpeed, ZRotateSpeed);
	}
}
