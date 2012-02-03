using UnityEngine;
using System.Collections.Generic;

public struct Pair <T>
{
	public T a;
	public T b;
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[ExecuteInEditMode]
public class BezierMesh : MonoBehaviour
{
	public Vector3 Depth = Vector3.forward * 2;
	public int Steps     = 10;
	
	public Vector3 StartPoint       = Vector3.zero;
	public Vector3 StartPointHandle = Vector3.up;
	public Vector3 EndPoint         = new Vector3(4f, 0f, 0f);
	public Vector3 EndPointHandle   = Vector3.down;
	
	private Mesh _mesh;
	
	void Start()
	{
		BuildMesh();
	}
	
	public void RebuildMesh()
	{
		if (_mesh != null) _mesh.Clear();
		BuildMesh();
	}
	
	public void BuildMesh()
	{
		_mesh = CreateBezierMesh(Depth);
		
		GetComponent<MeshCollider>().sharedMesh = _mesh;
		GetComponent<MeshFilter>().mesh         = _mesh;
	}
		
	public void DrawDebugBezier()
	{
		foreach (Pair<Vector3> segment in BezierSegments())
		{
			Debug.DrawLine(segment.a, segment.b, Color.green);
		}
	}
	
	public Mesh CreateBezierMesh(Vector3 p_offset)
	{
		Mesh mesh = new Mesh();
		List<Vector3> verts = new List<Vector3>();
		List<Vector2> uvs   = new List<Vector2>();
		
		foreach (Vector3 point in BezierPoints())
		{
			verts.Add(point);
			verts.Add(point + p_offset);
			
			uvs.Add(new Vector2(0,0));
			uvs.Add(new Vector2(0,1));
		}
		
		int numtriangles = Steps * 2;
		
		List<int> triangles  = new List<int>(new int[numtriangles * 3]);
		
		bool even = true;
		
		for (int triangle = 0; triangle < numtriangles; triangle++)
		{
			int i = triangle * 3;
			
			if (even)
			{
				triangles[i]     = triangle;
				triangles[i + 1] = triangle + 1;
				triangles[i + 2] = triangle + 2;
			}
			else
			{
				triangles[i]     = triangle + 1;
				triangles[i + 1] = triangle;
				triangles[i + 2] = triangle + 2;
				
			}
			
			even = !even;
		}
		
		mesh.vertices  = verts.ToArray();
		mesh.uv        = uvs.ToArray();
		mesh.triangles = triangles.ToArray();//System.Array.Reverse(tris);
		mesh.RecalculateNormals();

		return mesh;
	}
	
	IEnumerable<Vector3> BezierPoints()
	{
		for (int i = 0; i <= Steps; i += 1)
		{
			yield return CubicBezier((float)i/Steps, StartPoint, StartPointHandle, EndPointHandle, EndPoint);
		}
	}
	
	IEnumerable<Pair<Vector3>> BezierSegments()
	{
		Vector3 lastpoint = StartPoint;
		foreach (Vector3 point in BezierPoints())
		{
			yield return new Pair<Vector3>() { a = lastpoint, b = point };
			lastpoint = point;
		}
	}
	
	Vector3 CubicBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		t = Mathf.Clamp01(t);
		float t2 = 1 - t;
		return Mathf.Pow(t2, 3) * p0 + 3 * Mathf.Pow(t2, 2) * t * p1 + 3 * t2 * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
	}
}
