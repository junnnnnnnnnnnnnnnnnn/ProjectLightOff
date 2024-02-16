using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    private Color originalColor; // 오브젝트의 원래 색상 저장용 변수

    void Start()
    {
        // 오브젝트의 원래 색상 저장
        originalColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        // 메인 카메라 찾기
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found in the scene!");
            return;
        }

        // 카메라의 시야 안에 있는지 여부 확인
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        bool inSight = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        // object 태그가 붙은 큐브일 때만 색상을 변경하거나 복원함
        if (inSight && gameObject.CompareTag("object"))
        {
            HighlightCube();
        }
        else
        {
            RestoreColor();
        }
    }

    // 큐브의 색상을 노랑으로 변경하는 함수
    void HighlightCube()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    // 원래 색상으로 복원하는 함수
    void RestoreColor()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
}
