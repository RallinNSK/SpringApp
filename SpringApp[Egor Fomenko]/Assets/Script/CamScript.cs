using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    [System.Serializable]
    public class AspectRatio
    {
        public string aspectRatioName;
        public Vector3 backroundSize; 
    }

    [Header("Spring Object")]
    public Transform Head;
    public Transform Tall;

    [Header("Background")]
    public GameObject background;
    public AspectRatio[] aspectRatio;

    private void Awake()
    {
        SetupPostition();
    }

    void Update () {
        if (GameController.instance.camFlag)
            Camera.main.transform.position = new Vector3(Head.transform.position.x + 2.5f, Camera.main.transform.position.y, Camera.main.transform.position.z);
        else
            Camera.main.transform.position = new Vector3(Tall.transform.position.x + 2.5f, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    private void SetupPostition()
    {
        if (Camera.main.aspect >= 0.75f) // 4:3
        {
            background.transform.localScale = aspectRatio[4].backroundSize;
        }
        else if (Camera.main.aspect >= 0.66f) // 3:2
        {
            background.transform.localScale = aspectRatio[3].backroundSize;
        }
        else if (Camera.main.aspect > 0.625f) // 16:10
        {
            background.transform.localScale = aspectRatio[2].backroundSize;
        }
        else if (Camera.main.aspect >= 0.6f) // 5:3
        {
            background.transform.localScale = aspectRatio[1].backroundSize;
        }
        else if (Camera.main.aspect > 0.56f) // 16:9
        {
            background.transform.localScale = aspectRatio[0].backroundSize;
        }
    }
}
