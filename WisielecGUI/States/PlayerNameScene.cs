using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WiesielecLogika;

namespace WisielecGUI.States
{
    class PlayerNameScene : DrawableGameComponent
    {
        Game1 game;
        GraWisielec graWisielec = new GraWisielec();
        Dictionary<string, Texture2D> tekstury = new Dictionary<string, Texture2D>();
        SpriteFont font;
        MouseState mouseState;
        Rectangle Cursor;
        Rectangle recBackButton;
        Rectangle recEasyGameButton;
        Rectangle recHardGameButton;
        Color HardGameButtonColor;
        Color BackButtonColor;
        string imie="";
        private Color EasyGameButtonColor;

        public PlayerNameScene(Game1 game) : base(game)
        {
            this.game = game;
            LoadContent();
        }
        protected override void LoadContent()
        {
            tekstury.Add("BackButtonTexture", game.Content.Load<Texture2D>("Powrot"));
            tekstury.Add("TextBoxTexture", game.Content.Load<Texture2D>("TextBox"));
            tekstury.Add("EasyGameButtonTexture", game.Content.Load<Texture2D>("Latwy"));
            tekstury.Add("HardGameButtonTexture", game.Content.Load<Texture2D>("Trudny"));
            font = game.Content.Load<SpriteFont>("FontMenu");

        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Podaj nazwe gracza:", new Vector2(GraphicsDevice.Viewport.Width / 2-218, GraphicsDevice.Viewport.Height/4), Color.White);
            spriteBatch.DrawString(font, imie, new Vector2(GraphicsDevice.Viewport.Width / 2-8*imie.Length-50, GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.Draw(tekstury["BackButtonTexture"], recBackButton, BackButtonColor);
            spriteBatch.Draw(tekstury["EasyGameButtonTexture"], recEasyGameButton, EasyGameButtonColor);
            spriteBatch.Draw(tekstury["HardGameButtonTexture"], recHardGameButton, HardGameButtonColor);
            spriteBatch.End();

        }

        public void Update()
        {
            UpdateCursorPosition();
            CalculateItemsPositions();
            CalculateItemsSize();
            ButtonsEvents();
            imie = InputManager.GetNapis();

        }

        private void UpdateCursorPosition()
        {
            mouseState = Mouse.GetState();
            Cursor.X = mouseState.X;
            Cursor.Y = mouseState.Y;
        }

        private void CalculateItemsPositions()
        {
            recBackButton.X = GraphicsDevice.Viewport.Width / 10 - recBackButton.Size.X / 10;
            recBackButton.Y = 9*GraphicsDevice.Viewport.Height / 10 - 9*recBackButton.Size.Y / 10-40;
            recEasyGameButton.X = GraphicsDevice.Viewport.Width / 10 - recEasyGameButton.Size.X / 10;
            recEasyGameButton.Y = 6 * GraphicsDevice.Viewport.Height / 10 - 6 * recEasyGameButton.Size.Y / 10;
            recHardGameButton.X = 9*GraphicsDevice.Viewport.Width / 10 - 9*recHardGameButton.Size.X / 10;
            recHardGameButton.Y = 6 * GraphicsDevice.Viewport.Height / 10 - 6 * recHardGameButton.Size.Y / 10;
        }

        private void CalculateItemsSize()
        {
            recBackButton.Height = GraphicsDevice.Viewport.Height / 18;
            recBackButton.Width = GraphicsDevice.Viewport.Width / 9;
            recEasyGameButton.Height = GraphicsDevice.Viewport.Height / 18;
            recEasyGameButton.Width = GraphicsDevice.Viewport.Width / 9;
            recHardGameButton.Height = GraphicsDevice.Viewport.Height / 18;
            recHardGameButton.Width = GraphicsDevice.Viewport.Width / 9;
        }

        private void ButtonsEvents()
        {
            if ((recBackButton.Intersects(Cursor)))
            {
                BackButtonColor = Color.Yellow;
                if (InputManager.LeftButtonPressed())
                {
                    BackButtonColor = Color.Red;
                    InputManager.NapisFlush();
                    GameState.IsShowMainMenuScene = true;
                }
            }
            else
                BackButtonColor = Color.White;

            if ((recEasyGameButton.Intersects(Cursor)))
            {
                EasyGameButtonColor = Color.Green;
                if (InputManager.LeftButtonPressed())
                {
                    EasyGameButtonColor = Color.Red;
                    InputManager.NapisFlush();
                    game.SetImie(imie);
                    graWisielec.NowaGra(1, imie);
                    game.SetGraWisielec(graWisielec);
                    imie = "";
                    GameState.IsShowGameScene = true;
                    
                }
            }
            else
                EasyGameButtonColor = Color.White;
            //jeśli kliknie sie w grę trudną
            if ((recHardGameButton.Intersects(Cursor)))
            {
                HardGameButtonColor = Color.Red;
                if (InputManager.LeftButtonPressed())
                {
                    HardGameButtonColor = Color.Red;
                    InputManager.NapisFlush();
                    game.SetImie(imie);
                    graWisielec.NowaGra(2, imie);
                    game.SetGraWisielec(graWisielec);
                    imie = "";
                    GameState.IsShowGameScene = true;

                }
            }
            else
                HardGameButtonColor = Color.White;
        }
        public String GetImie()
        {
            return imie;
        }
    }
}
