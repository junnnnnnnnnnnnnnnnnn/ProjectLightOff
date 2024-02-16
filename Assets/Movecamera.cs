using UnityEngine;

public class Movecamera : MonoBehaviour
{
    public GameObject player; // ���ΰ� Capsule ������Ʈ
    public float mouseSensitivity = 100f; // ���콺 ����
    public float distanceFromPlayer = 2f; // ī�޶�� ���ΰ� ������ �Ÿ�
    public float minYAngle = -90f; // ī�޶��� �ּ� ����
    public float maxYAngle = 90f; // ī�޶��� �ִ� ����

    private float rotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� �����

        if (player == null)
        {
            player = GameObject.Find("Capsule"); // Capsule ������Ʈ�� ã�� �Ҵ�
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
            // ���콺 �Է� �ޱ�
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // ���� ȸ��
            player.transform.Rotate(Vector3.up * mouseX);

            // ���� ȸ��
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);
            transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            // ī�޶� ��ġ ����
            transform.position = player.transform.position;
            transform.Translate(Vector3.back * distanceFromPlayer);
        }
    }
}


