using UnityEngine;

public class Bullet : MonoBehaviour // Bullet Ŭ����
{
    public float speed = 1f; // bullet �ӵ�
    Vector3 direction; // vecor��(����) ����

    void Start()
    {
        Vector3 PlayerPos = GameObject.Find("Player").transform.position; // Player�� ��ġ �Ҵ�
        Vector3 BulletPos = transform.position; // bullet�� ��ġ �Ҵ�
        direction = (PlayerPos - BulletPos).normalized; // vector���� 1�� ���� ���⸸ ����

        Destroy(gameObject, 5f); // ���� 5�ʵ� �Ѿ� ����
    }
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime; // bullet �̵�
    }
}
