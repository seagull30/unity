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
        // 1. 마우스 Primart 버튼을 클릭하면
        if (Input.GetMouseButtonDown(0))
        {
            // 2. 그 지점을 얻어내서
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
                // 3. 땅 위에 아이템을 스폰
                Vector3 spawnPosition = hit.point;
                spawnPosition.y += 0.5f;
                GameObject item = Instantiate(ItemPrefab, spawnPosition, Quaternion.identity);
                Destroy(item, 5f);
            }
        }


    }
}
