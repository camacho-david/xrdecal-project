// Shatter Toolkit
// Copyright 2015 Gustav Olsson
using System.Collections.Generic;
using UnityEngine;

namespace ShatterToolkit
{
    public abstract class ColorMapper : MonoBehaviour
    {
        public abstract void Map(IList<Vector3> points, Vector3 planeNormal, out Color32[] colorsA, out Color32[] colorsB);
    }
}