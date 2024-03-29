﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IterativeMAPEstimation
{
   public static class HypothesisImageFunctions
    {

       public static FeatureVector createFirstHypothesis(Image<Bgr, Byte> imagen)
       {
           List<PointF> fingertips = new List<PointF>();

           fingertips.Add(new PointF(110,220));
           fingertips.Add(new PointF(175, 60));
           fingertips.Add(new PointF(270, 4));
           fingertips.Add(new PointF(410, 26));
           fingertips.Add(new PointF(640, 200));


           PointF punto = new PointF(400, 400);
           //List<PointF> newFingertips = new List<PointF>();
           List<float> angles = calculateFingerAngles(fingertips, punto);

           FeatureVector vector = new FeatureVector(fingertips, angles, punto, 5);

           //dibujar punto central mano
           PointF puntoC = new PointF(400, 400);
           Point punt = new Point(400, 400);
           CircleF centerCircle = new CircleF(puntoC, 5f);
           imagen.Draw(centerCircle, new Bgr(Color.Brown), 3);

           foreach (PointF p in fingertips)
           {

               CircleF circle = new CircleF(p, 5f);

               imagen.Draw(circle, new Bgr(Color.Red), 3);


               Point pun = new Point(int.Parse(p.X.ToString()), int.Parse(p.Y.ToString()));
               LineSegment2D lineaDedoCentro = new LineSegment2D(pun, punt);
               imagen.Draw(lineaDedoCentro, new Bgr(Color.Green), 2);
           
           }

           Point p1 = new Point(int.Parse((puntoC.X - 90).ToString()), int.Parse((puntoC.Y - 90).ToString()));
           Point p2 = new Point(int.Parse((puntoC.X - 90).ToString()), int.Parse((puntoC.Y + 90).ToString()));
           Point p3 = new Point(int.Parse((puntoC.X + 90).ToString()), int.Parse((puntoC.Y - 90).ToString()));
           Point p4 = new Point(int.Parse((puntoC.X + 90).ToString()), int.Parse((puntoC.Y + 90).ToString()));

           LineSegment2D line = new LineSegment2D(p1, p2);
           LineSegment2D line1 = new LineSegment2D(p1, p3);
           LineSegment2D line2 = new LineSegment2D(p3, p4);
           LineSegment2D line3 = new LineSegment2D(p2, p4);

           imagen.Draw(line, new Bgr(Color.Brown), 3);
           imagen.Draw(line1, new Bgr(Color.Brown), 3);
           imagen.Draw(line2, new Bgr(Color.Brown), 3);
           imagen.Draw(line3, new Bgr(Color.Brown), 3);

           return vector;
       
       }

       public static List<float> calculateFingerAngles(List<PointF> fingertips, PointF center)
        {
            List<float> listaAngulos = new List<float>();

            foreach (PointF p in fingertips)
            {

                float c1 = center.X - p.X;
                float c2 = center.Y - p.Y;

                float h = float.Parse(Math.Sqrt((c1 * c1) + (c2 * c2)).ToString());

                float sinAlpha = c2 / h;

                Double angulo = Math.Asin(sinAlpha);
                Double anguloGrad = (angulo * 360) / (2 * 3.14158);
                float alpha = float.Parse(anguloGrad.ToString());

                if (p.X > center.X)
                {

                    alpha = 180 - alpha;

                }

                listaAngulos.Add(alpha);

            }


            return listaAngulos;
        }
    }
    }

