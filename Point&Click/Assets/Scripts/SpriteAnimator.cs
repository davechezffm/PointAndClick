using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    public AnimationData baseAnimation;
    Coroutine previousAnimation;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        PlayAnimation(baseAnimation);
        
    }
    public void PlayAnimation(AnimationData data)
    {
        if (previousAnimation != null)
        
            StopCoroutine(previousAnimation);
        
          previousAnimation = StartCoroutine(PlayAnimationCorutine(data)); 
        
    }
    
   
    public IEnumerator PlayAnimationCorutine(AnimationData data)
    {
        if (data == null)
        {//If no Animation is selected, the default base animation is played.
            data = baseAnimation;
        }
        float waitTime = data.framesOfGap * AnimationData.targetFrameTime;
        int spritesAmount = data.sprites.Length, i=0, soundsAmount=data.sounds.Length;

        while (i < spritesAmount)
        {if(i<soundsAmount)
                //Play a sound at a specific point in the animation. The sound will always match the sprite as they both use the same int i.
            gameManager.PlaySound(data.sounds[i]);
            mySpriteRenderer.sprite = data.sprites[i++];//Changes the sprite and increases
                yield return new WaitForSeconds(waitTime);
            //Loops the animation
            if (data.loop &&i>=spritesAmount)
            {
                i = 0;
            }

            if (data.returnToBase&&data != baseAnimation)
            {//When an animation is complete, the default base animation is played
                PlayAnimation(baseAnimation);

            }
        }
        yield return null;
    }
}
