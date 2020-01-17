using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using SharpGL.SceneGraph;
using SharpGL;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Globalization;

namespace AssimpSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Atributi

        /// <summary>
        ///	 Instanca OpenGL "sveta" - klase koja je zaduzena za iscrtavanje koriscenjem OpenGL-a.
        /// </summary>
        World m_world = null;

        #endregion Atributi

        #region Konstruktori

        public MainWindow()
        {
            // Inicijalizacija komponenti
            InitializeComponent();

            // Kreiranje OpenGL sveta
            try
            {
                m_world = new World(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "3D Models\\Wooden Barrel"), "Wooden Barrel.3ds", (int)openGLControl.Width, (int)openGLControl.Height, openGLControl.OpenGL);
            }
            catch (Exception e)
            {
                MessageBox.Show("Neuspesno kreirana instanca OpenGL sveta. Poruka greške: " + e.Message, "Poruka", MessageBoxButton.OK);
                this.Close();
            }
        }

        #endregion Konstruktori

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            m_world.Draw(args.OpenGL);
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            m_world.Initialize(args.OpenGL);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            m_world.Resize(args.OpenGL, (int)openGLControl.Width, (int)openGLControl.Height);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_world.getAnimacija())
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Q: this.Close(); break;
                case Key.W: if (m_world.RotationX > -90.0f) m_world.RotationX -= 5.0f; break; //maksimalno se moze zarotirati scena za 90 step ka desno
                case Key.S: if (m_world.RotationX < 90.0f) m_world.RotationX += 5.0f; break;
                case Key.A: m_world.RotationY -= 5.0f; break;
                case Key.D: m_world.RotationY += 5.0f; break;
                case Key.Add: m_world.SceneDistance -= 50.0f; break;
                case Key.Subtract: m_world.SceneDistance += 50.0f; break;
                case Key.C: m_world.RotationX = 0.0f; m_world.RotationY = -90.0f; m_world.PokreniAnimaciju(); break;
                case Key.F2:
                    OpenFileDialog opfModel = new OpenFileDialog();
                    bool result = (bool)opfModel.ShowDialog();
                    if (result)
                    {

                        try
                        {
                            World newWorld = new World(Directory.GetParent(opfModel.FileName).ToString(), Path.GetFileName(opfModel.FileName), (int)openGLControl.Width, (int)openGLControl.Height, openGLControl.OpenGL);
                            m_world.Dispose();
                            m_world = newWorld;
                            m_world.Initialize(openGLControl.OpenGL);
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show("Neuspesno kreirana instanca OpenGL sveta:\n" + exp.Message, "GRESKA", MessageBoxButton.OK);
                        }
                    }
                    break;
            }
        }

        private void skaliranje_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (m_world.getAnimacija())
            {
                return;
            }

            bool check = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$").IsMatch(e.Text);
            float scaleFactor;

            if (check == true)
            {
                scaleFactor = float.Parse(this.skaliranje.Text + e.Text);
                m_world.setSkaliranjeFaktor(scaleFactor);
            }

            e.Handled = !check;
        }


        //private void bojaR_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    if (m_world.getAnimacija())
        //    {
        //        return;
        //    }
        //    bool check = new Regex("[0-9]").IsMatch(e.Text);
        //    float value;
        //    if (check == true)
        //    {
        //        value = float.Parse(this.skaliranje.Text + e.Text);
        //        m_world.setRValue(value/255f);
        //    }

        //    e.Handled = !check;
        //}

        //private void bojaG_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    if (m_world.getAnimacija())
        //    {
        //        return;
        //    }
        //    bool check = new Regex("[0-9]").IsMatch(e.Text);
        //    float value;
        //    if (check == true)
        //    {
        //        value = float.Parse(this.skaliranje.Text + e.Text);
        //        m_world.setGValue(value/255f);
        //    }
        //    e.Handled = !check;
        //}

        //private void bojaB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    if (m_world.getAnimacija())
        //    {
        //        return;
        //    }
        //    bool check = new Regex("[0-9]").IsMatch(e.Text);
        //    float value;
        //    if (check == true)
        //    {
        //        value = float.Parse(this.skaliranje.Text + e.Text);
        //        m_world.setBValue((value/255));
        //        Console.WriteLine(value);
        //    }
        //    e.Handled = !check;
        //}


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (m_world.getAnimacija())
            {
                return;
            }
            m_world.setPolozajX(m_world.getPolozajX() + 5);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (m_world.getAnimacija())
            {
                return;
            }
            m_world.setPolozajX(m_world.getPolozajX() - 5);
        }


        private void Pomeraj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (m_world.getAnimacija())
                {
                    return;
                }
                //bool check = new Regex(@"[\+-]?[0-9]").IsMatch(e.Text);
                float X;
                float numericValue;
                bool isSucessful = float.TryParse(this.Pomeraj.Text, out numericValue);
                if (isSucessful)
                {
                    X = float.Parse(this.Pomeraj.Text);
                    Console.WriteLine("Success");
                    Console.WriteLine(X);
                    m_world.setPolozajX(X);
                }
                else
                    Console.WriteLine(this.Pomeraj.Text);
                // if (check == true)
                {
                    // X = float.Parse(this.Pomeraj.Text + e.Text);
                    // m_world.setPolozajX(X);
                }
                // e.Handled = !check;
            }
        }



        private void bojaR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (m_world.getAnimacija())
                {
                    return;
                }
                bool check = new Regex("[0-9]").IsMatch(this.bojaR.Text);
                bool check1 = new Regex("[0-9]").IsMatch(this.bojaG.Text);
                bool check2 = new Regex("[0-9]").IsMatch(this.bojaB.Text);
                float value;
                if (check && check1 && check2)
                {
                    value = float.Parse(this.bojaR.Text);
                    m_world.setRValue((value / 255));
                    Console.WriteLine(value);
                    value = float.Parse(this.bojaG.Text);
                    m_world.setGValue(value / 255f);
                    value = float.Parse(this.bojaB.Text);
                    m_world.setBValue((value / 255));
                    Console.WriteLine(value);
                }
                e.Handled = !check;
            }
        }

        private void bojaG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (m_world.getAnimacija())
                {
                    return;
                }
                bool check = new Regex("[0-9]").IsMatch(this.bojaR.Text);
                bool check1 = new Regex("[0-9]").IsMatch(this.bojaG.Text);
                bool check2 = new Regex("[0-9]").IsMatch(this.bojaB.Text);
                float value;
                if (check && check1 && check2)
                {
                    {
                        value = float.Parse(this.bojaR.Text);
                        m_world.setRValue((value / 255));
                        Console.WriteLine(value);
                        value = float.Parse(this.bojaG.Text);
                        m_world.setGValue(value / 255f);
                        value = float.Parse(this.bojaB.Text);
                        m_world.setBValue((value / 255));
                        Console.WriteLine(value);
                    }
                    e.Handled = !check;
                }
            }
        }

        private void bojaB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (m_world.getAnimacija())
                {
                    return;
                }
                bool check = new Regex("[0-9]").IsMatch(this.bojaR.Text);
                bool check1 = new Regex("[0-9]").IsMatch(this.bojaG.Text);
                bool check2 = new Regex("[0-9]").IsMatch(this.bojaB.Text);
                float value;
                if (check && check1 && check2)
                {
                    {
                        value = float.Parse(this.bojaR.Text);
                        m_world.setRValue((value / 255));
                        Console.WriteLine(value);
                        value = float.Parse(this.bojaG.Text);
                        m_world.setGValue(value / 255f);
                        value = float.Parse(this.bojaB.Text);
                        m_world.setBValue((value / 255));
                        Console.WriteLine(value);
                    }
                    e.Handled = !check;
                }
            }

        }

        private void skaliranje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (m_world.getAnimacija())
                {
                    return;
                }

                bool check = new Regex("^[.][0-9]+$|^[0-9]+[.]{0,1}[0-9]*$").IsMatch(this.skaliranje.Text);
                float scaleFactor;

                if (check == true)
                {
                    scaleFactor = float.Parse(this.skaliranje.Text);
                    m_world.setSkaliranjeFaktor(scaleFactor);
                }

                e.Handled = !check;
            }
        }
    }
}
