﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Myra.Graphics2D.UI.Styles;
using Myra.Utility;

#if !XENKO
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#else
using Xenko.Core.Mathematics;
using Xenko.Graphics;
using Xenko.Input;
using Xenko.Core.Collections;
#endif

namespace Myra.Graphics2D.UI
{
	public class Desktop
	{
		public const int DoubleClickIntervalInMs = 500;

		private RenderContext _renderContext;

		private bool _layoutDirty = true;
		private Rectangle _bounds;
		private bool _widgetsDirty = true;
		private Widget _focusedWidget;
		private readonly List<Widget> _widgetsCopy = new List<Widget>();
		protected readonly ObservableCollection<Widget> _widgets = new ObservableCollection<Widget>();
		private readonly List<Widget> _focusableWidgets = new List<Widget>();
		private DateTime _lastTouch;
		private float _lastMouseWheel;
		private readonly bool[] _lastMouseButtonsDownState = new bool[Enum.GetNames(typeof(MouseButtons)).Length];
		private IReadOnlyCollection<Keys> _lastDownKeys;

		internal Point LastMousePosition
		{
			get; private set;
		}

		public Point MousePosition { get; private set; }
		public float MouseWheel { get; private set; }
		public HorizontalMenu MenuBar { get; set; }

		public Func<Point> MousePositionGetter
		{
			get; set;
		}

		public Func<MouseButtons, bool> IsMouseButtonDownChecker
		{
			get; set;
		}

		public Func<float> MouseWheelGetter
		{
			get; set;
		}

		public Func<IReadOnlyCollection<Keys>> DownKeysGetter
		{
			get; set;
		}

		internal List<Widget> ChildrenCopy
		{
			get
			{
				UpdateWidgetsCopy();
				return _widgetsCopy;
			}
		}

		public ObservableCollection<Widget> Widgets
		{
			get { return _widgets; }
		}

		public Rectangle Bounds
		{
			get { return _bounds; }

			set
			{
				if (value == _bounds)
				{
					return;
				}

				_bounds = value;
				InvalidateLayout();
			}
		}

		public Widget ContextMenu { get; private set; }

		public Widget FocusedWidget
		{
			get { return _focusedWidget; }

			set
			{
				if (value == _focusedWidget)
				{
					return;
				}

				if (_focusedWidget != null)
				{
					_focusedWidget.IterateFocusable(w => w.IsFocused = false);
				}

				_focusedWidget = value;

				if (_focusedWidget != null)
				{
					_focusedWidget.IterateFocusable(w => w.IsFocused = true);
				}
			}
		}

		private RenderContext RenderContext
		{
			get
			{
				EnsureRenderContext();

				return _renderContext;
			}
		}

		/// <summary>
		/// Parameters passed to SpriteBatch.Begin
		/// </summary>
		public SpriteBatchBeginParams SpriteBatchBeginParams
		{
			get
			{
				return RenderContext.SpriteBatchBeginParams;
			}

			set
			{
				RenderContext.SpriteBatchBeginParams = value;
			}
		}

		public float Opacity { get; set; }

		public bool IsMouseOverGUI
		{
			get
			{
				return IsPointOverGUI(MousePosition);
			}
		}

		public bool IsTouchDown
		{
			get; private set;
		}

		public event EventHandler MouseMoved;
		public event EventHandler<GenericEventArgs<MouseButtons>> MouseDown;
		public event EventHandler<GenericEventArgs<MouseButtons>> MouseUp;
		public event EventHandler<GenericEventArgs<MouseButtons>> MouseDoubleClick;

		public event EventHandler TouchDown;
		public event EventHandler TouchUp;

		public event EventHandler<GenericEventArgs<float>> MouseWheelChanged;

		public event EventHandler<GenericEventArgs<Keys>> KeyUp;
		public event EventHandler<GenericEventArgs<Keys>> KeyDown;
		public event EventHandler<GenericEventArgs<char>> Char;

		public event EventHandler<ContextMenuClosingEventArgs> ContextMenuClosing;
		public event EventHandler<GenericEventArgs<Widget>> ContextMenuClosed;

		public Desktop()
		{
			Opacity = 1.0f;
			_widgets.CollectionChanged += WidgetsOnCollectionChanged;

			MousePositionGetter = DefaultMousePositionGetter;
			IsMouseButtonDownChecker = DefaultIsMouseButtonDownChecker;
			MouseWheelGetter = DefaultMouseWheelGetter;
			DownKeysGetter = DefaultDownKeysGetter;

#if MONOGAME
			MyraEnvironment.Game.Window.TextInput += (s, a) =>
			{
				OnChar(a.Character);
			};
#elif FNA
			TextInputEXT.TextInput += c =>
			{
				OnChar(c);
			};
#endif
		}

#if !XENKO
		public static Point DefaultMousePositionGetter()
		{
			var state = Mouse.GetState();
			return new Point(state.X, state.Y);
		}

		public static bool DefaultIsMouseButtonDownChecker(MouseButtons mb)
		{
			var state = Mouse.GetState();
			switch (mb)
			{
				case MouseButtons.Left:
					return state.LeftButton == ButtonState.Pressed;
				case MouseButtons.Middle:
					return state.MiddleButton == ButtonState.Pressed;
				case MouseButtons.Right:
					return state.RightButton == ButtonState.Pressed;
			}

			return false;
		}

		public static float DefaultMouseWheelGetter()
		{
			var state = Mouse.GetState();
			return state.ScrollWheelValue;
		}

		public static IReadOnlyCollection<Keys> DefaultDownKeysGetter()
		{
			return Keyboard.GetState().GetPressedKeys();
		}
#else
		public static Point DefaultMousePositionGetter()
		{
			var input = MyraEnvironment.Game.Input;

			var v = input.AbsoluteMousePosition;
			return new Point((int)v.X, (int)v.Y);
		}

		public static bool DefaultIsMouseButtonDownChecker(MouseButtons mb)
		{
			var input = MyraEnvironment.Game.Input;

			switch (mb)
			{
				case MouseButtons.Left:
					return input.IsMouseButtonDown(MouseButton.Left);
				case MouseButtons.Middle:
					return input.IsMouseButtonDown(MouseButton.Middle);
				case MouseButtons.Right:
					return input.IsMouseButtonDown(MouseButton.Right);
			}

			return false;
		}

		public static float DefaultMouseWheelGetter()
		{
			var input = MyraEnvironment.Game.Input;

			return input.MouseWheelDelta;
		}

		public static IReadOnlyCollection<Keys> DefaultDownKeysGetter()
		{
			var input = MyraEnvironment.Game.Input;

			return input.Keyboard.DownKeys;
		}
#endif

		public Widget GetChild(int index)
		{
			return ChildrenCopy[index];
		}

		private void OnChar(char c)
		{
			var ev = Char;
			if (ev != null)
			{
				ev(this, new GenericEventArgs<char>(c));
			}

			if (_focusedWidget != null)
			{
				_focusedWidget.IterateFocusable(w => w.OnChar(c));
			}
		}

		private void InputOnMouseDown()
		{
			if (ContextMenu != null && !ContextMenu.Bounds.Contains(MousePosition))
			{
				var ev = ContextMenuClosing;
				if (ev != null)
				{
					var args = new ContextMenuClosingEventArgs(ContextMenu);
					ev(this, args);

					if (args.Cancel)
					{
						return;
					}
				}

				HideContextMenu();
			}
		}

		public void ShowContextMenu(Widget menu, Point position)
		{
			if (menu == null)
			{
				throw new ArgumentNullException("menu");
			}

			HideContextMenu();

			ContextMenu = menu;

			if (ContextMenu != null)
			{
				ContextMenu.HorizontalAlignment = HorizontalAlignment.Left;
				ContextMenu.VerticalAlignment = VerticalAlignment.Top;

				ContextMenu.Left = position.X;
				ContextMenu.Top = position.Y;

				ContextMenu.Visible = true;

				_widgets.Add(ContextMenu);
			}
		}

		public void HideContextMenu()
		{
			if (ContextMenu == null)
			{
				return;
			}

			_widgets.Remove(ContextMenu);
			ContextMenu.Visible = false;

			var ev = ContextMenuClosed;
			if (ev != null)
			{
				ev(this, new GenericEventArgs<Widget>(ContextMenu));
			}

			ContextMenu = null;
		}

		private void WidgetsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			if (args.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (Widget w in args.NewItems)
				{
					w.Desktop = this;
					w.MeasureChanged += WOnMeasureChanged;
				}
			}
			else if (args.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (Widget w in args.OldItems)
				{
					w.MeasureChanged -= WOnMeasureChanged;
					w.Desktop = null;
				}
			}

			InvalidateLayout();

			_widgetsDirty = true;
		}

		private void WOnMeasureChanged(object sender, EventArgs eventArgs)
		{
			InvalidateLayout();
		}

		private void EnsureRenderContext()
		{
			if (_renderContext == null)
			{
				var spriteBatch = new SpriteBatch(MyraEnvironment.GraphicsDevice);
				_renderContext = new RenderContext
				{
					Batch = spriteBatch
				};
			}
		}

		public void Render()
		{
			if (Bounds.IsEmpty)
			{
				return;
			}

			UpdateInput();
			UpdateLayout();

			EnsureRenderContext();

			var oldScissorRectangle = CrossEngineStuff.GetScissor();

			_renderContext.Begin();

			CrossEngineStuff.SetScissor(Bounds);
			_renderContext.View = Bounds;
			_renderContext.Opacity = Opacity;

			if (Stylesheet.Current.DesktopStyle != null && 
				Stylesheet.Current.DesktopStyle.Background != null)
			{
				_renderContext.Draw(Stylesheet.Current.DesktopStyle.Background, Bounds);
			}

			foreach (var widget in ChildrenCopy)
			{
				if (widget.Visible)
				{
					widget.Render(_renderContext);
				}
			}

			_renderContext.End();

			CrossEngineStuff.SetScissor(oldScissorRectangle);
		}

		public void InvalidateLayout()
		{
			_layoutDirty = true;
		}

		public void UpdateLayout()
		{
			if (!_layoutDirty)
			{
				return;
			}

			ProcessWidgets();

			foreach (var widget in ChildrenCopy)
			{
				if (widget.Visible)
				{
					widget.Layout(_bounds);
				}
			}

			_layoutDirty = false;
		}

		public int CalculateTotalWidgets(bool visibleOnly)
		{
			var result = 0;
			foreach (var w in _widgets)
			{
				if (visibleOnly && !w.Visible)
				{
					continue;
				}

				++result;

				var asContainer = w as Container;
				if (asContainer != null)
				{
					result += asContainer.CalculateTotalChildCount(visibleOnly);
				}
			}

			return result;
		}

		public void HandleButton(MouseButtons buttons)
		{
			var isDown = IsMouseButtonDownChecker(buttons);
			var wasDown = _lastMouseButtonsDownState[(int)buttons];

			if (isDown && !wasDown)
			{
				IsTouchDown = true;

				var ev = MouseDown;
				if (ev != null)
				{
					ev(this, new GenericEventArgs<MouseButtons>(buttons));
				}

				InputOnMouseDown();
				ChildrenCopy.HandleMouseDown(buttons);

				var td = TouchDown;
				if (td != null)
				{
					td(this, EventArgs.Empty);
				}

				ChildrenCopy.HandleTouchDown();

				if ((DateTime.Now - _lastTouch).TotalMilliseconds < DoubleClickIntervalInMs)
				{
					// Double click
					var ev2 = MouseDoubleClick;
					if (ev2 != null)
					{
						ev2(this, new GenericEventArgs<MouseButtons>(buttons));
					}

					ChildrenCopy.HandleMouseDoubleClick(buttons);

					_lastTouch = DateTime.MinValue;
				}
				else
				{
					_lastTouch = DateTime.Now;
				}
			}
			else if (!isDown && wasDown)
			{
				IsTouchDown = false;

				var ev = MouseUp;
				if (ev != null)
				{
					ev(this, new GenericEventArgs<MouseButtons>(buttons));
				}

				ChildrenCopy.HandleMouseUp(buttons);

				var tu = TouchUp;
				if (tu != null)
				{
					tu(this, EventArgs.Empty);
				}

				ChildrenCopy.HandleTouchUp();
			}

			_lastMouseButtonsDownState[(int)buttons] = isDown;
		}

		public void UpdateInput()
		{
			if (MousePositionGetter != null)
			{
				MousePosition = MousePositionGetter();

				if (SpriteBatchBeginParams.TransformMatrix != null)
				{
					// Apply transform
					var t = Vector2.Transform(
						new Vector2(MousePosition.X, MousePosition.Y),
						SpriteBatchBeginParams.InverseTransform);

					MousePosition = new Point((int)t.X, (int)t.Y);
				}

				if (MousePosition.X != LastMousePosition.X || 
					MousePosition.Y != LastMousePosition.Y)
				{
					var ev = MouseMoved;
					if (ev != null)
					{
						ev(this, EventArgs.Empty);
					}

					ChildrenCopy.HandleMouseMovement();
				}

				LastMousePosition = MousePosition;
			}

			if (IsMouseButtonDownChecker != null)
			{
				HandleButton(MouseButtons.Left);
				HandleButton(MouseButtons.Middle);
				HandleButton(MouseButtons.Right);
			}

			if (MouseWheelGetter != null)
			{
				MouseWheel = MouseWheelGetter();

				if (MouseWheel != _lastMouseWheel)
				{
					var delta = MouseWheel;
#if !XENKO
					delta -= _lastMouseWheel;
#endif

					var ev = MouseWheelChanged;
					if (ev != null)
					{
						ev(null, new GenericEventArgs<float>(delta));
					}

					if (_focusedWidget != null)
					{
						_focusedWidget.IterateFocusable(w => w.OnMouseWheel(delta));
					}
				}

				_lastMouseWheel = MouseWheel;
			}

			if (DownKeysGetter != null)
			{
				var pressedKeys = DownKeysGetter();

				if (pressedKeys != null)
				{
					MyraEnvironment.ShowUnderscores = (MenuBar != null && MenuBar.OpenMenuItem != null) ||
													  pressedKeys.Contains(Keys.LeftAlt) ||
													  pressedKeys.Contains(Keys.RightAlt);

					if (_lastDownKeys != null)
					{
						foreach (var key in pressedKeys)
						{
							if (!_lastDownKeys.Contains(key))
							{
								var ev = KeyDown;
								if (ev != null)
								{
									ev(this, new GenericEventArgs<Keys>(key));
								}

								if (MenuBar != null && MyraEnvironment.ShowUnderscores)
								{
									MenuBar.OnKeyDown(key);
								}
								else
								{
									if (_focusedWidget != null)
									{
										_focusedWidget.IterateFocusable(w => w.OnKeyDown(key));

#if XENKO
										var ch = key.ToChar(pressedKeys.Contains(Keys.LeftShift) ||
															pressedKeys.Contains(Keys.RightShift));
										if (ch != null)
										{
											_focusedWidget.IterateFocusable(w => w.OnChar(ch.Value));
										}
#endif
									}
								}

								if (key == Keys.Escape && ContextMenu != null)
								{
									HideContextMenu();
								}
							}
						}

						foreach (var key in _lastDownKeys)
						{
							if (!pressedKeys.Contains(key))
							{
								// Key had been released
								var ev = KeyUp;
								if (ev != null)
								{
									ev(this, new GenericEventArgs<Keys>(key));
								}

								if (_focusedWidget != null)
								{
									_focusedWidget.IterateFocusable(w => w.OnKeyUp(key));
								}
							}
						}
					}
				}

				_lastDownKeys = pressedKeys.ToArray();
			}
		}

		internal void AddFocusableWidget(Widget w)
		{
			w.MouseDown += FocusableWidgetOnMouseDown;
			_focusableWidgets.Add(w);
		}

		internal void RemoveFocusableWidget(Widget w)
		{
			w.MouseDown -= FocusableWidgetOnMouseDown;
			_focusableWidgets.Remove(w);
		}

		private void ProcessWidgets(IEnumerable<Widget> widgets)
		{
			foreach (var w in widgets)
			{
				if (!w.Visible)
				{
					continue;
				}


				if (MenuBar == null && w is HorizontalMenu)
				{
					MenuBar = (HorizontalMenu)w;
				}

				var asContainer = w as Container;
				if (asContainer != null)
				{
					ProcessWidgets(asContainer.ChildrenCopy);
				}
			}
		}

		private void ProcessWidgets()
		{
			MenuBar = null;

			ProcessWidgets(_widgets);
		}

		private void FocusableWidgetOnMouseDown(object sender, GenericEventArgs<MouseButtons> genericEventArgs)
		{
			var widget = (Widget)sender;

			if (!widget.IsFocused)
			{
				FocusedWidget = widget;
			}
		}

		private void UpdateWidgetsCopy()
		{
			if (!_widgetsDirty)
			{
				return;
			}

			_widgetsCopy.Clear();
			_widgetsCopy.AddRange(_widgets);

			_widgetsDirty = false;
		}

		private bool InternalIsPointOverGUI(Point p, Widget w)
		{
			if (!w.Visible || !w.ActualBounds.Contains(p))
			{
				return false;
			}

			// Non containers are completely solid
			var asContainer = w as Container;
			if (asContainer == null)
			{
				return true;
			}

			// Not real containers are solid as well
			if (!(w is Grid ||
				w is Panel ||
				w is SplitPane ||
				w is ScrollPane))
			{
				return true;
			}

			// Real containers are solid only if backround is set
			if (w.Background != null)
			{
				return true;
			}

			var asScrollPane = w as ScrollPane;
			if (asScrollPane != null)
			{
				// Special case
				if (asScrollPane._horizontalScrollbarVisible && asScrollPane._horizontalScrollbarFrame.Contains(p) ||
					asScrollPane._verticalScrollbarVisible && asScrollPane._verticalScrollbarFrame.Contains(p))
				{
					return true;
				}
			}

			// Or if any child is solid
			foreach (var ch in asContainer.ChildrenCopy)
			{
				if (InternalIsPointOverGUI(p, ch))
				{
					return true;
				}
			}

			return false;
		}

		public bool IsPointOverGUI(Point p)
		{
			foreach (var widget in ChildrenCopy)
			{
				if (InternalIsPointOverGUI(p, widget))
				{
					return true;
				}
			}

			return false;
		}
	}
}