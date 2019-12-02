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
    public class RankingScene : DrawableGameComponent
    {
        Game game;
        Ranking ranking;
        SpriteFont font;
        MouseState mouseState;
        Rectangle Cursor;
        Rectangle recRankTitle;
        Rectangle recBackButton;
        Vector2 listPosition;
        int iterator = 2;
        Color listColor;
        Color BackButtonColor;
        IOrderedEnumerable<KeyValuePair<string, int>> sortedList;
        public static Dictionary<string, Texture2D> tekstury = new Dictionary<string, Texture2D>();

        public RankingScene(Game game) : base(game)
        {
            this.game = game;
            this.ranking = new Ranking();
            LoadContent();
        }

        protected override void LoadContent()
        {
            sortedList = ranking.GetSortedRanking();
            font = game.Content.Load<SpriteFont>("FontRanking");
            tekstury.Add("RankingTitleTexture", game.Content.Load<Texture2D>("RankingTitle"));
            tekstury.Add("BackButtonTexture", game.Content.Load<Texture2D>("Powrot"));
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tekstury["RankingTitleTexture"], recRankTitle, Color.White);
            foreach (KeyValuePair<string, int> pair in sortedList)
            {
                ManageRankList(iterator, (iterator+". "+pair.Key + " : " + pair.Value).Length);
                spriteBatch.DrawString(font, (iterator -1)+ ". " + pair.Key+" : "+pair.Value, listPosition , listColor);
                iterator++;
            }
            iterator = 2;
            spriteBatch.Draw(tekstury["BackButtonTexture"], recBackButton, BackButtonColor);
            spriteBatch.End();
        }

        public void Update()
        {
            CalculateItemsPositions();
            CalculateItemsSize();
            UpdateCursorPosition();
            ButtonsEvents();
        }

        public void ManageRankList(int iter, int length)
        {
            listPosition.X = GraphicsDevice.Viewport.Width / 2-(5*length);
            listPosition.Y = iter*GraphicsDevice.Viewport.Height / (sortedList.Count()+3)+recRankTitle.Y;
            if (iter == 2)
            {
                listColor = Color.Gold;
            }
            else if (iter == 3)
            {
                listColor = Color.Silver;
            }
            else if(iter ==4)
            {
                listColor = Color.Brown;
            }
            else
            {
                listColor = Color.White;
            }
        }

        private void CalculateItemsPositions()
        {
            recRankTitle.X = GraphicsDevice.Viewport.Width / 2 - recRankTitle.Size.X / 2;
            recRankTitle.Y = GraphicsDevice.Viewport.Height /(sortedList.Count()+3);
            recBackButton.X = GraphicsDevice.Viewport.Width / 10;
            recBackButton.Y = 550;
        }

        private void CalculateItemsSize()
        {
            recRankTitle.Height = GraphicsDevice.Viewport.Height / 6;
            recRankTitle.Width = GraphicsDevice.Viewport.Width / 3;
            recBackButton.Height = GraphicsDevice.Viewport.Height / 12;
            recBackButton.Width = GraphicsDevice.Viewport.Width / 6;
        }

        private void UpdateCursorPosition()
        {
            mouseState = Mouse.GetState();
            Cursor.X = mouseState.X;
            Cursor.Y = mouseState.Y;
        }

        private void ButtonsEvents()
        {
            if ((recBackButton.Intersects(Cursor)))
            {
                BackButtonColor = Color.Yellow;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    BackButtonColor = Color.Red;
                    GameState.IsShowMainMenuScene = true;
                }
            }
            else
                BackButtonColor = Color.White;
        }
        public void SetRanking(Ranking ranking)
        {
            this.ranking = ranking;
            sortedList = ranking.GetSortedRanking();
        }
    }
}
