using UnityEngine;
using System.Collections;

public class MultiInput : MonoBehaviour
{
	public string HorizontalAxis = "Horizontal1";
	public string VerticalAxis = "Vertical1";
	public string KillSwitch = "KillSwitch1";
	public string Action = "Action1";
	public float Speed = 5f;
	
	public Transform Explosion;
		
	private bool _startPressed = false;
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton(Action))
		{
			Kill();
		}
		
		if (Input.GetButton(KillSwitch))
		{
			float xinput = Input.GetAxisRaw(HorizontalAxis);
			float yinput = Input.GetAxisRaw(VerticalAxis);
		
			transform.Translate(xinput * Time.deltaTime * Speed, yinput * Time.deltaTime * Speed, 0f);
			
			_startPressed = true;
		}
		else if (_startPressed == true)
		{
			Kill();
		}
	}
	
	void Kill()
	{
		Instantiate(Explosion, transform.position, Quaternion.identity);
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
