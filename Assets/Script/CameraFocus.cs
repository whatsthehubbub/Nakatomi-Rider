using UnityEngine;
using System.Collections.Generic;

public class CameraFocus : MonoBehaviour
{

	public List<Transform> Focusers;
	public float Speed = 5f;
	private Vector3 _focuspoint;
	
	void Start()
	{
		Focusers = new List<Transform>();
		_focuspoint = transform.position;
		
		PlayerSpawner.OnSpawn += AddTransform;
		MultiInput.OnDie      += RemoveTransform;
	}
	
	void OnDestroy()
	{
		PlayerSpawner.OnSpawn -= AddTransform;
		MultiInput.OnDie      -= RemoveTransform;
	}
	
	void AddTransform(Transform p_object)
	{
		Focusers.Add(p_object);
	}
	
	void RemoveTransform(Transform p_object)
	{
		Focusers.Remove(p_object);
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if (Focusers.Count > 0)
		{
			Vector3 average = Vector3.zero;
			
			foreach (Transform t in Focusers)
			{
				average += t.position;
			}
			average /= Focusers.Count;
			float z = transform.position.z;
			
			_focuspoint = Vector3.Lerp(transform.position, average, Time.fixedDeltaTime * Speed);
			_focuspoint.z = z;
			transform.position = _focuspoint;//.Set(average.x, average.y, average.z);//transform.position.z);
		}
		
	}
}