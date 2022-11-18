using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ImageSprite
{
    public string image;
}


[Serializable]
public class ImageListVO
{
    public string text;
    public int imageCount;
    public List<ImageSprite> images;
}
