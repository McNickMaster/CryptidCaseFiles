using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleTextData", menuName = "Text/NewTextData", order = 1)]
public class SimpleTextData : ScriptableObject
{
    public string file = "";
    public int order = -1;
}
