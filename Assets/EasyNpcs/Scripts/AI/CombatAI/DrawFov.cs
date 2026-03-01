using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AIPackage
{
    public class DrawFov : MonoBehaviour
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected, typeof(Transform))]
        public static void DrawGizmo(Transform npc, GizmoType gizmoType)
        {
            Gizmos.color = Color.green;
            CapsuleCollider collider = npc.GetComponent<CapsuleCollider>();
            Vector3 npcPos = npc.transform.position;
            Gizmos.matrix = Matrix4x4.TRS(new Vector3(npcPos.x, collider == null ? npcPos.y : npcPos.y + collider.height * 0.9f, npcPos.z),
                npc.transform.rotation, npc.transform.lossyScale);

            if (Selection.Contains(npc.gameObject))
            {
                AI_Stats npcInfo = npc.GetComponent<AI_Stats>();
                if (npcInfo != null)
                {
                    Gizmos.DrawFrustum(Vector3.zero, npcInfo.visionAngle, npcInfo.visionRange, 0f, 2.5f);
                }
            }
        }
    }
}