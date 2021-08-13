using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace EssentialMechanics
{
    class ShaderInterpolation
    {
        public Vector2 getWetPercent(Essentials3D.Character character, GameObject Water, float interactiveDistance,float minGroundDistance)
        {
            Vector2 wetBoundsPercent = new Vector2();
            if (Water.TryGetComponent<MeshFilter>(out MeshFilter waterMeshFilter))
            {
                Vector3[] waterVertex = waterMeshFilter.mesh.vertices;
                List<Vector3> interactiveVertex = new List<Vector3>();
                foreach (Vector3 vert in waterVertex)
                    if (interactiveDistance <= Vector2.Distance(new Vector2(vert.x, vert.z), new Vector2(character.abdomen.transform.position.x, character.abdomen.transform.position.z)))
                        interactiveVertex.Add(vert);

                Vector3 minXVertex = interactiveVertex[0];
                Vector3 maxXVertex = interactiveVertex[0];
                Vector3 maxYVertex = interactiveVertex[0];

                for (int i = 0; i < interactiveVertex.Count; i++)
                {
                    if (interactiveVertex[i].y > maxYVertex.y)
                        maxYVertex = interactiveVertex[i];
                    if (interactiveVertex[i].x > maxXVertex.x)
                        maxXVertex = interactiveVertex[i];
                    if (interactiveVertex[i].x < minXVertex.x)
                        minXVertex = interactiveVertex[i];
                }
                wetBoundsPercent.x = Mathf.Min(Mathf.Abs(maxXVertex.x - minXVertex.x) / interactiveDistance, 1);
                wetBoundsPercent.y = Mathf.Min(Mathf.Abs((maxYVertex).y - Logic.Vector3D.arithmeticAverage(character.footLeft.transform.position, character.footRight.transform.position).y) / minGroundDistance, 1);
            }
            else
            {
                wetBoundsPercent.x = 1;
                wetBoundsPercent.y = 1;
            }
            return wetBoundsPercent;
        }
    }
}