using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Attract))]
public class DynamicSpring : MonoBehaviour
{
	private Attract _spring;
	
	public string targetTag = "Player1";
	
	// Use this for initialization
	void Start()
	{
		MultiInput.OnDie += Died;
		_spring = GetComponent<Attract>();
	}
	
	void Died(Transform other)
	{
		if (other == _spring.Target)
		{
			_spring.Target = null;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		// keep looking for a target as long as we don't have
		if (_spring.Target == null)
		{
			GameObject target = GameObject.FindWithTag(targetTag);
			if (target != null)
			{
				_spring.Target = target.transform;
			}
		
		}
		else
		{
			Debug.DrawLine(transform.position, _spring.Target.position, Color.yellow);
		}
	}
}
