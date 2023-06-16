using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject objectPrefab; // Ҫ���õ�����Ԥ����
    public LayerMask terrainLayer; // ���ε�ͼ��
    public Camera mainCamera;
    public Vector2 placementOffset = new Vector2(5f, 10f);
    public float maxDistance = 40f; // ������

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, terrainLayer))
            {
                // �������λ��
                Vector3 placementPosition = ray.origin + ray.direction * (hit.distance - Random.Range(placementOffset.x, placementOffset.y));

                // �ڷ���λ��ʵ��������
                Instantiate(objectPrefab, placementPosition, Quaternion.identity);
            }
            else
            {
                // ����������ʱ����������������ߵ���Զλ�ô�
                Vector3 placementPosition = ray.origin + ray.direction * maxDistance;

                // �ڷ���λ��ʵ��������
                Instantiate(objectPrefab, placementPosition, Quaternion.identity);
            }
        }
    }
}
