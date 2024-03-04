using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;

    public bool isDead;
    public float Speed
    {
        get { return speed; }
    }
    public int Health
    {
        get { return health; }
    }
    public Animator Animator
    {
        get { return animator; }
    } 
    public GameObject Player
    {
        get { return player; }
    }
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        if (player == null)
        {
            Debug.LogError("Player reference is not set in Enemy script!");
        }
    }

    
}
