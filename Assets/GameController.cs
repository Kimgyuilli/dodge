using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour //���� �� ��Ʈ�� Ŭ����
{
    public GameObject BulletPrefeb; // Bullet ������ ������Ʈ
    public GameObject BombPrefeb; // ��ź ������ ������Ʈ
    public GameObject joystickObject; // ���̽�ƽ ������Ʈ
    public GameObject uiStartObject; // START��ư ������Ʈ
    public GameObject uiEndObject; // GAMEOVER UI ������Ʈ
    public Text TimeUi; // ���� �ð�(���� ����) ������Ʈ
    public Text scoreUi; // �ְ��� ������Ʈ

    private int sec; // ���� ����
    private int HighScore; // �ְ�����
    private float bulletSpawnInterval = 1f; // �Ѿ� ���� �ֱ�
    private float bulletSpeed = 10f; // �Ѿ� �ӵ�



    public void PressStart() // START ��ư Ŭ��
    {
        sec = 0; // ���ӽð�(����) 0���� �ʱ�ȭ
        uiStartObject.SetActive(false); // START��ư ��Ȱ��ȭ
        joystickObject.SetActive(true); // ���̽�ƽ Ȱ��ȭ
        InvokeRepeating("Bombprod", 1f, 5f); // ��ź ���� �Լ� 1�ʺ��� 5�ʸ��� ȣ��
        InvokeRepeating("Bulletprod", 0f, bulletSpawnInterval); 
        // �Ѿ� ���� �Լ� �������ڸ��� �Ѿ� ���� �ֱ⸶�� ȣ��
        InvokeRepeating("SetTime", 1f, 1f);
        // ���� �ð� �Լ� 1�ʸ��� ȣ��
        JoystickMove.playerSpeed = 0f;
        // �÷��̾� �ӵ� 0���� ����
        Invoke("SetPlayerSpeed", 0.3f);
        // 0.3�� �� �÷��̾� �ӵ� ����ȭ
        // ���� ���۽� �̵��� ������� �Ǵ� ���� �ذ��
    }

    public void PressReStart() // ���� ����� ��ư Ŭ��
    {
        sec = 0; // �ð�(����) 0���� �ʱ�ȭ
        // int�� sec char������ ����ȯ �� Time ������Ʈ�� �Ҵ�
        bulletSpeed = 10f; // Bullet �ӵ� �ʱ�ȭ
        bulletSpawnInterval = 1f; // Bullet �����ֱ� �ʱ�ȭ

        GameObject player = GameObject.Find("Player"); //Player ������Ʈ �Ҵ�
        player.transform.position = Vector3.zero; //Player ��ġ �ʱ�ȭ
        
        uiEndObject.SetActive(false); //gameoverUI ��Ȱ��ȭ
        JoystickMove.GameOver = false; //GameOver �÷��� false
        joystickObject.SetActive(true); // ���̽�ƽ Ȱ��ȭ

        JoystickMove.playerSpeed = 0f; 
        Invoke("SetPlayerSpeed", 0.4f);
        // �÷��̾� �̵� ����ȭ

        InvokeRepeating("Bombprod", 1f, 5f); // ��ź ���� �Լ� 1�ʺ��� 5�ʸ��� ȣ��
        InvokeRepeating("Bulletprod", 0f, bulletSpawnInterval);
        // �Ѿ� ���� �Լ� �������ڸ��� �Ѿ� ���� �ֱ⸶�� ȣ��
        InvokeRepeating("SetTime", 1f, 1f);
        // ���� �ð� �Լ� 1�ʸ��� ȣ��
    }

    void SetTime() // Timer �Լ�
    {
        sec++; // �ð�(����) 1 ����
        if (sec > HighScore) //�ְ��� ���Ž�
        {
            HighScore = sec; // �ְ��� ������Ʈ
        }
        TimeUi.text = "" + sec; // ���� txt ������Ʈ
        scoreUi.text = "" + HighScore; // �ְ��� txt
        // Increase difficulty over time
        if (sec % 10 == 0) // 10�ʸ���
        {
            bulletSpawnInterval *= 0.9f; //�Ѿ� ���� �ֱ� ����
            bulletSpeed *= 1.1f; // �Ѿ� �ӵ� ����
            CancelInvoke("Bulletprod"); // ���� �Ѿ� ���� �Լ� ȣ�� ����
            InvokeRepeating("Bulletprod", 0f, bulletSpawnInterval); // ���ҵ� ���� �ֱ�� �Լ� ȣ��
        }
    }
    public void GameOver() // ���� ���� �Լ�
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet"); // bullet �±׸� ���� ������Ʈ�� �Ҵ�
        foreach (GameObject bullet in bullets) Destroy(bullet); // bullet ����
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb"); // bomb �±׸� ���� ������Ʈ�� �Ҵ�
        foreach (GameObject bomb in bombs) Destroy(bomb); // bomb ����
        CancelInvoke("Bulletprod"); // bullet ���� �Լ� ȣ�� �ߴ�
        CancelInvoke("Bombprod"); // bomb ���� �Լ� ȣ�� �ߴ�
        CancelInvoke("SetTime"); // Ÿ�̸� �Լ� ȣ�� �ߴ�
        uiEndObject.SetActive(true); // GAMEOVER UI ���
        joystickObject.SetActive(false); // ���̽�ƽ ��Ȱ��ȭ
        JoystickMove.GameOver = true; // ���̽�ƽ GAMEOVER ���� ����
        JoystickMove.rb.velocity = Vector2.zero; // �÷��̾��� �ӵ��� 0���� ����
    }

    private void SetPlayerSpeed() // �÷��̾� �ӵ� ����ȭ �Լ�
    {
        JoystickMove.playerSpeed = 10f; // �÷��̾��� �ӵ��� 10���� ����
    }

    void Bombprod() // bomb ���� �Լ�
    {
        float xbomb = Random.Range(-12f, 12f); // ��ź x��ǥ ��������
        float ybomb = Random.Range(-20, 19f); // ��ź y��ǥ ��������
        Instantiate(BombPrefeb, new Vector3(xbomb, ybomb, 0f), Quaternion.identity); // ���� ��ǥ�� ��ź �̹��� ����
    }

    void Bulletprod() // bullet ���� �Լ�
    {
        GameObject bullet; // bullet ������Ʈ
        float xbullet = Random.Range(-26f, 26f); // bullet�� x��ǥ ��������
        float ybullet = Random.Range(-24f, 24f); // bullet�� y��ǥ ��������

        if (Random.value > 0.5f) // ���� �Ѿ� ��ġ
        {
            if (Random.value > 0.5f) // �����¿� 4���� ����� ��
            {
                bullet = Instantiate(BulletPrefeb, new Vector3(24f, ybullet, 0f), Quaternion.identity); 
                // ȭ�� ���� x: 24, y: ���� ��ǥ
            }
            else
            {
                bullet = Instantiate(BulletPrefeb, new Vector3(-24f, ybullet, 0f), Quaternion.identity); 
                // ȭ�� ���� x: -24, y: ���� ��ǥ
            }
        }
        else
        {
            if (Random.value > 0.5f)
            {
                bullet = Instantiate(BulletPrefeb, new Vector3(xbullet, 26f, 0f), Quaternion.identity);
                // ȭ�� ��� x: ������ǥ, y: 26
            }
            else
            {
                bullet = Instantiate(BulletPrefeb, new Vector3(xbullet, -26f, 0f), Quaternion.identity);
                // ȭ�� �ϴ� x: ������ǥ, y -26
            }
        }
        bullet.GetComponent<Bullet>().speed = bulletSpeed; // bullet �ӵ� �Ҵ�
    }
}
