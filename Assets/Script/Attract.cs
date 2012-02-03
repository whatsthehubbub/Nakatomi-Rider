using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class Attract : MonoBehaviour
{
	public Transform Target;
	public float Strenght = 5f;
	public float ReleasePenaltyStrength = 10f;
	
	public Vector3? _penaltyForce = null;
	
	void Start()
	{
		MultiInput.OnKillSwitchRelease += Penalty;
	}
	
	void OnDestroy()
	{
		MultiInput.OnKillSwitchRelease -= Penalty;
	}
	
	void Penalty()
	{
		if (Target != null)
		{
			_penaltyForce = (Target.position - transform.position) * ReleasePenaltyStrength;
			//rigidbody.AddForce(diff * ReleasePenaltyStrength);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if (Target != null)
		{
			Vector3 diff = Target.position - transform.position;
			
			rigidbody.AddForce(diff * Strenght);
			
			if (_penaltyForce.HasValue)
			{
				rigidbody.AddForce(_penaltyForce.Value);
				_penaltyForce = null;
			}
		}
	}
}
