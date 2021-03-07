using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes // типа пути
    {
        linear, //линейный  
        loop, //зацыкленый
    }

    public enum MovementType //выбор типа движения
    {
        Moveing,
        Lerping
    }

    public PathTypes PathType; //определить путь
    public int movementDirection = 1; // направдление движения
    public int moveingTo = 0; // точка к какой нуждно двигатся
    public Transform[] Points; //масив из точек междду которыми двигается обект

    public MovementType Type = MovementType.Moveing; // тип движения по пути
    public MovementPath MyPath; // задать путь
    public float speed = 1; // скорость движение по пути
    public float maxDistanse = 0.1f; // растояние на которое обект должен подойти к точке, чтобы понять пто он на нужной точке

    private IEnumerator<Transform> pointInPath;

    void Start()
    {
        if (MyPath == null) // проверка прикреплен ли путь
        {
            Debug.Log("Примени путь");
            return;
        }

        pointInPath = MyPath.GetNextPathPoint(); // обрашение к коротину GetNextPathPoint

        pointInPath.MoveNext(); //получение следушей точки в пути

        if (pointInPath.Current == null) // проверка точки к которой нужно двигатся
        {
            Debug.Log("Не задани точки");

            return;
        }

        transform.position = pointInPath.Current.position;
    }
    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null) // проверка отсутствия  пути
        {
            if (gameObject.name == "Points")
            {
                return;
            }
            else
            {
                Debug.Log("Не задан путь");  
                return;
            }

        }

        Vector3 targetPos = Vector3.zero;

        if (Type == MovementType.Moveing)
        {
            targetPos = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed); // движение обекта к следушей точки
        }
        else if (Type == MovementType.Lerping)
        {
            targetPos = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }

        transform.LookAt(targetPos);
        transform.position = targetPos;

        var distanceSquer = (transform.position - pointInPath.Current.position).sqrMagnitude; //проверка дошли ли мы к точке чтобы двигатся к следушей точке
        if (distanceSquer < maxDistanse * maxDistanse) // достаточно ли мы близко
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1)); // задаем рандомний цвет 3d обекта
            pointInPath.MoveNext();
        }
    }



    public void OnDrawGizmos() 
    {
        if (Points == null || Points.Length < 2) // проверка на наличие минимум 2 точек
        {
            return;
        }
        for (var i = 1; i < Points.Length; i++)
        {
            Gizmos.DrawLine(Points[i - 1].position, Points[i].position); // рисуем линии между точкоми
        }

        if (PathType == PathTypes.loop) // если выбран замкнутый путь
        {
            Gizmos.DrawLine(Points[0].position, Points[Points.Length - 1].position); //нарисовать от 0 до последней точки

        }

    }
    public IEnumerator<Transform> GetNextPathPoint() // получение положение следушей точки
    {
        if (Points == null || Points.Length < 1) // проверка точек
        {
            yield break; // позволяет выйти из коротина если нашел несоответствие
        }

        while (true)
        {
            yield return Points[moveingTo]; // возвращает текущее положение точки

            if (Points.Length == 1) // если точка всего одна, выйти
            {
                continue;
            }
            if (PathType == PathTypes.linear) // если путь не зациклен
            {
                if (moveingTo <= 0) // если двигаемся по нарастающей
                {
                    movementDirection = 1; // добавим 1 к движению
                }
                else if (moveingTo >= Points.Length - 1) // если >= -1 двигаемся по убывающей
                {
                    movementDirection = -1; // убраем 1 из движения
                }
            }

            moveingTo = moveingTo + movementDirection; // диапазон движения 

            if (PathType == PathTypes.loop) // если путь зациклен
            {
                if (moveingTo >= Points.Length)
                {
                    moveingTo = 0;
                }
                if (moveingTo < 0)
                {
                    moveingTo = Points.Length - 1;
                }
            }
        }
    }

}

