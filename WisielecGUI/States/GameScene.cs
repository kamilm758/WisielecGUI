using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiesielecLogika;

namespace WisielecGUI.States
{
    class GameScene : DrawableGameComponent
    {
        Game1 game;
        Dictionary<string, Texture2D> tekstury = new Dictionary<string, Texture2D>();
        SpriteFont font;
        StringBuilder haslo;
        string wpisanaLitera="";
        MouseState mouseState;
        Rectangle Cursor;
        Rectangle recConfitmButton;
        Rectangle hangman;
        Color ConfirmButtonColor;
        List<int> pozycjeLiter = new List<int>();
        int czyZawiera;
        private Rectangle recBackButton;
        Color BackButtonColor;

        public GameScene(Game1 game) : base(game)
        {
            this.game = game;
            LoadContent();
        }

        protected override void LoadContent()
        {
            font = game.Content.Load<SpriteFont>("GameFont");
            tekstury.Add("ConfirmButtonTexture", game.Content.Load<Texture2D>("ConfirmButton"));
            tekstury.Add("1", game.Content.Load<Texture2D>("hangman/1"));
            tekstury.Add("2", game.Content.Load<Texture2D>("hangman/2"));
            tekstury.Add("3", game.Content.Load<Texture2D>("hangman/3"));
            tekstury.Add("4", game.Content.Load<Texture2D>("hangman/4"));
            tekstury.Add("5", game.Content.Load<Texture2D>("hangman/5"));
            tekstury.Add("6", game.Content.Load<Texture2D>("hangman/6"));
            tekstury.Add("7", game.Content.Load<Texture2D>("hangman/7"));
            tekstury.Add("8", game.Content.Load<Texture2D>("hangman/8"));
            tekstury.Add("9", game.Content.Load<Texture2D>("hangman/9"));
            tekstury.Add("10", game.Content.Load<Texture2D>("hangman/10"));
            tekstury.Add("BackButtonTexture", game.Content.Load<Texture2D>("BackButton"));
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            if (game.GetGraWisielec().KoniecGry() == 3)
            {
                spriteBatch.Draw(tekstury[(10 - game.GetGraWisielec().GetLifes()).ToString()], hangman, Color.White);
                spriteBatch.DrawString(font, "Haslo jest o kategorii " + game.GetGraWisielec().GetSlowo().GetKategoria(), new Vector2(GraphicsDevice.Viewport.Width / 2 - 235,  GraphicsDevice.Viewport.Height / 10), Color.White);
                if (haslo != null)
                    spriteBatch.DrawString(font, haslo.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2 - 70, 3*GraphicsDevice.Viewport.Height / 10), Color.Red);
                spriteBatch.DrawString(font, "Wpisana przez gracza litera to: " + wpisanaLitera, new Vector2(GraphicsDevice.Viewport.Width / 2 - 170, 7 * GraphicsDevice.Viewport.Height / 10), Color.White);
                spriteBatch.Draw(tekstury["ConfirmButtonTexture"], recConfitmButton, ConfirmButtonColor);
                spriteBatch.DrawString(font, "Lifes" + game.GetGraWisielec().GetLifes(), new Vector2(1100,110), Color.White);
            }
            else if (game.GetGraWisielec().KoniecGry() == 1)
            {
                
                haslo = null;
                spriteBatch.Draw(tekstury[(10 - game.GetGraWisielec().GetLifes()).ToString()], hangman, Color.White);
                spriteBatch.Draw(tekstury["BackButtonTexture"], recBackButton, BackButtonColor);
                spriteBatch.DrawString(font, "Niestety przegrales :(", new Vector2(GraphicsDevice.Viewport.Width / 2 - 130, GraphicsDevice.Viewport.Height / 4), Color.White);
            }
            else if (game.GetGraWisielec().KoniecGry() == 2)
            {
                haslo = null;
                spriteBatch.Draw(tekstury["BackButtonTexture"], recBackButton, BackButtonColor);
                spriteBatch.DrawString(font, "Brawo! Wygrywasz :)", new Vector2(GraphicsDevice.Viewport.Width / 2 - 130, GraphicsDevice.Viewport.Height / 4), Color.White);
            }
            spriteBatch.End();
        }

        public void Update()
        {

            if (haslo == null && game.GetGraWisielec() != null)
            {
                haslo = new StringBuilder(game.GetGraWisielec().GetSlowo().GetSlowo().Length);
                for (int i = 0; i < game.GetGraWisielec().GetSlowo().GetSlowo().Length; i++)
                    haslo.Append("?");
            }
            wpisanaLitera = InputManager.GetJednaLitere();
            if (wpisanaLitera.Length == 0)
                wpisanaLitera = "X";
            UpdateCursorPosition();
            CalculateItemsPositions();
            CalculateItemsSize();
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
            recConfitmButton.X = GraphicsDevice.Viewport.Width / 2 - recConfitmButton.Size.X / 2;
            recConfitmButton.Y = 9*GraphicsDevice.Viewport.Height / 10 - 9*recConfitmButton.Size.Y / 10;
            hangman.X = 66;
            hangman.Y = 73;
            recBackButton.X = 5*GraphicsDevice.Viewport.Width / 6 - recBackButton.Size.X / 2;
            recBackButton.Y = 9 * GraphicsDevice.Viewport.Height / 10 - 9 * recBackButton.Size.Y / 10;
        }

        private void CalculateItemsSize()
        {
            recConfitmButton.Height = GraphicsDevice.Viewport.Height / 12;
            recConfitmButton.Width = GraphicsDevice.Viewport.Width / 6;
            hangman.Height = GraphicsDevice.Viewport.Height -160;
            hangman.Width = GraphicsDevice.Viewport.Width -900;
            recBackButton.Height = GraphicsDevice.Viewport.Height / 12;
            recBackButton.Width = GraphicsDevice.Viewport.Width / 6;
        }

        private void ButtonsEvents()
        {
            if ((recConfitmButton.Intersects(Cursor)))
            {
                ConfirmButtonColor = Color.Green;
                if (InputManager.LeftButtonPressed() && game.GetGraWisielec().GetLifes()!=0)
                {
                    ConfirmButtonColor = Color.Red;
                    //do something
                    czyZawiera = game.GetGraWisielec().SprawdzCzyJest(Convert.ToChar(wpisanaLitera));
                    if (czyZawiera == 3)
                    {
                        pozycjeLiter = game.GetGraWisielec().ZwrocIndexOdgadnietej(Convert.ToChar(wpisanaLitera));
                        foreach (var value in pozycjeLiter)
                            haslo[value] = Convert.ToChar(wpisanaLitera);
                    }
                }
            }
            else
                ConfirmButtonColor = Color.White;

            if ((recBackButton.Intersects(Cursor)))
            {
                BackButtonColor = Color.Green;
                if (InputManager.LeftButtonPressed() && game.GetGraWisielec().GetLifes() == 0)
                {
                    BackButtonColor = Color.Red;
                    //do something
                    //pamiętać aby w przypadku wygranej także zerować hasło
                    haslo = null;
                    InputManager.NapisFlush();
                    GameState.IsShowMainMenuScene = true;
                }

                if (InputManager.LeftButtonPressed() && game.GetGraWisielec().GetLifes() != 0 && game.GetGraWisielec().KoniecGry()!=3)
                {
                    BackButtonColor = Color.Red;
                    //do something
                    //pamiętać aby w przypadku wygranej także zerować hasło
                    haslo = null;
                    InputManager.NapisFlush();
                    GameState.IsShowMainMenuScene = true;
                }
            }
            else
                BackButtonColor = Color.White;
        }

        }
}
