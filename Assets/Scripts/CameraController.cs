using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float dumping = 1.5f; // степень отклонения камеры
    public Vector2 offset = new Vector2(2f, 2f); // офсет
    public bool isLeft; // взгляд персонажа
    private Transform player;
    private int lastX; // последнее координата по х

    void Start()
    {
        Time.timeScale = 1f;
        offset = new Vector2(Mathf.Abs(offset.x), offset.y); // переопределяем офсет
        FindPlayer(isLeft); // поиск игрока
    }

    void Update()
    {
        if(player) // игрок найден
        {
            int currentX = Mathf.RoundToInt(player.position.x); // определяем положение х
            //  сравниваем текущее положение с прошлым
            if (currentX > lastX) isLeft = false; else if (currentX < lastX) isLeft = true; 
            lastX = Mathf.RoundToInt(player.position.x);

            Vector3 target;
            if(isLeft) // если взгляд влево отклоняем камеру влево и наоборот
            {
                target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }


    // метод поиска игрока на сцене
    public void FindPlayer(bool playerIsLeft)
    {
        // поиск по тэгу Player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // определяем последнее положение x
        lastX = Mathf.RoundToInt(player.position.x);
        // определяем направление взгляда и смещаем камеру
        if(playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }
}
