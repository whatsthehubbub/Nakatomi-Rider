using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Attract))]
public class DynamicSpring : MonoBehaviour
{
	private Attract _spring;
	public string targetTag = "Player1";
	public bool   BeamVisible = false;
	public MagicBeam Beam;
	private MagicBeam _beamInstance;
	
	void Start()
	{
		MultiInput.OnDie += Died;
		_spring = GetComponent<Attract>();
		
		if (Beam != null)
		{
			_beamInstance = Instantiate(Beam, transform.position, Quaternion.identity) as MagicBeam;
			_beamInstance.transform.localScale = Vector3.zero;
		}
	}
	
	void OnDestroy()
	{
		if (Beam != null)
			Destroy(_beamInstance);
	}
	
	void Died(Transform other)
	{
		if (other == _spring.Target)
		{
			_spring.Target = null;
		}
		
		if (_beamInstance != null)
		{
			_beamInstance.transform.localScale = Vector3.zero;
		}
	}
	
	Vector3 _scale = Vector3.one;
	Vector3 _rotation = Vector3.zero;
	
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
				
				if (Beam != null)
				{
					_beamInstance.A = transform;
					_beamInstance.B = _spring.Target;
				}
			}
		}
	}
}
