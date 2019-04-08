using System;
namespace SpaceGame
{
  public class Mat3
  {
    private double[] _data;
    public double[] Data { get => _data; set => _data = value; }


    public Mat3()
    {
      Data = new double[]
      {
        1, 0, 0,
        0, 1, 0,
        0, 0, 1
      };
    }


    public void SetRotation(double angle)
    {
      double sin = Math.Sin(angle);
      double cos = Math.Cos(angle);

      Data = new double[]
      {
         cos, -sin,   0,
         sin,  cos,   0,
           0,    0,   1
      };
    }


    public void SetScaling(double val)
    {
      Data = new double[]
      {
        val,    0,   0,
          0,  val,   0,
          0,    0,   1
      };
    }


    public void SetTranslation(double tx = 0, double ty = 0)
    {
      Data = new double[]
      {
        1,  0,   0,
        0,  1,   0,
       tx, ty,   1
      };
    }


    public void Rotate(double angle)
    {
      Mat3 newMatrix = new Mat3();
      newMatrix.SetRotation(angle);

      Data = (newMatrix * this).Data;
    }


    public void Scale(double scale)
    {
      Mat3 newMatrix = new Mat3();
      newMatrix.SetScaling(scale);

      Data = (newMatrix * this).Data;
    }


    public void Translate(double tx = 0, double ty = 0)
    {
      Mat3 newMatrix = new Mat3();
      newMatrix.SetTranslation(tx, ty);

      Data = (newMatrix * this).Data;
    }


    public static Mat3 operator *(Mat3 mat1, Mat3 mat2)
    {
      double ai0, ai1, ai2;

      Mat3 mat3 = new Mat3();

      var a = mat1.Data;
      var b = mat2.Data;
      var c = mat3.Data;

      for (int i = 0; i < 3; i++)
      {
        ai0 = a[i]; 
        ai1 = a[i + 3];
        ai2 = a[i + 6];

        c[i] = ai0 * b[0] + ai1 * b[1] + ai2 * b[2];
        c[i + 3] = ai0 * b[3] + ai1 * b[4] + ai2 * b[5];
        c[i + 6] = ai0 * b[6] + ai1 * b[7] + ai2 * b[8];
      }

      return mat3;
    }


    public static Mat3 operator *(Mat3 mat1, double val)
    {
      Mat3 mat3 = new Mat3();

      var a = mat1.Data;
      var b = mat3.Data;

      for (int i = 0; i < b.Length; i++)
      {
        b[i] = a[i] * val;
      }

      return mat3;
    }


    public static Mat3 operator +(Mat3 mat1, Mat3 mat2)
    {
      Mat3 mat3 = new Mat3();

      var a = mat1.Data;
      var b = mat2.Data;
      var c = mat3.Data;

      for (int i = 0; i < c.Length; i++)
      {
        c[i] = a[i] + b[i];
      }

      return mat3;
    }


    public static Mat3 operator -(Mat3 mat1, Mat3 mat2)
    {
      Mat3 mat3 = new Mat3();

      var a = mat1.Data;
      var b = mat2.Data;
      var c = mat3.Data;

      for (int i = 0; i < c.Length; i++)
      {
        c[i] = a[i] - b[i];
      }

      return mat3;
    }



    public static Mat3 operator /(Mat3 mat1, double val)
    {
      Mat3 mat3 = new Mat3();

      var a = mat1.Data;
      var b = mat3.Data;

      for (int i = 0; i < b.Length; i++)
      {
        b[i] = a[i] / val;
      }

      return mat3;
    }


    public double this [int index]
    {
      get => Data[index];
      set => Data[index] = value;
    }


    public override string ToString()
    {
      return $"{Data[0]} {Data[1]} {Data[2]}\n" +
             $"{Data[3]} {Data[4]} {Data[5]}\n" +
             $"{Data[6]} {Data[7]} {Data[8]}\n";
    }
  }
}
