using UnityEngine;
using System.Collections.Generic;

public class TouchCollector : MonoBehaviour
{
	public delegate void TouchEvent(Collider other);
	public event TouchEvent OnTouch;
	public event TouchEvent OnUnTouch;
	
	public event TouchEvent OnFirstTouch;
	public event TouchEvent OnLastUnTouch;
	
	public delegate void CollisionEvent(Collision other);
	public event CollisionEvent OnCollide;
	
	private HashSet<Collider> _touchers;
	
	void Awake()
	{
		_touchers = new HashSet<Collider>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (_touchers.Count == 0)
		{
			if (OnFirstTouch != null)
				OnFirstTouch(other);
		}
		
		if (OnTouch != null)
			OnTouch(other);
		
		_touchers.Add(other);
	}
	
	void OnTriggerExit(Collider other)
	{
		if (OnUnTouch != null)
			OnUnTouch(other);

		_touchers.Remove(other);
		if (_touchers.Count == 0)
		{
			if (OnLastUnTouch != null)
				OnLastUnTouch(other);
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (OnCollide != null)
			OnCollide(collision);
	}
}
