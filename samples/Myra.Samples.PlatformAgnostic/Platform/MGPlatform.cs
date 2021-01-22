﻿using System;
using System.Drawing;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using Myra.Platform;

namespace Myra.Samples.AllWidgets
{
	internal class MGPlatform : IMyraPlatform
	{
		private readonly GraphicsDevice _device;

		private readonly ContentManager _contentManager;

		public Point ViewSize
		{
			get
			{
				return new Point(_device.Viewport.Width, _device.Viewport.Height);
			}
		}

		public MGPlatform(GraphicsDevice device, ContentManager contentManager)
		{
			if (device == null)
			{
				throw new ArgumentNullException(nameof(device));
			}

			_device = device;
			_contentManager = contentManager;
		}

		public object CreateTexture(int width, int height)
		{
			var texture2d = new Texture2D(_device, width, height);

			return texture2d;
		}

		public void SetTextureData(object texture, Rectangle bounds, byte[] data)
		{
			var xnaTexture = (Texture2D)texture;
			xnaTexture.SetData(0, bounds.ToXNA(), data, 0, bounds.Width * bounds.Height * 4);
		}

		public IMyraRenderer CreateRenderer()
		{
			return new MGRenderer(_device);
		}

		public MouseInfo GetMouseInfo()
		{
			var state = Mouse.GetState();

			var result = new MouseInfo
			{
				Position = new Point(state.X, state.Y),
				IsLeftButtonDown = state.LeftButton == ButtonState.Pressed,
				IsMiddleButtonDown = state.MiddleButton == ButtonState.Pressed,
				IsRightButtonDown = state.RightButton == ButtonState.Pressed,
				Wheel = state.ScrollWheelValue
			};

			return result;
		}

		public void SetKeysDown(bool[] keys)
		{
			var state = Keyboard.GetState();
			for (var i = 0; i < keys.Length; ++i)
			{
				keys[i] = state.IsKeyDown((Microsoft.Xna.Framework.Input.Keys)i);
			}
		}

		public TouchCollection GetTouchState()
		{
			// Do not bother with accurately returning touch state for now
			return TouchCollection.Empty;
		}

		public void PlaySound(string path)
		{
			Console.WriteLine($"Play sound: {path}");
			return;
			var sound = _contentManager.Load<SoundEffect>(path);
			sound.Play();
		}
	}
}
