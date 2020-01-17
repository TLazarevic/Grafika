// -----------------------------------------------------------------------
// <file>World.cs</file>
// <copyright>Grupa za Grafiku, Interakciju i Multimediju 2013.</copyright>
// <author>Srđan Mihić</author>
// <author>Aleksandar Josić</author>
// <summary>Klasa koja enkapsulira OpenGL programski kod.</summary>
// -----------------------------------------------------------------------
using System;
using Assimp;
using System.IO;
using System.Reflection;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Cameras;
using SharpGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Threading;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneGraph.Assets;

namespace AssimpSample
{


    /// <summary>
    ///  Klasa enkapsulira OpenGL kod i omogucava njegovo iscrtavanje i azuriranje.
    /// </summary>
    public class World : IDisposable
    {
        #region Atributi

        /// <summary>
        ///	 Scena koja se prikazuje.
        /// </summary>
        private AssimpScene m_scene;

        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        private float m_xRotation = 0.0f;

        /// <summary>
        ///	 Ugao rotacije sveta oko Y ose.
        /// </summary>
        private float m_yRotation = 0.0f;

        /// <summary>
        ///	 Udaljenost scene od kamere.
        /// </summary>
        private float m_sceneDistance = 80.0f;

        /// <summary>
        ///	 Sirina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_width;

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_height;

        private enum TextureObjects { Wood = 0, Metal, Concrete};

        private readonly int m_textureCount =
        Enum.GetNames(typeof(TextureObjects)).Length;
        /// <summary>
        /// Identifikatori OpenGL tekstura
        /// </summary>
        private uint[] m_textures = null;
        /// <summary>
        /// Putanje do slika koje se koriste za teksture
        /// </summary>
        private string[] m_textureFiles = {
        "..//..//images//wood.jpg", "..//..//images//metal4.jpg",
        "..//..//images//concrete.jpg" };

        private string m_modelTexture =  "..//..//images//roses.bmp" ;

        private bool animacija=false;
        private float skaliranjeFaktor = 1;
        private float Rvalue = 0.9f;
        private float Gvalue = 0.1f;
        private float Bvalue = 0.1f;
        private float polozajX = 30f;
        //atributi za animaciju
        private float rotacijaKocke = 0f;
        public DispatcherTimer timer1;
        public DispatcherTimer timer2;
        private float[] pozicijeBureta;
        private bool kotrljanje = false;
        private bool rotiranjeDrzaca = false;
        private bool propadanje = false;
        private float pomeraj = 0f;
        private float angle = 0f;
        private float spustanje = 0f;
        private float ugaoKotrljanja = 0f;
        private float blagoPodizanjeDrzaca = 0f;
        private float blagoPodizanjeBureta = 0f;


        #endregion Atributi

        #region Properties

        /// <summary>
        ///	 Scena koja se prikazuje.
        /// </summary>
        public AssimpScene Scene
        {
            get { return m_scene; }
            set { m_scene = value; }
        }

        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        public float RotationX
        {
            get { return m_xRotation; }
            set { m_xRotation = value; }
        }

        /// <summary>
        ///	 Ugao rotacije sveta oko Y ose.
        /// </summary>
        public float RotationY
        {
            get { return m_yRotation; }
            set { m_yRotation = value; }
        }

        /// <summary>
        ///	 Udaljenost scene od kamere.
        /// </summary>
        public float SceneDistance
        {
            get { return m_sceneDistance; }
            set { m_sceneDistance = value; }
        }

        /// <summary>
        ///	 Sirina OpenGL kontrole u pikselima.
        /// </summary>
        public int Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        public int Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        #endregion Properties

        #region Konstruktori

        /// <summary>
        ///  Konstruktor klase World.
        /// </summary>
        public World(String scenePath, String sceneFileName, int width, int height, OpenGL gl)
        {
            this.m_scene = new AssimpScene(scenePath, sceneFileName, gl);
            this.m_width = width;
            this.m_height = height;
        }

        /// <summary>
        ///  Destruktor klase World.
        /// </summary>
        ~World()
        {
            this.Dispose(false);
        }

        #endregion Konstruktori

        #region Metode

        /// <summary>
        ///  Korisnicka inicijalizacija i podesavanje OpenGL parametara.
        /// </summary>
        public void Initialize(OpenGL gl)
        {
            #region osvetljenje

        

            gl.Enable(OpenGL.GL_LIGHTING);

            float[] ambijentalnaKomponenta = { 0.1f, 0.1f, 0.1f, 1.0f };
            float[] difuznaKomponenta = { 0.1f, 0.1f, 0.1f, 1.0f };
            float[] spekularnaKomponenta = { 0.3f, 0.3f, 0.3f, 1 };
            float[] smer = { 0.0f, 0.0f, -1.0f };
            // Pridruži komponente svetlosnom izvoru 0
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT,
            ambijentalnaKomponenta);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE,
            difuznaKomponenta);
             gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR,
             spekularnaKomponenta);
            // Podesi parametre izvora
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_DIRECTION, smer);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_CUTOFF, 180.0f); //tackasti je refl sa cutoff 180
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_LINEAR_ATTENUATION, 0.5f);  //Sa ADD stapanjem s materijalom svetlost je bila prejaka. Ovako sam oslabila svetlo
            // Ukljuci svetlosni izvor
            gl.Enable(OpenGL.GL_LIGHT0);
            float[] pozicija = { 200.0f, 550.0f,0, 1.0f }; //x,y,z,w gde je 1 poziciono svetlo. Visok izvor za vise slabljenja i manje jako svetlo
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION,
            pozicija);



            float[] ambijentalnaKomponenta2 = { Rvalue, Gvalue, Bvalue, 1.0f };
            float[] difuznaKomponenta2 = { 0.1f, 0.1f, 0.1f, 1.0f };
            // float[] spekularnaKomponenta2 = { 0.1f, 0.1f, 0.1f, 1 };

            float[] smer2 = { 0f, -1f, 0f };
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_AMBIENT,
            ambijentalnaKomponenta2);
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_DIFFUSE,
            difuznaKomponenta2);
              //gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPECULAR,
             //spekularnaKomponenta);
            // Podesi parametre izvora
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPOT_DIRECTION, smer2);
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPOT_CUTOFF, 30.0f); //tackasti je refl sa cutoff 180

            // Ukljuci svetlosni izvor
            gl.Enable(OpenGL.GL_LIGHT1);

            float[] pozicija2 = { polozajX, 150.0f, -m_sceneDistance + 25, 1.0f }; //x,y,z,w gde je 1 
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_POSITION,
            pozicija2);

            // Uikljuci color tracking mehanizam
            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
            // Podesi na koje parametre materijala se odnose pozivi glColor funkcije
            gl.ColorMaterial(OpenGL.GL_FRONT,
            OpenGL.GL_AMBIENT_AND_DIFFUSE);
            // Ukljuci automatsku normalizaciju nad normalama
            gl.Enable(OpenGL.GL_NORMALIZE);



            #endregion osvetljenje

            #region teksture

            gl.Enable(OpenGL.GL_TEXTURE_2D);

            gl.TexParameter(OpenGL.GL_TEXTURE_2D,
               OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR); // Linear Filtering
            gl.TexParameter(OpenGL.GL_TEXTURE_2D,
            OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR); // Linear Filtering
            gl.TexParameter(OpenGL.GL_TEXTURE_2D,
            OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D,
            OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
            gl.TexEnv(OpenGL.GL_TEXTURE_ENV,
            OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_ADD);

            m_textures = new uint[m_textureCount];

            // Ucitaj slike i kreiraj teksture
            gl.GenTextures(m_textureCount, m_textures);
            for (int i = 0; i < m_textureCount; ++i)
            {
                // Pridruzi teksturu odgovarajucem identifikatoru
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[i]);

                // Ucitaj sliku i podesi parametre teksture
                
                Bitmap image = new Bitmap(m_textureFiles[i]);
                // rotiramo sliku zbog koordinantog sistema opengl-a
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
                // RGBA format (dozvoljena providnost slike tj. alfa kanal)
                BitmapData imageData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                                      System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                gl.Build2DMipmaps(OpenGL.GL_TEXTURE_2D, (int)OpenGL.GL_RGBA8, image.Width, image.Height, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, imageData.Scan0);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);      // Linear Filtering
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);      // Linear Filtering

                image.UnlockBits(imageData);
                image.Dispose();
            }

            #endregion teksture

            // Model sencenja na flat (konstantno)
            gl.ShadeModel(OpenGL.GL_SMOOTH);


            m_scene.LoadScene();
            m_scene.Initialize();
        }


        /// <summary>
        ///  Iscrtavanje OpenGL kontrole.
        /// </summary>
        public void Draw(OpenGL gl)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.LoadIdentity();

            gl.Color(0.4f, 0.4f, 0.4f);

            /*
             * 
             double eyex, double eyey, double eyez,
             double centerx, double centery, double centerz,
             double upx, double upy, double upz)
             * 
             */
            gl.LookAt(-40, 20, -m_sceneDistance + 10, 0, 0, -m_sceneDistance + 10, 0, 1, 0);

            float[] ambijentalnaKomponenta2 = { Rvalue, Gvalue, Bvalue, 1.0f };
            float[] difuznaKomponenta2 = { 0.1f, 0.1f, 0.1f, 1.0f };
            // float[] spekularnaKomponenta2 = { 0.1f, 0.1f, 0.1f, 1 };

            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_AMBIENT,
            ambijentalnaKomponenta2);
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_DIFFUSE,
            difuznaKomponenta2);
            //  gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPECULAR,
            // spekularnaKomponenta2);

            float[] pozicija2 = { polozajX, 100.0f, -m_sceneDistance + 25, 1.0f }; //x,y,z,w gde je 1 
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_POSITION,
            pozicija2);

            gl.PushMatrix();

     

            gl.PushMatrix();

           

            gl.PopMatrix();

            gl.PushMatrix();



            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.FrontFace(OpenGL.GL_CCW);


            gl.Translate(5f, -10.0f, -m_sceneDistance + 10);
            


            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.FrontFace(OpenGL.GL_CCW);

            gl.Rotate(m_xRotation, 1.0f, 0.0f, 0.0f);
            gl.Rotate(m_yRotation, 0.0f, 1.0f, 0.0f);

            gl.PushMatrix();
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)TextureObjects.Wood]);
            gl.Translate(-20f, 0f, 0f);
            gl.Rotate(0f, 0f, -90f);
            gl.Rotate(90f, 0f, 0f);

            if (rotiranjeDrzaca)
            {
                gl.Rotate(0f,-angle, 0f);
                gl.Translate(0f, 0f, blagoPodizanjeDrzaca);
            }
            gl.Translate(4.0f, 0f, 0);
            gl.Scale(3f, 7f, 5f);
            //gl.Color(0.5f, 0.5f, 0.5f);

            Cube cube = new Cube();
            gl.Normal(0f, -1f, 0f);

            cube.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);


            gl.PopMatrix();

            gl.PushMatrix();
            
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)TextureObjects.Metal]);
            gl.FrontFace(OpenGL.GL_CCW);
            gl.Translate(30.0f, -6.5f, 0f);
            //gl.Color(0.5f, 0.5f, 0.5f);
            gl.Rotate(90f, 0f, 0f);
            Disk disk = new Disk();
            disk.Material = new SharpGL.SceneGraph.Assets.Material();
            disk.Material.Shininess = 10f;
            disk.OuterRadius = 10f;
            disk.CreateInContext(gl);;
            disk.TextureCoords = true;
            

            gl.MatrixMode(OpenGL.GL_TEXTURE);
            gl.LoadIdentity();
            gl.Scale(0.7f, 0.7f, 0.7f);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.Rotate(0f, -180f, 0f);
            disk.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);
           
            disk.NormalGeneration = SharpGL.SceneGraph.Quadrics.Normals.Smooth;
            disk.NormalOrientation = Orientation.Outside;

            gl.PopMatrix();

            gl.FrontFace(OpenGL.GL_CCW);

            gl.PushMatrix();
            gl.Translate(0f, -7f, 0f);
            
            gl.BindTexture(OpenGL.GL_TEXTURE_2D,
  m_textures[(int)TextureObjects.Concrete]);


            gl.MatrixMode(OpenGL.GL_TEXTURE);
            gl.LoadIdentity();
            gl.Scale(1.4f, 1.4f, 1.4f);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            gl.Begin(OpenGL.GL_QUADS);

            //gl.Color(0.9f, 0.9f, 0.9f);
            gl.Normal(0f, 1f, 0f);
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(50, 0, 50);
            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(50, 0, -50);
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(-50, 0, -50);
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(-50, 0, 50);

            gl.End();

            gl.PopMatrix();


            //gl.Enable(OpenGL.GL_TEXTURE_2D);
            // gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)TextureObjects.Roses]);
            //  gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_ADD);
            gl.PushMatrix();

            gl.Translate(-20f, 0f, -7f);
            if (kotrljanje)
            {

                gl.Translate(pomeraj, 0f, 0f);
                gl.Rotate (0f, 0f, ugaoKotrljanja);

            }
            if (propadanje)
            {
                gl.Translate(0f, spustanje, 0f);
                
            }
            
            gl.Translate(0f, blagoPodizanjeBureta, 0f);

            gl.Scale(1, skaliranjeFaktor, 1);

            // gl.Scale(1,1 , skaliranjeFaktor);

            //if (skaliranjeFaktor != 1)
            //{
            //    float centarDrzaca = 10f / 2f;
            //    Console.WriteLine(centarDrzaca);
            //    float NoviCentar = (skaliranjeFaktor * 10f )/ 2f;
            //    Console.WriteLine(NoviCentar);
            //    float pomerajBuretaNakonSkaliranja = centarDrzaca - NoviCentar;
            //    Console.WriteLine(pomerajBuretaNakonSkaliranja);
            //    gl.Translate(0f, 0f, pomerajBuretaNakonSkaliranja);
            //}

            gl.Rotate(0f, 0f, -90f);
            gl.Rotate(90f, 0f, 0f);



            m_scene.Draw();
            gl.PopMatrix();
        
       
            gl.PushMatrix(); 
           
                // (clipping) prozora u fizički prozor
                //aplikacije – mapiranje logičkog koordinatnog sistema u fizički
                gl.Viewport(m_width/2, m_height/2 ,m_width/2 , m_height/2 );  //X,Y,W,H
               
                gl.MatrixMode(OpenGL.GL_PROJECTION); //matrica definiše vidljivi volumen, kao i tip projekcije scene na ekran (ortogonalna ili perspektiva)
                gl.LoadIdentity();
                gl.Ortho2D(-15, 15, -15, 15); //left, right – min/maks po x-osi, bottom, top – min / maks po y-osi,definiše „2D“ projekciju.

                gl.MatrixMode(OpenGL.GL_MODELVIEW); //Izbor matrice nad kojom se vrše transformacije
                gl.LoadIdentity();
                gl.Color(1f, 1f, 0f); 
            
                    
                    gl.PushMatrix();
                    gl.Translate(3f, -10f, 0f);
                    gl.DrawText3D("Arial", 14f, 1f, 0.1f, "Predmet: Racunarska grafika");
                    gl.PopMatrix();
                 
                    gl.PushMatrix();
                    gl.Translate(3f, -11f, 0f);
                    gl.DrawText3D("Arial", 14f, 1f, 0.1f, "Sk.god: 2019/20.");
                    gl.PopMatrix();
                    
                    gl.PushMatrix();
                    gl.Translate(3f, -12f, 0f);
                    gl.DrawText3D("Arial", 14f, 1f, 0.1f, "Ime: Tamara");
                    gl.PopMatrix();
                    
                    gl.PushMatrix();
                    gl.Translate(3f, -13f, 0f);
                    gl.DrawText3D("Arial", 14f, 1f, 0.1f, "Prezime: Lazarevic");
                    gl.PopMatrix();
                    
                    gl.PushMatrix();
                    gl.Translate(3f, -14f, 0f);
                    gl.DrawText3D("Arial", 14f, 1f, 0.1f, "Sifra zad: 13.2");
                    gl.PopMatrix();
                    
            
            gl.PopMatrix();

            gl.PopMatrix();


            gl.PopMatrix();
            gl.Viewport(0, 0, m_width, m_height);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(60f, (double)m_width / m_height, 1f, 20000f);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();


            // Oznaci kraj iscrtavanja
            gl.Flush();
        }


        /// <summary>
        /// Podesava viewport i projekciju za OpenGL kontrolu.
        /// </summary>
        public void Resize(OpenGL gl, int width, int height)
        {
            m_width = width;
            m_height = height;
            gl.Viewport(0, 0, m_width, m_height);
            gl.MatrixMode(OpenGL.GL_PROJECTION);      // selektuj Projection Matrix
            gl.LoadIdentity();
            gl.Perspective(60f, (double)width / height, 1f, 20000f);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();                // resetuj ModelView Matrix
        }

        public void PokreniAnimaciju()
        {

            //kotrljanje = true;
            animacija = true;
            rotiranjeDrzaca = true;
            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(100);
            timer1.Tick += new EventHandler(SledeciKorakAnimacije);
            timer1.Start();

        }

        public void SledeciKorakAnimacije(object sender, EventArgs e)
        {
            if (animacija)
            {
                if (angle < 50)
                {
                    rotiranjeDrzaca = true;
                    angle += 20;
                    blagoPodizanjeDrzaca += 2;
                    blagoPodizanjeBureta += 2;
                }
                else
                {
                    if (pomeraj > 45)
                    {
                        ugaoKotrljanja = 0;
                        propadanje = true;
                        spustanje -= 10f;
                        if (spustanje <= -40)
                        {
                            timer1.Stop();
                            animacija = false;
                            rotiranjeDrzaca = false;
                            kotrljanje = false;
                            propadanje = false;
                            pomeraj = 0f;
                            angle = 0f;
                            spustanje = 0f;
                            RotationY += 90;
                            blagoPodizanjeBureta = 0f;
                            blagoPodizanjeDrzaca = 0f;
                        }
                    }
                    else
                    {
                        blagoPodizanjeBureta = 0f;
                        kotrljanje = true;
                        pomeraj += 5;
                        ugaoKotrljanja += -45;
                    }
                }
            }
        }


        /// <summary>
        ///  Implementacija IDisposable interfejsa.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_scene.Dispose();
            }
        }

        #endregion Metode

        #region IDisposable metode

        /// <summary>
        ///  Dispose metoda.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable metode
        public bool getAnimacija()
        {
            return animacija;
        }

        public void setAnimacija(bool a)
        {
            this.animacija = a;
        }

        public void setSkaliranjeFaktor(float f)
        {
            this.skaliranjeFaktor = f;
        }

        public void setRValue(float f)
        {
            this.Rvalue = f;
        }
        public void setGValue(float f)
        {
            this.Gvalue = f;
        }
        public void setBValue(float f)
        {
            this.Bvalue = f;
        }
        public void setPolozajX(float f)
        {
            this.polozajX = f;
        }
        public float getPolozajX()
        {
            return this.polozajX;
        }
    }
}
