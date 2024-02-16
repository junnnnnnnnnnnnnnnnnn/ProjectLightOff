using UnityEngine;

public class Follow : MonoBehaviour
{
    public float cameraHeight = 10f; // 카메라 높이 설정

    void Update()
    {
        // 현재 마우스 커서 위치를 가져옵니다.
        Vector3 mousePos = Input.mousePosition;

        // 마우스 커서의 화면 좌표를 월드 좌표로 변환합니다.
        mousePos.z = cameraHeight;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // 카메라를 마우스 커서 위치로 이동시킵니다.
        transform.position = worldPos;
    }
}
