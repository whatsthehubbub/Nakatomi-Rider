using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TouchCollector))]
public class Collectible : MonoBehaviour
{
	public string IgnoreTag;
	public float Boost = 10f;
	public delegate void CollectEvent (Transform p_object);
	public static event CollectEvent OnCollect;
	public AudioClip moan;
	
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
		if (other.gameObject.CompareTag(IgnoreTag) == false)
		{
			AudioSource.PlayClipAtPoint(moan, transform.position);

			if (OnCollect != null)
				OnCollect(other.transform);
			
			/*if (other.gameObject.rigidbody)
			{
				rigidbody.AddForce(transform.TransformDirection(transform.forward * Boost), ForceMode.Impulse);
			}*/
			
			Destroy(gameObject);
		}	
	}
}
