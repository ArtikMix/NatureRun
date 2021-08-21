using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartAnim()
    {
        anim.SetBool("jump_start", true);
    }
}
