using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class BonsaiManager_meitei : MonoBehaviour
{
    public Transform referencePoint;  // 頂点の移動開始位置
    public float growthDuration = 5f; // 成長にかける時間
    public AnimationCurve growthCurve; // 成長速度を調整するカーブ

    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] targetVertices;

    // **頂点情報を格納するクラス**
    private class VertexData
    {
        public int Index;
        public Vector3 Position;
        public float Distance;

        public VertexData(int index, Vector3 position, float distance)
        {
            Index = index;
            Position = position;
            Distance = distance;
        }
    }

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || referencePoint == null)
        {
            Debug.LogError("MeshFilter または ReferencePoint が設定されていません！");
            return;
        }

        mesh = meshFilter.mesh;
        originalVertices = mesh.vertices;
        targetVertices = new Vector3[originalVertices.Length];

        // **頂点の並び順を決定（基準位置からの距離でソート）**
        List<VertexData> sortedVertices = originalVertices
            .Select((v, i) => new VertexData(i, v, Vector3.Distance(v, referencePoint.position)))
            .OrderBy(entry => entry.Distance)
            .ToList();

        // **ターゲットの頂点位置を設定（現在の位置を保持しつつ移動させる）**
        for (int i = 0; i < sortedVertices.Count; i++)
        {
            int index = sortedVertices[i].Index;
            Vector3 direction = (originalVertices[index] - referencePoint.position).normalized; // 移動方向
            targetVertices[index] = originalVertices[index] + direction * 2f; // 2.0f だけ伸ばす
        }

        // **アニメーション開始**
        StartCoroutine(AnimateGrowth(sortedVertices));
    }

    IEnumerator AnimateGrowth(List<VertexData> sortedVertices)
    {
        float elapsedTime = 0f;
        Vector3[] currentVertices = (Vector3[])originalVertices.Clone();

        while (elapsedTime < growthDuration)
        {
            float t = elapsedTime / growthDuration;
            float curveValue = growthCurve.Evaluate(t); // カーブを適用

            // **近い頂点から順番に移動**
            for (int i = 0; i < sortedVertices.Count; i++)
            {
                int index = sortedVertices[i].Index;
                float delayFactor = (float)i / sortedVertices.Count; // 頂点ごとの遅延
                float adjustedT = Mathf.Clamp01(t - delayFactor); // 順番に成長

                currentVertices[index] = Vector3.Lerp(originalVertices[index], targetVertices[index], growthCurve.Evaluate(adjustedT));
            }

            // **メッシュを更新**
            mesh.vertices = currentVertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // **最終状態を確定**
        mesh.vertices = targetVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
