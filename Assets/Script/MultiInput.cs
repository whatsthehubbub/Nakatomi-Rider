using UnityEngine;
using System.Collections;

public class MultiInput : MonoBehaviour
{
	public string HorizontalAxis = "Horizontal1";
	public string VerticalAxis = "Vertical1";
	public string KillSwitch = "KillSwitch1";
	public string Action = "Action1";
	public float Speed = 5f;
	public float RotateSpeed = 90f;
	public Transform Explosion;
	public AnimationCurve WingAnim;
	private float _wingRestRotation;
	public AnimationCurve Powerboost;
	private float _powerboostTime;
	//private bool _startPressed = false;
	
	
	
	// events
	public delegate void DieEvent (Transform p_object);

	public static event DieEvent OnDie;
	
	public delegate void KillSwitchReleaseEvent ();

	public static event KillSwitchReleaseEvent OnKillSwitchRelease;
	
	private Transform _wing;

	void Start()
	{
		Killer.OnKill += OnKill;
		Collectible.OnCollect += Collect;
		
		_wing = transform.Find("Parts/Wing");
		_wingRestRotation = _wing.rotation.eulerAngles.z;
		
		_powerboostTime = -1;
	}
	
	void OnDestroy()
	{
		Killer.OnKill -= OnKill;
	}
	
	void OnKill(Transform other)
	{
		if (other == transform)
		{
			Kill();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		/*if (Input.GetButton(Action))
		{
		}*/
		/**/
		Vector3 rot = _wing.rotation.eulerAngles;
		rot.z = (Input.GetButton(KillSwitch)) ? WingAnim.Evaluate(Time.realtimeSinceStartup) : _wingRestRotation;
		_wing.rotation = Quaternion.Euler(rot);
		/**/
		
		/**/
		float power;
		if (_powerboostTime < 0f)
		{
			power = Speed;
		}
		else
		{
			_powerboostTime += Time.deltaTime; 
			power = Speed * Powerboost.Evaluate(_powerboostTime);
			
			if (_powerboostTime > Powerboost.keys[Powerboost.length-1].time)
				_powerboostTime = -1;
		}
		/**/
		
		if (Input.GetButton(KillSwitch))
		{
			float xinput = Input.GetAxisRaw(HorizontalAxis);
			float yinput = -Input.GetAxisRaw(VerticalAxis);
			
			/** /
			transform.Rotate(0f, 0f, -xinput * Time.deltaTime * RotateSpeed);
			transform.Translate(0f, -yinput * Time.deltaTime * Speed, 0f);
			/**/
			Vector3 steer = Vector3.ClampMagnitude(new Vector3(xinput, yinput, 0), 1f);
			
			Debug.DrawLine(transform.position, transform.position + steer * power/5f, Color.grey);
			rigidbody.AddForce(steer * Time.fixedDeltaTime * power, ForceMode.VelocityChange);
			//transform.Translate(xinput * Time.deltaTime * Speed, yinput * Time.deltaTime * Speed, 0f);
		}
		else if (Input.GetButtonUp(KillSwitch))
		{
			if (OnKillSwitchRelease != null)
				OnKillSwitchRelease();
		}
	}
	
	void Kill()
	{
		Instantiate(Explosion, transform.position, Quaternion.identity);
		
		if (OnDie != null)
		{
			OnDie(transform);
		}
		
		Destroy(gameObject);
	}
	
	void Collect(Transform other)
	{
		_powerboostTime = 0f;
	}
}
