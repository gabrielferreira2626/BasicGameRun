using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace SharpGL_CG_TDM
{
    public class Face
    {
    
        //Lista de Vertices
        List<Vertice> LVFace;
        //Lista de Pontos Textura
        List<VertexTexture> LFT;

        public Face()
        {
            LVFace = new List<Vertice>();
            LFT = new List<VertexTexture>();
        }
        
        //Adiciona Vértice
        public void Add(Vertice V) { LVFace.Add(V); }

        //Adiciona um Vertice de Textura
        public void AddTexture(VertexTexture FT) { LFT.Add(FT); }


        public int GetNVertices() { return LVFace.Count; }
        
        public void Desenhar(OpenGL gl,int Color,bool textura)
        {

            gl.Begin(OpenGL.GL_TRIANGLES);
            int i = 0;

            foreach (Vertice V in LVFace)
            {
                if (textura == false)
                {
                    //Cor dos Cones
                    if (Color == 0) gl.Color(1.0, 0.56, 0.21);
                    if (Color == 1) gl.Color(1.0, 1.0, 1.0);
                    if (Color == 2) gl.Color(0.95, 1.0, 0.21);
                }
                else
                {
                    //Textura da Bola
                    VertexTexture TEXT = LFT[i];
                    gl.TexCoord(TEXT.GetX(), TEXT.GetY());
                }

                gl.Vertex(V.GetX(), V.GetY(), V.GetZ());

            }                  
            gl.End();
        }
        public void DesenharArestas(OpenGL gl,int Color, bool textura)
        {
            gl.Begin(OpenGL.GL_LINES);
            foreach (Vertice V in LVFace)
            {
                if (!textura)
                {
                    //Cor das Arestas
                    if (Color == 0) gl.Color(1.0, 0.56, 0.21);
                    if (Color == 1) gl.Color(1.0, 1.0, 1.0);
                    if (Color == 2) gl.Color(0.95, 1.0, 0.21);
                    gl.Vertex(V.GetX(), V.GetY(), V.GetZ());
                }
            } 
            gl.End();
        }
    }
}
