using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public static AnimManager instance { get; private set; } = null;
    public Animator flashAnim;
    public Animator uiAnim;
    public Animator uiAnim2;
    private void Awake()
    {
        AnimManager.instance = this;
    }
    // Start is called before the first frame update
    public void StartListAnim()
    {
        uiAnim.SetTrigger("ListUp");
    }
    public void StartBookAnim()
    {
        uiAnim2.SetTrigger("BookUp");
    }
    public void StartFlash()
    {
        flashAnim.SetTrigger("Flash");
    }
}
