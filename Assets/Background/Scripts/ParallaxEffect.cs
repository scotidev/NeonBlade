using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private MeshRenderer mr;
    private Transform cam;
    private Vector3 camStartPos;
    public float speedScrolling = 0.5f;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        cam = Camera.main.transform;
        camStartPos = cam.position;
    }

    private void LateUpdate()
    {
        float distance = cam.position.x - camStartPos.x;

        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);
        mr.material.mainTextureOffset = new Vector2(distance, 0) * speedScrolling / 10;
    }
}
