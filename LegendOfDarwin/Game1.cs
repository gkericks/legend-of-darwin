using System;
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

namespace LegendOfDarwin
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Darwin darwin;
        Zombie firstZombie;
        Switch firstSwitch;
        GameBoard board;
        GraphicsDevice device;
        bool keyIsHeldDown = false;
        private int counter;
        private int counterReady;

        Stairs firstStair, secondStair;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            
            device = graphics.GraphicsDevice;
            InitializeGraphics();

            board = new GameBoard(new Vector2(25, 25), new Vector2(device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight));
            darwin = new Darwin(board); 
            firstZombie = new Zombie(10, 10, 15, 5, 15, 5, board);

            firstStair = new Stairs(board);
            secondStair = new Stairs(board);

            //array of squares that the switch will control



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

            BasicObject[] squares = new BasicObject[8] {s1, s2, s3, s4, s5, s6, s7, s8};

            BasicObject switchSquare = new BasicObject(board);
            switchSquare.X = 10;
            switchSquare.Y = 3;

            firstSwitch = new Switch(switchSquare, board, squares);

            // Initial starting position
            darwin.setGridPosition(5, 5);

            if(board.isGridPositionOpen(darwin))
            {
                board.setGridPositionOccupied(darwin.X, darwin.Y);
                darwin.setPosition(board.getPosition(darwin).X, board.getPosition(darwin).Y);
            }

            // Darwin's lag movement
            counterReady = counter = 5;


            if (board.isGridPositionOpen(20, 2))
            {
                firstStair.setGridPosition(20, 2);
                firstStair.setDestination(board.getPosition(20, 2));
            }
            if (board.isGridPositionOpen(19, 2))
            {
                secondStair.setGridPosition(19, 2);
                secondStair.setDestination(board.getPosition(19, 2));
            }


            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D darwinTex = Content.Load<Texture2D>("Darwin");

            Texture2D darwinUpTex = Content.Load<Texture2D>("DarwinUp");
            Texture2D darwinDownTex = Content.Load<Texture2D>("Darwin");
            Texture2D darwinRightTex = Content.Load<Texture2D>("DarwinRight");
            Texture2D darwinLeftTex = Content.Load<Texture2D>("DarwinLeft");

            Texture2D zombieDarwinTex = Content.Load<Texture2D>("ZombieDarwin");
            Texture2D zombieTex = Content.Load<Texture2D>("Zombie");

            // Test
            Texture2D basicGridTex = Content.Load<Texture2D>("grid_outline");
            Texture2D basicMenuTex = Content.Load<Texture2D>("grid_menu_outline");

            Texture2D basicStairUpTex = Content.Load<Texture2D>("stairsUp");
            Texture2D basicStairDownTex = Content.Load<Texture2D>("stairsDown");
            // Texture for the wall
            Texture2D wallTex = Content.Load<Texture2D>("Wall");

            firstStair.LoadContent(basicStairUpTex, basicStairDownTex, "Up");
            secondStair.LoadContent(basicStairUpTex, basicStairDownTex, "Down");
            firstSwitch.LoadContent(wallTex);

            // Test
            board.LoadContent(basicGridTex);
            board.LoadBackgroundContent(basicMenuTex);

            //darwin.LoadContent(graphics.GraphicsDevice, darwinTex, zombieDarwinTex);
            darwin.LoadContent(graphics.GraphicsDevice, darwinUpTex, darwinDownTex, darwinRightTex, darwinLeftTex, zombieDarwinTex);
            firstZombie.LoadContent(zombieTex);
        }

        protected override void UnloadContent(){}

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState ks = Keyboard.GetState();

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

            firstZombie.setPictureSize(board.getSquareWidth(), board.getSquareLength());
            firstZombie.Update(gameTime,darwin);

            firstSwitch.Update(ks);

            base.Update(gameTime);
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            board.Draw(spriteBatch);


            firstStair.Draw(spriteBatch);
            secondStair.Draw(spriteBatch);


            darwin.Draw(spriteBatch);
            firstZombie.Draw(spriteBatch);
            firstSwitch.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void InitializeGraphics()
        {
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1080;
            graphics.ApplyChanges();
        }

    }
}