using UnityEngine;

public class JoystickMove : MonoBehaviour //조이스틱 클래스
{
    public Joystick movementJoystick; // 조이스틱 입력받기 위한 객체
    public static float playerSpeed = 10f; // Player의 이동속도
    public static Rigidbody2D rb; // Rigidbody를 이용해 플레이어 제어
    public static bool GameOver = false; // 게임 오버 여부

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // player의 Rigidbody 할당
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameOver) // 게임 오버 상태가 아닐 때만 움직임을 처리
        {
            if (movementJoystick.Direction.y != 0) // 조이스틱에 움직임이 있으면
            {
                rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed); 
                // 조이스틱 방향 값에 따라 player 이동
            }
            else // 조이스틱 조작이 없을 때
            {
                rb.velocity = Vector2.zero; // player 이동 x
            }
        }

    }
}