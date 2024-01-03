using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;
using System;

namespace Snake_Game
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Snake snake;
        private SnakeFood snakeFood;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferHeight = 800;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            snakeFood = new SnakeFood(_spriteBatch);
            snake = new Snake(_spriteBatch, snakeFood);

            // TODO: use this.Content to load your game content here
            snake.bodyTexture = Content.Load<Texture2D>("body");
            snake.headTexture = Content.Load<Texture2D>("head");
            snakeFood.foodTexture = Content.Load<Texture2D>("apol");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            snake.Update();
            snakeFood.Update();
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();


            snakeFood.Draw();
            snake.Draw();
            


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class SnakeFood
    {
        private Random random = new Random();
        private SpriteBatch _sprite;
        public Texture2D foodTexture { get; set; }

        public int FoodScale = 32;
        public bool isSpawned { get; set; } = false;
        public Vector2 position { get; set; }
        private int _positionX = 0;
        private int _positionY = 0;

        public SnakeFood(SpriteBatch sprite)
        {
            _sprite = sprite;
            position = new Vector2(random.Next(0,800), random.Next(0,800));
            isSpawned = true;
        }

        public void Update()
        {
            if(isSpawned == false)
            {
                
                _positionX = Math.Clamp(random.Next(800), 0, 800);
                _positionY = Math.Clamp(random.Next(800), 0, 800);
                 isSpawned = true;
                  position = new Vector2(_positionX, _positionY);
            }
        }

        public void Draw()
        {
            _sprite.Draw(foodTexture, new Vector2(position.X - FoodScale, position.Y - FoodScale), Color.White);
        }

    }
}
