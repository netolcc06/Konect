using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using Coding4Fun.Kinect.Wpf.Controls;
using Microsoft.Kinect;
using System.IO;


namespace KinectingTheDotsUserControl
{
    public partial class MainWindow : Window
    {


        int cont = 1;
        int linhaAtual = 1;
        KinectSensor runtime = KinectSensor.KinectSensors.FirstOrDefault();
        GameComponent jogo = new GameComponent();
        Ellipse[] elipses;
        TextBlock[] textos;
                           
        

        public MainWindow()
        {
           
                       
            InitializeComponent();

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            Unloaded += new RoutedEventHandler(MainWindow_Unloaded);

            vermelho.Click += new RoutedEventHandler(vermelho_Click);
            verde.Click += new RoutedEventHandler(verde_Click);
            azul.Click += new RoutedEventHandler(azul_Click);
            marrom.Click += new RoutedEventHandler(marrom_Click);
            roxo.Click += new RoutedEventHandler(roxo_Click);
            rosa.Click += new RoutedEventHandler(rosa_Click);
            azulescuro.Click += new RoutedEventHandler(azulescuro_Click);
            amarelo.Click += new RoutedEventHandler(amarelo_Click);
            verifica.Click += new RoutedEventHandler(verifica_Click);
            limpar.Click += new RoutedEventHandler(limpar_Click);
            reiniciar.Click += new RoutedEventHandler(reiniciar_Click);
            try
            {
               
                runtime.ColorFrameReady += sensor_ColorFrameReady;
                runtime.SkeletonFrameReady += runtime_SkeletonFrameReady;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);

            } 

        }

        void reiniciar_Click(object sender, RoutedEventArgs e)
        {
            

            cont=1;
            linhaAtual = 1;
            for (int i = 0; i < elipses.Length; i++)
            {
                elipses[i].Fill = Brushes.LightGray;
            }

            for (int i = 0; i < textos.Length; i++)
            {
                textos[i].Text = "-";    
            }

            try
            {
                principal.Source = jogo.nova_imagem("/Resources/mastermind.png");
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
            rectangle1.Fill = Brushes.White;

            gerarSenha();
        }

        void limpar_Click(object sender, RoutedEventArgs e)
        {
            if (cont != 1 && cont != 6 && cont != 11 && cont != 16 && cont != 21 && cont != 26 && cont != 31 && cont != 36 && cont != 41 && cont != 46)
            {
                elipses[cont - linhaAtual - 1].Fill = Brushes.LightGray; 
                cont--;
            }
        }

        void verifica_Click(object sender, RoutedEventArgs e)
        {
            if (cont == 5)
            {
                setarbolas1();
                linhaAtual++;
                cont++;
            }
            if (cont == 10)
            {
                setarbolas2();
                linhaAtual++;
                cont++;
            }
            if (cont == 15)
            {
                setarbolas3();
                linhaAtual++;
                cont++;
            }
            if (cont == 20)
            {
                setarbolas4();
                linhaAtual++;
                cont++;
            }
            if (cont == 25)
            {
                setarbolas5();
                linhaAtual++;
                cont++;
            }
            if (cont == 30)
            {
                setarbolas6();
                linhaAtual++;
                cont++;
            }
            if (cont == 35)
            {
                setarbolas7();
                linhaAtual++;
                cont++;
            }
            if (cont == 40)
            {
                setarbolas8();
                linhaAtual++;
                cont++;
            }
            if (cont == 45)
            {
                setarbolas9();
                linhaAtual++;
                cont++;
            }
            if (cont == 50)
            {
                setarbolas10();
                linhaAtual++;
                cont++;
            }
        }

        void vermelho_Click(object sender, RoutedEventArgs e)
        {

            if (cont % 5 != 0)
            {
                elipses[cont - linhaAtual].Fill = Brushes.Red;
                cont++;
            }
        }
        void verde_Click(object sender, RoutedEventArgs e)
        {
            if (cont % 5 != 0)
            {
                elipses[cont - linhaAtual].Fill = Brushes.Green;
                cont++;
            }
        }

        void azul_Click(object sender, RoutedEventArgs e)
        {
            if (cont % 5 != 0)
            {
                elipses[cont - linhaAtual].Fill = Brushes.LightBlue;
                cont++;
            }
        }

        void marrom_Click(object sender, RoutedEventArgs e)
        {
            if (cont % 5 != 0)
            {
                elipses[cont - linhaAtual].Fill = Brushes.Brown;
                cont++;
            }
        }

        void rosa_Click(object sender, RoutedEventArgs e)
        {
            if (cont % 5 != 0)
            {
                elipses[cont - linhaAtual].Fill = Brushes.LightPink;
                cont++;
            }
        }

        void azulescuro_Click(object sender, RoutedEventArgs e)
        {
            if (cont % 5 != 0)
            {
                elipses[cont - linhaAtual].Fill = Brushes.DarkBlue;
                cont++;
            }
        }

        void amarelo_Click(object sender, RoutedEventArgs e)
        {
            if (cont % 5 != 0)
            {
                elipses[cont - linhaAtual].Fill = Brushes.Yellow;
                cont++;
            }
        }

        void roxo_Click(object sender, RoutedEventArgs e)
        {
            if (cont % 5 != 0)
            {
                elipses[cont - linhaAtual].Fill = Brushes.Purple;
                cont++;
            }
        }

        private Boolean pertence(int[] senha, int x)
        {
            for (int i = 0; i < 4; i++)
            {
                if (x == senha[i])
                {
                    return true;
                }
            }
            return false;
        }

        private void gerarSenha()
        {
            Random random = new Random();
            int number = random.Next(1, 8);


            int[] senha = new int[4];
            int indice;

            for (int i = 0; i < 4; i++)
            {
                indice = random.Next(1, 9);
                if (pertence(senha, indice) == false)
                {
                    senha[i] = indice;
                }
                else
                {
                    i--;
                }
            }
            pintarSenha(senha);

        }

        void pintarSenha(int[] senha)
        {

            
            
            if (senha[0] == 1)
                senha1.Fill = Brushes.Red;
            if (senha[0] == 2)
                senha1.Fill = Brushes.DarkBlue;
            if (senha[0] == 3)
                senha1.Fill = Brushes.Green;
            if (senha[0] == 4)
                senha1.Fill = Brushes.Yellow;
            if (senha[0] == 5)
                senha1.Fill = Brushes.LightPink;
            if (senha[0] == 6)
                senha1.Fill = Brushes.LightBlue;
            if (senha[0] == 7)
                senha1.Fill = Brushes.Purple;
            if (senha[0] == 8)
                senha1.Fill = Brushes.Brown;

            if (senha[1] == 1)
                senha2.Fill = Brushes.Red;
            if (senha[1] == 2)
                senha2.Fill = Brushes.DarkBlue;
            if (senha[1] == 3)
                senha2.Fill = Brushes.Green;
            if (senha[1] == 4)
                senha2.Fill = Brushes.Yellow;
            if (senha[1] == 5)
                senha2.Fill = Brushes.LightPink;
            if (senha[1] == 6)
                senha2.Fill = Brushes.LightBlue;
            if (senha[1] == 7)
                senha2.Fill = Brushes.Purple;
            if (senha[1] == 8)
                senha2.Fill = Brushes.Brown;

            if (senha[2] == 1)
                senha3.Fill = Brushes.Red;
            if (senha[2] == 2)
                senha3.Fill = Brushes.DarkBlue;
            if (senha[2] == 3)
                senha3.Fill = Brushes.Green;
            if (senha[2] == 4)
                senha3.Fill = Brushes.Yellow;
            if (senha[2] == 5)
                senha3.Fill = Brushes.LightPink;
            if (senha[2] == 6)
                senha3.Fill = Brushes.LightBlue;
            if (senha[2] == 7)
                senha3.Fill = Brushes.Purple;
            if (senha[2] == 8)
                senha3.Fill = Brushes.Brown;

            if (senha[3] == 1)
                senha4.Fill = Brushes.Red;
            if (senha[3] == 2)
                senha4.Fill = Brushes.DarkBlue;
            if (senha[3] == 3)
                senha4.Fill = Brushes.Green;
            if (senha[3] == 4)
                senha4.Fill = Brushes.Yellow;
            if (senha[3] == 5)
                senha4.Fill = Brushes.LightPink;
            if (senha[3] == 6)
                senha4.Fill = Brushes.LightBlue;
            if (senha[3] == 7)
                senha4.Fill = Brushes.Purple;
            if (senha[3] == 8)
                senha4.Fill = Brushes.Brown;


        }

        public void setarbolas1()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e1.Fill)
                bolabranca++;
            if (senha2.Fill == e2.Fill)
                bolabranca++;
            if (senha3.Fill == e3.Fill)
                bolabranca++;
            if (senha4.Fill == e4.Fill)
                bolabranca++;

            if (senha1.Fill == e2.Fill)
                bolapreta++;
            if (senha1.Fill == e3.Fill)
                bolapreta++;
            if (senha1.Fill == e4.Fill)
                bolapreta++;

            if (senha2.Fill == e1.Fill)
                bolapreta++;
            if (senha2.Fill == e3.Fill)
                bolapreta++;
            if (senha2.Fill == e4.Fill)
                bolapreta++;

            if (senha3.Fill == e2.Fill)
                bolapreta++;
            if (senha3.Fill == e1.Fill)
                bolapreta++;
            if (senha3.Fill == e4.Fill)
                bolapreta++;

            if (senha4.Fill == e2.Fill)
                bolapreta++;
            if (senha4.Fill == e3.Fill)
                bolapreta++;
            if (senha4.Fill == e1.Fill)
                bolapreta++;


            t1.Text = bolabranca.ToString();
            t2.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try
                {
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);                
                }
                rectangle1.Fill = Brushes.Transparent;
            }

        }

        public void setarbolas2()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e5.Fill)
                bolabranca++;
            if (senha2.Fill == e6.Fill)
                bolabranca++;
            if (senha3.Fill == e7.Fill)
                bolabranca++;
            if (senha4.Fill == e8.Fill)
                bolabranca++;

            if (senha1.Fill == e6.Fill)
                bolapreta++;
            if (senha1.Fill == e7.Fill)
                bolapreta++;
            if (senha1.Fill == e8.Fill)
                bolapreta++;

            if (senha2.Fill == e5.Fill)
                bolapreta++;
            if (senha2.Fill == e7.Fill)
                bolapreta++;
            if (senha2.Fill == e8.Fill)
                bolapreta++;

            if (senha3.Fill == e6.Fill)
                bolapreta++;
            if (senha3.Fill == e5.Fill)
                bolapreta++;
            if (senha3.Fill == e8.Fill)
                bolapreta++;

            if (senha4.Fill == e6.Fill)
                bolapreta++;
            if (senha4.Fill == e7.Fill)
                bolapreta++;
            if (senha4.Fill == e5.Fill)
                bolapreta++;


            t3.Text = bolabranca.ToString();
            t4.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try
                {
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;
            }

        }

        public void setarbolas3()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e9.Fill)
                bolabranca++;
            if (senha2.Fill == e10.Fill)
                bolabranca++;
            if (senha3.Fill == e11.Fill)
                bolabranca++;
            if (senha4.Fill == e12.Fill)
                bolabranca++;

            if (senha1.Fill == e10.Fill)
                bolapreta++;
            if (senha1.Fill == e11.Fill)
                bolapreta++;
            if (senha1.Fill == e12.Fill)
                bolapreta++;

            if (senha2.Fill == e9.Fill)
                bolapreta++;
            if (senha2.Fill == e11.Fill)
                bolapreta++;
            if (senha2.Fill == e12.Fill)
                bolapreta++;

            if (senha3.Fill == e10.Fill)
                bolapreta++;
            if (senha3.Fill == e12.Fill)
                bolapreta++;
            if (senha3.Fill == e9.Fill)
                bolapreta++;

            if (senha4.Fill == e10.Fill)
                bolapreta++;
            if (senha4.Fill == e11.Fill)
                bolapreta++;
            if (senha4.Fill == e9.Fill)
                bolapreta++;


            t5.Text = bolabranca.ToString();
            t6.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try
                {
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;
            }

        }

        public void setarbolas4()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e13.Fill)
                bolabranca++;
            if (senha2.Fill == e14.Fill)
                bolabranca++;
            if (senha3.Fill == e15.Fill)
                bolabranca++;
            if (senha4.Fill == e16.Fill)
                bolabranca++;

            if (senha1.Fill == e14.Fill)
                bolapreta++;
            if (senha1.Fill == e15.Fill)
                bolapreta++;
            if (senha1.Fill == e16.Fill)
                bolapreta++;

            if (senha2.Fill == e13.Fill)
                bolapreta++;
            if (senha2.Fill == e16.Fill)
                bolapreta++;
            if (senha2.Fill == e15.Fill)
                bolapreta++;

            if (senha3.Fill == e14.Fill)
                bolapreta++;
            if (senha3.Fill == e16.Fill)
                bolapreta++;
            if (senha3.Fill == e13.Fill)
                bolapreta++;

            if (senha4.Fill == e14.Fill)
                bolapreta++;
            if (senha4.Fill == e15.Fill)
                bolapreta++;
            if (senha4.Fill == e13.Fill)
                bolapreta++;


            t7.Text = bolabranca.ToString();
            t8.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try
                {
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;
            }
        }


        public void setarbolas5()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e17.Fill)
                bolabranca++;
            if (senha2.Fill == e18.Fill)
                bolabranca++;
            if (senha3.Fill == e19.Fill)
                bolabranca++;
            if (senha4.Fill == e20.Fill)
                bolabranca++;

            if (senha1.Fill == e18.Fill)
                bolapreta++;
            if (senha1.Fill == e19.Fill)
                bolapreta++;
            if (senha1.Fill == e20.Fill)
                bolapreta++;

            if (senha2.Fill == e17.Fill)
                bolapreta++;
            if (senha2.Fill == e19.Fill)
                bolapreta++;
            if (senha2.Fill == e20.Fill)
                bolapreta++;

            if (senha3.Fill == e17.Fill)
                bolapreta++;
            if (senha3.Fill == e18.Fill)
                bolapreta++;
            if (senha3.Fill == e20.Fill)
                bolapreta++;

            if (senha4.Fill == e17.Fill)
                bolapreta++;
            if (senha4.Fill == e18.Fill)
                bolapreta++;
            if (senha4.Fill == e19.Fill)
                bolapreta++;


            t9.Text = bolabranca.ToString();
            t10.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try 
                { 
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png"); 
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;
            }
        }

        public void setarbolas6()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e21.Fill)
                bolabranca++;
            if (senha2.Fill == e22.Fill)
                bolabranca++;
            if (senha3.Fill == e23.Fill)
                bolabranca++;
            if (senha4.Fill == e24.Fill)
                bolabranca++;

            if (senha1.Fill == e22.Fill)
                bolapreta++;
            if (senha1.Fill == e23.Fill)
                bolapreta++;
            if (senha1.Fill == e24.Fill)
                bolapreta++;

            if (senha2.Fill == e21.Fill)
                bolapreta++;
            if (senha2.Fill == e23.Fill)
                bolapreta++;
            if (senha2.Fill == e24.Fill)
                bolapreta++;

            if (senha3.Fill == e21.Fill)
                bolapreta++;
            if (senha3.Fill == e22.Fill)
                bolapreta++;
            if (senha3.Fill == e24.Fill)
                bolapreta++;

            if (senha4.Fill == e21.Fill)
                bolapreta++;
            if (senha4.Fill == e22.Fill)
                bolapreta++;
            if (senha4.Fill == e23.Fill)
                bolapreta++;


            t11.Text = bolabranca.ToString();
            t12.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try
                {
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;
            }
        }

        public void setarbolas7()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e25.Fill)
                bolabranca++;
            if (senha2.Fill == e26.Fill)
                bolabranca++;
            if (senha3.Fill == e27.Fill)
                bolabranca++;
            if (senha4.Fill == e28.Fill)
                bolabranca++;

            if (senha1.Fill == e26.Fill)
                bolapreta++;
            if (senha1.Fill == e27.Fill)
                bolapreta++;
            if (senha1.Fill == e28.Fill)
                bolapreta++;

            if (senha2.Fill == e25.Fill)
                bolapreta++;
            if (senha2.Fill == e27.Fill)
                bolapreta++;
            if (senha2.Fill == e28.Fill)
                bolapreta++;

            if (senha3.Fill == e25.Fill)
                bolapreta++;
            if (senha3.Fill == e26.Fill)
                bolapreta++;
            if (senha3.Fill == e28.Fill)
                bolapreta++;

            if (senha4.Fill == e25.Fill)
                bolapreta++;
            if (senha4.Fill == e26.Fill)
                bolapreta++;
            if (senha4.Fill == e27.Fill)
                bolapreta++;


            t13.Text = bolabranca.ToString();
            t14.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try
                {
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;
            }
        }

        public void setarbolas8()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e29.Fill)
                bolabranca++;
            if (senha2.Fill == e30.Fill)
                bolabranca++;
            if (senha3.Fill == e31.Fill)
                bolabranca++;
            if (senha4.Fill == e32.Fill)
                bolabranca++;

            if (senha1.Fill == e30.Fill)
                bolapreta++;
            if (senha1.Fill == e31.Fill)
                bolapreta++;
            if (senha1.Fill == e32.Fill)
                bolapreta++;

            if (senha2.Fill == e29.Fill)
                bolapreta++;
            if (senha2.Fill == e31.Fill)
                bolapreta++;
            if (senha2.Fill == e32.Fill)
                bolapreta++;

            if (senha3.Fill == e29.Fill)
                bolapreta++;
            if (senha3.Fill == e30.Fill)
                bolapreta++;
            if (senha3.Fill == e32.Fill)
                bolapreta++;

            if (senha4.Fill == e29.Fill)
                bolapreta++;
            if (senha4.Fill == e30.Fill)
                bolapreta++;
            if (senha4.Fill == e31.Fill)
                bolapreta++;


            t15.Text = bolabranca.ToString();
            t16.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try
                {
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;
            }

        }

        public void setarbolas9()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e33.Fill)
                bolabranca++;
            if (senha2.Fill == e34.Fill)
                bolabranca++;
            if (senha3.Fill == e35.Fill)
                bolabranca++;
            if (senha4.Fill == e36.Fill)
                bolabranca++;

            if (senha1.Fill == e34.Fill)
                bolapreta++;
            if (senha1.Fill == e35.Fill)
                bolapreta++;
            if (senha1.Fill == e36.Fill)
                bolapreta++;

            if (senha2.Fill == e33.Fill)
                bolapreta++;
            if (senha2.Fill == e35.Fill)
                bolapreta++;
            if (senha2.Fill == e36.Fill)
                bolapreta++;

            if (senha3.Fill == e33.Fill)
                bolapreta++;
            if (senha3.Fill == e34.Fill)
                bolapreta++;
            if (senha3.Fill == e36.Fill)
                bolapreta++;

            if (senha4.Fill == e33.Fill)
                bolapreta++;
            if (senha4.Fill == e34.Fill)
                bolapreta++;
            if (senha4.Fill == e35.Fill)
                bolapreta++;


            t17.Text = bolabranca.ToString();
            t18.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try
                {
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;
            }
        }

        public void setarbolas10()
        {
            int bolabranca = 0;
            int bolapreta = 0;

            if (senha1.Fill == e37.Fill)
                bolabranca++;
            if (senha2.Fill == e38.Fill)
                bolabranca++;
            if (senha3.Fill == e39.Fill)
                bolabranca++;
            if (senha4.Fill == e40.Fill)
                bolabranca++;

            if (senha1.Fill == e38.Fill)
                bolapreta++;
            if (senha1.Fill == e39.Fill)
                bolapreta++;
            if (senha1.Fill == e40.Fill)
                bolapreta++;

            if (senha2.Fill == e37.Fill)
                bolapreta++;
            if (senha2.Fill == e39.Fill)
                bolapreta++;
            if (senha2.Fill == e40.Fill)
                bolapreta++;

            if (senha3.Fill == e37.Fill)
                bolapreta++;
            if (senha3.Fill == e38.Fill)
                bolapreta++;
            if (senha3.Fill == e40.Fill)
                bolapreta++;

            if (senha4.Fill == e37.Fill)
                bolapreta++;
            if (senha4.Fill == e38.Fill)
                bolapreta++;
            if (senha4.Fill == e39.Fill)
                bolapreta++;


            t19.Text = bolabranca.ToString();
            t20.Text = bolapreta.ToString();

            if (bolabranca == 4)
            {
                cont = 100;
                try 
                { 
                    principal.Source = jogo.nova_imagem("/Resources/venceu.png"); 
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;

            }
            else
            {
                cont = 100;
                try
                {
                    principal.Source = jogo.nova_imagem("/Resources/perdeu.png");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                rectangle1.Fill = Brushes.Transparent;

            }

        }


        void runtime_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            
            Skeleton novo = jogo.SkeletonFrameReady(sender, e);
            if (novo != null)
            {
                jogo.SetEllipsePosition(RightHand, novo.Joints[JointType.HandRight]);
            }

            jogo.CheckButton(vermelho, RightHand);
            jogo.CheckButton(verde, RightHand);
            jogo.CheckButton(azul, RightHand);
            jogo.CheckButton(rosa, RightHand);
            jogo.CheckButton(roxo, RightHand);
            jogo.CheckButton(amarelo, RightHand);
            jogo.CheckButton(marrom, RightHand);
            jogo.CheckButton(azulescuro, RightHand);
            jogo.CheckButton(verifica, RightHand);
            jogo.CheckButton(limpar, RightHand);
            jogo.CheckButton(reiniciar, RightHand);


        }


        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            runtime.Stop();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            elipses = new Ellipse[40] { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15, e16, e17, e18, e19, 
                e20, e21, e22, e23, e24, e25, e26, e27, e28, e29, e30, e31, e32, e33, e34, e35, e36, e37, e38, e39, e40  };

            textos = new TextBlock[20] { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19, t20 };

            try
            {
                jogo.habilitarsensores();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }

            gerarSenha();
        }

        void sensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {

            try
            {
                videoImage.Source = jogo.sensor_ColorFrameReady(sender, e);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception: {0}", ex.Message); 
            }
        }

    } 
}
