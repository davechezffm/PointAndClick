using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems=new List<int>();
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    [Header("Setup")]
    public AnimationData[] playerAnimations;
    public RectTransform nameTag, hintBox;

    [Header("Local Scenes")]
    public Image blockingImage;
    public GameObject[] localScenes;
    int activeLocalScene=0;
    public Transform []  playerStartingPositions;
    

    public IEnumerator MoveToPoint(Transform myObject, Vector2 point)
    {
        Vector2 positionDifference = point - (Vector2)myObject.position;
        //Change animation of the Player
        if(myObject.GetComponentInChildren<SpriteRenderer>()&& positionDifference.x != 0){
            //If the X difference is bigger than zero, X is flipped so the player animation goes to the right.
            myObject.GetComponentInChildren<SpriteRenderer>().flipX = positionDifference.x > 0;

        }
        while (positionDifference.magnitude > moveAccuracy)
        {
            myObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime);
            positionDifference = point - (Vector2)myObject.position;
            yield return null;
        }
        myObject.position = point;

        if (myObject == FindObjectOfType<ClickManager>().player)
            FindObjectOfType<ClickManager>().playerWalking = false;
        yield return null;
    }

    public void UpdateNameTag(ItemData item)
    {
        if (item == null)
        {
            nameTag.parent.gameObject.SetActive(false);
            return;
        }
        //Change the name
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;
        //Change the size of the name tag box
        nameTag.sizeDelta = item.nameTagSize;
        //Move the name tag so it is on the screen
        nameTag.localPosition = new Vector2(item.nameTagSize.x / 2, -0.5f);
    }

    public void UpdateHintBox(ItemData item, bool playerFlipped)
    {
        if (item == null)
        {//Hide Hint Box
            hintBox.gameObject.SetActive(false);
            return;
        }

        // Show Hint Box
        hintBox.gameObject.SetActive(true);
        //Chnage name
        hintBox.GetComponentInChildren<TextMeshProUGUI>().text = item.hintMessage;
        //Change Size
        hintBox.sizeDelta = item.hintBoxSize;
        //Move Tag
        if (playerFlipped)
            hintBox.localPosition = new Vector2(-1, 2);
        else hintBox.localPosition = Vector2.zero;
    }

    public void CheckSpecialConditions(ItemData item, bool cangetitem)
    {// Checks the value of the item
        switch (item.itemID)
        {
            case -11:
                //Go to Scene 1
                StartCoroutine(ChangeScene(1, 0));
                break;

            case -12:
                //Go to Scene 2
                StartCoroutine(ChangeScene(2, 0));
                break;

            case -1:
                //Win the Game
                if (cangetitem)
                {
                    float delay =
                    item.successAnimation.sprites.Length * item.successAnimation.framesOfGap * AnimationData.targetFrameTime;
                    StartCoroutine(ChangeScene(3, delay));
                }
                break;
            default:
                break;

        }
    }

    public IEnumerator ChangeScene(int sceneNumber, float delay)
    {// scene goes black and no clicking allowed

        yield return new WaitForSeconds(delay);
        //If end Game, remove the player.

        if (sceneNumber == 3){
            FindObjectOfType<ClickManager>().player.gameObject.SetActive(false);
            yield return new WaitForSeconds (0.5f);
        }
        Color c = blockingImage.color;
        //Screen goes black in one second
        blockingImage.enabled = true;
        while (blockingImage.color.a < 1)
        {
            c.a += Time.deltaTime;
            blockingImage.color = c;
            

        }//Change the Scene
        //Hide the old Scene
        localScenes[activeLocalScene].SetActive(false);
        //Show the new Scene
        localScenes[sceneNumber].SetActive(true);
        //Save the current scene
        activeLocalScene = sceneNumber;
        //Teleport the player to the correct position
        FindObjectOfType<ClickManager>().player.position = playerStartingPositions[sceneNumber].position;
        //Hide the hintbox loading a new scene
        UpdateHintBox(null, false);
        //Hide Name Tag
        UpdateNameTag(null);
        //Reset the animations
        foreach(SpriteAnimator spriteAnimator in FindObjectsOfType<SpriteAnimator>())
        {
            spriteAnimator.PlayAnimation(null);
        }
        while (blockingImage.color.a > 0)
        {
            c.a -= Time.deltaTime;
            blockingImage.color = c;
        }
        blockingImage.enabled = false;
        yield return null;
    }
    //Reloads the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        StartCoroutine(ChangeScene(1, 0));
    }
}
