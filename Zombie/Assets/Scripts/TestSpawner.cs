using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;

    private Camera _mainCam;
    void Start()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // 1. ���콺 Primart ��ư�� Ŭ���ϸ�
        if (Input.GetMouseButtonDown(0))
        {
            // 2. �� ������ ����
            Ray mouseRay = _mainCam.ScreenPointToRay(Input.mousePosition);

            LayerMask targetLayer = LayerMask.GetMask("Ground");
            int layerMask = (1 << targetLayer);


            RaycastHit hit;
            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 100f, targetLayer))
            {
                if (hit.collider.gameObject.layer != targetLayer.value)
                {
                    return;
                }
                // 3. �� ���� �������� ����
                Vector3 spawnPosition = hit.point;
                spawnPosition.y += 0.5f;
                GameObject item = Instantiate(ItemPrefab, spawnPosition, Quaternion.identity);
                Destroy(item, 5f);
            }
        }


    }
}
