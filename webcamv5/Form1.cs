using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
using System.Runtime;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Speech.Synthesis;

namespace webcamv5
{
    public partial class Form1 : Form
    {
        int fram = 0;
        private FilterInfoCollection videoDevices; // Liste des périphériques vidéo disponibles
        private VideoCaptureDevice videoSource; // Périphérique vidéo sélectionné
        private Bitmap image1; // Déclaration de l'image1
        private Bitmap image2;

        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialiser la collection de périphériques vidéo disponibles
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0) // Vérifier s'il y a des périphériques vidéo disponibles
            {
                // Sélectionner le premier périphérique vidéo trouvé
                videoSource = new VideoCaptureDevice(videoDevices[1].MonikerString);

                // Définir directement la résolution souhaitée (par exemple, 1280x720)
                SetResolution(videoSource, 640, 480);

                // Abonner à l'événement NewFrame qui se déclenche à chaque nouvelle frame capturée
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);

                // Démarrer la capture vidéo
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("No video sources found");
            }
        }

        private void SetResolution(VideoCaptureDevice videoSource, int width, int height)
        {
            foreach (var capability in videoSource.VideoCapabilities)
            {
                if (capability.FrameSize.Width == width && capability.FrameSize.Height == height)
                {
                    videoSource.VideoResolution = capability;
                    return;
                }
            }

            MessageBox.Show($"La résolution {width}x{height} n'est pas disponible. Utilisation de la résolution par défaut.");
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            fram++;

            if (fram == 30)
            {
                // Cloner l'image capturée pour la traiter
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                // Convertir l'image en termes hexadécimaux
                var (therme, largeur, hauteur) = ImageEnTermesHex(bitmap);
                // Analyser les valeurs hexadécimales
                AnalyserHexValues(therme, largeur, hauteur);
                fram = 0;
            }
        }

        private void AnalyserHexValues(string[,] therme, int largeur, int hauteur)
        {
            int x = 0;
            int y = 0;
            string hex = "";
            int rouge = 0;
            int rhx = 0;
            int rx = 0;
            int R = 0;
            int V = 0;
            int B = 0;
            int verif = 0;
            int validation = 0;
            int rougehautx = 0;
            int rougehauty = 0;
            int rougeinterbasy = 0;
            int rougeinterhauty = 0;
            int milieu = 0;
            int rougeintergeuche = 0;
            int rougeinterdroite = 0;
            int rond = 0;
            int rougenul = 0;
            int noir = 0;
            int milieux = 0;
            int poucentage = 0;
            for (int t = 0; t < 32; t++)
            {
                //Créer une nouvelle instance de Bitmap pour l'image modifiée
                image1 = new Bitmap(320, 480);

                string textBoxName = "TBcases" + (t + 1); // Exemple : "TBcases1", "TBcases2", ...

                // Vérifier si le contrôle existe dans les Controls du formulaire
                if (this.Controls.ContainsKey(textBoxName))
                {
                    // Récupérer le contrôle TextBox correspondant
                    System.Windows.Forms.TextBox targetTextBox = this.Controls[textBoxName] as System.Windows.Forms.TextBox;

                    // Vérifier si Invoke est nécessaire pour accéder au contrôle depuis un autre thread
                    if (targetTextBox.InvokeRequired)
                    {
                        targetTextBox.BeginInvoke(new Action(() =>
                        {
                            targetTextBox.Text = poucentage.ToString();
                        }));
                    }
                    else
                    {
                        targetTextBox.Text = poucentage.ToString();
                    }
                }
            }


            //afficher limage
            for (y = 0; y < hauteur; y++)
            {
                rougenul = 0;
                rouge = 0;
                for (x = 0; x < largeur - 1; x++)
                {

                    hex = therme[x, y];
                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                    // Rouge clair
                    if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50)
                    {
                        rouge++;
                    }

                    else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 100)
                    {
                        rouge++;
                    }

                    else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 100)
                    {
                        rouge++;
                    }

                    // Rouge orangé
                    else if (R >= 180 && V >= 50 && V <= 100 && B >= 0 && B <= 50)
                    {
                        rouge++;
                    }

                    // Rouge brique
                    else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100)
                    {
                        rouge++;
                    }

                    // Rouge rosé
                    else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150)
                    {
                        rouge++;
                    }

                    // Rouge pourpre
                    else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150)
                    {
                        rouge++;
                    }

                    // Rouge violacé
                    else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170)
                    {
                        rouge++;
                    }

                    // Rouge intense
                    else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30)
                    {
                        rouge++;
                    }

                    // Rouge classique
                    else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95)
                    {
                        rouge++;
                    }

                    // Rouge feu
                    else if (R >= 250 && V >= 60 && V <= 90 && B >= 0 && B <= 30)
                    {
                        rouge++;
                    }
                    else if (R >= 125 && R <= 200 && V >= 5 && V <= 40 && B >= 30 && B <= 65)
                    {
                        rouge++;
                    }

                    else
                    {
                        rougenul++;
                        //R = 0;
                        //V = 0;
                        //B = 0;

                    }

                    //verifier ligne de rouge
                    if (rouge < 2 && rougenul > 0)
                    {
                        rouge = 0;
                        rougenul = 0;
                    }

                    // si il y a une ligne de rouge
                    if (rouge > 1 && rougenul > 0)
                    {
                        rouge = rouge / 2;

                        //verifier si on a deja le panneau
                        if (verif == 0)
                        {
                            rx = 0;
                            rougehauty = y;
                            rougehautx = 0;

                            //trouver le milieux de la ligne rouge
                            while (rouge != 0 && rx <= largeur - 1)
                            {
                                hex = therme[rx, y];
                                R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                rx++;
                                rougehautx++;

                                // Rouge clair
                                if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50)
                                {
                                    rouge--;
                                }

                                else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 100)
                                {
                                    rouge--;
                                }

                                else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 100)
                                {
                                    rouge--;
                                }

                                // Rouge orangé
                                else if (R >= 180 && V >= 50 && V <= 100 && B >= 0 && B <= 50)
                                {
                                    rouge--;
                                }

                                // Rouge brique
                                else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100)
                                {
                                    rouge--;
                                }

                                // Rouge rosé
                                else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150)
                                {
                                    rouge--;
                                }

                                // Rouge pourpre
                                else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150)
                                {
                                    rouge--;
                                }

                                // Rouge violacé
                                else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170)
                                {
                                    rouge--;
                                }

                                // Rouge intense
                                else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30)
                                {
                                    rouge--;
                                }

                                // Rouge classique
                                else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95)
                                {
                                    rouge--;
                                }

                                // Rouge feu
                                else if (R >= 250 && V >= 60 && V <= 90 && B >= 0 && B <= 30)
                                {
                                    rouge--;
                                }
                                else if (R >= 125 && R <= 200 && V >= 5 && V <= 40 && B >= 30 && B <= 65)
                                {
                                    rouge--;
                                }
                            }

                            //etre sur daoir les deux extremiter du panneau
                            while (validation < 2)
                            {
                                rougehauty++;

                                // verif si on est pas rn bas du tableau
                                if (rougehauty >= hauteur - 1 && validation < 2)
                                {
                                    validation = 4;
                                    rouge = 0;
                                    rougenul = 0;
                                    rougehautx = 0;
                                    verif = 0;
                                }
                                else
                                {
                                    rouge = 0;

                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                }

                                // Rouge clair
                                if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge foncé
                                else if (R >= 80 && R <= 190 && V >= 0 && V <= 110 && B >= 0 && B <= 125 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 80 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge orangé
                                else if (R >= 180 && V >= 50 && V <= 100 && B >= 0 && B <= 50 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge brique
                                else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge rosé
                                else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge pourpre
                                else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge violacé
                                else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge intense
                                else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge classique
                                else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 100 && V > 70 && B > 80)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge feu
                                else if (R >= 250 && V >= 60 && V <= 90 && B >= 0 && B <= 30 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                else if (R >= 125 && R <= 200 && V >= 5 && V <= 40 && B >= 30 && B <= 65 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                    else { rougehauty--; }
                                }

                                // Rouge clair
                                if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                }
                                else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 80 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                }
                                // Rouge foncé
                                else if (R >= 85 && R <= 150 && V >= 0 && V <= 75 && B >= 0 && B <= 70 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;

                                }

                                // Rouge orangé
                                else if (R >= 180 && V >= 50 && V <= 100 && B >= 0 && B <= 50 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;

                                }

                                // Rouge brique
                                else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                }

                                // Rouge rosé
                                else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;

                                }

                                // Rouge pourpre
                                else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                }

                                // Rouge violacé
                                else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                }

                                // Rouge intense
                                else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                }

                                // Rouge classique
                                else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                }

                                // Rouge feu
                                else if (R >= 250 && V >= 60 && V <= 90 && B >= 0 && B <= 30 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;

                                }

                                else if (R >= 125 && R <= 200 && V >= 5 && V <= 40 && B >= 30 && B <= 65 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                }
                            }
                        }

                        if (validation == 2 && rougehautx > 0 && rougehautx < largeur - 1)
                        {
                            validation = 0;
                            milieux = rougeinterhauty + ((rougeinterbasy - rougeinterhauty) / 2);
                            rougeintergeuche = rougehautx;
                            rougeinterdroite = rougehautx;

                            while (validation != 1)
                            {
                                rougeintergeuche--;

                                if (rougeintergeuche <= 1 || rougeintergeuche >= largeur - 2 || milieux < 1 || milieux > hauteur - 1)
                                {
                                    validation = 1;
                                    verif = 0;
                                }
                                else if (rougeintergeuche > 0 && rougeintergeuche < largeur)
                                {
                                    hex = therme[rougeintergeuche, milieux];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                    // Rouge clair
                                    if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50)
                                    {
                                        validation = 1;
                                    }

                                    // Rouge foncé
                                    else if (R >= 85 && R <= 150 && V >= 0 && V <= 75 && B >= 0 && B <= 70)
                                    {
                                        validation = 1;
                                    }

                                    // Rouge orangé
                                    else if (R >= 180 && V >= 50 && V <= 100 && B >= 0 && B <= 50)
                                    {
                                        validation = 1;
                                    }

                                    // Rouge brique
                                    else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100)
                                    {
                                        validation = 1;
                                    }

                                    // Rouge rosé
                                    else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150)
                                    {
                                        validation = 1;
                                    }

                                    // Rouge pourpre
                                    else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150)
                                    {
                                        validation = 1;
                                    }

                                    // Rouge violacé
                                    else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170)
                                    {
                                        validation = 1;
                                    }

                                    // Rouge intense
                                    else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30)
                                    {
                                        validation = 1;
                                    }

                                    // Rouge classique
                                    else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95)
                                    {
                                        validation = 1;
                                    }
                                }

                            }
                            while (validation < 2)
                            {
                                rougeinterdroite++;
                                if (rougeinterdroite >= largeur - 2 || rougeinterdroite < 1)
                                {
                                    validation = 2;
                                }
                                else
                                {
                                    hex = therme[rougeinterdroite, milieux];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    // Rouge clair
                                    if (R >= 200 && V >= 35 && V <= 70 && B >= 0 && B <= 60)
                                    {
                                        validation++;
                                        verif++;
                                    }

                                    // Rouge foncé
                                    else if (R >= 85 && R <= 150 && V >= 0 && V <= 75 && B >= 0 && B <= 70)
                                    {
                                        validation++;
                                        verif++;
                                    }

                                    // Rouge orangé
                                    else if (R >= 95 && V >= 50 && V <= 100 && B >= 0 && B <= 60)
                                    {
                                        validation++;
                                        verif++;
                                    }

                                    // Rouge brique
                                    else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100)
                                    {
                                        validation++;
                                        verif++;
                                    }

                                    // Rouge rosé
                                    else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150)
                                    {
                                        validation++;
                                        verif++;
                                    }

                                    // Rouge pourpre
                                    else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150)
                                    {
                                        validation++;
                                        verif++;
                                    }

                                    // Rouge violacé
                                    else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170)
                                    {
                                        validation++;
                                        verif++;
                                    }

                                    // Rouge intense
                                    else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30)
                                    {
                                        validation++;
                                        verif++;
                                    }

                                    // Rouge classique
                                    else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95)
                                    {
                                        validation++;
                                        verif++;
                                    }
                                }
                            }


                        }
                    }
                    System.Drawing.Color newColor = System.Drawing.Color.FromArgb(255, R, V, B);
                    image1.SetPixel(x, y, newColor);
                }

            }

            if (verif == 0)
            {
                validation = 0;
            }

            if (validation == 2)
            {
                largeur = rougeinterdroite - rougeintergeuche;
                hauteur = rougeinterbasy - rougeinterhauty;
                // Bitmap image2 = new Bitmap(largeur, hauteur);
                image2 = new Bitmap(320, 480);

                string[,] panneau = new string[largeur, hauteur];
                x = 0; y = 0;
                for (int py = rougeinterhauty; py < rougeinterbasy; py++)
                {

                    for (int px = rougeintergeuche; px < rougeinterdroite; px++)
                    {

                        hex = therme[px, py];
                        panneau[x, y] = hex;
                        x++;
                    }
                    y++;
                    x = 0;
                }

                for (int pyy = 0; pyy < hauteur; pyy++)
                {
                    for (int pxx = 0; pxx < largeur; pxx++)
                    {
                        hex = panneau[pxx, pyy];
                        R = Convert.ToInt32(hex.Substring(0, 2), 16);
                        V = Convert.ToInt32(hex.Substring(2, 2), 16);
                        B = Convert.ToInt32(hex.Substring(4, 2), 16);
                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(1, R, V, B);
                        image2.SetPixel(pxx, pyy, newColor);
                    }
                }

                int interlarg = 0;
                int interhaut = 0;

                for (int t = 0; t < 32; t++)
                {
                    noir = 0;
                    int h = (hauteur / 8) * (2 + interhaut);
                    int h2 = (hauteur / 8) * (3 + interhaut);
                    int l = (largeur / 8) * interlarg;
                    int l2 = (largeur / 8) * (1 + interlarg);

                    for (int pyy = h; pyy < h2; pyy++)
                    {
                        for (int pxx = l; pxx < l2; pxx++)
                        {
                            hex = panneau[pxx, pyy];
                            R = Convert.ToInt32(hex.Substring(0, 2), 16);
                            V = Convert.ToInt32(hex.Substring(2, 2), 16);
                            B = Convert.ToInt32(hex.Substring(4, 2), 16);

                            if (R < 60 && V < 60 && B < 60)
                            {
                                noir++;
                            }
                        }
                    }

                    interhaut++;
                    if (interhaut > 3)
                    {
                        interhaut = 0;
                        interlarg++;
                    }

                    poucentage = 0;
                    if (hauteur > 0 && largeur > 0 && noir > 0)
                    {
                        poucentage = (noir * 100) / ((hauteur / 8) * (largeur / 8));
                    }



                     string textBoxName = "TBcases" + (t + 1); // Exemple : "TBcases1", "TBcases2", ...

                    // Vérifier si le contrôle existe dans les Controls du formulaire
                    if (this.Controls.ContainsKey(textBoxName))
                    {
                        // Récupérer le contrôle TextBox correspondant
                        System.Windows.Forms.TextBox targetTextBox = this.Controls[textBoxName] as System.Windows.Forms.TextBox;

                        // Vérifier si Invoke est nécessaire pour accéder au contrôle depuis un autre thread
                        if (targetTextBox.InvokeRequired)
                        {
                            targetTextBox.BeginInvoke(new Action(() =>
                            {
                                targetTextBox.Text = poucentage.ToString();
                            }));
                        }
                        else
                        {
                            targetTextBox.Text = poucentage.ToString();
                        }
                    }
                }



                for (int coupe = 1; coupe < 5; coupe++)
                {

                    for (y = 0; y < hauteur; y++)
                    {
                        System.Drawing.Color colorbalckhaut = System.Drawing.Color.FromArgb(1, 0, 0, 0);
                        image2.SetPixel((largeur / 4) * coupe, y, colorbalckhaut);
                    }
                    for (x = 0; x < largeur; x++)
                    {
                        System.Drawing.Color colorbalckhaut = System.Drawing.Color.FromArgb(1, 0, 0, 0);
                        image2.SetPixel(x, (hauteur / 4) * coupe, colorbalckhaut);
                    }
                }

                 pictureBox2.Image = image2;

                if (validation == 2)
                {
                    System.Drawing.Color newColor1 = System.Drawing.Color.FromArgb(1, 0, 0, 255);
                    image1.SetPixel(rougeintergeuche, milieux, newColor1);
                    image1.SetPixel(rougeinterdroite, milieux, newColor1);
                }

                System.Drawing.Color newColor2 = System.Drawing.Color.FromArgb(1, 0, 0, 255);
                image1.SetPixel(rougehautx, milieux, newColor2);
                image1.SetPixel(rougehautx, rougeinterhauty, newColor2);
                image1.SetPixel(rougehautx, rougeinterbasy, newColor2);

                if (int.Parse(TBcases1.Text) == int.Parse(TBcases5.Text) && int.Parse(TBcases9.Text) == int.Parse(TBcases13.Text))
                {
                    label1.Text = "";
                }
                else if (int.Parse(TBcases7.Text) < 10 && int.Parse(TBcases11.Text) < int.Parse(TBcases10.Text) && int.Parse(TBcases14.Text) < 14)
                {
                    label1.Text = "50 Kilomètre  heure";
                }
                else if (int.Parse(TBcases9.Text) > int.Parse(TBcases10.Text) && int.Parse(TBcases11.Text) < int.Parse(TBcases12.Text) && int.Parse(TBcases7.Text) > int.Parse(TBcases11.Text))
                {
                    label1.Text = "80 Kilomètre  heure";
                }
                else if (int.Parse(TBcases13.Text) > int.Parse(TBcases14.Text) && int.Parse(TBcases16.Text) > int.Parse(TBcases15.Text))
                {
                    label1.Text = "130 Kilomètre  heure";
                }
                else if (int.Parse(TBcases13.Text) > int.Parse(TBcases17.Text) && int.Parse(TBcases14.Text) > int.Parse(TBcases18.Text) && int.Parse(TBcases15.Text) > int.Parse(TBcases19.Text) && int.Parse(TBcases16.Text) > int.Parse(TBcases20.Text) && int.Parse(TBcases5.Text) > int.Parse(TBcases9.Text))
                {
                    label1.Text = "110 Kilomètre  heure";
                }
                else if (int.Parse(TBcases10.Text) < 10 && int.Parse(TBcases13.Text) < int.Parse(TBcases14.Text) && int.Parse(TBcases10.Text) < int.Parse(TBcases6.Text))
                {
                    label1.Text = "90 Kilomètre  heure";
                }
                else if (int.Parse(TBcases7.Text) < 10 && int.Parse(TBcases6.Text) < 10 && int.Parse(TBcases15.Text) < 15 && int.Parse(TBcases16.Text) < 15)
                {
                    label1.Text = "70 Kilomètre  heure";
                }
                else if (int.Parse(TBcases7.Text) < 10 && int.Parse(TBcases6.Text) < 10)
                {
                    label1.Text = "30 Kilomètre  heure";
                }


                if (label1.Text != "")
                {
                    SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                    synthesizer.Volume = 100;  // 0...100
                    synthesizer.Rate = -2;     // -10...10

                    // Synchronous
                    synthesizer.Speak(label1.Text);

                    // Asynchronous
                    synthesizer.SpeakAsync(label1.Text);

                }
            }
            pictureBox1.Image = image1;

        }

        private (string[,], int, int) ImageEnTermesHex(Bitmap bitmap)
        {
            int largeur = bitmap.Width / 2;
            int hauteur = bitmap.Height;
            string[,] therme = new string[largeur, hauteur];

            // Parcourir chaque pixel de l'image
            for (int y = 0; y < hauteur; y++)
            {
                for (int x = largeur; x < bitmap.Width - 1; x++)
                {
                    Color couleurPixel = bitmap.GetPixel(x, y);
                    string hexValue = $"{couleurPixel.R:X2}{couleurPixel.G:X2}{couleurPixel.B:X2}";
                    therme[x - largeur, y] = hexValue;
                }
            }

            return (therme, largeur, hauteur);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Arrêter la capture vidéo proprement lors de la fermeture du formulaire
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

    }
}
