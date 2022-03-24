using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemData : MonoBehaviour
{[Header("Setup")]
    public string objectName;
    public Transform goToPoint;
    public int itemID, requiredItemID;
    public Vector2 nameTagSize = new Vector2(3, 0.85f);
    public string itemName;
    public Vector2 itemNameTagSize = new Vector2(3, 0.85f);

    [Header("Success")]
    public GameObject[] objectsToRemove;
    public GameObject[] objectsToSetActive;
    public AnimationData successAnimation;
    public Sprite itemSlotSprite;

    [Header("Failure")]
    [TextArea(3, 3)]
    public string hintMessage;
    public Vector2 hintBoxSize = new Vector2(6, 0.85f);
    public string itemTextInInventory;
}
