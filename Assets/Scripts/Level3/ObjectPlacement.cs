using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject objectPrefab; // 要放置的物体预制体
    public LayerMask terrainLayer; // 地形的图层
    public Camera mainCamera;
    public Vector2 placementOffset = new Vector2(5f, 10f);
    public float maxDistance = 40f; // 最大距离

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, terrainLayer))
            {
                // 计算放置位置
                Vector3 placementPosition = ray.origin + ray.direction * (hit.distance - Random.Range(placementOffset.x, placementOffset.y));

                // 在放置位置实例化物体
                Instantiate(objectPrefab, placementPosition, Quaternion.identity);
            }
            else
            {
                // 超过最大距离时，将物体放置在射线的最远位置处
                Vector3 placementPosition = ray.origin + ray.direction * maxDistance;

                // 在放置位置实例化物体
                Instantiate(objectPrefab, placementPosition, Quaternion.identity);
            }
        }
    }
}
