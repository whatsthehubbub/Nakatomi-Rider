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
		
	private bool _startPressed = false;
	private bool _attackAllowed = false;
	
	// events
	public delegate void DieEvent(Transform p_object);
	public static event DieEvent OnDie;

	void Start()
	{
		Killer.OnKill += OnKill;
	}
	
	void OnDestroy()
	{
		Killer.OnKill -= OnKill;
	}
	
	void OnTouch(Collider other)
	{
		if (other.gameObject != gameObject)
		{
			_attackAllowed = true;
		}
	}
	
	void OnUnTouch(Collider other)
	{
		if (other.gameObject != gameObject)
		{
			_attackAllowed = false;
		}
	}
	
	void OnKill(Transform other)
	{
		if (other == transform )
		{
			Kill();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if (Input.GetButton(Action))
		{
			if (_attackAllowed)
			{
				Debug.DrawLine(Vector3.zero, transform.position, Color.red);
				Kill();
			}
		}
		
		if (Input.GetButton(KillSwitch))
		{
			float xinput = Input.GetAxisRaw(HorizontalAxis);
			float yinput = -Input.GetAxisRaw(VerticalAxis);
			
			/** /
			transform.Rotate(0f, 0f, -xinput * Time.deltaTime * RotateSpeed);
			transform.Translate(0f, -yinput * Time.deltaTime * Speed, 0f);
			/**/
			
			//transform.Translate(xinput * Time.deltaTime * Speed, yinput * Time.deltaTime * Speed, 0f);
			Debug.DrawLine(transform.position, transform.position + new Vector3(xinput, yinput, 0), Color.grey);
			rigidbody.AddForce(xinput * Time.fixedDeltaTime * Speed, yinput * Time.fixedDeltaTime * Speed, 0f, ForceMode.VelocityChange);
			_startPressed = true;
		}
		else if (_startPressed == true)
		{
			//Kill();
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
	 
	/*
	void OnGUI()
	{
		
		float y = 20f;
		float step = 25f;
		GUI.Label(new Rect(0f, y, 300f, 20f), string.Format("KS1: {0}", Input.GetButton("KillSwitch1"))); 
		y += step;
		GUI.Label(new Rect(0f, y, 300f, 20f), string.Format("KS2: {0}", Input.GetButton("KillSwitch2"))); 
		y += step;
		GUI.Label(new Rect(0f, y, 300f, 20f), string.Format("A1: {0}", Input.GetButton("Action1"))); 
		y += step;
		GUI.Label(new Rect(0f, y, 300f, 20f), string.Format("A2: {0}", Input.GetButton("Action2"))); 
		y += step;
		
	}
	*/
}
