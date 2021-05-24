using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float tileSize = 5;
    private float lastMovedOnbeat;
    private bool inputHeld;
    private bool isMoving;

    private IEnumerator moveCoroutine;
    private GameObject animParent;

    private LevelController parent;
    void Start()
    {
        lastMovedOnbeat = -1;
        inputHeld = false;
        isMoving = false;
        animParent = transform.Find("AnimParent").gameObject;
        parent = GetComponentInParent<LevelController>();
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

                    float currentbeat = parent.conductor.songPositionInBeats;
                    if (parent.isOnBeat() && currentbeat >= (lastMovedOnbeat + 0.5))
                    {
                        Debug.Log("HIT");
                        //transform.Translate(moveDir);
                        moveCoroutine = move(moveDir);
                        StartCoroutine(moveCoroutine);
                        lastMovedOnbeat = currentbeat;
                    }
                    else
                    {
                        Debug.Log("MISS");
                    }
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
        var length = 30.0f;
        animParent.transform.eulerAngles = new Vector3(0, deg, 0);

        for (int i = 1; i <= length; i++)
        {
            transform.Translate((1.0f / length) * dir);
            animParent.transform.Rotate((1.0f / length) * 90, 0, 0, Space.Self);
            yield return null;
        }
        isMoving = false;
    }
}
