using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour //게임 내 컨트롤 클래스
{
    public GameObject BulletPrefeb; // Bullet 프리팹 오브젝트
    public GameObject BombPrefeb; // 폭탄 프리팹 오브젝트
    public GameObject joystickObject; // 조이스틱 오브젝트
    public GameObject uiStartObject; // START버튼 오브젝트
    public GameObject uiEndObject; // GAMEOVER UI 오브젝트
    public Text TimeUi; // 게임 시간(현재 점수) 오브젝트
    public Text scoreUi; // 최고기록 오브젝트

    private int sec; // 현재 점수
    private int HighScore; // 최고점수
    private float bulletSpawnInterval = 1f; // 총알 생성 주기
    private float bulletSpeed = 10f; // 총알 속도



    public void PressStart() // START 버튼 클릭
    {
        sec = 0; // 게임시간(점수) 0으로 초기화
        uiStartObject.SetActive(false); // START버튼 비활성화
        joystickObject.SetActive(true); // 조이스틱 활성화
        InvokeRepeating("Bombprod", 1f, 5f); // 폭탄 생성 함수 1초부터 5초마다 호출
        InvokeRepeating("Bulletprod", 0f, bulletSpawnInterval); 
        // 총알 생성 함수 시작하자마자 총알 생성 주기마다 호출
        InvokeRepeating("SetTime", 1f, 1f);
        // 게임 시간 함수 1초마다 호출
        JoystickMove.playerSpeed = 0f;
        // 플레이어 속도 0으로 설정
        Invoke("SetPlayerSpeed", 0.3f);
        // 0.3초 후 플레이어 속도 정상화
        // 게임 시작시 이동이 마음대로 되는 오류 해결용
    }

    public void PressReStart() // 게임 재시작 버튼 클릭
    {
        sec = 0; // 시간(점수) 0으로 초기화
        // int형 sec char형으로 형변환 후 Time 오브젝트에 할당
        bulletSpeed = 10f; // Bullet 속도 초기화
        bulletSpawnInterval = 1f; // Bullet 생성주기 초기화

        GameObject player = GameObject.Find("Player"); //Player 오브젝트 할당
        player.transform.position = Vector3.zero; //Player 위치 초기화
        
        uiEndObject.SetActive(false); //gameoverUI 비활성화
        JoystickMove.GameOver = false; //GameOver 플래그 false
        joystickObject.SetActive(true); // 조이스틱 활성화

        JoystickMove.playerSpeed = 0f; 
        Invoke("SetPlayerSpeed", 0.4f);
        // 플레이어 이동 정상화

        InvokeRepeating("Bombprod", 1f, 5f); // 폭탄 생성 함수 1초부터 5초마다 호출
        InvokeRepeating("Bulletprod", 0f, bulletSpawnInterval);
        // 총알 생성 함수 시작하자마자 총알 생성 주기마다 호출
        InvokeRepeating("SetTime", 1f, 1f);
        // 게임 시간 함수 1초마다 호출
    }

    void SetTime() // Timer 함수
    {
        sec++; // 시간(점수) 1 증가
        if (sec > HighScore) //최고기록 갱신시
        {
            HighScore = sec; // 최고기록 업데이트
        }
        TimeUi.text = "" + sec; // 점수 txt 업데이트
        scoreUi.text = "" + HighScore; // 최고기록 txt
        // Increase difficulty over time
        if (sec % 10 == 0) // 10초마다
        {
            bulletSpawnInterval *= 0.9f; //총알 생성 주기 감소
            bulletSpeed *= 1.1f; // 총알 속도 증가
            CancelInvoke("Bulletprod"); // 기존 총알 생성 함수 호출 제거
            InvokeRepeating("Bulletprod", 0f, bulletSpawnInterval); // 감소된 생성 주기로 함수 호출
        }
    }
    public void GameOver() // 게임 오버 함수
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet"); // bullet 태그를 가진 오브젝트들 할당
        foreach (GameObject bullet in bullets) Destroy(bullet); // bullet 제거
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb"); // bomb 태그를 가진 오브젝트들 할당
        foreach (GameObject bomb in bombs) Destroy(bomb); // bomb 제거
        CancelInvoke("Bulletprod"); // bullet 생성 함수 호출 중단
        CancelInvoke("Bombprod"); // bomb 생성 함수 호출 중단
        CancelInvoke("SetTime"); // 타이머 함수 호출 중단
        uiEndObject.SetActive(true); // GAMEOVER UI 출력
        joystickObject.SetActive(false); // 조이스틱 비활성화
        JoystickMove.GameOver = true; // 조이스틱 GAMEOVER 상태 설정
        JoystickMove.rb.velocity = Vector2.zero; // 플레이어의 속도를 0으로 설정
    }

    private void SetPlayerSpeed() // 플레이어 속도 정상화 함수
    {
        JoystickMove.playerSpeed = 10f; // 플레이어의 속도를 10으로 설정
    }

    void Bombprod() // bomb 생성 함수
    {
        float xbomb = Random.Range(-12f, 12f); // 폭탄 x좌표 랜덤생성
        float ybomb = Random.Range(-20, 19f); // 폭탄 y좌표 랜덤생성
        Instantiate(BombPrefeb, new Vector3(xbomb, ybomb, 0f), Quaternion.identity); // 랜덤 좌표에 폭탄 이미지 생성
    }

    void Bulletprod() // bullet 생성 함수
    {
        GameObject bullet; // bullet 오브젝트
        float xbullet = Random.Range(-26f, 26f); // bullet의 x좌표 랜덤생성
        float ybullet = Random.Range(-24f, 24f); // bullet의 y좌표 랜덤생성

        if (Random.value > 0.5f) // 랜덤 총알 위치
        {
            if (Random.value > 0.5f) // 상하좌우 4가지 경우의 수
            {
                bullet = Instantiate(BulletPrefeb, new Vector3(24f, ybullet, 0f), Quaternion.identity); 
                // 화면 우측 x: 24, y: 랜덤 좌표
            }
            else
            {
                bullet = Instantiate(BulletPrefeb, new Vector3(-24f, ybullet, 0f), Quaternion.identity); 
                // 화면 좌측 x: -24, y: 랜덤 좌표
            }
        }
        else
        {
            if (Random.value > 0.5f)
            {
                bullet = Instantiate(BulletPrefeb, new Vector3(xbullet, 26f, 0f), Quaternion.identity);
                // 화면 상단 x: 랜덤좌표, y: 26
            }
            else
            {
                bullet = Instantiate(BulletPrefeb, new Vector3(xbullet, -26f, 0f), Quaternion.identity);
                // 화면 하단 x: 랜덤좌표, y -26
            }
        }
        bullet.GetComponent<Bullet>().speed = bulletSpeed; // bullet 속도 할당
    }
}
