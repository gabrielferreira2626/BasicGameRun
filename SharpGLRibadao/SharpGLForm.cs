using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace SharpGL_CG_TDM
{
    
    public partial class SharpGLForm : Form
    {
        bool fechar = false;
        bool playing = false;
        Modelo Mod;
        int Position = 0;
        List<Modelo> LModelos;
        Modelo Jogador;
        Modelo ObstaculoObj;
        List<Modelo> Obstaculo;
        int[] XPista = {-6,-3,0,3,6};
        int YPista = 20;
        float Velocidade = 0.8f;
        int Pontuacao = 0;
        bool PontuacaoFinal;
        int MelhorPontuacao = 0;
        float YCamera = 0;
        float YViewPoint = 5;
        bool colisao = false;
        int MelhorResultadoTxt;
        double yMove = 0;
        Texture[] VT;

        StreamReader bestScoreTxt;

        System.Media.SoundPlayer fundosound = new System.Media.SoundPlayer("..\\..\\musica\\fundo.wav");

        public SharpGLForm()
        {
            InitializeComponent();
            Mod = new Modelo();
            LModelos = new List<Modelo>();
            Obstaculo = new List<Modelo>();

            OpenGL gl = openGLControl.OpenGL;

            fundosound.Play();

            bestScoreTxt = new StreamReader("..\\..\\..\\files\\bestscore.txt");
            string line = "";

            while((line = bestScoreTxt.ReadLine()) != null)
            {
                MelhorResultadoTxt = Convert.ToInt32(line);

                MelhorPontuacao = MelhorResultadoTxt;
            }

            bestScoreTxt.Close();


            txtPontFinal.Visible = false;
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();

            gl.Color(1.0, 1.0, 1.0);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            VT[0].Bind(gl);
            DesenharFundo(gl, true);
            gl.Disable(OpenGL.GL_TEXTURE_2D);

            

            //Verificar se Existe Colisão
            if (colisao == false && playing == true)
            {
                foreach (Modelo obs in Obstaculo)
                {
                    if (obs.Ymin <= Jogador.Ymax && Jogador.Ymax <= obs.Ymax && Jogador.pos1 == obs.pos1 ||
                        obs.Ymin <= Jogador.Ymin && Jogador.Ymin <= obs.Ymax && Jogador.pos1 == obs.pos1)
                    {
                        colisao = true;
                    }
                }
            }

            //Verificar Melhor Pontuação
            if (MelhorPontuacao < Pontuacao)
                MelhorPontuacao = Pontuacao;

            //Perdeu
            if (colisao == true && playing == true)
            {
                playing = false;

                if (PontuacaoFinal == false)
                {
                    txtStart.Visible = true;
                    txtStart.Text = "Pressione a tecla ESPAÇO para recomeçar";

                    TeclasText.Visible = true;


                    txtPontFinal.Visible = true;
                    txtPontFinal.Text = "A sua pontuação foi: " + Pontuacao;
                    PontuacaoFinal = true;

                    Pontuacao = 0;
                    Velocidade = 0.8f;
                }

                //Gravar Melhor Resultado em Ficheiro
                if(MelhorPontuacao > MelhorResultadoTxt)
                {
                    StreamWriter NewBestScore = new StreamWriter("..\\..\\..\\files\\bestscore.txt");
                    NewBestScore.WriteLine(MelhorPontuacao);
                    NewBestScore.Close();
                }
            }


            //Desenhar Cones
            
            foreach (Modelo M in Obstaculo)
            {
                M.Desenhar(gl,M.Color,false);
            }
            

            //Desenhar Bola
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            VT[1].Bind(gl);
            foreach (Modelo M in LModelos)
            {
                M.Desenhar(gl,M.Color,true);
            }
            gl.Disable(OpenGL.GL_TEXTURE_2D);

            //Alterar Resultado
            txtPontuacao.Text = "Pontuação: " + Pontuacao;
            txtBestScore.Text = "Melhor Resultado: " + MelhorPontuacao;

            //Adicionar Obstáculos às Pistas(Cone)
            if (playing == true && Obstaculo.Count() < 3)
            {
                Random randNobjects = new Random();
                int indexNobjects = randNobjects.Next(1,2);

                for (int i = 0;i < indexNobjects; i++)
                {
                    //Determina a Pista
                    Random rand = new Random();
                    int index = rand.Next(XPista.Length);

                    //Determina a Cor
                    Random Cor = new Random();
                    int c = Cor.Next(0, 3);

                    Modelo X = (Modelo)ObstaculoObj.Clone();
                    X.Posicionar(XPista[index], YPista, 0, c);
                    Obstaculo.Add(X);
                }

                //Distancia entre cones
                YPista += 8;
            }

            //-----------------------------------------------------
            if (playing == true)
            {
                //Aumentar Pontuação
                Pontuacao++;

                //Movimentar Jogador
                Jogador.Movimentar(0, Velocidade, 0);

                //Movimentar Câmera
                YCamera += Velocidade;
                YViewPoint += Velocidade;

                //Apagar Objetos
                for (int i = 0; i < Obstaculo.Count(); i++)
                {
                    if (Obstaculo[i].Ymax < Jogador.Ymin - 5)
                    {
                        Obstaculo.Remove(Obstaculo[i]);
                    }
                }
            }

            //Movimentar Câmera
            MoveCamera();

            //Aumentar Dificuldade
            if (Pontuacao == 100)
                Velocidade = 1.0f;
            else if (Pontuacao == 200)
                Velocidade = 1.2f;
            else if (Pontuacao == 400)
                Velocidade = 1.4f;
            else if (Pontuacao == 600)
                Velocidade = 1.5f;
            else if (Pontuacao == 1000)
                Velocidade = 1.7f;
            else if (Pontuacao == 1300)
                Velocidade = 1.8f;
            else if (Pontuacao == 1500)
                Velocidade = 2.0f;
        }

        

        //Desenhar Fundo
        void DesenharFundo(OpenGL gl, bool textura = true)
        {

            float TAM = 100.0f;
            double xDiferent = 15.0;
            double yDiferent = 25.0;

            gl.Begin(OpenGL.GL_POLYGON);
            if (textura) gl.TexCoord(0.0, 0.0);
                gl.Vertex(-xDiferent, 0.0 - yDiferent + yMove, 0.0);
            if (textura) gl.TexCoord(1.0, 0.0);
                gl.Vertex(TAM, 0.0 - yDiferent + yMove, 0.0);
            if (textura) gl.TexCoord(1.0, 1.0);
                gl.Vertex(TAM, TAM - yDiferent + yMove, 0.0);
            if (textura) gl.TexCoord(0.0, 1.0);
                gl.Vertex(-xDiferent, TAM - yDiferent + yMove, 0.0);
            gl.End();

            //Fundo acompanhar a Bola
            if (playing == true)
            {
                yMove += Velocidade;
            }
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            //  Set the clear color.
            gl.ClearColor(1, 1, 0, 0);

            VT = new Texture[3];
            for (int i = 1; i <= 3; i++)
            {
                VT[i - 1] = new Texture();
                bool ret = VT[i - 1].Create(gl, "..\\..\\Texturas\\Text" + i + ".bmp");
            }
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity.
            gl.LoadIdentity();
            //  Create a perspective transformation.
            gl.Perspective(50.0f, (double)Width / (double)Height, 0.01, 100.0);
            //  Use the 'look at' helper function to position and aim the camera.
            
            gl.LookAt(0, YCamera, 15, 0, YViewPoint, 0, 0, 2, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        //Mover Camera
        private void MoveCamera()
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            gl.PushMatrix();
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity.
            gl.LoadIdentity();
            //  Create a perspective transformation.
            gl.Perspective(50.0f, (double)Width / (double)Height, 0.01, 100.0);
            //  Use the 'look at' helper function to position and aim the camera.

            gl.LookAt(0, YCamera, 15, 0, YViewPoint, 0, 0, 2, 0);
            gl.PopMatrix();
        }

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }

        //Ler Modelos
        private void SharpGLForm_Load(object sender, EventArgs e)
        {
            Jogador = new Modelo();
            Jogador.LerFicheiro("..\\..\\..\\Modelos_OBJ\\esfera.obj");
            Jogador.Posicionar(0, 1, 0,0);
            LModelos.Add(Jogador);

            ObstaculoObj = new Modelo();
            ObstaculoObj.LerFicheiro("..\\..\\..\\Modelos_OBJ\\sinalizador.obj");
        }

        private void tecladown(object sender, KeyEventArgs e)
        {
            //Fechar Jogo
            if (e.KeyCode == Keys.Escape && fechar == false)
            {
                Close();
            }

            //Iniciar Jogo
            if(e.KeyCode == Keys.Space && colisao == false && playing == false)
            {
                playing = true;
                txtStart.Visible = false;
                TeclasText.Visible = false;
            }

            //Recomeçar Jogo
            if (e.KeyCode == Keys.Space && colisao == true && playing == false)
            {
                playing = true;
                colisao = false;
                PontuacaoFinal = false;
                txtStart.Visible = false;
                txtPontFinal.Visible = false;
                TeclasText.Visible = false;
                Obstaculo.Clear();
            }

            //Movimentar User
            float DX = 3.0f;

            if(playing == true)
            {
                if (Position == 0)
                {
                    if (e.KeyCode == Keys.I)
                    {
                        Jogador.Movimentar(-DX, 0.0f, 0.0f);
                        Position = -1;
                    }
                    else if (e.KeyCode == Keys.O)
                    {
                        Jogador.Movimentar(DX, 0.0f, 0.0f);
                        Position = 1;
                    }
                }
                else if (Position == 1)
                {
                    if (e.KeyCode == Keys.I)
                    {
                        Jogador.Movimentar(-DX, 0.0f, 0.0f);
                        Position = 0;
                    }
                    else if (e.KeyCode == Keys.O)
                    {
                        Jogador.Movimentar(DX, 0.0f, 0.0f);
                        Position = 2;
                    }
                }
                else if (Position == -1)
                {
                    if (e.KeyCode == Keys.I)
                    {
                        Jogador.Movimentar(-DX, 0.0f, 0.0f);
                        Position = -2;
                    }
                    else if (e.KeyCode == Keys.O)
                    {
                        Jogador.Movimentar(DX, 0.0f, 0.0f);
                        Position = 0;
                    }
                }
                else if (Position == -2)
                {
                    if (e.KeyCode == Keys.O)
                    {
                        Jogador.Movimentar(DX, 0.0f, 0.0f);
                        Position = -1;
                    }
                }
                else if (Position == 2)
                {
                    if (e.KeyCode == Keys.I)
                    {
                        Jogador.Movimentar(-DX, 0.0f, 0.0f);
                        Position = 1;
                    }
                }
            }
            
        }
    }
}
