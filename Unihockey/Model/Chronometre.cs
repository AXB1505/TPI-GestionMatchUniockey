using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unihockey.Model
{
    internal class Chronometre
    {
        // Constante pour le temps de pause
        private const int MINUTES_PAUSE = 5;


        // Variables à spécifier
        public TimeSpan dureePeriode;
        private int nombrePeriode;
        Stopwatch chrono = new Stopwatch();
        bool estCroissant = false;

        // Variables à obtenir et afficher
        private TimeSpan dureeActuel;
        private TimeSpan tempsRestant;
        private string temps = "";

        // Constructeurs par défaut
        public Chronometre()
        {

        }

        // Constructeur avec paramètres
        public Chronometre(int periodeMinutes, int periodeSecondes, int periode)
        {
            dureePeriode = new TimeSpan(0, periodeMinutes, periodeSecondes);
            nombrePeriode = periode;
            dureeActuel = dureePeriode;
        }

        public int getNombrePeriode()
        {
            return nombrePeriode;
        }
        public int getMinutesPeriode()
        {
            return dureePeriode.Minutes;
        }

        // Méthodes pour la récupération du status du chrono
        public bool getStatus()
        {
            return chrono.IsRunning;
        }

        // Méthodes pour la fixation du mode de décompte
        public void setCroissant(bool estCroissant)
        {
            this.estCroissant = estCroissant;
        }

        // Méthodes pour la récupération de la duree de la période
        public string getTempsReset()
        {
            if (estCroissant)
            {
                return "00:00";
            }
            else
            {
                return dureeActuel.ToString(@"mm\:ss");
            }
        }

        // Méthodes pour la récupération du temps restant
        public string GetTempsRestant()
        {
            if (estCroissant)
            {
                tempsRestant = chrono.Elapsed;
            }
            else
            {
                tempsRestant = dureeActuel - chrono.Elapsed;
            }

            // Verification si le temps est écoulé (check si tempsRestant est plus grande que dureeActuel et non égal car manque de précision au millième de secondes)
            if (chrono.Elapsed >= dureeActuel)
            {
                chrono.Reset();
                if (nombrePeriode == 2)
                {
                    nombrePeriode = 0;
                    dureeActuel = new TimeSpan(0, 0, MINUTES_PAUSE);
                }
                else if (nombrePeriode == 0)
                {
                    nombrePeriode = 1;
                    dureeActuel = dureePeriode;
                }

                return getTempsReset();
            }

            temps = tempsRestant.ToString(@"mm\:ss");

            return temps;
        }

        // Méthodes pour le démarrage du chrono
        public void Start()
        {
            if (chrono.IsRunning)
            {
                return;
            }

            chrono.Start();
        }

        // Méthodes pour l'arrêt du chrono
        public void Pause()
        {
            if (chrono.IsRunning)
            {
                chrono.Stop();
            }
            return;
        }
    }
}
