using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    public AnimationData baseAnimation;
    Coroutine previousAnimation;
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
        {
            data = baseAnimation;
        }
        float waitTime = data.framesOfGap * AnimationData.targetFrameTime;
        int spritesAmount = data.sprites.Length, i=0;

        while (i < spritesAmount)
        {
            mySpriteRenderer.sprite = data.sprites[i++];//Changes the sprite and increases
                yield return new WaitForSeconds(waitTime);
            //Loops the animation
            if (data.loop &&i>=spritesAmount)
            {
                i = 0;
            }

            if (data.returnToBase&&data != baseAnimation)
            {
                PlayAnimation(baseAnimation);

            }
        }
        yield return null;
    }
}
