using UnityEngine;

public class HighlightObjectInCenter : MonoBehaviour
{
    public GameObject[] objectsToHighlight; // private으로 지정된 오브젝트
    private Color originalColor; // 오브젝트의 원래 색상 저장용 변수
    private bool isHighlighted = false; // 오브젝트가 하이라이트되었는지 여부를 나타내는 변수
    private float originalDistance; // 카메라와 물체의 원래 거리를 저장하는 변수

    void Start()
    {
        objectsToHighlight = GameObject.FindGameObjectsWithTag("object");
        // 오브젝트의 원래 색상 저장
        originalColor = objectsToHighlight[0].GetComponent<MeshRenderer>().material.color;

        // 초기 카메라와 물체 사이의 거리 저장
        originalDistance = Vector3.Distance(Camera.main.transform.position, objectsToHighlight.transform.position);
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

        // 카메라의 시야 안에 있는 모든 오브젝트를 검사합니다.
        RaycastHit[] hits;
        hits = Physics.RaycastAll(mainCamera.transform.position, mainCamera.transform.forward, Mathf.Infinity);

        // 시야 안에 있는 큐브에만 반응합니다.
        foreach (RaycastHit hit in hits)
        {
            print("1");
            GameObject objectInSight = hit.collider.gameObject;
            if (objectInSight.CompareTag("object"))
            {
                print("2");
                // 카메라 시야 안에 있는 object 태그가 붙은 큐브일 때만 색상을 변경하거나 복원함
                if (objectInSight == objectsToHighlight)
                {
                    // 마우스 우클릭 시 물체를 카메라에 종속
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

        // 시야에 큐브가 없을 때 원래 색상으로 복원함
        RestoreColor();
        isHighlighted = false;
    }

    // 큐브의 색상을 노랑으로 변경하는 함수
    void HighlightCube()
    {
        print("Hightlight");
        objectsToHighlight.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    // 원래 색상으로 복원하는 함수
    void RestoreColor()
    {
        objectsToHighlight.GetComponent<MeshRenderer>().material.color = originalColor;
    }

    // 물체를 카메라에 종속하거나 종속을 해제하는 함수
    void ToggleParenting()
    {
        if (objectsToHighlight.transform.parent == null)
        {
            // 물체를 카메라의 자식으로 설정
            objectsToHighlight.transform.parent = Camera.main.transform;

            // 종속됐을 때 카메라와 물체 사이의 거리를 줄임
            float newDistance = originalDistance / 8f;
            objectsToHighlight.transform.localPosition = new Vector3(0f, 0f, newDistance);
        }
        else
        {
            // 물체의 부모를 제거하여 종속을 해제
            objectsToHighlight.transform.parent = null;
        }
    }
}
