using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace CampoMinato
{
    class Button
    {
        public event EventHandler Click;
        public event EventHandler Releas;
        public event EventHandler MouseOn;
        public event EventHandler MouseLeft;

        public RectangleShape shape;
        public readonly Text text;
        private bool isPressed;
        private bool isMouseOver;

        public Button(Vector2f position, string buttonText, int charSize, Color colorButton, Color colorText, Font font)
        {
            isPressed = false;
            isMouseOver = false;
            shape = new RectangleShape(new Vector2f(buttonText.Length * CharSize + 10 * 2, CharSize + 10 * 2))
            {
                Position = position,
                FillColor = colorButton
            };
            text = new Text(buttonText, font)
            {
                Position = new Vector2f(position.X + 10, position.Y + 10),
                FillColor = colorText,
                CharacterSize = (uint)charSize
            };
            this.CharSize = (uint)charSize;
        }

        public Button(Vector2f position, Color colorButton, Text text)
        {
            isPressed = false;
            isMouseOver = false;
            shape = new RectangleShape(new Vector2f(text.DisplayedString.Length * text.CharacterSize + 10 * 2, CharSize + 10 * 2))
            {
                Position = position,
                FillColor = colorButton
            };
            this.text = new Text(text);
            this.text.Position = new Vector2f(position.X + 10, position.Y + 10);
            this.CharSize = this.text.CharacterSize;
        }
        public Button(Button value) : this(value.Position,value.shape.FillColor, value.text)
        {

        }


        public string Message
        {
            get
            {
                return text.DisplayedString;
            }
            set
            {
                text.DisplayedString = value;
            }
        }

        public Vector2f Position
        {
            get
            {
                return shape.Position;
            }
            set
            {
                shape.Position = value;
                text.Position = new Vector2f(shape.Position.X + 10, shape.Position.Y + 10);
            }
        }

        public Vector2f Size
        {
            get
            {
                return shape.Size;
            }
        }

        protected uint charSize;
        public uint CharSize
        {
            get
            {
                return charSize;
            }
            set
            {
                if (value <= 0)
                {
                    charSize = 1;
                }
                else
                {
                    charSize = value;
                }
                text.CharacterSize = value;
                shape.Size = new Vector2f(Message.Length * value + 10, value + 10);
            }
        }

        public virtual void Draw(RenderWindow window)
        {
            DrawAspect(window);
            Update(window);
        }

        public void DrawAspect(RenderWindow window)
        {
            window.Draw(shape);
            window.Draw(text);
        }

        public virtual void Update(RenderWindow window)
        {

            Vector2i mousePosition = Mouse.GetPosition(window);
            bool previouslyOver = isMouseOver;
            isMouseOver = shape.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y);

            if(isMouseOver && ! previouslyOver)
            {
                OnMouseEntered();
            }
            else if(!isMouseOver && previouslyOver)
            {
                OnMouseLeft();
            }

            if (isMouseOver && Mouse.IsButtonPressed(Mouse.Button.Left) && !isPressed)
            {
                OnClicked();
                isPressed = true;
            }
            else if (isPressed && window.HasFocus() && !Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                OnReleased();
                isPressed = false;
            }
        }

        protected virtual void OnClicked()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnReleased()
        {
            Releas?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMouseEntered()
        {
            MouseOn?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMouseLeft()
        {
            MouseLeft?.Invoke(this, EventArgs.Empty);
        }
    }
}
