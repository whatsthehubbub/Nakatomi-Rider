using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour
{
	public Transform Target;
	public float Strenght = 5f;

	// Update is called once per frame
	void Update()
	{
		if (Target != null)
		{
			Vector3 diff = Target.position - transform.position;
			
			rigidbody.AddForce(diff * Strenght);
		}
	}
}
