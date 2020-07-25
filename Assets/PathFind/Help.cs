using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public CubeState state;
    Animator animator;
    TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        animator = GetComponent<Animator>();
        switch(state)
        {
            case CubeState.normal:
                animator.SetTrigger("normal");
                textMesh.text = "路径";
                break;
            case CubeState.obstacle:
                animator.SetTrigger("obstacle");
                textMesh.text = "障碍";
                break;
            case CubeState.start:
                animator.SetTrigger("start");
                textMesh.text = "起点";
                break;
            case CubeState.end:
                animator.SetTrigger("end");
                textMesh.text = "终点";
                break;
            case CubeState.highlight:
                animator.SetTrigger("highlight");
                textMesh.text = "选中";
                break;
        }
    }
}
