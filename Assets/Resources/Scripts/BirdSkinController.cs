using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BirdSkinController
{
    public static Sprite birdSprite;

    public void SetBirdSkin(int i)
    {
        birdSprite = Resources.Load($"Sprites/BirdSkins/Bird{i}") as Sprite;
    }
}
