using UnityEngine;

public class Follow : MonoBehaviour
{
    public float cameraHeight = 10f; // ī�޶� ���� ����

    void Update()
    {
        // ���� ���콺 Ŀ�� ��ġ�� �����ɴϴ�.
        Vector3 mousePos = Input.mousePosition;

        // ���콺 Ŀ���� ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ�մϴ�.
        mousePos.z = cameraHeight;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // ī�޶� ���콺 Ŀ�� ��ġ�� �̵���ŵ�ϴ�.
        transform.position = worldPos;
    }
}
