using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfItemsCollected : MonoBehaviour
{
    public GameManager gm;
    public Animator open;
    public Animator endopen;

    private void Update()
    {
        if (gm.items >= 1)
        {
            open.SetTrigger("Smell");
        }

        if (gm.items >= 6)
        {
            endopen.SetTrigger("end");
        }
    }
}
