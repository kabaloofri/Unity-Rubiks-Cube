using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AutoRotate : MonoBehaviour {
    public static Stack<string> moves = new();
    static readonly List<string> allMoves = new List<string> {
        "U", "D", "L", "R", "B", "F",
        "U'", "D'", "L'", "R'", "B'", "F'",
        "U2", "D2", "L2", "R2", "B2", "F2"
    };

    private CubeState cubeState;
    private ReadCube readCube;
    void Start() {
        cubeState = GetComponent<CubeState>();
        readCube = GetComponent<ReadCube>();
    }
    void Update() {
        if (moves.Count > 0 && !cubeState.autoRotating && cubeState.initialized) {
            // execute move
            DoMove(moves.Pop());
        }
    }

    public void ShuffleCube() {
        for (int i = 0; i < Random.Range(10, 30); i++) {
            int index = Random.Range(0, allMoves.Count);
            moves.Push(allMoves[index]);
        }
    }

    void DoMove(string move) {
        cubeState.autoRotating = true;
        readCube.ReadState();

        switch (move) {
            case "U":
                RotateSide(cubeState.up, -90f);
                break;
            case "U'":
                RotateSide(cubeState.up, 90f);
                break;
            case "U2":
                RotateSide(cubeState.up, 180);
                break;

            case "D":
                RotateSide(cubeState.down, 180);
                break;
            case "D'":
                RotateSide(cubeState.down, 180);
                break;
            case "D2":
                RotateSide(cubeState.down, 180);
                break;

            case "R":
                RotateSide(cubeState.right, 180);
                break;
            case "R'":
                RotateSide(cubeState.right, 180);
                break;
            case "R2":
                RotateSide(cubeState.right, 180);
                break;

            case "L":
                RotateSide(cubeState.left, 180);
                break;
            case "L'":
                RotateSide(cubeState.left, 180);
                break;
            case "L2":
                RotateSide(cubeState.left, 180);
                break;

            case "B":
                RotateSide(cubeState.back, 180);
                break;
            case "B'":
                RotateSide(cubeState.back, 180);
                break;
            case "B2":
                RotateSide(cubeState.back, 180);
                break;

            case "F":
                RotateSide(cubeState.front, 180);
                break;
            case "F'":
                RotateSide(cubeState.front, 180);
                break;
            case "F2":
                RotateSide(cubeState.front, 180);
                break;

            default:
                print("no move found: " + move);
                break;
        }
    }


    void RotateSide(List<GameObject> side, float angle) {
        var pivot = side[4].transform.parent.GetComponent<PivotRotation>();
        pivot.StartAutoRotate(side, angle);
    }

}
