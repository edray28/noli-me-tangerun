using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour {
    public PieceType type;
    private Piece currentPiece;
    public void Spawn()
    {
        int amountobj = 0;
        switch(type)
        {
            case PieceType.jump:
                amountobj = LevelManager.Instance.jumps.Count;
                break;
            case PieceType.ramp:
                amountobj = LevelManager.Instance.ramps.Count;
                break;
            case PieceType.longblock:
                amountobj = LevelManager.Instance.longblocks.Count;
                break;
            case PieceType.slide:
                amountobj = LevelManager.Instance.slides.Count;
                break;
            case PieceType.question:
                amountobj = LevelManager.Instance.questions.Count;
                break;



        }
        currentPiece = LevelManager.Instance.GetPiece(type, Random.Range(0,amountobj-1 ));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void Despawn()
    {
        currentPiece.gameObject.SetActive(false);
    }
}
