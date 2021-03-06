﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

namespace Swords.Util.Animations
{
    class Animation
    {
        public Texture2D[] Textures { get { return textures; } }
        public int Length { get { return textures.Length; } }
        public float Time { get { return time; } }

        private Texture2D[] textures;
        private float time;

        public Animation(Texture2D[] textures, float time)
        {
            this.textures = textures;
            this.time = time;
        }

        public bool IsEmpty()
        {
            if(textures == null) { return true; }
            if(textures.Length == 0) { return true; }
            return false;
        }
    }
}
