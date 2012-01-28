using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TouchCollector))]
public class Killer : MonoBehaviour
{
	public string FilterTag;
	
	public delegate void KillEvent(Transform p_object);
	public static event KillEvent OnKill;
	
	TouchCollector _touch;
	
	void Start()
	{
		_touch = GetComponent<TouchCollector>();
		_touch.OnCollide     += Collide;
	}
	
	void OnDestroy()
	{
		_touch.OnCollide -= Collide;
	}
	
	// Update is called once per frame
	void Collide(Collision collision)
	{
		if (collision.gameObject.CompareTag(FilterTag))
		{
			//Destroy(collision.gameObject, Delay);
			if (OnKill != null)
				OnKill(collision.transform);
		}
	}
}
