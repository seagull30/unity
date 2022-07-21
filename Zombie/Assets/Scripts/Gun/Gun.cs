using System.Collections;
using UnityEngine;

// 총을 구현한다
public class Gun : MonoBehaviour
{
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    public State CurrentState { get; private set; }

    public Transform FireTransform;

    public ParticleSystem MuzzleFlashEffect;
    public ParticleSystem ShellEjectEffect;

    private LineRenderer _bulletLineRenderer;

    public GunData Data;

    private AudioSource _audioSource;


    public int RemainAmmoCount; // 남은 전체 탄약
    public int AmmoInMagazine; // 현재 탄창에 남아있는 탄약

    private float _fireDistance = 50f; // 사정거리
    private float _lastFireTime; // 총을 마지막으로 발사한 시점


    private void Awake()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        _bulletLineRenderer = GetComponent<LineRenderer>();
        _bulletLineRenderer.positionCount = 2;
        _bulletLineRenderer.enabled = false;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // 총 상태 초기화
        RemainAmmoCount = Data.InitialAmmoCount;
        AmmoInMagazine = Data.MagazineCapacity;
        CurrentState = State.Ready;
        _lastFireTime = 0f;
    }

    // 발사 시도
    public void Fire()
    {
        // 발사가 가능할 때?
        // 1. 상태가 레디일 때
        // 2. 쿨타임이 다 찼을 때
        if (CurrentState != State.Ready || Time.time < _lastFireTime + Data.FireCooltime)
            return;
        _lastFireTime = Time.time;
        Shot();
    }

    // 실제 발사 처리
    private void Shot()
    {
        Vector3 hitPosition;

        RaycastHit hit;

        if (Physics.Raycast(FireTransform.position, transform.forward, out hit, _fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(Data.Damage, hit.point, hit.normal);
            }
            // target?.OnDamage(Data.Damage, hit.point, hit.normal);

            hitPosition = hit.point;
        }
        else
        {
            hitPosition = FireTransform.position + transform.forward * _fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));

        --AmmoInMagazine;
        if (AmmoInMagazine <= 0)
        {
            CurrentState = State.Empty;
        }

    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        _bulletLineRenderer.SetPosition(0, FireTransform.position);
        _bulletLineRenderer.SetPosition(1, hitPosition);
        _bulletLineRenderer.enabled = true;

        _audioSource.PlayOneShot(Data.ShotClip);

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        _bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    public bool TryReload()
    {
        if (CurrentState != State.Reloading && RemainAmmoCount != 0)
        {
            StartCoroutine(ReloadRoutine());
            return true;
        }
        else
            return false;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 상태로 전환
        CurrentState = State.Reloading;

        _audioSource.PlayOneShot(Data.ReloadClip);

        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(Data.ReloadTime);

        RemainAmmoCount += AmmoInMagazine - Data.MagazineCapacity;
        AmmoInMagazine = Data.MagazineCapacity;

        // 총의 현재 상태를 발사 준비된 상태로 변경
        CurrentState = State.Ready;
    }
}