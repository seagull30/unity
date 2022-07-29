using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    None,
    Idle,       // ���
    Walk,       // ����
    Run,        // ����
    Attack,     // ����
    Knockback,  // �ǰ�
    Hide        // ����
}

public class EnemyAI : MonoBehaviour
{
    public EnemyState state;
    public EnemyState prevState = EnemyState.None;

    private Animator _animator;

    // �̵� ����
    private Vector3 _targetPosition;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private float _rotationSpeed = 1f;

    // �� Ž�� ����
    public GameObject target;
    private bool _isFindEnemy = false;
    private Camera _eye;
    private Plane[] _eyePlanes;

    // ���� �浹 ����
    GameObject weaponCollider;

    // ����
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
    // �� �����Ӹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
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
        // ���������� �̵��ϴ� �ڽ�
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
        // ���������� �̵��ϴ� �ڽ�
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
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        Debug.Log("������ ����");
        _animator.SetBool(AnimID.ISIDLE, true);

        while (true)
        {
            yield return new WaitForSeconds(2f);
            // �ð����� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
            ChangeState(EnemyState.Hide);
            yield break;
        }
    }

    IEnumerator CoroutineWalk()
    {
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        Debug.Log("���� ���� ����");

        _animator.SetBool(AnimID.ISWALK, true);
        _dissolveCtrl.ChangeState(DissolveCtrl.State.Hide_Off);

        // ������ ����
        _targetPosition = transform.position + new Vector3(Random.Range(-7f, 7f), 0f, Random.Range(-7f, 7f));

        while (true)
        {
            yield return new WaitForSeconds(10f);
            // �ð����� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
            ChangeState(EnemyState.Idle);
        }
    }

    IEnumerator CoroutineRun()
    {
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        Debug.Log("���� ���� ����");
        _animator.SetBool(AnimID.ISRUN, true);
        _targetPosition = target.transform.position;

        while (true)
        {
            yield return new WaitForSeconds(5f);
            // �ð����� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        }
    }

    IEnumerator CoroutineAttack()
    {
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        _animator.SetTrigger(AnimID.ISATTACK);

        yield return new WaitForSeconds(1f);
        ChangeState(EnemyState.Idle);
        yield break;

    }

    IEnumerator CoroutineKnockback()
    {
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)

        while (true)
        {
            yield return new WaitForSeconds(2f);
            // �ð����� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
        }
    }
    IEnumerator CoroutineHide()
    {
        // �ѹ��� �����ؾ� �ϴ� ���� (���°� �ٲ� �� ����)
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
