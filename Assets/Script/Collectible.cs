using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TouchCollector))]
public class Collectible : MonoBehaviour
{
	public string FilterTag;
	
	public delegate void CollectEvent (Transform p_object);
	public static event CollectEvent OnCollect;
	
	private TouchCollector _touch;
	
	void Start()
	{
		_touch = GetComponent<TouchCollector>();
		_touch.OnTouch += Collect;
	}
	
	void OnDestroy()
	{
		_touch.OnTouch -= Collect;
	}
	
	void Collect(Collider other)
	{
		if (other.gameObject.CompareTag(FilterTag))
		{
			if (OnCollect != null)
				OnCollect(other.transform);
			
			Destroy(gameObject);
		}	
	}
}
