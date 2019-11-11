using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace WisielecGUI.States
{
    class MenuScene : DrawableGameComponent
    {
        Game game;
        public static Dictionary<string, Texture2D> tekstury = new Dictionary<string, Texture2D>();
        SpriteFont font;
        MouseState mouseState;
        Rectangle Cursor;
        Rectangle recNewGame;
        Rectangle recRanking;
        Rectangle recExit;
        Color NewGameButtonColor;
        Color RankingButtonColor;
        Color ExitButtonColor;

        public MenuScene(Game game) : base(game)
        {
            this.game = game;
            LoadContent();
        }

        protected override void LoadContent()
        {
            font = game.Content.Load<SpriteFont>("FontMenu");
            tekstury.Add("NewGameButtonTexture", game.Content.Load<Texture2D>("NewGameButton"));
            tekstury.Add("RankingButtonTexture", game.Content.Load<Texture2D>("RankingButton"));
            tekstury.Add("ExitButtonTexture", game.Content.Load<Texture2D>("ExitButton"));
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tekstury["NewGameButtonTexture"], recNewGame, NewGameButtonColor);
            spriteBatch.Draw(tekstury["RankingButtonTexture"], recRanking, RankingButtonColor);
            spriteBatch.Draw(tekstury["ExitButtonTexture"], recExit, ExitButtonColor);
            spriteBatch.End();
        }

        public void Update()
        {
            CalculateItemsPositions();
            CalculateItemsSize();
            UpdateCursorPosition();
            ButtonsEvents();

        }

        private void UpdateCursorPosition()
        {
            mouseState = Mouse.GetState();
            Cursor.X = mouseState.X; 
            Cursor.Y = mouseState.Y;
        }

        private void CalculateItemsPositions()
        {
            recNewGame.X = GraphicsDevice.Viewport.Width / 2 - recNewGame.Size.X / 2;
            recNewGame.Y = GraphicsDevice.Viewport.Height / 4 - recNewGame.Size.Y / 4;
            recRanking.X = GraphicsDevice.Viewport.Width / 2 - recNewGame.Size.X / 2;
            recRanking.Y = GraphicsDevice.Viewport.Height / 2 - recNewGame.Size.Y / 2;
            recExit.X = GraphicsDevice.Viewport.Width / 2 - recNewGame.Size.X / 2;
            recExit.Y = 3*GraphicsDevice.Viewport.Height / 4 - 3*recNewGame.Size.Y / 4;
        }

        private void CalculateItemsSize()
        {
            recNewGame.Height = GraphicsDevice.Viewport.Height / 6;
            recNewGame.Width = GraphicsDevice.Viewport.Width / 3;
            recRanking.Height = GraphicsDevice.Viewport.Height / 6;
            recRanking.Width = GraphicsDevice.Viewport.Width / 3;
            recExit.Height = GraphicsDevice.Viewport.Height / 6;
            recExit.Width = GraphicsDevice.Viewport.Width / 3;
        }

        private void ButtonsEvents()
        {
            if ((recNewGame.Intersects(Cursor)))
            {
                NewGameButtonColor = Color.Green;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    NewGameButtonColor = Color.Red;
                    GameState.IsShowGameScene = true;
                }
            }
            else
                NewGameButtonColor = Color.White;

            if ((recRanking.Intersects(Cursor)))
            {
                RankingButtonColor = Color.Green;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    RankingButtonColor = Color.Red;
                    GameState.IsRankingScene = true;
                }
            }
            else
                RankingButtonColor = Color.White;

            if ((recExit.Intersects(Cursor)))
            {
                ExitButtonColor = Color.Green;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    ExitButtonColor = Color.Red;
                    game.Exit();
                }
            }
            else
                ExitButtonColor = Color.White;
        }
    }
}
