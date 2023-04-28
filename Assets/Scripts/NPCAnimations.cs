using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimations : MonoBehaviour
{
    public Animator npcAnimator;

    string currentMotion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk()
    {
        currentMotion = "walking";
        npcAnimator.SetBool("walking", true);
    }

    public void Stop()
    {
        npcAnimator.SetBool(currentMotion, false);
    }

    public void HeavyWeaponSwing()
    {
        npcAnimator.SetBool("heavyWeaponSwing", true);
        StartCoroutine(HeavyWeaponSwingCoroutine());
    }

    IEnumerator HeavyWeaponSwingCoroutine()
    {
        yield return new WaitForSeconds(5.23f);
        npcAnimator.SetBool("heavyWeaponSwing", false);
    }

    public void Die()
    {
        npcAnimator.SetBool("dead", true);
    }

    public void Float()
    {
        npcAnimator.SetBool("floating", true);
        StartCoroutine(StopFloat());
    }

    IEnumerator StopFloat()
    {
        yield return new WaitForSeconds(5);
        npcAnimator.SetBool("floating", false);
        npcAnimator.SetBool("falling", true);
        yield return new WaitForSeconds(3);
        npcAnimator.SetBool("falling", false);
        npcAnimator.SetBool("gettingUp", true);
        yield return new WaitForSeconds(3);
        npcAnimator.SetBool("gettingUp", false);
    }

    public void Fall()
    {
        npcAnimator.SetBool("falling", true);
    }

    public void GetUp()
    {
        StartCoroutine(StopGetUp());
    }

    IEnumerator StopGetUp()
    {
        npcAnimator.SetBool("falling", false);
        npcAnimator.SetBool("gettingUp", true);
        yield return new WaitForSeconds(3);
        npcAnimator.SetBool("gettingUp", false);
    }
}
