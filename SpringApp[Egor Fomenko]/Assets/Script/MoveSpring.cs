using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpring : MonoBehaviour
{
    public Transform[] points;
    //для включения физики падения(условная красивость, хотя скорее костыли)
    public Rigidbody2D Head;
    public Rigidbody2D Tall;

    private bool Switch = true;

    public static MoveSpring _instance;
    public static MoveSpring instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MoveSpring>();
            }

            return _instance;
        }
    }

    public void StretchSpring()
    {
        switch (Switch)
        {
            case true:
                StartCoroutine(Courutine_StretchSpring_1());
                break;
            case false:
                StartCoroutine(Courutine_StretchSpring_2());
                break;
        }
    }

    public void SpringMove()
    {
        StopAllCoroutines();
        switch (Switch)
        {
            case true:
                StartCoroutine(Courutine_SpringMove_1());
                break;

            case false:
                StartCoroutine(Courutine_SpringMove_2());
                break;
        }
    }
    
    //Первый шаг перемешения
    IEnumerator Courutine_SpringMove_1()
    {
        Vector3 p1, p2, p3;
        p1 = new Vector3(points[0].localPosition.x, points[3].localPosition.y, 0f);
        p2 = new Vector3(points[3].localPosition.x + points[3].localPosition.y / 2, points[3].localPosition.y, 0f);
        p3 = new Vector3(points[3].localPosition.x + points[3].localPosition.y / 2, points[0].localPosition.y, 0f);

        points[1].localPosition = p1;
        points[2].localPosition = p1;

        //Опускаем голову
        while (points[3].localPosition != p3)
        {
            points[3].localPosition = Vector3.MoveTowards(points[3].localPosition, p3, 0.1f);
            points[2].localPosition = Vector3.MoveTowards(points[2].localPosition, p2, 0.1f);
            yield return null;
        }
        Head.simulated = !Head.simulated;
        //Подбираем хвост
        while (points[0].localPosition != p3 + Vector3.up)
        {
            points[2].localPosition = Vector3.MoveTowards(points[2].localPosition, p3 + Vector3.up, 0.1f);
            points[1].localPosition = Vector3.MoveTowards(points[1].localPosition, p3 + Vector3.up, 0.1f);
            points[0].localPosition = Vector3.MoveTowards(points[0].localPosition, p3 + Vector3.up, 0.05f);
            yield return null;
        }
        Tall.simulated = !Tall.simulated;
        points[2].localPosition = p3 + Vector3.up;
        points[1].localPosition = p3 + Vector3.up;

        Switch = !Switch;
    }

    //Второй шаг перемешения
    IEnumerator Courutine_SpringMove_2()
    {
        Vector3 p0, p1, p2;
        p1 = new Vector3(points[0].localPosition.x + points[0].localPosition.y / 2, points[0].localPosition.y, 0f);
        p2 = new Vector3(points[3].localPosition.x, points[0].localPosition.y, 0f);
        p0 = new Vector3(points[0].localPosition.x + points[0].localPosition.y/2, points[3].localPosition.y, 0f);

        points[1].localPosition = p2;
        points[2].localPosition = p2;

        ////Опускаем голову
        while (points[0].localPosition != p0)
        {
            points[0].localPosition = Vector3.MoveTowards(points[0].localPosition, p0, 0.1f);
            points[1].localPosition = Vector3.MoveTowards(points[1].localPosition, p1, 0.1f);
            yield return null;
        }
        Tall.simulated = !Tall.simulated;
        ////Подбираем хвост
        while (points[3].localPosition != p0 + Vector3.up)
        {
            points[1].localPosition = Vector3.MoveTowards(points[1].localPosition, p0 + Vector3.up, 0.1f);
            points[2].localPosition = Vector3.MoveTowards(points[2].localPosition, p0 + Vector3.up, 0.1f);
            points[3].localPosition = Vector3.MoveTowards(points[3].localPosition, p0 + Vector3.up, 0.05f);
            yield return null;
        }
        Head.simulated = !Head.simulated;
        points[2].localPosition = p0 + Vector3.up;
        points[3].localPosition = p0 + Vector3.up;

        Switch = !Switch;
    }

    IEnumerator Courutine_StretchSpring_1()
    {
        while (points[3].localPosition.y < 5f)
        {
            points[3].position = Vector3.MoveTowards(points[3].position, points[3].position + Vector3.up, 0.05f);
            yield return null;
        }
    }

    IEnumerator Courutine_StretchSpring_2()
    {
        while (points[0].localPosition.y < 5f)
        {
            points[0].position = Vector3.MoveTowards(points[0].position, points[0].position + Vector3.up, 0.05f);
            yield return null;
        }
    }
}