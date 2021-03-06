﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Swords.Util;
using Swords.Content;
using Swords.Util.Component;
using Swords.Util.Animations;

namespace Swords.Levels.GameObjects
{
    class GameObjectFactory
    {
        public static GameObject GetEntity(string name, Location loc)
        {
            switch (name)
            {
                case "Player":
                    return new GameObject(
                        loc,
                        new AnimationPlayer(
                            new List<Animation>()
                            {
                                ContentRegistry.Animations.Get("Grass-Animation")
                            }),
                        "Player")
                    .AddBehavior(new Collider(new Swords.Util.Shapes.Rectangle(32, 32), true))
                    .AddBehavior(new RigidBody(100,0.01f, 750, 0.001f, new Vector2(), 0))
                    .AddBehavior(new PlayerMovement(250,500000));
                case "Object":
                    return new GameObject(
                        loc,
                        new AnimationPlayer(
                            new List<Animation>()
                            {
                                ContentRegistry.Animations.Get("Grass-Animation")
                            }), 
                        "Object")
                    .AddBehavior(new Collider(new Swords.Util.Shapes.Rectangle(32, 32), true))
                    .AddBehavior(new RigidBody(250,0.01f, 100, 0.001f, new Vector2(), 0));
                case "Sword":
                    return new GameObject(loc,
                        new AnimationPlayer(
                            new List<Animation>()
                            {
                                ContentRegistry.Animations.Get("Grass-Animation")
                            }), "Sword");
            }

            return null;
        }

    }
}
