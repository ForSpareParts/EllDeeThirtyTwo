﻿using UnityEngine;
using System.Collections;

namespace LD32
{
    public class EnemyInput : BaseBehaviour, IInput
    {
        BaseBehaviour _target;
        BaseBehaviour target
        {
            get
            {
                return _target;
            }
            set
            {
                if (_target == value || value == null)
                    return;

                if (_target != null)
                {
                    //if the old target's still around, stop listening for
                    //events on it...
                }

                _target = value;

                //WARNING: this might not be true, especially if we
                //implement pooling later on
                targetIsAlive = true;
                target.messageBus.destroyed.AddListener(TargetDestroyed);
            }
        }
        Vector2 lastTargetPosition;
        Team team = Team.EVIL;

        bool targetIsAlive = false;

        #region Unity Hooks
        public override void Awake()
        {
            base.Awake();

            messageBus.teamChanged.AddListener(TeamChanged);
        }

        public override void Start()
        {
            target = GetTarget();
        }
        #endregion

        #region Public API
        public Vector2 GetMoveVector()
        {
            return Vector2.zero;
        }

        public Vector2 lookAt
        {
            get
            {
                if (targetIsAlive)
                {
                    lastTargetPosition = target.transform.position;
                }

                return lastTargetPosition;
            }
        }

        public bool fire
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region Internal Methods
        BaseBehaviour GetTarget()
        {
            if (team == Team.EVIL)
            {
                return FindObjectOfType<PlayerController>();
            }
            else
            {
                return FindObjectOfType<EnemyController>();
            }
        }
        #endregion

        #region Event Callbacks
        void TargetDestroyed(BaseBehaviour destroyed)
        {
            targetIsAlive = false;
        }

        void TeamChanged(Team newTeam)
        {
            team = newTeam;

            //we should re-find our target:
            target = GetTarget();
        }
        #endregion
    }

}