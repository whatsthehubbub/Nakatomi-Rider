using UnityEngine;
using System.Collections;

public class ShoulderButtonLevelLoad : MonoBehaviour
{
	public string Level;
	public float  InputDelay = 1f;
	
	private bool inputEnabled = false;
	
	void Start()
	{
		StartCoroutine(Enable());
	}
	
	IEnumerator Enable()
	{
		yield return new WaitForSeconds(InputDelay);
		inputEnabled = true;
	}
	
	void Update()
	{
		if (inputEnabled && 
			Input.GetButton("KillSwitch1") &&
			Input.GetButton("KillSwitch2"))
		{
			Application.LoadLevel(Level);
		}
	}
}
