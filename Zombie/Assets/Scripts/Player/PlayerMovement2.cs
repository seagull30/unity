using UnityEngine;
using UnityEngine.AI;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 5f; // 앞뒤 움직임의 속도
    [SerializeField]
    private float RotateSpeed = 180f; // 좌우 회전 속도

    private NavMeshAgent _navMeshAgent;
    private PlayerInput _input; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody _rigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator _animator; // 플레이어 캐릭터의 애니메이터

    private void Start()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = MoveSpeed;        
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate()
    {
        move();
        rotate();

        _animator.SetFloat(PlayerAnimID.MOVE, _input.MoveDirection);

    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void move()
    {
       
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void rotate()
    {
       
    }
}