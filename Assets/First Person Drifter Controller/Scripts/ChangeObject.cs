using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    private Color originalColor; // ������Ʈ�� ���� ���� ����� ����

    void Start()
    {
        // ������Ʈ�� ���� ���� ����
        originalColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        // ���� ī�޶� ã��
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found in the scene!");
            return;
        }

        // ī�޶��� �þ� �ȿ� �ִ��� ���� Ȯ��
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        bool inSight = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        // object �±װ� ���� ť���� ���� ������ �����ϰų� ������
        if (inSight && gameObject.CompareTag("object"))
        {
            HighlightCube();
        }
        else
        {
            RestoreColor();
        }
    }

    // ť���� ������ ������� �����ϴ� �Լ�
    void HighlightCube()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    // ���� �������� �����ϴ� �Լ�
    void RestoreColor()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
}
