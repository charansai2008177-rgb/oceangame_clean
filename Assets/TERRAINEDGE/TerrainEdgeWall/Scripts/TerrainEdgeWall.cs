using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gen90Software.Tools
{
	[AddComponentMenu("Rendering/Terrain Edge Wall", 600)]
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[ExecuteInEditMode]
	[HelpURL("https://gen90software.com/terrainedgewall-documentation.pdf")]
	public class TerrainEdgeWall : MonoBehaviour
	{
		#region Data Structures

		public Terrain terrain;
		[Space(10)]
		[Range(-1000.0f, 0.0f)]
		public float basePosition = -50.0f;
		public Material wallMaterial;
		[Space(10)]
		public bool bottomPlane = false;
		public Material bottomMaterial;

		private MeshFilter mf;
		private MeshRenderer mr;
		private Mesh mesh;

		#endregion

		#region Mono Methods

		public void Awake()
		{
			if (mf == null)
			{
				mf = GetComponent<MeshFilter>();
			}

			if (mr == null)
			{
				mr = GetComponent<MeshRenderer>();
			}
		}

		public void Start()
		{
			if (HasWallMesh())
				return;

			RegenerateWall();
		}

		public void OnDestroy()
		{
			DestroyWall();
		}

		#endregion

		#region Public Methods

		public void RegenerateWall()
		{
			if (terrain == null)
				return;

			Generate();
		}

		public void DestroyWall()
		{
			if (mesh == null)
				return;

			DestroyMesh();
		}

		public bool HasWallMesh()
		{
			return mesh != null;
		}

		#endregion

		#region Private Methods

		private void Generate()
		{
			TerrainData data = terrain.terrainData;

			List<Vector3> verts = new List<Vector3>();
			List<int> tris = new List<int>();
			List<Vector3> normals = new List<Vector3>();
			List<Vector2> uvs = new List<Vector2>();

			int hmRes = data.heightmapResolution;
			int hmLast = hmRes - 1;

			//WallX+
			for (int i = 0; i < hmRes; i++)
			{
				float rateX = i / (float)(hmLast);
				float posX = rateX * data.size.x;

				float height = data.GetHeight(i, 0);

				verts.Add(new Vector3(posX, basePosition, 0f));
				verts.Add(new Vector3(posX, height, 0f));

				normals.Add(Vector3.back);
				normals.Add(Vector3.back);

				float rateW = rateX * 0.25f;
				uvs.Add(new Vector2(rateW, 0f));
				uvs.Add(new Vector2(rateW, 1f));
			}

			//WallZ+
			for (int i = 0; i < hmRes; i++)
			{
				float rateZ = i / (float)(hmLast);
				float posZ = rateZ * data.size.z;

				float height = data.GetHeight(hmRes, i);

				verts.Add(new Vector3(data.size.z, basePosition, posZ));
				verts.Add(new Vector3(data.size.z, height, posZ));

				normals.Add(Vector3.right);
				normals.Add(Vector3.right);

				float rateW = rateZ * 0.25f + 0.25f;
				uvs.Add(new Vector2(rateW, 0.0f));
				uvs.Add(new Vector2(rateW, 1.0f));
			}

			//WallX-
			for (int i = 0; i < hmRes; i++)
			{
				float rateX = 1.0f - (i / (float)(hmLast));
				float posX = rateX * data.size.x;

				float height = data.GetHeight(hmLast - i, hmRes);

				verts.Add(new Vector3(posX, basePosition, data.size.x));
				verts.Add(new Vector3(posX, height, data.size.x));

				normals.Add(Vector3.forward);
				normals.Add(Vector3.forward);

				float rateW = rateX * 0.25f + 0.5f;
				uvs.Add(new Vector2(rateW, 0.0f));
				uvs.Add(new Vector2(rateW, 1.0f));
			}

			//WallZ-
			for (int i = 0; i < hmRes; i++)
			{
				float rateZ = 1.0f - (i / (float)(hmLast));
				float posZ = rateZ * data.size.z;

				float height = data.GetHeight(0, hmLast - i);

				verts.Add(new Vector3(0.0f, basePosition, posZ));
				verts.Add(new Vector3(0.0f, height, posZ));

				normals.Add(Vector3.left);
				normals.Add(Vector3.left);

				float rateW = rateZ * 0.25f + 0.75f;
				uvs.Add(new Vector2(rateW, 0.0f));
				uvs.Add(new Vector2(rateW, 1.0f));
			}

			for (int i = 0; i < verts.Count - 3; i += 2)
			{
				tris.Add(i);
				tris.Add(i + 1);
				tris.Add(i + 2);
				tris.Add(i + 1);
				tris.Add(i + 3);
				tris.Add(i + 2);
			}

			if (bottomPlane)
			{
				verts.Add(new Vector3(0.0f, basePosition, 0.0f));
				verts.Add(new Vector3(0.0f, basePosition, data.size.z));
				verts.Add(new Vector3(data.size.x, basePosition, 0.0f));
				verts.Add(new Vector3(data.size.x, basePosition, data.size.z));
				normals.Add(Vector3.down);
				normals.Add(Vector3.down);
				normals.Add(Vector3.down);
				normals.Add(Vector3.down);
				int bottomVertsBase = verts.Count - 4;
				tris.Add(bottomVertsBase);
				tris.Add(bottomVertsBase + 2);
				tris.Add(bottomVertsBase + 1);
				tris.Add(bottomVertsBase + 1);
				tris.Add(bottomVertsBase + 2);
				tris.Add(bottomVertsBase + 3);
				uvs.Add(new Vector2(0.0f, 1.0f));
				uvs.Add(new Vector2(0.0f, 0.0f));
				uvs.Add(new Vector2(1.0f, 1.0f));
				uvs.Add(new Vector2(1.0f, 0.0f));
			}

			if (mesh != null)
			{
				DestroyMesh();
			}
			PopulateMesh(ref verts, ref tris, ref normals, ref uvs);
		}

		private void PopulateMesh(ref List<Vector3> verts, ref List<int> tris, ref List<Vector3> normals, ref List<Vector2> uvs)
		{
			mesh = new Mesh
			{
				name = terrain.name + "_EdgeWall",
				vertices = verts.ToArray(),
				normals = normals.ToArray(),
				uv = uvs.ToArray()
			};

			if (bottomPlane)
			{
				int bottomTrisBase = tris.Count - 6;
				mesh.subMeshCount = 2;
				mesh.SetTriangles(tris.Take(bottomTrisBase).ToArray(), 0);
				mesh.SetTriangles(tris.Skip(bottomTrisBase).ToArray(), 1);
				mr.sharedMaterials = new Material[] { wallMaterial, bottomMaterial };
			}
			else
			{
				mesh.subMeshCount = 1;
				mesh.SetTriangles(tris.ToArray(), 0);
				mr.sharedMaterials = new Material[] { wallMaterial };
			}

			mesh.RecalculateTangents();
			mesh.RecalculateBounds();

			mf.mesh = mesh;

			transform.position = terrain.transform.position;
		}

		private void DestroyMesh()
		{
			mf.mesh = null;
			if (Application.isEditor && !Application.isPlaying)
			{
				DestroyImmediate(mesh);
			}
			else
			{
				Destroy(mesh);
			}
			mesh = null;
		}

		#endregion
	}
}
