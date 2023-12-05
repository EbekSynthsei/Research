using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteAlways]
public class BarrelManager : Singleton<BarrelManager>
{
    public static List<Barrel> barrels = new List<Barrel>();

    
    private void OnDrawGizmos()
    {
        foreach (Barrel barrel in barrels)
        {
            Vector3 managerPos = transform.position;
            Vector3 barrelPos = barrel.transform.position;

            float halfHeight = (managerPos.y - barrelPos.y) * .5f;
            Vector3 tangentOffset = Vector3.up * halfHeight;

            Handles.DrawBezier(managerPos,
                barrelPos,
                managerPos - tangentOffset,
                barrelPos + tangentOffset,
                barrel.color,
                EditorGUIUtility.whiteTexture,
                1f
                );
            //Gizmos.DrawLine(transform.position, barrel.transform.position);
        }
    }

}
