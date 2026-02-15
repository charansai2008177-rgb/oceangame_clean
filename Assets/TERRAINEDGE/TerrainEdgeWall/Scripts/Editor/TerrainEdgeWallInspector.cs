using UnityEditor;
using UnityEngine;

namespace Gen90Software.Tools
{
	[CustomEditor(typeof(TerrainEdgeWall))]
	[CanEditMultipleObjects]
	public class TerrainEdgeWallInspector : Editor
	{
		[MenuItem("GameObject/Rendering/Terrain Edge Wall", false, 600)]
		public static void CreateTerrainEdgeWall(MenuCommand menuCommand)
		{
			GameObject go = new GameObject("Terrain Edge Wall", typeof(MeshFilter), typeof(MeshRenderer), typeof(TerrainEdgeWall));
			Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
			Selection.activeObject = go;
		}

		public void Awake()
		{
			TerrainEdgeWall wall = (TerrainEdgeWall)target;

			bool needRegenerate = false;

			if (wall.terrain == null)
			{
				wall.terrain = FindObjectOfType<Terrain>();
				needRegenerate = true;
			}

			if (wall.wallMaterial == null)
			{
				wall.wallMaterial = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Material.mat");
				needRegenerate = true;
			}

			if (wall.bottomMaterial == null)
			{
				wall.bottomMaterial = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Material.mat");
				needRegenerate = true;
			}

			if (!wall.HasWallMesh() || needRegenerate)
			{
				wall.RegenerateWall();
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			TerrainEdgeWall wall = target as TerrainEdgeWall;

			if (wall.terrain == null)
			{
				EditorGUILayout.Space();
				EditorGUILayout.HelpBox("No Terrain!", MessageType.Warning);
				return;
			}

			GUILayout.Space(50);
			if (GUILayout.Button("Regenerate"))
			{
				wall.RegenerateWall();
			}
		}
	}
}
