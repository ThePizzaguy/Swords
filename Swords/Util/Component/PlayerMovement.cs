﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Swords.Util;
using Swords.Levels.GameObjects;

namespace Swords.Util.Component
{
    class PlayerMovement : Component
    {
        private GameObject entity;
        private RigidBody rigidbody;
        private float maxSpeed;
        private float acceleration;

        public PlayerMovement(float maxSpeed, float acceleration)
        {
            this.maxSpeed = maxSpeed;
            this.acceleration = acceleration;
        }

        public void Start(GameObject entity)
        {
            this.entity = entity;
            this.rigidbody = entity.GetBehavior<RigidBody>();
        }

        public void Update(float time)
        {
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                Vector2 vec = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
                vec.Y = -vec.Y;

                entity.Location.Add(vec * maxSpeed);
                entity.Location.SetRotation(GamePad.GetState(PlayerIndex.One).ThumbSticks.Right);
            }
            else
            {
                Vector2 vec = new Vector2(
                        (Keyboard.GetState().IsKeyDown(Keys.D) ? 1 : 0) + (Keyboard.GetState().IsKeyDown(Keys.A) ? -1 : 0),
                        (Keyboard.GetState().IsKeyDown(Keys.S) ? 1 : 0) + (Keyboard.GetState().IsKeyDown(Keys.W) ? -1 : 0));
                if (vec.Length() > 0)
                {
                    vec /= vec.Length();
                }
                if ((rigidbody.Velocity).Length()  > maxSpeed)
                {
                    this.rigidbody.Velocity /= rigidbody.Velocity.Length();
                    this.rigidbody.Velocity *= maxSpeed;
                }
                this.rigidbody.AddForce(acceleration * time, vec);
            }
        }
    }
}
