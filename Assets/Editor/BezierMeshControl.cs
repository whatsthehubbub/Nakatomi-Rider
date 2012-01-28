using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BezierMesh))]
[CanEditMultipleObjects]
public class BezierMeshControl : Editor
{

	void OnSceneGUI()
	{
		BezierMesh bezier = target as BezierMesh;
		float width       = HandleUtility.GetHandleSize(Vector3.zero);
		
		PointHandle(ref bezier.StartPoint);
		PointHandle(ref bezier.EndPoint);
		
		HandleHandle(ref bezier.StartPointHandle, bezier.StartPoint);
		HandleHandle(ref bezier.EndPointHandle, bezier.EndPoint);
		
		bezier.DrawDebugBezier();
		
		if (GUI.changed)
		{
			bezier.RebuildMesh();
			
			Event.current.Use();	
			EditorUtility.SetDirty(target);
		}
	}
	
	public override void OnInspectorGUI()
	{
		BezierMesh bezier = target as BezierMesh;
		DrawDefaultInspector();
		if (GUI.changed)
		{
			bezier.RebuildMesh();
		}
	}
	
	void PointHandle(ref Vector3 pos)
	{
		pos = Handles.PositionHandle(pos, Quaternion.identity);
		
		/*Handles.color = Color.red;
		bezier.a = Handles.Slider2D( 
			bezier.a, 
			Vector3.back, 
			Vector3.up, 
			Vector3.left, 
			0.5f, 
			Handles.CircleCap, 
			Vector2.one);*/
	}
	
	void HandleHandle(ref Vector3 handlepos, Vector3 pos)
	{
		Handles.color = Color.gray;
		Handles.DrawLine(pos, handlepos);
		
		handlepos = Handles.PositionHandle(handlepos, Quaternion.identity);
		/*
		Handles.color = Color.yellow;
		handlepos = Handles.Slider2D( 
			handlepos, 
			Vector3.back, 
			Vector3.up, 
			Vector3.left, 
			0.3f, 
			Handles.CircleCap, 
			Vector2.one);
		*/
	}
}
