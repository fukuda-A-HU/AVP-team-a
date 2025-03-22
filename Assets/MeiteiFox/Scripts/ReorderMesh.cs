using UnityEngine;

public class ReorderMesh
{
    public Mesh ReorderMeshWithTransform(Mesh mesh, Transform transform){
        Vector3[] originalVertices = mesh.vertices;
        Vector2[] originalUVs = mesh.uv;
        Vector3[] originalNormals = mesh.normals;
        int[] originalTriangles = mesh.triangles;
        var sortedVertices = originalVertices
            .Select((v, i) => new { Index = i, Position = v, Distance = v.sqrMagnitude })
            .OrderBy(entry => entry.Distance)
            .ToList();

        // **ステップ2: 新しいインデックスマッピングを作成**
        Dictionary<int, int> indexMapping = new Dictionary<int, int>();
        for (int newIndex = 0; newIndex < sortedVertices.Count; newIndex++)
        {
            indexMapping[sortedVertices[newIndex].Index] = newIndex;
        }

        // **ステップ3: 頂点の新しい順序を適用**
        Vector3[] newVertices = sortedVertices.Select(v => v.Position).ToArray();
        Vector2[] newUVs = sortedVertices.Select(v => originalUVs[v.Index]).ToArray();
        Vector3[] newNormals = sortedVertices.Select(v => originalNormals[v.Index]).ToArray();

        // **ステップ4: 三角形のインデックスを変換**
        int[] newTriangles = originalTriangles.Select(i => indexMapping[i]).ToArray();

        // **ステップ5: 新しいメッシュを作成**
        Mesh newMesh = new Mesh();
        newMesh.vertices = newVertices;
        newMesh.uv = newUVs;
        newMesh.normals = newNormals;
        newMesh.triangles = newTriangles;
        newMesh.RecalculateBounds(); // 必要に応じてバウンディングボックスを再計算

        // **メッシュを更新**
        return newMesh;a
    }
}
