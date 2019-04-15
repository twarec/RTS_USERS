using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkil
{
    void Active();


    Sprite Icon { get; set; }
    string Name { get; set; }

    System.Action EndAction { get; set; }
}
