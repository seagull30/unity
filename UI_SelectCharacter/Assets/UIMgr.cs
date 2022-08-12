using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    public GameObject[] characters;

    private int SelectedID { get; set; }

    public void ChangeCharacter(int index)
    {
        if (index >= characters.Length) return;
    }
}
