﻿using UnityEngine;
using System.Collections;

namespace LD32
{
    public class PlayerInput : BaseBehaviour, IInput
    {

        Vector2 currentMove;
        Vector2 _lookAt;

        // Update is called once per frame
        void Update()
        {
            currentMove = Vector3.zero;

            currentMove.x = Input.GetAxis("Horizontal");
            currentMove.y = Input.GetAxis("Vertical");

            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            _lookAt = mousePosition;

            if (Input.GetButtonDown("Fire"))
            {
                messageBus.fireBullet.Invoke();
            }

            if (Input.GetButtonDown("Reload Level"))
            {
                messageBus.global.manualReloadLevel.Invoke();
            }
        }

        public Vector2 GetMoveVector()
        {
            return currentMove;
        }

        public Vector2 lookAt
        {
            get
            {
                return _lookAt;
            }
        }
    }

}