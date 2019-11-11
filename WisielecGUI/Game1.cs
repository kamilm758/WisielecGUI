using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using WiesielecLogika;
using WisielecGUI.States;

namespace WisielecGUI
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraWisielec graWisielec = new GraWisielec();
        MenuScene menuScene;
        GameScene gameScene;
        RankingScene rankingScene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graWisielec.NowaGra(1, "name");
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            menuScene = new MenuScene(this);
            gameScene = new GameScene(this);
            rankingScene = new RankingScene(this);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            GameState.IsShowMainMenuScene = true;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                GameState.IsShowMainMenuScene=true;

            // TODO: Add your update logic here
            if (GameState.IsShowMainMenuScene)
                menuScene.Update();
            if (GameState.IsShowGameScene)
                gameScene.Update();
            if (GameState.IsRankingScene)
                rankingScene.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
                if (GameState.IsShowMainMenuScene)
                    menuScene.Draw(spriteBatch, gameTime);
                if (GameState.IsShowGameScene)
                    gameScene.Draw(spriteBatch, gameTime);
                if (GameState.IsRankingScene)
                    rankingScene.Draw(spriteBatch, gameTime);

                base.Draw(gameTime);
        }
    }
}
