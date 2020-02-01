using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursion : MonoBehaviour
{
    void recursion(int depth)
    {
        recursion(depth + 1);
    }
}

