using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class StatsCounter : MonoBehaviour
{
	public string FilterTag;
	public int Priority;
	
	public string TextString = "{0}";
	private TextMesh _textMesh;
	
	private uint _kills;
	private uint _score;
	
	// Use this for initialization
	void Start()
	{
		_textMesh = GetComponent<TextMesh>();
		_textMesh.renderer.material.renderQueue = Priority;
		
		Killer.OnKill         += KillCount;
	}
	
	void OnDestroy()
	{
		Killer.OnKill         -= KillCount;
	}
	
	void KillCount(Transform other)
	{
		if (other.CompareTag(FilterTag))
		{
			_kills++;
			_textMesh.text = string.Format(TextString, _kills);
		}
	}
}
