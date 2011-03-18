﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using LegendOfDarwin.GameObject;
using LegendOfDarwin.MenuObject;

namespace LegendOfDarwin
{
    public class Level1
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public GraphicsDevice device;

        private GameState gameState;
        private GameStart gameStart;

        private Darwin darwin;
        private Zombie firstZombie, secondZombie, thirdZombie;
        private Switch firstSwitch;
        private Brain brain;
        private GameBoard board;
        private ZombieTime zTime;
        private Vortex vortex;
        private Potion potion;
        private Stairs stairs;

        private BasicObject[] walls;
        private Texture2D wallTex;

        public SpriteFont messageFont;
        public bool keyIsHeldDown = false;
        public bool gameOver = false;
        public bool gameWin = false;

        private int counter;
        private int counterReady;
        public Texture2D gameOverTexture;
        public Texture2D gameWinTexture;

        Vector2 gameOverPosition = Vector2.Zero;


        // things for managing message boxes
        bool messageMode = false;
        int messageModeCounter = 0;
        private MessageBox zombieMessage;
        private MessageBox darwinMessage;
        private MessageBox switchMessage;
        private MessageBox brainMessage;

        public Song song;
        public Game1 mainGame;

        public Level1(Game1 myMainGame)
        {
            mainGame = myMainGame;
        }

        public void Initialize()
        {
            gameOverPosition.X = 320;
            gameOverPosition.Y = 130;

            device = graphics.GraphicsDevice;

            gameState = new GameState();
            gameStart = new GameStart(device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);

            board = new GameBoard(new Vector2(25, 25), new Vector2(device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight));
            darwin = new Darwin(board);
            firstZombie = new Zombie(10, 10, 15, 5, 15, 5, board);
            secondZombie = new Zombie(10, 16, 15, 5, 15, 5, board);
            thirdZombie = new Zombie(16, 10, 15, 5, 15, 5, board);

            String zombieString = "This a zombie,\n don't near him \nas a human!!";
            zombieMessage = new MessageBox(board.getPosition(12, 8).X, board.getPosition(10, 10).Y, zombieString);

            String darwinString = "This is darwin,\n move with arrows, \n z to transform, \n a for actions";
            darwinMessage = new MessageBox(board.getPosition(12, 8).X, board.getPosition(10, 10).Y, darwinString);

            String switchString = "This is a switch\n face it and press A\n to see what happens!!";
            switchMessage = new MessageBox(board.getPosition(12, 8).X, board.getPosition(10, 10).Y, switchString);

            String brainString = "Move the brain as a \nzombie.\n Zombie's like brains!!";
            brainMessage = new MessageBox(board.getPosition(12, 8).X, board.getPosition(10, 10).Y, brainString);

            stairs = new Stairs(board);

            brain = new Brain(board, 3, 3);

            BasicObject[] removableWalls = setRemovableWallsInLevelOne();

            BasicObject switchSquare = new BasicObject(board);
            switchSquare.X = 11;
            switchSquare.Y = 2;

            firstSwitch = new Switch(switchSquare, board, removableWalls);

            // Initial starting position
            darwin.setGridPosition(2, 20);

            if (board.isGridPositionOpen(darwin))
            {
                board.setGridPositionOccupied(darwin.X, darwin.Y);
                darwin.setPosition(board.getPosition(darwin).X, board.getPosition(darwin).Y);
            }

            // Darwin's lag movement
            counterReady = counter = 5;

            if (board.isGridPositionOpen(21, 20))
            {
                stairs.setGridPosition(21, 20);
                stairs.setDestination(board.getPosition(21, 20));
            }

            zTime = new ZombieTime(board);

            vortex = new Vortex(board, 15, 15);

            setPotionPositionInLevelOne(20, 4);

            setWalls();
        }

        private BasicObject[] setRemovableWallsInLevelOne()
        {
            //later add an x and y to the constructor
            BasicObject s1 = new BasicObject(board);
            s1.X = 20;
            s1.Y = 19;

            BasicObject s2 = new BasicObject(board);
            s2.X = 20;
            s2.Y = 20;

            BasicObject s3 = new BasicObject(board);
            s3.X = 20;
            s3.Y = 21;

            BasicObject s4 = new BasicObject(board);
            s4.X = 20;
            s4.Y = 22;

            BasicObject s5 = new BasicObject(board);
            s5.X = 20;
            s5.Y = 19;

            BasicObject s6 = new BasicObject(board);
            s6.X = 21;
            s6.Y = 19;

            BasicObject s7 = new BasicObject(board);
            s7.X = 22;
            s7.Y = 19;

            BasicObject s8 = new BasicObject(board);
            s8.X = 23;
            s8.Y = 19;

            BasicObject[] removableWalls = new BasicObject[8] { s1, s2, s3, s4, s5, s6, s7, s8 };
            return removableWalls;
        }

        private void setPotionPositionInLevelOne(int x, int y)
        {
            potion = new Potion(board);
            potion.setDestination(board.getPosition(x, y));
            potion.setGridPosition(x, y);
            board.setGridPositionOccupied(x, y);
        }

        private void setWalls()
        {
            BasicObject w1 = new BasicObject(board);
            BasicObject w2 = new BasicObject(board);
            BasicObject w3 = new BasicObject(board);
            BasicObject w4 = new BasicObject(board);
            BasicObject w5 = new BasicObject(board);
            BasicObject w6 = new BasicObject(board);
            BasicObject w7 = new BasicObject(board);
            BasicObject w8 = new BasicObject(board);
            BasicObject w9 = new BasicObject(board);
            BasicObject w10 = new BasicObject(board);

            BasicObject w11 = new BasicObject(board);
            BasicObject w12 = new BasicObject(board);
            BasicObject w13 = new BasicObject(board);
            BasicObject w14 = new BasicObject(board);
            BasicObject w15 = new BasicObject(board);
            BasicObject w16 = new BasicObject(board);
            BasicObject w17 = new BasicObject(board);
            BasicObject w18 = new BasicObject(board);
            BasicObject w19 = new BasicObject(board);
            BasicObject w20 = new BasicObject(board);
            BasicObject w21 = new BasicObject(board);

            w1.setGridPosition(9, 1);
            board.setGridPositionOccupied(9, 1);
            w2.setGridPosition(9, 2);
            board.setGridPositionOccupied(9, 2);
            w3.setGridPosition(9, 3);
            board.setGridPositionOccupied(9, 3);
            w4.setGridPosition(9, 5);
            board.setGridPositionOccupied(9, 5);
            w5.setGridPosition(10, 5);
            board.setGridPositionOccupied(10, 5);
            w6.setGridPosition(13, 1);
            board.setGridPositionOccupied(13, 1);
            w7.setGridPosition(13, 2);
            board.setGridPositionOccupied(13, 2);
            w8.setGridPosition(13, 3);
            board.setGridPositionOccupied(13, 3);
            w9.setGridPosition(9, 4);
            board.setGridPositionOccupied(9, 4);
            w10.setGridPosition(11, 5);
            board.setGridPositionOccupied(11, 5);

            w11.setGridPosition(5, 18);
            board.setGridPositionOccupied(5, 18);
            w12.setGridPosition(5, 19);
            board.setGridPositionOccupied(5, 19);
            w13.setGridPosition(5, 20);
            board.setGridPositionOccupied(5, 20);
            w14.setGridPosition(5, 17);
            board.setGridPositionOccupied(5, 17);
            w15.setGridPosition(5, 16);
            board.setGridPositionOccupied(5, 16);
            w18.setGridPosition(5, 21);
            board.setGridPositionOccupied(5, 21);
            w16.setGridPosition(5, 22);
            board.setGridPositionOccupied(5, 22);
            w17.setGridPosition(5, 15);
            board.setGridPositionOccupied(5, 15);
            w19.setGridPosition(5, 14);
            board.setGridPositionOccupied(5, 14);
            w20.setGridPosition(5, 13);
            board.setGridPositionOccupied(5, 13);
            w21.setGridPosition(5, 12);
            board.setGridPositionOccupied(5, 12);

            walls = new BasicObject[21] { w1, w2, w3, w4, w5, w6, w7, w8, w9, w10, w11, w12, w13, w14, w15, w16, w17, w18, w19, w20, w21 };
        }

        public void LoadContent()
        {
            messageFont = mainGame.Content.Load<SpriteFont>("TimesNewRoman");

            Texture2D darwinTex = mainGame.Content.Load<Texture2D>("DarwinPic/Darwin");
            Texture2D darwinUpTex = mainGame.Content.Load<Texture2D>("DarwinPic/DarwinUp");
            Texture2D darwinDownTex = mainGame.Content.Load<Texture2D>("DarwinPic/Darwin");
            Texture2D darwinRightTex = mainGame.Content.Load<Texture2D>("DarwinPic/DarwinRight");
            Texture2D darwinLeftTex = mainGame.Content.Load<Texture2D>("DarwinPic/DarwinLeft");
            Texture2D zombieDarwinTex = mainGame.Content.Load<Texture2D>("DarwinPic/ZombieDarwin");
            darwin.LoadContent(graphics.GraphicsDevice, darwinUpTex, darwinDownTex, darwinRightTex, darwinLeftTex, zombieDarwinTex);

            gameOverTexture = mainGame.Content.Load<Texture2D>("gameover");
            gameWinTexture = mainGame.Content.Load<Texture2D>("gamewin");

            stairs.LoadContent(mainGame.Content.Load<Texture2D>("StaticPic/stairsUp"),
                mainGame.Content.Load<Texture2D>("StaticPic/stairsDown"), "Down");

            firstSwitch.LoadContent(mainGame.Content.Load<Texture2D>("StaticPic/Wall"),
                mainGame.Content.Load<Texture2D>("StaticPic/Switch"));

            brain.LoadContent(mainGame.Content.Load<Texture2D>("brain"));

            board.LoadContent(mainGame.Content.Load<Texture2D>("StaticPic/metal_tile"));
            board.LoadBackgroundContent(mainGame.Content.Load<Texture2D>("StaticPic/side_wall"));

            wallTex = mainGame.Content.Load<Texture2D>("StaticPic/side_wall");

            //darwin.LoadContent(graphics.GraphicsDevice, darwinTex, zombieDarwinTex);

            firstZombie.LoadContent(mainGame.Content.Load<Texture2D>("ZombiePic/Zombie"));
            secondZombie.LoadContent(mainGame.Content.Load<Texture2D>("ZombiePic/Zombie"));
            thirdZombie.LoadContent(mainGame.Content.Load<Texture2D>("ZombiePic/Zombie"));
            zombieMessage.LoadContent(mainGame.Content.Load<Texture2D>("messageBox"));
            darwinMessage.LoadContent(mainGame.Content.Load<Texture2D>("messageBox"));
            switchMessage.LoadContent(mainGame.Content.Load<Texture2D>("messageBox"));
            brainMessage.LoadContent(mainGame.Content.Load<Texture2D>("messageBox"));

            gameStart.LoadContent(mainGame.Content.Load<Texture2D>("startScreen"));
            zTime.LoadContent(mainGame.Content.Load<Texture2D>("humanities_bar"));
            vortex.LoadContent(mainGame.Content.Load<Texture2D>("vortex"));
            potion.LoadContent(mainGame.Content.Load<Texture2D>("StaticPic/potion"));
        }
            

        //protected override void UnloadContent() { }

        public void Update(GameTime gameTime)
        {
            switch (gameState.getState())
            {
                case GameState.state.Start:
                    UpdateStartState();
                    break;
                case GameState.state.Level:
                    if (!messageMode)
                        UpdateLevelState(gameTime);
                    else
                        UpdateMessageMode();
                    break;
                case GameState.state.End:
                    UpdateEndState();
                    break;
            }
            
        }

        private void UpdateMessageMode()
        {
            KeyboardState ks = Keyboard.GetState();
            messageModeCounter++;

            zombieMessage.pointToSquare(firstZombie.X, firstZombie.Y, board);
            darwinMessage.pointToSquare(darwin.X, darwin.Y, board);
            switchMessage.pointToSquare(firstSwitch.X, firstSwitch.Y, board);
            brainMessage.pointToSquare(brain.X, brain.Y, board);
            if (ks.IsKeyDown(Keys.H) && messageModeCounter > 10)
            {
                messageMode = false;
                messageModeCounter = 0;
            }
        }

        private void UpdateStartState()
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Enter))
            {
                MediaPlayer.Play(song);
                gameState.setState(GameState.state.Level);
            }
        }

        private void UpdateLevelState(GameTime gameTime)
        {
            if (darwin.isZombie())
            {
                if (zTime.isTimedOut())
                {
                    gameOver = true;
                }
                else
                {
                    zTime.Update(gameTime);
                }
            }

            KeyboardState ks = Keyboard.GetState();

            checkForExitGame(ks);

            updateKeyHeldDown(ks);

            if (keyIsHeldDown)
            {
                if (counter > counterReady)
                {
                    darwin.Update(gameTime, ks, board, darwin.X, darwin.Y);
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }
            else
            {
                darwin.Update(gameTime, ks, board, darwin.X, darwin.Y);
            }

            stairs.Update(gameTime, darwin);

            //firstZombie.setPictureSize(board.getSquareWidth(), board.getSquareLength());
            firstZombie.Update(gameTime, darwin, brain);
            //secondZombie.setPictureSize(board.getSquareWidth(), board.getSquareLength());
            secondZombie.Update(gameTime, darwin, brain);
            //thirdZombie.setPictureSize(board.getSquareWidth(), board.getSquareLength());
            thirdZombie.Update(gameTime, darwin, brain);

            firstSwitch.Update(gameTime, ks, darwin);

            brain.Update(gameTime, ks, darwin);

            vortex.Update(gameTime, ks);
            vortex.CollisionWithZombie(firstZombie);
            vortex.CollisionWithZombie(secondZombie);
            vortex.CollisionWithZombie(thirdZombie);
            vortex.CollisionWithBO(brain, board);

            potion.Update(gameTime, ks, darwin, zTime);

            if (!darwin.isZombie())
            {
                checkForGameOver(firstZombie);
                checkForGameOver(secondZombie);
                checkForGameOver(thirdZombie);
            }
            checkForGameOver(vortex);
            //checkForGameWin();
            checkForSwitchToLevelTwo();

            if (gameOver || gameWin)
            {
                gameState.setState(GameState.state.End);
            }

            if (ks.IsKeyDown(Keys.H) && messageModeCounter > 10)
            {
                messageMode = true;
                messageModeCounter = 0;
            }
            messageModeCounter++;
        }

        private void UpdateEndState()
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Q))
            {
                mainGame.Exit();
            }
            if (ks.IsKeyDown(Keys.R))
            {
                
                gameOver = false;
                gameWin = false;

                board.setGridPositionOpen(darwin);
                darwin.setGridPosition(2, 20);

                zTime.reset();

                board.setGridPositionOpen(firstZombie);
                board.setGridPositionOpen(secondZombie);
                board.setGridPositionOpen(thirdZombie);
                board.setGridPositionOpen(brain);
                board.setGridPositionOpen(potion);

                firstZombie.setGridPosition(10, 10);
                board.setGridPositionOccupied(firstZombie.X, firstZombie.Y);
                firstZombie.setZombieAlive(true);

                secondZombie.setGridPosition(10, 16);
                board.setGridPositionOccupied(secondZombie.X, secondZombie.Y);
                secondZombie.setZombieAlive(true);

                thirdZombie.setGridPosition(16, 10);
                board.setGridPositionOccupied(thirdZombie.X, thirdZombie.Y);
                thirdZombie.setZombieAlive(true);

                potion.setGridPosition(20, 4);
                board.setGridPositionOccupied(potion.X, potion.Y);

                brain.setGridPosition(3, 3);
                board.setGridPositionOccupied(brain.X, brain.Y);

                potion.reset();
                darwin.setHuman();
                gameState.setState(GameState.state.Level);
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                
            }

        }

        private void checkForExitGame(KeyboardState ks)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                mainGame.Exit();

            if (ks.IsKeyDown(Keys.Q) || ks.IsKeyDown(Keys.Space))
            {
                mainGame.Exit();
            }
        }

        private void checkForGameOver(Zombie myZombie)
        {
            if (darwin.isOnTop(myZombie))
            {
                gameOver = true;
            }

            if (darwin.collision)
            {
                Rectangle rightSideOfDarwin = darwin.destination;
                rightSideOfDarwin.X = rightSideOfDarwin.X + board.getSquareWidth();

                Rectangle leftSideOfDarwin = darwin.destination;
                leftSideOfDarwin.X = leftSideOfDarwin.X - board.getSquareWidth();

                Rectangle onTopOfDarwin = darwin.destination;
                onTopOfDarwin.Y = onTopOfDarwin.Y - board.getSquareLength();

                Rectangle onBottomOfDarwin = darwin.destination;
                onBottomOfDarwin.Y = onBottomOfDarwin.Y + board.getSquareLength();


                if (rightSideOfDarwin == myZombie.destination ||
                    leftSideOfDarwin == myZombie.destination ||
                    onTopOfDarwin == myZombie.destination ||
                    onBottomOfDarwin == myZombie.destination)
                {
                    gameOver = true;
                }
            }
        }

        private void checkForGameOver(Vortex myVortex)
        {
            if (darwin.isOnTop(myVortex))
            {
                gameOver = true;
            }
        }

        private void checkForGameWin()
        {
            if (darwin.isOnTop(stairs))
            {
                gameWin = true;
            }
        }

        private void checkForSwitchToLevelTwo()
        {
            if (darwin.isOnTop(stairs)) 
            {
                //reset everything dynamic on the level
                board.setGridPositionOpen(darwin);
                darwin.setGridPosition(2, 2);

                mainGame.setCurLevel(Game1.LevelState.Level2);
                mainGame.setZTimeLevel(zTime,Game1.LevelState.Level2);
                board.setGridPositionOpen(firstZombie);
                board.setGridPositionOpen(secondZombie);
                board.setGridPositionOpen(thirdZombie);
                board.setGridPositionOpen(brain);
                board.setGridPositionOpen(potion);

                firstZombie.setGridPosition(10, 10);
                board.setGridPositionOccupied(firstZombie.X, firstZombie.Y);
                firstZombie.setZombieAlive(true);

                secondZombie.setGridPosition(10, 16);
                board.setGridPositionOccupied(secondZombie.X, secondZombie.Y);
                secondZombie.setZombieAlive(true);

                thirdZombie.setGridPosition(16, 10);
                board.setGridPositionOccupied(thirdZombie.X, thirdZombie.Y);
                thirdZombie.setZombieAlive(true);

                potion.setGridPosition(20, 4);
                board.setGridPositionOccupied(potion.X, potion.Y);

                brain.setGridPosition(2, 18);
                board.setGridPositionOccupied(brain.X, brain.Y);

                potion.reset();
                firstSwitch.turnOn();
                darwin.setHuman();
                gameState.setState(GameState.state.Level);
                gameOver = false;
                gameWin = false;
            }
        }

        private void updateKeyHeldDown(KeyboardState ks)
        {
            if (ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.Down))
            {
                keyIsHeldDown = false;
            }
            else
            {
                keyIsHeldDown = true;
            }
        }

        public void Draw(GameTime gameTime)
        {
            switch (gameState.getState())
            {
                case GameState.state.Start:
                    DrawStartState();
                    break;
                case GameState.state.Level:
                    DrawLevelState(gameTime);
                    break;
                case GameState.state.End:
                    DrawEndState();
                    break;
            }
            
        }

        private void DrawStartState()
        {
            mainGame.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            gameStart.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void DrawLevelState(GameTime gameTime)
        {
            mainGame.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            board.Draw(spriteBatch);
            stairs.Draw(spriteBatch);

            darwin.Draw(spriteBatch);
            firstZombie.Draw(spriteBatch);
            secondZombie.Draw(spriteBatch);
            thirdZombie.Draw(spriteBatch);
            firstSwitch.Draw(spriteBatch);
            brain.Draw(spriteBatch);
            zTime.Draw(spriteBatch);
            vortex.Draw(spriteBatch);
            potion.Draw(spriteBatch);

            if (messageMode)
            {
                zombieMessage.Draw(spriteBatch, messageFont);
                darwinMessage.Draw(spriteBatch, messageFont);
                brainMessage.Draw(spriteBatch, messageFont);
                //switchMessage.Draw(spriteBatch, messageFont);
            }

            foreach(BasicObject a in walls)
            {
                spriteBatch.Draw(wallTex, board.getPosition(a.X, a.Y), Color.White);
            }

            spriteBatch.End();
        }

        private void DrawEndState()
        {
            spriteBatch.Begin();
            if (gameOver)
            {
                gameOverPosition.X = 320;
                gameOverPosition.Y = 130;
                spriteBatch.Draw(gameOverTexture, gameOverPosition, Color.White);
            }

            if (gameWin)
            {
                spriteBatch.Draw(gameWinTexture, gameOverPosition, Color.White);
            }
            spriteBatch.End();
        }

        
    }
}
