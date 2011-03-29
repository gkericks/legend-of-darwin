﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LegendOfDarwin.MenuObject;

namespace LegendOfDarwin.GameObject
{
    class Potion : BasicObject
    {
        private Texture2D potionTex;
        private bool isConsumed;
        private const int healthReplenished = 10;

        public Potion(GameBoard board) : base(board)
        {
            isConsumed = false;
            board.setGridPositionOpen(this);
        }

        public void LoadContent(Texture2D potTex)
        {
            potionTex = potTex;
        }

        public void Update(GameTime gameTime, KeyboardState ks, Darwin darwin, ZombieTime zTime)
        { 
            if(this.isOnTop(darwin) && !isConsumed)
            {
                consumePotion(zTime);
            }
        }

        private void consumePotion(ZombieTime zTime)
        {
            board.setGridPositionOpen(this);
            isConsumed = true;

            int updateTime = zTime.getTime() - healthReplenished;
            zTime.setTime(updateTime);

        }

        public void reset()
        {
            isConsumed = false;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (isConsumed == false)
            {
                spriteBatch.Draw(potionTex, this.destination, Color.White);
            }
        }
    }
}