using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public Transform player;
    GameManager gameManager;
    public bool playerWalking;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void GoToItem(ItemData item)
    {//Hide Hint Box
        gameManager.UpdateHintBox(null, false);
        //Set the bool to true
        playerWalking = true;
        //Select the animation to use
        player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);
        //Start Moving the Player
        StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position));
      
        //Check if the item can be pickedup
        TryGettingItem(item);
        
    }

  

    private void TryGettingItem(ItemData item)
    {//-1 allows an item to be instantly picked up.
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);
        if (canGetItem)
        {
            GameManager.collectedItems.Add(item.itemID);
           
        }

        StartCoroutine(UpdateSceneAfterItem(item,canGetItem));
    }

    private IEnumerator UpdateSceneAfterItem(ItemData item, bool canGetItem)
    {
        while (playerWalking){    //Wait for the player to reach the item.
            yield return new WaitForSeconds(0.1f);
        }
        //Play Base Animation
        player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[0]);
        yield return new WaitForSeconds(0.5f);
        //Destroy the game object of the item you pick up.
        if (canGetItem)
        {//Play the use animation
            player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[2]);
            
            //Stops name tag appearing after collecting the item.
            gameManager.UpdateNameTag(null);

            //Remove certain objects. The one the player has picked up, for example.
            foreach (GameObject g in item.objectsToRemove)

                Destroy(g);
            //Show Objects on screen
            foreach (GameObject g in item.objectsToSetActive)

                g.SetActive(true);
            if(item.successAnimation)
            item.GetComponent<SpriteAnimator>().PlayAnimation(item.successAnimation);

            Debug.Log("Item Collected");
        }


        else { gameManager.UpdateHintBox(item, player.GetComponentInChildren<SpriteRenderer>().flipX);
           
        }
        //Changes the scene, checks if the game is completed.
        gameManager.CheckSpecialConditions(item, canGetItem);
        yield return null;
    }
}
