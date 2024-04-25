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

        // Chronomètre utilisé pour mesurer le temps
        private Stopwatch chrono = new Stopwatch();

        // Variables à spécifier dans le constructeur

        // Durée de chaque période de jeu
        private TimeSpan dureePeriode;
        // Nombre total de périodes
        private int nombrePeriode;
        // Indiquation si le compteur est croissant ou décroissant
        private bool estCroissant = false;

        // Variables à obtenir et afficher

        // Durée actuelle du chronomètre pour la période actuelle ou la pause
        private TimeSpan dureeActuel;
        // Temps restant du chronomètre
        private TimeSpan tempsRestant;
        // Indique si le chronomètre est fini
        private bool estFini = false;

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

        // Méthodes pour le démarrage du chrononomètre
        public void Start()
        {
            if (chrono.IsRunning == false)
            {
                chrono.Start();
            }
            return;

        }

        // Méthodes pour mettre en pause le chrononomètre
        public void Pause()
        {
            if (chrono.IsRunning)
            {
                chrono.Stop();
            }
            return;
        }

        // Méthodes pour réinitialiser le chrononomètre
        public void Stop()
        {
            chrono.Reset();
        }

        // Méthode pour définir si le compteur est croissant ou non
        public void setCroissant(bool estCroissant)
        {
            this.estCroissant = estCroissant;
        }

        // Méthode pour obtenir le nombre de périodes
        public int getNombrePeriode()
        {
            return nombrePeriode;
        }

        // Méthode pour obtenir la durée en minutes de chaque période
        public int getMinutesPeriode()
        {
            return dureePeriode.Minutes;
        }

        // Méthode pour obtenir le statut du chronomètre (en cours ou non)
        public bool getStatus()
        {
            return chrono.IsRunning;
        }

        // Méthode pour obtenir le temps formatté après un reset
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

        // Méthode pour savoir si le chronomètre a fini
        public bool getEstFini()
        {
            return estFini;
        }

        // Méthodes pour la récupération du temps restant en string
        public string GetTempsRestant()
        {
            // Vérification que le chronomètre est croissant ou non et calcule le temps restant en conséquence
            if (estCroissant)
            {
                tempsRestant = chrono.Elapsed;
            }
            else
            {
                tempsRestant = dureeActuel - chrono.Elapsed;
            }

            // Verification que le temps est écoulé
            if (chrono.Elapsed >= dureeActuel)
            {
                // Si le temps est écoulé, on arrête le chronomètre et on le réinitialise
                chrono.Reset();
                // Verification qu'il y ait 2 périodes
                if (nombrePeriode == 2)
                {
                    // Si il y a 2 périodes le nombre de périodes passe à 0 (pour la pause)
                    nombrePeriode = 0;
                    // Fixation de la durée actuelle à la durée de la pause
                    dureeActuel = new TimeSpan(0, MINUTES_PAUSE, 0);
                }
                // Verification si on est en pause
                else if (nombrePeriode == 0)
                {
                    // Si il y a 0 périodes le nombre de périodes passe à 1 (pour la dernière période)
                    nombrePeriode = 1;
                    // Fixation de la durée actuelle à la durée de la période
                    dureeActuel = dureePeriode;
                }
                // Verification si on est à la dernière période
                else if (nombrePeriode == 1)
                {
                    // Si il y a 1 période le chronomètre est fini (pour le chronomètre principal)
                    estFini = true;
                }
                // Retour du temps restant réinitialiser
                return getTempsReset();
            }

            // Retour du temps restant
            return tempsRestant.ToString(@"mm\:ss");
        }
    }
}
