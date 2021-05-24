using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float tileSize = 5;
    private bool inputHeld;
    private bool isMoving;

    private IEnumerator moveCoroutine;
    private GameObject animParent;
    void Start()
    {
        inputHeld = false;
        isMoving = false;
        animParent = transform.Find("AnimParent").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handleMovementInput();
    }

    void handleMovementInput()
    {
        if (!isMoving)
        {
            float horiz = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");

            if (horiz != 0 || vert != 0)
            {
                if (!inputHeld)
                {
                    vert = horiz == 0 ? vert : 0;  //block diagonal
                    Vector3 moveDir = tileSize * new Vector3(horiz, 0, vert);
                    moveCoroutine = move(moveDir);
                    StartCoroutine(moveCoroutine);
                    inputHeld = true;
                }

            }
            else
            {
                inputHeld = false;
            }
        }

    }

    private IEnumerator move(Vector3 dir)
    {
        isMoving = true;
        float deg = 0f;
        if (dir.z > 0)
            deg = 0f;
        else if (dir.z < 0)
            deg = 180f;
        else if (dir.x > 0)
            deg = 90f;
        else if (dir.x < 0)
            deg = -90f;
        animParent.transform.eulerAngles = new Vector3(0, deg, 0);

        for (int i = 1; i <= 100; i++)
        {
            transform.Translate(0.01f * dir);
            animParent.transform.Rotate(0.01f * 90, 0, 0, Space.Self);
            yield return null;
        }
        isMoving = false;
    }
}
