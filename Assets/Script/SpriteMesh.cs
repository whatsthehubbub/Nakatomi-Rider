using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class SpriteMesh : MonoBehaviour
{
	// Creates a plane like with its vertices/faces in this order:
	//
	// 0---3
	// |  /|
	// |/  |
	// 1---2
	public int Priority;
	
	void Start()
	{
		Vector3[] newVertices =
		{
			new Vector3(-0.5f, -0.5f, 0.0f), 
			new Vector3(0.5f, -0.5f, 0.0f), 
			new Vector3(0.5f, 0.5f, 0.0f), 
			new Vector3(-0.5f, 0.5f, 0.0f)
		};

		Vector2[] newUV =
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(1, 1),
			new Vector2(0, 1)
		};
		
		Vector3[] newNormals =
		{
			new Vector3(0, 0, 1),
			new Vector3(0, 0, 1),
			new Vector3(0, 0, 1),
			new Vector3(0, 0, 1)
		};
		
		Vector4[] newTangents =
		{
			new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
			new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
			new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
			new Vector4(1.0f, 0.0f, 0.0f, 1.0f)
		};
		
		//int[] newTriangles = { 0, 1, 3, 1, 2, 3 };
		int[] newTriangles = { 3, 2, 1, 3, 1, 0 }; // reversed
		
		//Color[] newColors = { Color.white, Color.white, Color.red, Color.gray }; // seems unneeded

		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		
		mesh.vertices = newVertices;
		mesh.triangles = newTriangles;
		mesh.uv = newUV;
		//mesh.colors = newColors;
		mesh.normals = newNormals;
		mesh.tangents = newTangents;
		//mesh.Optimize(); // TODO check if this actually matters, it seems not to matter
		
		//MeshRenderer renderer = GetComponent<MeshRenderer>();
		if (Application.isPlaying &&
			renderer != null)
		{
			renderer.material.renderQueue = Priority;
		}
	}
}
