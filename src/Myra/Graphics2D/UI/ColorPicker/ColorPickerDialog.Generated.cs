/* Generated by MyraPad at 25.09.2019 23:20:49 */
using Myra.Graphics2D.UI;

#if !XENKO
using Microsoft.Xna.Framework;
#else
using Xenko.Core.Mathematics;
#endif

namespace Myra.Graphics2D.UI.ColorPicker
{
	partial class ColorPickerDialog: Dialog
	{
		private void BuildUI()
		{
			_imageColor = new Image();
			_imageColor.Id = "_imageColor";
			_imageColor.Height = 50;
			_imageColor.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_imageColor.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;

			var horizontalSeparator1 = new HorizontalSeparator();
			horizontalSeparator1.Height = 4;

			var textBlock1 = new TextBlock();
			textBlock1.Text = "R";
			textBlock1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;

			_spinButtonR = new SpinButton();
			_spinButtonR.Maximum = 255;
			_spinButtonR.Minimum = 0;
			_spinButtonR.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_spinButtonR.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_spinButtonR.Id = "_spinButtonR";
			_spinButtonR.GridColumn = 1;

			_sliderR = new HorizontalSlider();
			_sliderR.Maximum = 255;
			_sliderR.Id = "_sliderR";
			_sliderR.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_sliderR.GridColumn = 2;

			var textBlock2 = new TextBlock();
			textBlock2.Text = "G";
			textBlock2.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			textBlock2.GridRow = 1;

			_spinButtonG = new SpinButton();
			_spinButtonG.Maximum = 255;
			_spinButtonG.Minimum = 0;
			_spinButtonG.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_spinButtonG.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_spinButtonG.Id = "_spinButtonG";
			_spinButtonG.GridColumn = 1;
			_spinButtonG.GridRow = 1;

			_sliderG = new HorizontalSlider();
			_sliderG.Maximum = 255;
			_sliderG.Id = "_sliderG";
			_sliderG.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_sliderG.GridColumn = 2;
			_sliderG.GridRow = 1;

			var textBlock3 = new TextBlock();
			textBlock3.Text = "B";
			textBlock3.GridRow = 2;

			_spinButtonB = new SpinButton();
			_spinButtonB.Maximum = 255;
			_spinButtonB.Minimum = 0;
			_spinButtonB.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_spinButtonB.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_spinButtonB.Id = "_spinButtonB";
			_spinButtonB.GridColumn = 1;
			_spinButtonB.GridRow = 2;

			_sliderB = new HorizontalSlider();
			_sliderB.Maximum = 255;
			_sliderB.Id = "_sliderB";
			_sliderB.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_sliderB.GridColumn = 2;
			_sliderB.GridRow = 2;

			var textBlock4 = new TextBlock();
			textBlock4.Text = "A";
			textBlock4.GridRow = 3;

			_spinButtonA = new SpinButton();
			_spinButtonA.Maximum = 255;
			_spinButtonA.Minimum = 0;
			_spinButtonA.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_spinButtonA.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_spinButtonA.Id = "_spinButtonA";
			_spinButtonA.GridColumn = 1;
			_spinButtonA.GridRow = 3;

			_sliderA = new HorizontalSlider();
			_sliderA.Maximum = 255;
			_sliderA.Id = "_sliderA";
			_sliderA.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_sliderA.GridColumn = 2;
			_sliderA.GridRow = 3;

			var grid1 = new Grid();
			grid1.ColumnSpacing = 8;
			grid1.RowSpacing = 4;
			grid1.DefaultRowProportion = new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			};
			grid1.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			});
			grid1.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Pixels,
				Value = 50,
			});
			grid1.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Fill,
			});
			grid1.PaddingTop = 4;
			grid1.PaddingBottom = 4;
			grid1.Widgets.Add(textBlock1);
			grid1.Widgets.Add(_spinButtonR);
			grid1.Widgets.Add(_sliderR);
			grid1.Widgets.Add(textBlock2);
			grid1.Widgets.Add(_spinButtonG);
			grid1.Widgets.Add(_sliderG);
			grid1.Widgets.Add(textBlock3);
			grid1.Widgets.Add(_spinButtonB);
			grid1.Widgets.Add(_sliderB);
			grid1.Widgets.Add(textBlock4);
			grid1.Widgets.Add(_spinButtonA);
			grid1.Widgets.Add(_sliderA);

			var verticalSeparator1 = new VerticalSeparator();
			verticalSeparator1.Width = 3;
			verticalSeparator1.GridColumn = 1;

			var textBlock5 = new TextBlock();
			textBlock5.Text = "H";

			_spinButtonH = new SpinButton();
			_spinButtonH.Maximum = 360;
			_spinButtonH.Minimum = 0;
			_spinButtonH.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_spinButtonH.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_spinButtonH.Id = "_spinButtonH";
			_spinButtonH.GridColumn = 1;

			_sliderH = new HorizontalSlider();
			_sliderH.Maximum = 360;
			_sliderH.Id = "_sliderH";
			_sliderH.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_sliderH.GridColumn = 2;

			var textBlock6 = new TextBlock();
			textBlock6.Text = "S";
			textBlock6.GridRow = 1;

			_spinButtonS = new SpinButton();
			_spinButtonS.Maximum = 100;
			_spinButtonS.Minimum = 0;
			_spinButtonS.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_spinButtonS.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_spinButtonS.Id = "_spinButtonS";
			_spinButtonS.GridColumn = 1;
			_spinButtonS.GridRow = 1;

			_sliderS = new HorizontalSlider();
			_sliderS.Id = "_sliderS";
			_sliderS.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_sliderS.GridColumn = 2;
			_sliderS.GridRow = 1;

			var textBlock7 = new TextBlock();
			textBlock7.Text = "V";
			textBlock7.GridRow = 2;

			_spinButtonV = new SpinButton();
			_spinButtonV.Maximum = 100;
			_spinButtonV.Minimum = 0;
			_spinButtonV.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_spinButtonV.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_spinButtonV.Id = "_spinButtonV";
			_spinButtonV.GridColumn = 1;
			_spinButtonV.GridRow = 2;

			_sliderV = new HorizontalSlider();
			_sliderV.Id = "_sliderV";
			_sliderV.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_sliderV.GridColumn = 2;
			_sliderV.GridRow = 2;

			var textBlock8 = new TextBlock();
			textBlock8.Text = "#";
			textBlock8.GridRow = 3;

			_textFieldHex = new TextField();
			_textFieldHex.Text = "";
			_textFieldHex.Id = "_textFieldHex";
			_textFieldHex.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_textFieldHex.GridColumn = 1;
			_textFieldHex.GridRow = 3;
			_textFieldHex.GridColumnSpan = 2;

			var grid2 = new Grid();
			grid2.ColumnSpacing = 8;
			grid2.RowSpacing = 5;
			grid2.DefaultRowProportion = new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			};
			grid2.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			});
			grid2.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Pixels,
				Value = 50,
			});
			grid2.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Fill,
			});
			grid2.PaddingTop = 4;
			grid2.PaddingBottom = 4;
			grid2.GridColumn = 2;
			grid2.Widgets.Add(textBlock5);
			grid2.Widgets.Add(_spinButtonH);
			grid2.Widgets.Add(_sliderH);
			grid2.Widgets.Add(textBlock6);
			grid2.Widgets.Add(_spinButtonS);
			grid2.Widgets.Add(_sliderS);
			grid2.Widgets.Add(textBlock7);
			grid2.Widgets.Add(_spinButtonV);
			grid2.Widgets.Add(_sliderV);
			grid2.Widgets.Add(textBlock8);
			grid2.Widgets.Add(_textFieldHex);

			var grid3 = new Grid();
			grid3.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Part,
			});
			grid3.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			});
			grid3.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Part,
			});
			grid3.RowsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			});
			grid3.Widgets.Add(grid1);
			grid3.Widgets.Add(verticalSeparator1);
			grid3.Widgets.Add(grid2);

			var horizontalSeparator2 = new HorizontalSeparator();
			horizontalSeparator2.Height = 4;

			_gridUserColors = new Grid();
			_gridUserColors.ColumnSpacing = 4;
			_gridUserColors.RowSpacing = 4;
			_gridUserColors.DefaultColumnProportion = new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Part,
			};
			_gridUserColors.DefaultRowProportion = new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Part,
			};
			_gridUserColors.GridSelectionMode = Myra.Graphics2D.UI.GridSelectionMode.Cell;
			_gridUserColors.Id = "_gridUserColors";
			_gridUserColors.Height = 100;

			_buttonSaveColor = new TextButton();
			_buttonSaveColor.Text = "Save Color";
			_buttonSaveColor.Id = "_buttonSaveColor";
			_buttonSaveColor.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Right;
			_buttonSaveColor.GridRow = 1;

			var grid4 = new Grid();
			grid4.RowSpacing = 4;
			grid4.DefaultRowProportion = new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			};
			grid4.Widgets.Add(_gridUserColors);
			grid4.Widgets.Add(_buttonSaveColor);

			var horizontalSeparator3 = new HorizontalSeparator();
			horizontalSeparator3.Height = 4;

			var verticalBox1 = new VerticalBox();
			verticalBox1.Spacing = -1;
			verticalBox1.Proportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Fill,
				Value = 100,
			});
			verticalBox1.Widgets.Add(_imageColor);
			verticalBox1.Widgets.Add(horizontalSeparator1);
			verticalBox1.Widgets.Add(grid3);
			verticalBox1.Widgets.Add(horizontalSeparator2);
			verticalBox1.Widgets.Add(grid4);
			verticalBox1.Widgets.Add(horizontalSeparator3);

			
			Title = "Color Picker";
			Left = 299;
			Top = 27;
			Width = 450;
			Content = verticalBox1;
		}

		
		public Image _imageColor;
		public SpinButton _spinButtonR;
		public HorizontalSlider _sliderR;
		public SpinButton _spinButtonG;
		public HorizontalSlider _sliderG;
		public SpinButton _spinButtonB;
		public HorizontalSlider _sliderB;
		public SpinButton _spinButtonA;
		public HorizontalSlider _sliderA;
		public SpinButton _spinButtonH;
		public HorizontalSlider _sliderH;
		public SpinButton _spinButtonS;
		public HorizontalSlider _sliderS;
		public SpinButton _spinButtonV;
		public HorizontalSlider _sliderV;
		public TextField _textFieldHex;
		public Grid _gridUserColors;
		public TextButton _buttonSaveColor;
	}
}