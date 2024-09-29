using UnityEngine;

public class Player : MonoBehaviour // Player 충돌 판정 Class
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("bomb")) // 폭탄과 충돌시
        {
            Bomb bomb = FindObjectOfType<Bomb>(); // Bomb  호출
            bomb.Explode(); // Bomb Class 내 폭발 Explode 호출
        } 
        else if (collider.CompareTag("bullet")) // 총알과 충돌
        {
            GameController gameController = FindObjectOfType<GameController>(); // GameController Class 호출
            gameController.GameOver(); // GameController Class 내의 GameOver 함수 호출
        } 
    }
}
