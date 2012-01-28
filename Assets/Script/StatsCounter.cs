using UnityEngine;
using System.Collections;

public class StatsCounter : MonoBehaviour
{
	public string FilterTag;
	public Color GUIColor = Color.red;
	public Vector2 GUIPos = Vector2.zero;
	
	private uint _kills;
	private uint _score;
	
	// Use this for initialization
	void Start()
	{
		Killer.OnKill         += KillCount;
		Collectible.OnCollect += ScoreCount;;
	}
	
	void OnDestroy()
	{
		Killer.OnKill         -= KillCount;
		Collectible.OnCollect -= ScoreCount;;
	}
	
	void KillCount(Transform other)
	{
		if (other.CompareTag(FilterTag))
		{
			_kills++;
		}
	}
	
	void ScoreCount(Transform other)
	{
		if (other.CompareTag(FilterTag))
		{
			_score++;
		}
	}

	
	void OnGUI()
	{
		Rect r = new Rect(GUIPos.x, GUIPos.y, 200f, 20f);
		GUI.color = GUIColor;
		GUI.Label(r, string.Format("Deaths: {0}", _kills)); r.y += r.height;
		GUI.Label(r, string.Format("Score:  {0}", _score)); r.y += r.height;
	}
}
