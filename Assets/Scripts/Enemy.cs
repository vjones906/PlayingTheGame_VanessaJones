﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRB;
    private GameObject player;
    public float speed;
    private float outOfBounds = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(moveDir * speed);

        if (transform.position.y < outOfBounds)
        {
            Destroy(gameObject);
        }
    }
}
