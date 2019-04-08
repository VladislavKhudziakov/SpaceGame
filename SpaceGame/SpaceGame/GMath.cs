using System;
using CoreGraphics;
namespace SpaceGame
{
  public static class GMath
  {
    public static CGPoint Normalize(CGPoint vector)
    {
      double vectorLength = Length(vector);
      return new CGPoint(vector.X / vectorLength, vector.Y / vectorLength);
    }


    public static double Length(CGPoint vector)
    {
      return  Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
    }


    public static double Dot(CGPoint vector1, CGPoint vector2)
    {
      return vector1.X * vector2.X + vector1.Y * vector2.Y;
    }
  }
}
