using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
	public Transform Player;
	private Transform _spawn;
	
	public delegate void SpawnEvent(Transform p_object);
	public static event SpawnEvent OnSpawn;
	public AudioClip spawn;
	
	void Start()
	{
		Respawn();
		
		// subscribe to player death
		MultiInput.OnDie += DeathRespawn;
	}
	
	private void DeathRespawn(Transform p_object)
	{
		if (p_object == _spawn)
		{
			Respawn();
		}
	}
	
	public void Respawn()
	{
		StartCoroutine(DelayedSpawn());
	}
	
	IEnumerator DelayedSpawn()
	{
		yield return new WaitForSeconds(1.5f);
		
		_spawn = Instantiate(Player, transform.position, Quaternion.identity) as Transform;
		AudioSource.PlayClipAtPoint(spawn, transform.position);
		
		if (OnSpawn != null)
			OnSpawn(_spawn);
	}
}
