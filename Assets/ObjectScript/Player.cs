using UnityEngine;

public class Player : MonoBehaviour // Player �浹 ���� Class
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("bomb")) // ��ź�� �浹��
        {
            Bomb bomb = FindObjectOfType<Bomb>(); // Bomb  ȣ��
            bomb.Explode(); // Bomb Class �� ���� Explode ȣ��
        } 
        else if (collider.CompareTag("bullet")) // �Ѿ˰� �浹
        {
            GameController gameController = FindObjectOfType<GameController>(); // GameController Class ȣ��
            gameController.GameOver(); // GameController Class ���� GameOver �Լ� ȣ��
        } 
    }
}
