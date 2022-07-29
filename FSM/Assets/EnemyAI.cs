using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    None,
    Idle,       // 대기
    Walk,       // 순찰
    Run,        // 추적
    Attack,     // 공격
    Knockback,  // 피격
    Hide        // 은신
}

public class EnemyAI : MonoBehaviour
{
    public EnemyState state;
    public EnemyState prevState = EnemyState.None;

    private Animator _animator;

    // 이동 관련
    private Vector3 _targetPosition;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private float _rotationSpeed = 1f;

    // 적 탐지 관련
    public GameObject target;
    private bool _isFindEnemy = false;
    private Camera _eye;
    private Plane[] _eyePlanes;

    // 공격 충돌 관련
    GameObject weaponCollider;

    // 은신
    DissolveCtrl _dissolveCtrl;
    public static class AnimID
    {
        public static readonly int ISIDLE = Animator.StringToHash("isIdle");
        public static readonly int ISWALK = Animator.StringToHash("isWalk");
        public static readonly int ISRUN = Animator.StringToHash("isRun");
        public static readonly int ISATTACK = Animator.StringToHash("isAttack");
        public static readonly int ISKNOCKBACK = Animator.StringToHash("isKnockback");

    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _eye = GetComponentInChildren<Camera>();
        _dissolveCtrl = GetComponentInChildren<DissolveCtrl>();
        SphereCollider[] sphereColliders = GetComponentsInChildren<SphereCollider>();
        foreach (var sphereCollider in sphereColliders)
        {
            if (sphereCollider.name == "WeaponCollider")
                weaponCollider = sphereCollider.gameObject;
            break;
        }
        weaponCollider.SetActive(false);
    }

    void Start()
    {
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle: UpdateIdle(); break;
            case EnemyState.Walk: UpdateWalk(); break;
            case EnemyState.Run: UpdateRun(); break;
            case EnemyState.Attack: UpdateAttack(); break;
            case EnemyState.Knockback: UpdateKnockback(); break;
            case EnemyState.Hide: UpdateHide(); break;

        }
    }

    #region UpdateDetail
    // 매 프레임마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
    void UpdateIdle()
    {

    }

    void UpdateWalk()
    {
        if (IsFindEnemy())
        {
            ChangeState(EnemyState.Run);
            return;
        }
        // 목적지까지 이동하는 코스
        Vector3 dir = _targetPosition - transform.position;
        if (dir.sqrMagnitude <= 0.2f)
        {
            ChangeState(EnemyState.Idle);
            return;
        }

        var targetRotation = Quaternion.LookRotation(_targetPosition - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * _moveSpeed * Time.deltaTime;

    }

    void UpdateRun()
    {
        // 목적지까지 이동하는 코스
        Vector3 dir = _targetPosition - transform.position;
        if (dir.sqrMagnitude <= 2f)
        {
            ChangeState(EnemyState.Attack);
            return;
        }

        var targetRotation = Quaternion.LookRotation(_targetPosition - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * 2f * Time.deltaTime);

        transform.position += transform.forward * _moveSpeed * 2f * Time.deltaTime;

    }

    void UpdateAttack()
    {

    }

    void UpdateKnockback()
    {

    }

    void UpdateHide()
    {

    }
    #endregion

    #region CoroutineDetail
    IEnumerator CoroutineIdle()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("대기상태 시작");
        _animator.SetBool(AnimID.ISIDLE, true);

        while (true)
        {
            yield return new WaitForSeconds(2f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
            ChangeState(EnemyState.Hide);
            yield break;
        }
    }

    IEnumerator CoroutineWalk()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("순찰 상태 시작");

        _animator.SetBool(AnimID.ISWALK, true);
        _dissolveCtrl.ChangeState(DissolveCtrl.State.Hide_Off);

        // 목적지 설정
        _targetPosition = transform.position + new Vector3(Random.Range(-7f, 7f), 0f, Random.Range(-7f, 7f));

        while (true)
        {
            yield return new WaitForSeconds(10f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
            ChangeState(EnemyState.Idle);
        }
    }

    IEnumerator CoroutineRun()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("추적 상태 시작");
        _animator.SetBool(AnimID.ISRUN, true);
        _targetPosition = target.transform.position;

        while (true)
        {
            yield return new WaitForSeconds(5f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
        }
    }

    IEnumerator CoroutineAttack()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        _animator.SetTrigger(AnimID.ISATTACK);

        yield return new WaitForSeconds(1f);
        ChangeState(EnemyState.Idle);
        yield break;

    }

    IEnumerator CoroutineKnockback()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)

        while (true)
        {
            yield return new WaitForSeconds(2f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
        }
    }
    IEnumerator CoroutineHide()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        _dissolveCtrl.ChangeState(DissolveCtrl.State.Hide_On);

        yield return new WaitForSeconds(3f);
        ChangeState(EnemyState.Walk);
        yield break;

    }
    #endregion

    void ChangeState(EnemyState nextState)
    {
        if (prevState == nextState) return;

        StopAllCoroutines();

        prevState = nextState;
        state = nextState;
        _animator.SetBool(AnimID.ISIDLE, false);
        _animator.SetBool(AnimID.ISWALK, false);
        _animator.SetBool(AnimID.ISRUN, false);
        _animator.SetBool(AnimID.ISKNOCKBACK, false);

        switch (state)
        {
            case EnemyState.Idle: StartCoroutine(CoroutineIdle()); break;
            case EnemyState.Walk: StartCoroutine(CoroutineWalk()); break;
            case EnemyState.Run: StartCoroutine(CoroutineRun()); break;
            case EnemyState.Attack: StartCoroutine(CoroutineAttack()); break;
            case EnemyState.Knockback: StartCoroutine(CoroutineKnockback()); break;
            case EnemyState.Hide: StartCoroutine(CoroutineHide()); break;
        }
    }

    bool IsFindEnemy()
    {

        if (!target.activeSelf) return false;
        Bounds targetBounds = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
        _eyePlanes = GeometryUtility.CalculateFrustumPlanes(_eye);
        _isFindEnemy = GeometryUtility.TestPlanesAABB(_eyePlanes, targetBounds);

        return _isFindEnemy;
    }

    void OnAttack(AnimationEvent animationEvent)
    {
        Debug.Log(animationEvent.intParameter);
        if (animationEvent.intParameter == 1)
        {
            weaponCollider.SetActive(true);
        }
        else
        {
            weaponCollider.SetActive(false);
        }
    }


}
