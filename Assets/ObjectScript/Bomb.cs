using UnityEngine;

public class Bomb : MonoBehaviour // bomb Ŭ����
{
    public GameObject ExplodPrefeb; // ���� ����Ʈ ������ 
    
    void Start()
    {
        Destroy(gameObject, 4.5f); // ���� �� 4.5�� �ڿ� ����
    }


    public void Explode() // ���� ó�� �Լ�
    {
        float explosionRadius = 7f; // ���� ����
        GameObject bomb = GameObject.FindGameObjectWithTag("bomb"); // bomb �±��� ������Ʈ �Ҵ�
        Destroy(bomb); // ��ź ����
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet"); // bullet �±��� ��� ������Ʈ �Ҵ�
        foreach (GameObject bullet in bullets) //bullet �Ѱ��� ó��
        {
            if (Vector3.Distance(transform.position, bullet.transform.position) <= explosionRadius) //bomb�� bullet ������ �Ÿ��� ���� �������� ���� ��
            {
                Destroy(bullet); // �ش��ϴ� bullet ����
            }
        }

        GameObject explosionEffect = Instantiate(ExplodPrefeb, transform.position, Quaternion.identity); // ��ź ��ġ�� ���� ������Ʈ ����
        Destroy(explosionEffect, 1f); // 1�� �Ŀ� ���� ����Ʈ ����
    }
}
