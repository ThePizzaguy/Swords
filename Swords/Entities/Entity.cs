﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Swords.Rendering;
using Swords.Util;
using Swords.Entities.Behaviors;

namespace Swords.Entities
{
    class Entity : IEntity, Renderable
    {
        private Location location;
        private Texture2D texture;
        private List<Behavior> behaviors;

        public Entity(Location location, Texture2D texture, List<Behavior> behaviors)
        {
            this.location = location;
            this.texture = texture;
            this.behaviors = behaviors;

            foreach (Behavior behavior in behaviors)
            {
                behavior.Start();
            }
        }

        public Entity(Location location, Texture2D texture) : this ( location, texture, new List<Behavior>()) {}


        public ISprite[] GetSprites()
        {
            return new ISprite[] { new Sprite(location, texture) };
        }

        public void Update()
        {
            foreach (Behavior behavior in behaviors)
            {
                behavior.Update();
            }
        }

        public void AddBehavior(Behavior behavior)
        {
            behaviors.Add(behavior);
            behavior.Start();
        }
    }
}
