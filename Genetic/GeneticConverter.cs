using System;
using System.Drawing;
using System.Text;

namespace Genetic
{
	public static class GeneticConverter
	{
		public static Bitmap ByteStringToBitmap (string bs)
		{
			var bitmap = new Bitmap (FitnessCalc.Width, FitnessCalc.Height);
			int index = 0;
			for (int i = 0; i < FitnessCalc.Height; i++) {
				for (int j = 0; j < FitnessCalc.Width; j++) {

					var rString = bs.Substring (index, Constants.COLOR_SIZE);
					index += Constants.COLOR_SIZE;
					var gString = bs.Substring (index, Constants.COLOR_SIZE);
					index += Constants.COLOR_SIZE;
					var bString = bs.Substring (index, Constants.COLOR_SIZE);
					index += Constants.COLOR_SIZE;

					var rByte = Convert.ToByte (rString, 2);
					var gByte = Convert.ToByte (gString, 2);
					var bByte = Convert.ToByte (bString, 2);

					bitmap.SetPixel (j, i, Color.FromArgb (
						(int)Math.Pow (rByte, 2), 
						(int)Math.Pow (gByte, 2), 
						(int)Math.Pow (bByte, 2)));
				}
			}

			return bitmap;
		}

		public static string BitmapToByteString (Bitmap bm)
		{
			var bitmapStringBuilder = new StringBuilder ();
			for (int i = 0; i < bm.Height; i++) {
				for (int j = 0; j < bm.Width; j++) {
					bitmapStringBuilder.Append (ColorToByteString (bm.GetPixel (j, i)));
				}
			}
			return bitmapStringBuilder.ToString ();
		}

		public static string ColorToByteString (Color c)
		{
			var colorStringBuilder = new StringBuilder ();
		
			colorStringBuilder.Append (SubColorToByteString (c.R));
			colorStringBuilder.Append (SubColorToByteString (c.G));
			colorStringBuilder.Append (SubColorToByteString (c.B));

			return colorStringBuilder.ToString ();
		}

		public static string SubColorToByteString (int sc)
		{
			int scSqrt = (int)Math.Sqrt (sc);
			var part = Convert.ToString (scSqrt, 2).PadLeft (Constants.COLOR_SIZE, '0');
			return part;
		}

		public static int ByteStringToSubColor (string bs)
		{
			if (bs.Length != Constants.COLOR_SIZE)
				throw new ArgumentException ("SubColor string lenght not equals to " + Constants.COLOR_SIZE);

			var squared = Convert.ToByte (bs, 2);
			var sc = Math.Pow (squared, 2);
			return (int)sc;
		}
	}
}

