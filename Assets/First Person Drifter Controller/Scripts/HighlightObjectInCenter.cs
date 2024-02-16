using UnityEngine;

public class HighlightObjectInCenter : MonoBehaviour
{
    public GameObject[] objectsToHighlight; // private���� ������ ������Ʈ
    private Color originalColor; // ������Ʈ�� ���� ���� ����� ����
    private bool isHighlighted = false; // ������Ʈ�� ���̶���Ʈ�Ǿ����� ���θ� ��Ÿ���� ����
    private float originalDistance; // ī�޶�� ��ü�� ���� �Ÿ��� �����ϴ� ����

    void Start()
    {
        objectsToHighlight = GameObject.FindGameObjectsWithTag("object");
        // ������Ʈ�� ���� ���� ����
        originalColor = objectsToHighlight[0].GetComponent<MeshRenderer>().material.color;

        // �ʱ� ī�޶�� ��ü ������ �Ÿ� ����
        originalDistance = Vector3.Distance(Camera.main.transform.position, objectsToHighlight.transform.position);
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

        // ī�޶��� �þ� �ȿ� �ִ� ��� ������Ʈ�� �˻��մϴ�.
        RaycastHit[] hits;
        hits = Physics.RaycastAll(mainCamera.transform.position, mainCamera.transform.forward, Mathf.Infinity);

        // �þ� �ȿ� �ִ� ť�꿡�� �����մϴ�.
        foreach (RaycastHit hit in hits)
        {
            print("1");
            GameObject objectInSight = hit.collider.gameObject;
            if (objectInSight.CompareTag("object"))
            {
                print("2");
                // ī�޶� �þ� �ȿ� �ִ� object �±װ� ���� ť���� ���� ������ �����ϰų� ������
                if (objectInSight == objectsToHighlight)
                {
                    // ���콺 ��Ŭ�� �� ��ü�� ī�޶� ����
                    if (Input.GetMouseButtonDown(1))
                    {
                        ToggleParenting();
                        print("3-1");
                    }
                    
                    if (!isHighlighted)
                    {
                        HighlightCube();
                        print("3-2");
                        isHighlighted = true;
                    }
                }
                else
                {
                    RestoreColor();
                    isHighlighted = false;
                }
                return;
            }
        }

        // �þ߿� ť�갡 ���� �� ���� �������� ������
        RestoreColor();
        isHighlighted = false;
    }

    // ť���� ������ ������� �����ϴ� �Լ�
    void HighlightCube()
    {
        print("Hightlight");
        objectsToHighlight.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    // ���� �������� �����ϴ� �Լ�
    void RestoreColor()
    {
        objectsToHighlight.GetComponent<MeshRenderer>().material.color = originalColor;
    }

    // ��ü�� ī�޶� �����ϰų� ������ �����ϴ� �Լ�
    void ToggleParenting()
    {
        if (objectsToHighlight.transform.parent == null)
        {
            // ��ü�� ī�޶��� �ڽ����� ����
            objectsToHighlight.transform.parent = Camera.main.transform;

            // ���ӵ��� �� ī�޶�� ��ü ������ �Ÿ��� ����
            float newDistance = originalDistance / 8f;
            objectsToHighlight.transform.localPosition = new Vector3(0f, 0f, newDistance);
        }
        else
        {
            // ��ü�� �θ� �����Ͽ� ������ ����
            objectsToHighlight.transform.parent = null;
        }
    }
}
