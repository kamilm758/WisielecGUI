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
        Game1 game;
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

        public MenuScene(Game1 game) : base(game)
        {
            this.game = game;
            LoadContent();
        }

        protected override void LoadContent()
        {
            font = game.Content.Load<SpriteFont>("TitleFont");
            tekstury.Add("NewGameButtonTexture", game.Content.Load<Texture2D>("NowaGra"));
            tekstury.Add("RankingButtonTexture", game.Content.Load<Texture2D>("Ranking"));
            tekstury.Add("ExitButtonTexture", game.Content.Load<Texture2D>("Wyjscie"));
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Wisielec", new Vector2(380,130), Color.White);
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
            recNewGame.Y = 2*GraphicsDevice.Viewport.Height / 6 - recNewGame.Size.Y / 6+70;
            recRanking.X = GraphicsDevice.Viewport.Width / 2 - recNewGame.Size.X / 2;
            recRanking.Y = 3*GraphicsDevice.Viewport.Height / 6 - recNewGame.Size.Y / 6+45;
            recExit.X = GraphicsDevice.Viewport.Width / 2 - recNewGame.Size.X / 2;
            recExit.Y = 4*GraphicsDevice.Viewport.Height / 6 - recNewGame.Size.Y / -6;
        }

        private void CalculateItemsSize()
        {
            recNewGame.Height = GraphicsDevice.Viewport.Height / 9;
            recNewGame.Width = GraphicsDevice.Viewport.Width / 5;
            recRanking.Height = GraphicsDevice.Viewport.Height / 10;
            recRanking.Width = GraphicsDevice.Viewport.Width / 6;
            recExit.Height = GraphicsDevice.Viewport.Height / 10;
            recExit.Width = GraphicsDevice.Viewport.Width / 6;
        }

        private void ButtonsEvents()
        {
            if ((recNewGame.Intersects(Cursor)))
            {
                NewGameButtonColor = Color.Yellow;
                if (InputManager.LeftButtonPressed())
                {
                    NewGameButtonColor = Color.Red;
                    GameState.IsPlayerNameScene = true;
                }
            }
            else
                NewGameButtonColor = Color.White;

            if ((recRanking.Intersects(Cursor)))
            {
                RankingButtonColor = Color.Yellow;
                if (InputManager.LeftButtonPressed())
                {
                    RankingButtonColor = Color.Red;
                    game.GetRankingScene().SetRanking(new WiesielecLogika.Ranking());
                    GameState.IsRankingScene = true;
                }
            }
            else
                RankingButtonColor = Color.White;

            if ((recExit.Intersects(Cursor)))
            {
                ExitButtonColor = Color.Yellow;
                if (InputManager.LeftButtonPressed())
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
