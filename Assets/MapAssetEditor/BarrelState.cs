using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LaniakeaCode;

namespace LaniakeaCode.Utilities
{
    class BarrelState : State
    {
        #region Values
        protected Barrel barrel;
        protected BarrelStateData barrelStateData;
        protected Vector2 origin;
        protected DamageType damageType;
        
        #endregion
        public BarrelState(Entity _entity, FSM _stateMachine, string _animBoolName, BarrelStateData barrelStateData, Barrel barrel) : base(_entity, _stateMachine, _animBoolName)
        {
            this.barrel = barrel;
            this.barrelStateData = barrelStateData;
        }


        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void StateDebug()
        {
            base.StateDebug();
        }
    }
}
