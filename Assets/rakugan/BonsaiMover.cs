using UnityEngine;

public class BonsaiMover : MonoBehaviour
{
    public bool rotate = false;

    void Rotate()
    {
        rotate = !rotate;
    }

    void Update()
    {
        if (rotate)
        {
            transform.Rotate(new Vector3(0, 30.0f, 0) * Time.deltaTime);
        }
    }
}
