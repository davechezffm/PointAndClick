using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName="AnimationData", menuName ="Scriptable Objects / Animation Data",order =1)]
public class AnimationData : ScriptableObject
{
    public Sprite[] sprites;
    public int framesOfGap;
    public static float targetFrameTime = 0.0167f;
    public bool loop;
    public bool returnToBase;
    public GameManager.soundsNames[] sounds;
}
