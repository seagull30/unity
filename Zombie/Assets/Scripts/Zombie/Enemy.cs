using System.Collections;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드를 가져오기

// 적 AI를 구현한다
public class Enemy : LivingEntity
{
    public ParticleSystem HitEffect; // 피격시 재생할 파티클 효과
    public AudioClip DeathSound; // 사망시 재생할 소리
    public AudioClip HitSound; // 피격시 재생할 소리
    public LayerMask TargetLayer; // 추적 대상 레이어

    private LivingEntity _target; // 추적할 대상

    private NavMeshAgent _navMashAgent; // 경로계산 AI 에이전트
    private Animator _animator; // 애니메이터 컴포넌트
    private AudioSource _audioSource; // 오디오 소스 컴포넌트
    private Renderer _renderer; // 렌더러 컴포넌트

    public float Damage = 20f; // 공격력
    public float timeBetAttack = 0.5f; // 공격 간격
    private float _lastAttackTime; // 마지막 공격 시점
    private Collider[] _targetCandidates = new Collider[5];
    private int _targetCandidateCount;

    // 추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool _hasTargetFound
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (_target != null && !_target.ISdead)
            {
                return true;
            }

            // 그렇지 않다면 false
            return false;
        }
    }

    private void Awake()
    {
        // 초기화
        _navMashAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponentInChildren<Renderer>();


    }

    // 적 AI의 초기 스펙을 결정하는 셋업 메서드
    public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor)
    {
        InitialHealth = newHealth;
        Damage = newDamage;
        _navMashAgent.speed = newSpeed;
        _renderer.material.color = skinColor;
    }

    private void Start()
    {
        // 게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(updatePath());
    }

    private void Update()
    {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션을 재생
        _animator.SetBool(ZombieAnimID.HASTARGET, _hasTargetFound);
    }

    // 주기적으로 추적할 대상의 위치를 찾아 경로를 갱신
    private IEnumerator updatePath()
    {
        // 살아있는 동안 무한 루프
        while (!ISdead)
        {
            if (_hasTargetFound)
            {
                _navMashAgent.isStopped = false;
                _navMashAgent.SetDestination(_target.transform.position);

            }
            else
            {
                _navMashAgent.isStopped = true;

                _targetCandidateCount = Physics.OverlapSphereNonAlloc(transform.position, 20f, _targetCandidates, TargetLayer);
                foreach (Collider targetCandidate in _targetCandidates)
                {
                    LivingEntity livingEntity = targetCandidate.GetComponent<LivingEntity>();
                    Debug.Assert(livingEntity != null);
                    if (livingEntity.ISdead == false)
                    {
                        _target = livingEntity;
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    // 데미지를 입었을때 실행할 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // LivingEntity의 OnDamage()를 실행하여 데미지 적용
        base.OnDamage(damage, hitPoint, hitNormal);
        if (!ISdead)
        {
            _audioSource.PlayOneShot(HitSound);
            HitEffect.transform.position = hitPoint;
            HitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            HitEffect.Play();
        }

    }

    // 사망 처리
    public override void Die()
    {
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        base.Die();
        _audioSource.PlayOneShot(DeathSound);
        _animator.SetTrigger(ZombieAnimID.Die);
        _navMashAgent.isStopped = true;
        _navMashAgent.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        // 트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행   
        if (ISdead || Time.time - _lastAttackTime < timeBetAttack)
        {
            return;
        }
        LivingEntity livingEntity = other.GetComponent<LivingEntity>();
        if (livingEntity == _target)
        {
            Vector3 hitPosition = other.ClosestPoint(transform.position);
            Vector3 hitNormal = transform.position - other.transform.position;
            livingEntity.OnDamage(Damage, hitPosition, hitNormal);
            _lastAttackTime = Time.time;
        }
    }
}