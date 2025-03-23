using UnityEngine;

public class SakuraRandomPlacement : MonoBehaviour
{
public GameObject[] objectPrefabs; // 配置するオブジェクトのプレハブ一覧
    public int objectCount = 10; // 配置するオブジェクトの数
    public Vector3 cubeSize = new Vector3(2f, 2f, 2f); // 2m立方体のサイズ
    public float minDistance = 0.2f; // オブジェクト同士の最小距離
    public float rotationVariation = 15f; // 回転のランダムなずれ（度数）

    private void Start()
    {
        PlaceObjects();
    }

    void PlaceObjects()
    {
        Vector3 cubeCenter = transform.position;
        float halfX = cubeSize.x / 2f;
        float halfY = cubeSize.y / 2f;
        float halfZ = cubeSize.z / 2f;

        int attempts = 100;
        GameObject[] placedObjects = new GameObject[objectCount];

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition;
            bool validPosition = false;
            int attempt = 0;

            while (!validPosition && attempt < attempts)
            {
                attempt++;
                randomPosition = new Vector3(
                    Random.Range(-halfX, halfX),
                    Random.Range(-halfY, halfY),
                    Random.Range(-halfZ, halfZ)
                ) + cubeCenter;

                validPosition = true;
                foreach (GameObject obj in placedObjects)
                {
                    if (obj != null && Vector3.Distance(randomPosition, obj.transform.position) < minDistance)
                    {
                        validPosition = false;
                        break;
                    }
                }

                if (validPosition)
                {
                    GameObject randomPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
                    Quaternion baseRotation = Quaternion.LookRotation(randomPosition - cubeCenter);
                    Quaternion randomRotation = Quaternion.Euler(
                        baseRotation.eulerAngles.x + Random.Range(-rotationVariation, rotationVariation),
                        baseRotation.eulerAngles.y + Random.Range(-rotationVariation, rotationVariation),
                        baseRotation.eulerAngles.z + Random.Range(-rotationVariation, rotationVariation)
                    );
                    placedObjects[i] = Instantiate(randomPrefab, randomPosition, randomRotation, transform);
                }
            }
        }
    }
}
