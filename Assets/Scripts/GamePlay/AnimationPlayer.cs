using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public bool allowOtherAnimations = true;

    public void PlayAnimation(string animation){
        
        if(!allowOtherAnimations) return;

        GetComponent<Animator>().Play(animation);
    }
}
