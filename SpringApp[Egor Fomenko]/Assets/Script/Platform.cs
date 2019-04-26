using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public bool checkPlatform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!checkPlatform)
        {
            GameController.instance.AddScore();
            checkPlatform = !checkPlatform;
        }
        MoveSpring.instance.StretchSpring();
        GameController.instance.clickFlag = true;
    }

    private void Update()
    {
        if(transform.position.x < Camera.main.transform.position.x - 3.5f)
        {
            Destroy(gameObject);
            GameController.instance.platfornCol--;
        }
    }
}
