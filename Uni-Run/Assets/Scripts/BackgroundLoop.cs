using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour
{
    private float _width;

    private Vector2 _resetPosition;
    private void Awake()
    {
        _width = GetComponent<BoxCollider2D>().size.x;
        _resetPosition = new Vector2(_width, 0f);       
    }

    private void Update()
    {
        // 게임 오브젝트를 왼쪽으로 일정 속도로 평행 이동하는 처리
        if (transform.position.x <= -_width)
        {
            transform.position = _resetPosition;
        }
    }
}