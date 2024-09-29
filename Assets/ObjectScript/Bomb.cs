using UnityEngine;

public class Bomb : MonoBehaviour // bomb 클래스
{
    public GameObject ExplodPrefeb; // 폭발 이펙트 프리펩 
    
    void Start()
    {
        Destroy(gameObject, 4.5f); // 생성 후 4.5초 뒤에 삭제
    }


    public void Explode() // 폭발 처리 함수
    {
        float explosionRadius = 7f; // 폭발 범위
        GameObject bomb = GameObject.FindGameObjectWithTag("bomb"); // bomb 태그의 오브젝트 할당
        Destroy(bomb); // 폭탄 삭제
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet"); // bullet 태그의 모든 오브젝트 할당
        foreach (GameObject bullet in bullets) //bullet 한개씩 처리
        {
            if (Vector3.Distance(transform.position, bullet.transform.position) <= explosionRadius) //bomb와 bullet 사이의 거리가 폭발 범위보다 작을 때
            {
                Destroy(bullet); // 해당하는 bullet 삭제
            }
        }

        GameObject explosionEffect = Instantiate(ExplodPrefeb, transform.position, Quaternion.identity); // 폭탄 위치에 폭발 오브젝트 생성
        Destroy(explosionEffect, 1f); // 1초 후에 폭발 이펙트 삭제
    }
}
