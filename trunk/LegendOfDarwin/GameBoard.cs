﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LegendOfDarwin
{
    class GameBoard
    {
        private Rectangle[,] grid;
        private Boolean[,] hasObject;

        public GameBoard(Vector2 boardSize, Vector2 windowSize)
        {
            int length = (int)(windowSize.Y / boardSize.Y);
            int width = (int)(windowSize.X / boardSize.X);

            grid = new Rectangle[(int)boardSize.X, (int)boardSize.Y];
            hasObject = new Boolean[(int)boardSize.X, (int)boardSize.Y];

            for (int i = 0; i < boardSize.X; i++)
            {
                for (int j = 0; j < boardSize.Y; j++)
                {
                    grid[i,j] = new Rectangle(i * width, j * length, width, length);
                    hasObject[i,j] = false;
                }
            }
        }

        public Boolean isTaken(int x, int y)
        {
            return hasObject[x,y];
        }

        public Boolean isGridPositionOpen(BasicObject bo)
        {
            if (isTaken(bo.X, bo.Y))
            {
                return false;
            }

            hasObject[bo.X,bo.Y] = true;

            return true;
        }

        public Rectangle getPosition(BasicObject bo)
        {
            return grid[bo.X,bo.Y];
        }

        public void freePosition(BasicObject bo)
        {
            hasObject[bo.X,bo.Y] = false;
        }

        public void setGridPositionOpen(int x, int y)
        {
            hasObject[x, y] = false;
        }
    }
}