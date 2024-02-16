using UnityEngine;

public class Movecamera : MonoBehaviour
{
    public GameObject player; // 주인공 Capsule 오브젝트
    public float mouseSensitivity = 100f; // 마우스 감도
    public float distanceFromPlayer = 2f; // 카메라와 주인공 사이의 거리
    public float minYAngle = -90f; // 카메라의 최소 각도
    public float maxYAngle = 90f; // 카메라의 최대 각도

    private float rotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨기기

        if (player == null)
        {
            player = GameObject.Find("Capsule"); // Capsule 오브젝트를 찾아 할당
            if (player == null)
            {
                Debug.LogError("Capsule object not found in the scene!");
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            // 마우스 입력 받기
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // 수평 회전
            player.transform.Rotate(Vector3.up * mouseX);

            // 수직 회전
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);
            transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            // 카메라 위치 조정
            transform.position = player.transform.position;
            transform.Translate(Vector3.back * distanceFromPlayer);
        }
    }
}


