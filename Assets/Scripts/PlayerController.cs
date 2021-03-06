﻿using UnityEngine;
using System.Collections;

namespace LD32
{
    public class PlayerController : BaseBehaviour
    {

        Rigidbody2D body;
        IInput input;

        public float acceleration;
        public float maxSpeed;


        // Use this for initialization
        public override void Start()
        {
            input = GetComponent<IInput>();
            body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            LookAt2D(input.lookAt);
        }

        void FixedUpdate()
        {
            Vector2 impulse = input.MoveVector;
            impulse = impulse.normalized * acceleration * Time.fixedDeltaTime;
            body.AddForce(impulse);

            if (body.velocity.magnitude > maxSpeed)
            {
                body.velocity = body.velocity.normalized * maxSpeed;
            }
        }

        void OnDestroy()
        {
            MessageBus.Global.OnPlayerDestroyed.Invoke();
        }

    }

}