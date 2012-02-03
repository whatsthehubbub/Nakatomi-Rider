using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class DelayedLevelLoad : MonoBehaviour
{
	public string Level;
	public float  LoadDelay = 1f;
	public int Priority = 0;
	
	private float endTime;
	public string TextString = "{0}";
	private TextMesh _textMesh;
	
	void Start()
	{
		_textMesh = GetComponent<TextMesh>();
		_textMesh.renderer.material.renderQueue = Priority;
		endTime = Time.realtimeSinceStartup + LoadDelay;
	}
	/*
	IEnumerator Enable()
	{
		yield return new WaitForSeconds(InputDelay);
		Application.LoadLevel(Level);
	}
	*/
	void Update()
	{
		int remaining = (int) (endTime - Time.realtimeSinceStartup);
		if (remaining < 0)
		{
			Application.LoadLevel(Level);
		}
		else
		{
			_textMesh.text = string.Format(TextString, remaining);
		}
	}
}
