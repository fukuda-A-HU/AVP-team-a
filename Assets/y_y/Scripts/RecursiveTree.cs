using UnityEngine;

public class RecursiveTree : MonoBehaviour
{
    [SerializeField]
    GameObject mBranch;

    // Start is called before the first frame update
    void Start()
    {
        CreateRecursiveTree(9, transform);
    }

    /// <summary>
    /// 再帰関数で木を生成するプログラム
    /// </summary>
    /// <param name="n">nには分岐したい回数を入れる</param>
    /// <param name="trans">transには原点のTransformを入れる</param>
    void CreateRecursiveTree(int n, Transform trans)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject obj = Instantiate(mBranch, trans.position + trans.up * trans.localScale.x, trans.rotation);
            //まず生成した枝を傾ける
            obj.transform.Rotate(0, 0, -20, Space.World);
            //生えてきた元の枝に沿ってランダムに回転
            obj.transform.rotation = Quaternion.AngleAxis(Random.RandomRange(10f + 180f * i, 170f + 180f * i), trans.up) * obj.transform.rotation;
            //生成した枝の大きさ調整
            obj.transform.localScale *= 0.6f;
            if (n > 0)
            {
                //再帰
                CreateRecursiveTree(n - 1, obj.transform);
            }
        }
    }
}
