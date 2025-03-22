using UnityEngine;

public class BonsaiMover : MonoBehaviour
{
    public bool rotate = false;

    void Rotate()
    {
        rotate = !rotate;
    }

    void ResetTransform()
    {
        transform.position = new Vector3(0, -0.22f, 0);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    void WhenShowCommit()
    {
        transform.position = new Vector3(-0.095f, -0.22f, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        if (rotate)
        {
            transform.Rotate(new Vector3(0, 30.0f, 0) * Time.deltaTime);
        }
    }
}
