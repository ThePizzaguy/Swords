﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

using Swords.Rendering;
using Swords.Levels.Entities.Animations;
using Swords.Levels.Entities.Behaviors;
using Swords.Util;

namespace Swords.Levels.Entities
{
    class Entity : IEntity, Renderable, ICloneable
    {
        public string Name { get { return name; } }
        public Location Location { get { return location; } set { location = value; } }

        private string name;
        private Location location;
        private AnimationPlayer animations;
        private List<Behavior> behaviors;
        private List<Entity> childeren;

        private Entity(Location location, AnimationPlayer animations, string name, List<Behavior> behaviors, List<Entity> childeren)
        {
            this.name = name;
            this.location = location;
            this.animations = animations;
            this.behaviors = behaviors;
            this.childeren = childeren;
            foreach (Behavior behavior in behaviors)
            {
                behavior.Start(this);
            }

        }

        public Entity(Location location, AnimationPlayer animations, string name) : this(location, animations, name, new List<Behavior>(), new List<Entity>()) { }
        public Entity(Location location, string name) : this(location, null, name) { }
        public Entity(Location location) : this(location, null, "Undefined") { }

        public ISprite[] GetSprites()
        {
            List<ISprite> sprites = new List<ISprite>();

            if (animations != null)
            {
                sprites.Add(new Sprite(location, animations.CurrentSprite, Position.Relative));
            }

            foreach (Entity child in childeren)
            {
                foreach (Sprite sprite in child.GetSprites())
                {
                    sprite.Location = Location.Add(location, sprite.Location);
                    sprites.Add(sprite);
                }
            }
            return sprites.ToArray();
        }

        public void Update()
        {
            animations.Update();
            foreach (Entity child in childeren)
            {
                child.Update();
            }
            foreach (Behavior behavior in behaviors)
            {
                behavior.Update();
            }
        }

        public Entity AddBehavior(Behavior behavior)
        {
            if (!HasBehavior(behavior.GetType()))
            {
                behaviors.Add(behavior);
                behavior.Start(this);
            }
            return this;
        }

        public bool HasBehavior<T>() where T : Behavior
        {
            return GetBehavior<T>() != null ? true : false;
        }

        private bool HasBehavior(Type type) {
            foreach (Behavior b in behaviors)
            {
                if (b.GetType() == type)
                {
                    return true;
                }
            }
            return false;
        }

        public T GetBehavior<T>() where T : Behavior
        {
            foreach (Behavior b in behaviors)
            {
                if (b is T)
                {
                    return (T) b;
                }
            }
            return default(T);
        }

        public Entity AddChild(Entity entity)
        {
            childeren.Add(entity);
            return this;
        }

        public Entity RemoveChild(string Name)
        {
            Entity entity = null;
            foreach (Entity child in childeren)
            {
                if (child.Name.Equals(Name))
                {
                    entity = child;
                    break;
                }
            }
            childeren.Remove(entity);
            return entity;
        }

        public void Remove()
        {
            Level.Instance.Remove(this);
        }

        public object Clone()
        {
            List<Entity> childeren = new List<Entity>();
            List<Behavior> behaviors = new List<Behavior>();

            foreach (Entity child in this.childeren)
            {
                childeren.Add((Entity)child.Clone());
            }

            foreach (Behavior behavior in this.behaviors)
            {
                behaviors.Add((Behavior)behavior.Clone());
            }
            Entity entity = new Entity(new Location(location.Vector.X, location.Vector.Y), animations, name, behaviors, childeren);

            foreach (Behavior behavior in entity.behaviors)
            {
                behavior.Start(entity);
            }

            return entity;
        }
    }
}