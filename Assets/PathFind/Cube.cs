using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public CubeState state = CubeState.normal;
    Animator animator;
    //bool mouseDown = false;
    float downTime;
    bool selectable
    {
        get
        {
            return state == CubeState.normal || state == CubeState.obstacle;
        }
    }
    public Tile tile;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        //mouseDown = false;
        animator.SetTrigger("highlight");
        tile.ShowHideNeighbor(true);
    }
    private void OnMouseExit()
    {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            switch (state)
            {
                case CubeState.normal:
                    animator.SetTrigger("normal");
                    break;
                case CubeState.obstacle:
                    animator.SetTrigger("obstacle");
                    break;
            }
        }
        else
        {
            if(selectable)
                Map.Instance.AddSelectIndex(tile.index);
            else
            {
                Map.Instance.UpdateStarAndEndTile();
            }
        }
        tile.ShowHideNeighbor(false);
    }
    

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            //mouseDown = true;
            downTime = Time.realtimeSinceStartup;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.realtimeSinceStartup - downTime > 0.5f)
            {
                if (selectable)
                    Map.Instance.AddSelectIndex(tile.index);
                Map.Instance.ChangeSelectionTileState(CubeState.obstacle);
            }
            else
            {
                if(tile.index == Map.Instance.endTileIndex)//起点终点互换
                {
                    Map.Instance.endTileIndex = Map.Instance.startTileIndex;
                }
                else
                {
                    Map.Instance.ChangeTileState(Map.Instance.startTileIndex, CubeState.normal);
                }
                Map.Instance.startTileIndex = tile.index;
                Map.Instance.UpdateStarAndEndTile();
            }
        }

        if(Input.GetMouseButtonUp(1))
        {
            if (Time.realtimeSinceStartup - downTime > 0.5f)
            {
                if (selectable)
                    Map.Instance.AddSelectIndex(tile.index);
                Map.Instance.ChangeSelectionTileState(CubeState.normal);
            }
            else
            {
                if (tile.index == Map.Instance.startTileIndex)//起点终点互换
                {
                    Map.Instance.startTileIndex = Map.Instance.endTileIndex;
                }
                else
                {
                    Map.Instance.ChangeTileState(Map.Instance.endTileIndex, CubeState.normal);
                }
                Map.Instance.endTileIndex = tile.index;
                Map.Instance.UpdateStarAndEndTile();
            }
        }
    }

    internal void ChangeState(CubeState state)
    {
        switch (state)
        {
            case CubeState.normal:
                animator.SetTrigger("normal");
                break;
            case CubeState.obstacle:
                animator.SetTrigger("obstacle");
                break;
            case CubeState.start:
                animator.SetTrigger("start");
                break;
            case CubeState.end:
                animator.SetTrigger("end");
                break;
        }
        this.state = state;
    }
}

public enum CubeState
{
    normal,
    highlight,
    obstacle,
    start,
    end
}
