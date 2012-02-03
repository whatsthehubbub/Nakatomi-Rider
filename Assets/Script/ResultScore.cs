using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class ResultScore : MonoBehaviour
{
	public string FilterTag;
	public int Priority;
	public string TextString = "{0}";
	public int ResultLevelNum = 1;
	private TextMesh _textMesh;
	private uint _kills;
	
	// Use this for initialization
	void Start()
	{
		_textMesh = GetComponent<TextMesh>();
		_textMesh.renderer.material.renderQueue = Priority;
		_textMesh.renderer.enabled = false;
		
		DontDestroyOnLoad(gameObject);
		
		Killer.OnKill += KillCount;
	}
	
	void OnDestroy()
	{
		Killer.OnKill -= KillCount;
	}
	
	void KillCount(Transform other)
	{
		if (other.CompareTag(FilterTag))
		{
			_kills++;
			_textMesh.text = string.Format(TextString, _kills);
		}
	}
	
	void OnLevelWasLoaded(int level)
	{
		if (level == ResultLevelNum)
		{
			_textMesh.renderer.enabled = true;
		}
		else if (level == 0)
		{
			Destroy(gameObject);
		}
		else
		{
			if (_textMesh != null)
				_textMesh.renderer.enabled = false;
		}
	}
}
