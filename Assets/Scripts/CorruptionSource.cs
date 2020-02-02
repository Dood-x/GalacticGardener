using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionSource : MonoBehaviour
{
    public List<Corruption> allcorupted;

    public float regenTime;
    
    public int startDepth;

    private bool IsAllMissionComplete()
    {
        for (int i = 0; i < allcorupted.Count; ++i)
        {
            if (allcorupted[i].healed == false)
            {
                return false;
            }
        }

        return true;
    }






}
