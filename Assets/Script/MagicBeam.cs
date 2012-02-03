using UnityEngine;
using System.Collections;

public class MagicBeam : MonoBehaviour
{
	public Transform A;
	public Transform B;
	Vector3 _scale = Vector3.one;
	Vector3 _rotation = Vector3.zero;
	
	void Start()
	{
		_scale.y = 0.5f;
	}
	
	void Update()
	{
		if (A != null && B != null)
		{
			transform.position = (A.position + B.position) / 2.0f;
			_scale.x = Vector3.Distance(A.position, B.position);
			_rotation.z = Mathf.Atan2(B.position.y - A.position.y, B.position.x - A.position.x) * Mathf.Rad2Deg;
		
			//Debug.DrawLine(transform.position, _spring.Target.position, Color.yellow);
			transform.localScale = _scale;
			transform.rotation = Quaternion.Euler(_rotation);
		}
	}
}
