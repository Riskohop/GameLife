using System;
using System.Collections;
using System.Collections.Generic;
using Array2DEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "New Brush", menuName = "PixelBrush", order = 51)]
public class Brush : ScriptableObject
{
    public Array2DBool brushMatrix;
}
