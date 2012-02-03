using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TouchCollector))]
public class Killer : MonoBehaviour
{
	public string IgnoreTag;
	
	public delegate void KillEvent(Transform p_object);
	public static event KillEvent OnKill;
	public AudioClip scream;
	
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
		if (collision.gameObject.CompareTag(IgnoreTag) == false)
		{
			AudioSource.PlayClipAtPoint(scream, transform.position);
			
			//Destroy(collision.gameObject, Delay);
			if (OnKill != null)
				OnKill(collision.transform);
		}
	}
}
