using UnityEngine;
using System.Collections;

public class AnyButtonLevelLoad : MonoBehaviour
{
	public string Level;
	public float  InputDelay = 1f;
	
	private bool inputEnabled = false;
	
	void Start()
	{
		StartCoroutine(Enable());
		Screen.showCursor = false;
	}
	
	IEnumerator Enable()
	{
		yield return new WaitForSeconds(InputDelay);
		inputEnabled = true;
	}
	void Update()
	{
		if (inputEnabled && Input.anyKeyDown)
		{
			Application.LoadLevel(Level);
		}
	}
}
