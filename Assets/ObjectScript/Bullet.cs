using UnityEngine;

public class Bullet : MonoBehaviour // Bullet 클래스
{
    public float speed = 1f; // bullet 속도
    Vector3 direction; // vecor값(방향) 변수

    void Start()
    {
        Vector3 PlayerPos = GameObject.Find("Player").transform.position; // Player의 위치 할당
        Vector3 BulletPos = transform.position; // bullet의 위치 할당
        direction = (PlayerPos - BulletPos).normalized; // vector값을 1로 만들어서 방향만 저장

        Destroy(gameObject, 5f); // 생성 5초뒤 총알 삭제
    }
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime; // bullet 이동
    }
}
