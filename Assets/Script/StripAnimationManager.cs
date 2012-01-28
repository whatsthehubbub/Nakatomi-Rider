using UnityEngine;
using System.Collections;

[System.Serializable]
public class StripAnimation
{
	public int NumberOfCells = 1;
	public int CellSize      = 16; 

	public int PlaybackSpeed = 10;
	public Texture StripTexture;

	public bool Loop = false;
	public int NextAnimation = 0;
	
	internal int m_frame;
	internal int m_frameTimeout;

	internal float m_normalizedframesize;
}


public class StripAnimationManager : MonoBehaviour
{
	public StripAnimation[] StripAnimations;
	private StripAnimation m_currentanimation;

	public int CurrentAnimation = 0;

	/// <summary>
	/// 
	/// </summary>
	void Start()
	{
		foreach (StripAnimation strip in StripAnimations)
		{
			int totalcells = strip.StripTexture.width / strip.CellSize;
			strip.m_normalizedframesize = 1f / totalcells;
//			Debug.Log(strip.m_normalizedframesize);
		}

		PlayAnimation(CurrentAnimation);
	}

	/// <summary>
	/// Play an animation
	/// </summary>
	/// <param name="p_animation">The index of the animation in the animations array</param>
	public void PlayAnimation(int p_animation)
	{
		if (m_currentanimation != StripAnimations[p_animation])
		{
			m_currentanimation = StripAnimations[p_animation];

			renderer.material.mainTexture = m_currentanimation.StripTexture;
			renderer.material.mainTextureScale = new Vector2(m_currentanimation.m_normalizedframesize, 1f);

			m_currentanimation.m_frame = 0;
			m_currentanimation.m_frameTimeout = m_currentanimation.PlaybackSpeed;

		}
	}

	/// <summary>
	/// 
	/// </summary>
	void Update()
	{
		m_currentanimation.m_frameTimeout -= 1;

		if (m_currentanimation.m_frameTimeout < 0)
		{
			m_currentanimation.m_frame += 1;
			m_currentanimation.m_frameTimeout = m_currentanimation.PlaybackSpeed;

			if (m_currentanimation.m_frame >= m_currentanimation.NumberOfCells)
			{
				if (m_currentanimation.Loop)
				{
					m_currentanimation.m_frame = 0;
				}
				else
				{
					PlayAnimation(m_currentanimation.NextAnimation);
				}
			}
		}

		renderer.material.mainTextureOffset = new Vector2(m_currentanimation.m_frame * m_currentanimation.m_normalizedframesize, 0);
	}
}
