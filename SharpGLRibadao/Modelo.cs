using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace SharpGL_CG_TDM
{
    public class Modelo : ICloneable
    {
        List<Vertice> LV;
        List<Face> LF;
        List<Triangulo> LT;
        List<VertexTexture> LVT;

        public int Color;

        public double Xmin, Xmax, Ymin, Ymax, Zmin, Zmax;

        public float pos1, pos2, pos3;  

        public Modelo()
        {
            LV = new List<Vertice>();
            LF = new List<Face>();
            LT = new List<Triangulo>();
            LVT = new List<VertexTexture>();
        }

        //Posiciona o Objecto
        public void Posicionar(float posicao1, float posicao2, float posicao3,int cor)
        {
            pos1 = posicao1;
            pos2 = posicao2;
            pos3 = posicao3;

            Xmin += posicao1; Xmax += posicao1;
            Ymin += posicao2; Ymax += posicao2;
            Zmin += posicao3; Zmax += posicao3;

            Color = cor;
        }

        //Move Objecto
        public void Movimentar(float dx, float dy, float dz)
        {
            pos1 += dx;
            pos2 += dy;
            pos3 += dz;

            Xmin += dx; Xmax += dx;
            Ymin += dy; Ymax += dy;
            Zmin += dz; Zmax += dz;
        }

        //Ler Ficheiro
        public bool LerFicheiro(string ficheiro)
        {           
            string FileDirectory = ficheiro;
            string line = "";
            bool Primeira_Passagem = true;

            StreamReader sr = new StreamReader(FileDirectory);
            while ((line = sr.ReadLine()) != null)
            {
                string[] file = line.Split(' ');
                    
                //Ler Vértices
                if (file[0] == "v")
                {
                    double X = Double.Parse(file[1].Replace(',', '.'), CultureInfo.InvariantCulture);
                    double Y = Double.Parse(file[2].Replace(',', '.'), CultureInfo.InvariantCulture);
                    double Z = Double.Parse(file[3].Replace(',', '.'), CultureInfo.InvariantCulture);
                    LV.Add(new Vertice(X, Y, Z));

                    if (Primeira_Passagem == true)
                    {

                        Xmin = Xmax = X;
                        Ymin = Ymax = Y;
                        Zmin = Zmax = Z;
                        
                        Primeira_Passagem = false;
                    }
                    else
                    {
                        //Verifica os Limites Minimos e Máximos
                        Xmin = Math.Min(Xmin, X);
                        Xmax = Math.Max(Xmax, X);

                        Ymin = Math.Min(Ymin, Y); 
                        Ymax = Math.Max(Ymax, Y);

                        Zmin = Math.Min(Zmin, Z); 
                        Zmax = Math.Max(Zmax, Z);
                    }
                }


                //Ler Vertices Textura
                if(file[0] == "vt")
                {
                    double X = Double.Parse(file[1].Replace(',', '.'), CultureInfo.InvariantCulture);
                    double Y = Double.Parse(file[2].Replace(',', '.'), CultureInfo.InvariantCulture);
                    LVT.Add(new VertexTexture(X,Y));
                }

                //Ler Faces
                if (file[0] == "f")
                {
                    Face F = new Face();

                    for (int i = 1; i < 4; i++)
                    {
                        string[] faceline = file[i].Split('/');
                        int V1 = Convert.ToInt32(faceline[0]);
                        F.Add(LV[V1 - 1]);
                        if(faceline.Length == 2)
                        {
                            int FT1 = Convert.ToInt32(faceline[1]);
                            F.AddTexture(LVT[FT1 - 1]);
                        }
                    }

                    LF.Add(F);
                }
            }
            return true;
        }

        //-------------------------------
        public void Mostrar()
        {
            Debug.WriteLine("Mostrar do Modelo");
            Debug.WriteLine("NV = " + LV.Count);
            Debug.WriteLine("NF = " + LF.Count);
        }
        //-------------------------------
        public void DesenharArestas(OpenGL gl, int Color, bool textura)
        {
            gl.Color(200,0,0);
            foreach (Face F in LF)
            {
                F.DesenharArestas(gl,Color,textura);
            }
        }
        //-------------------------------
        public void DesenharFaces(OpenGL gl,int Color, bool textura)
        {
            if (textura)
                gl.Color(1.0, 1.0, 1.0);

            foreach (Face F in LF)
            {
                F.Desenhar(gl,Color,textura);
            }
        }
        //-------------------------------
        public void Desenhar(OpenGL gl,int Color, bool textura)
        {
            gl.PushMatrix();
                gl.Translate(pos1, pos2, pos3);
                DesenharFaces(gl,Color,textura);
                DesenharArestas(gl,Color,textura);
            gl.PopMatrix();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
