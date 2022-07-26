using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int MaxPlatformcount = 3; // 생성할 발판의 개수

    public float MinSpawnCooltime = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float MaxSpawnCooltime = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float _nextSpawnCooltime; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] _platforms; // 미리 생성한 발판들
    private int _nextSpawnPlatformIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 _poolPosition = new Vector2(0, -20); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float _cooltime; // 마지막 배치 시점


    void Start()
    {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        _platforms = new GameObject[MaxPlatformcount];
        for (int i = 0; i < MaxPlatformcount; ++i)
        {
            _platforms[i] = Instantiate(platformPrefab, _poolPosition, Quaternion.identity);
            _platforms[i].SetActive(false);
        }
        _cooltime = 0;
    }

    void Update()
    {
        // 순서를 돌아가며 주기적으로 발판을 배치
        _cooltime += Time.deltaTime;
        if (_cooltime > _nextSpawnCooltime)
        {
            _cooltime = 0f;
            _nextSpawnCooltime = Random.Range(MinSpawnCooltime, MaxSpawnCooltime);

            Vector2 currnetPostion = new Vector2(xPos, Random.Range(yMin, yMax));
            GameObject currnetPlatform = _platforms[_nextSpawnPlatformIndex];
            currnetPlatform.transform.position = currnetPostion;
            currnetPlatform.SetActive(true);
            _nextSpawnPlatformIndex = (_nextSpawnPlatformIndex + 1) % MaxPlatformcount;

            // _platforms[_nextSpawnPlatformIndex].SetActive(false);
            // _platforms[_nextSpawnPlatformIndex].SetActive(true);
            // _platforms[_nextSpawnPlatformIndex].transform.position = new Vector2(xPos, Random.Range(yMin, yMax));
            //_nextSpawnPlatformIndex = (_nextSpawnPlatformIndex + 1) % MaxPlatformcount;
        }
    }
}