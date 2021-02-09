namespace SharpGL_CG_TDM
{

   //Estrutura de um VT
    public class VertexTexture
    {
        public double X;
        public double Y;

        public VertexTexture(double xp, double yp)
        {
            X = xp;
            Y = yp;
        }

        public double GetX() { return X; }
        public double GetY() { return Y; }
    }
}