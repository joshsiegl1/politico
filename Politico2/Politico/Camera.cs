using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Politico2.Politico
{
    public static class Camera
    {
        static float _zoom; //Camera Zoom
        static Matrix _transform; //Matrix Transform
        static Vector2 _pos; //Camera Position
        static Vector3 _origin; //Camera Zoom Point

        static float ShakeTime = 0f;
        static bool Shaking = false;
        public static bool _Shaking { get { return Shaking; } }

        static Camera()
        {
            _zoom = 1.0f;
            _pos = Vector2.Zero; 
        }

        public static float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; }
        }

        public static void Move(Vector2 amount)
        {
            _pos += amount; 
        }

        public static bool Earthqauake = false; 
        public static void Shake(float time, bool earthquake)
        {
            ShakeTime = time;
            Shaking = true;
            Earthqauake = earthquake; 
        }

        public static Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public static Vector3 Origin
        {
            get { return _origin; }
        }

        static float ShakeTimer = 0f;
        static float angle; 
        public static void Update(GameTime gametime)
        {
            if (Shaking)
            {
                angle++; 
                _pos.X = (float)Math.Sin(angle) * 5; 

                ShakeTimer += (float)gametime.ElapsedGameTime.TotalMilliseconds; 
                if (ShakeTimer >= ShakeTime)
                {
                    Shaking = false;
                    ShakeTimer = 0f;
                    _pos = Vector2.Zero;
                    Earthqauake = false; 
                }
            }
        }

        public static Matrix get_transformation(GraphicsDevice graphicsDevice, Matrix ScreenMatrix, MenuSystem.ScreenManager ScreenManager)
        {

            _origin = new Vector3(ScreenManager.Viewport.Width * 0.5f, ScreenManager.Viewport.Height * 0.5f, 0);

            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-ScreenManager.Viewport.Width * 0.5f, -ScreenManager.Viewport.Height * 0.5f, 0)) *
                                         Matrix.CreateRotationZ(0f) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         //Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, 
                                         //graphicsDevice.Viewport.Height * 0.5f, 0));
                                         Matrix.CreateTranslation(_origin) * ScreenMatrix; 
            return _transform;
        }

        public static Matrix get_transformation_cursor(MenuSystem.ScreenManager ScreenManager)
        {

            _origin = new Vector3(ScreenManager.Viewport.Width * 0.5f, ScreenManager.Viewport.Height * 0.5f, 0);

            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-ScreenManager.Viewport.Width * 0.5f, -ScreenManager.Viewport.Height * 0.5f, 0)) *
                                         Matrix.CreateRotationZ(0f) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         //Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, 
                                         //graphicsDevice.Viewport.Height * 0.5f, 0));
                                         Matrix.CreateTranslation(_origin);
            return _transform;
        }
    }
}
