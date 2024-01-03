using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Snake_Game
{
    public class Snake
    {
        private SpriteBatch _spriteBatch;
        private SnakeFood food;
        public Texture2D bodyTexture { get; set; }
        public Texture2D headTexture { get; set; }

        private float snakeVelocity = 10.0f;
        private int snakeScale = 32;
        private Vector2 Position { get; set; } = new Vector2(400, 400);
        private Direction snakeDirection { get; set; } = Direction.Right;

        private List<Vector2> snakeBody = new List<Vector2>();

        private bool LockMove { get; set; } = false;

        public Snake(SpriteBatch sprite, SnakeFood snakeFood)
        {
            _spriteBatch = sprite;
            food = snakeFood;

            snakeBody.Add(new Vector2(Position.X, Position.Y));
            snakeBody.Add(new Vector2(Position.X + 34, Position.Y + 34));
            snakeBody.Add(new Vector2(Position.X + 68, Position.Y + 68));
        }

        public void Update()
        {
            SnakeCollision();
            SnakeEatFood();
            Input();
            SnakeDirectionMove();
        }

        public void Draw()
        {
         
            for(int i = 0; i < snakeBody.Count; i++)
            {
                if(i == snakeBody.Count - 1)
                {
                    _spriteBatch.Draw(headTexture, new Vector2(snakeBody[i].X - snakeScale, snakeBody[i].Y - snakeScale), Color.White);
                }
                else
                {
                    _spriteBatch.Draw(bodyTexture, new Vector2(snakeBody[i].X - snakeScale, snakeBody[i].Y - snakeScale), Color.White);
                }
                
            }
        }

        private void Input()
        {
            KeyboardState keyState = Keyboard.GetState();

            foreach (Keys pressedKey in keyState.GetPressedKeys())
            {
                switch (pressedKey)
                {
                    case Keys.W:
                        if(snakeDirection != Direction.Downward)
                        snakeDirection = Direction.Forward;
                        break;
                    case Keys.S:
                        if (snakeDirection != Direction.Forward)
                            snakeDirection = Direction.Downward;
                        break;
                    case Keys.A:
                        if (snakeDirection != Direction.Right)
                            snakeDirection = Direction.Left;
                        break;
                    case Keys.D:
                        if (snakeDirection != Direction.Left)
                            snakeDirection = Direction.Right;
                        break;
                    case Keys.D7:
                        snakeBody.Insert(0, new Vector2(snakeBody[0].X, snakeBody[0].Y));
                        break;
                }
            }



            
        }
        private void SnakeCollision()
        {
            Vector2 maxPosition = new Vector2(800, 800);
            Vector2 minPosition = new Vector2(0, 0);
            for(int i = 0; i < snakeBody.Count; i++)
            {
                // row
                if (snakeBody[i].X > maxPosition.X)
                    snakeBody[i] = new Vector2(minPosition.X , snakeBody[i].Y);
                if (snakeBody[i].X < minPosition.X)
                    snakeBody[i] = new Vector2(maxPosition.X, snakeBody[i].Y);
                // column
                if (snakeBody[i].Y > maxPosition.Y)
                    snakeBody[i] = new Vector2(snakeBody[i].X, minPosition.Y);
                if (snakeBody[i].Y < minPosition.X)
                    snakeBody[i] = new Vector2(snakeBody[i].X, maxPosition.Y);

            }
        }

        private void SnakeDirectionMove()
        {
            int headPosition = snakeBody.Count - 1;
            Vector2 pos = snakeDirection switch
            {
                Direction.Forward => new Vector2(snakeBody[headPosition].X, snakeBody[headPosition].Y - snakeVelocity),
                Direction.Downward => new Vector2(snakeBody[headPosition].X, snakeBody[headPosition].Y + snakeVelocity),
                Direction.Left => new Vector2(snakeBody[headPosition].X - snakeVelocity, snakeBody[headPosition].Y),
                Direction.Right => new Vector2(snakeBody[headPosition].X + snakeVelocity, snakeBody[headPosition].Y),
                _ => throw new NotImplementedException()
            };
            snakeBody.Add(pos);

            snakeBody.RemoveAt(0);
        }

        public void SnakeEatFood()
        {
           float dist = Vector2.Distance(food.position, snakeBody[snakeBody.Count - 1]);
           if (dist < 34) 
            {
                snakeBody.Insert(0, new Vector2(snakeBody[0].X - 34, snakeBody[0].Y - 34));
                food.isSpawned = false;
            }
        }
        public enum Direction { Forward, Downward, Left, Right }

    }
}
