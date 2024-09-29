using UnityEngine;

public class JoystickMove : MonoBehaviour //���̽�ƽ Ŭ����
{
    public Joystick movementJoystick; // ���̽�ƽ �Է¹ޱ� ���� ��ü
    public static float playerSpeed = 10f; // Player�� �̵��ӵ�
    public static Rigidbody2D rb; // Rigidbody�� �̿��� �÷��̾� ����
    public static bool GameOver = false; // ���� ���� ����

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // player�� Rigidbody �Ҵ�
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameOver) // ���� ���� ���°� �ƴ� ���� �������� ó��
        {
            if (movementJoystick.Direction.y != 0) // ���̽�ƽ�� �������� ������
            {
                rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed); 
                // ���̽�ƽ ���� ���� ���� player �̵�
            }
            else // ���̽�ƽ ������ ���� ��
            {
                rb.velocity = Vector2.zero; // player �̵� x
            }
        }

    }
}