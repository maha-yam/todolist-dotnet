using System;
using System.Collections.Generic;

class ToDoListApp
{
    // Classe pour représenter une tâche
    class Tache
    {
        public string Description { get; set; }
        public bool EstTerminee { get; set; }

        public Tache(string description)
        {
            Description = description;
            EstTerminee = false;
        }

        public override string ToString()
        {
            string statut = EstTerminee ? "[✓]" : "[ ]";
            return $"{statut} {Description}";
        }
    }

    // Liste globale des tâches
    static List<Tache> listeTaches = new List<Tache>();

    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║   APPLICATION DE GESTION DE TÂCHES    ║");
        Console.WriteLine("║          (To-Do List)                  ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        bool continuer = true;

        while (continuer)
        {
            AfficherMenu();
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    AjouterTache();
                    break;
                case "2":
                    AfficherListeTaches();
                    break;
                case "3":
                    MarquerTacheTerminee();
                    break;
                case "4":
                    SupprimerTache();
                    break;
                case "5":
                    continuer = false;
                    Console.WriteLine("\n✓ Merci d'avoir utilisé l'application !");
                    Console.WriteLine("À bientôt ! 👋\n");
                    break;
                default:
                    Console.WriteLine("\n Option invalide ! Veuillez choisir entre 1 et 5.\n");
                    break;
            }

            if (continuer && choix != "2")
            {
                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

   

    static void AfficherMenu()
    {
        Console.WriteLine("┌────────────────────────────────────────┐");
        Console.WriteLine("│           MENU PRINCIPAL               │");
        Console.WriteLine("├────────────────────────────────────────┤");
        Console.WriteLine("│  1.     Ajouter une nouvelle tâche     │");
        Console.WriteLine("│  2.     Afficher la liste des tâches   │");
        Console.WriteLine("│  3.     Marquer une tâche comme finie  │");
        Console.WriteLine("│  4.     Supprimer une tâche            │");
        Console.WriteLine("│  5.     Quitter l'application          │");
        Console.WriteLine("└────────────────────────────────────────┘");
        Console.Write("\n Votre choix : ");
    }


    static void AjouterTache()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║      AJOUTER UNE NOUVELLE TÂCHE     ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        Console.Write(" Entrez la description de la tâche : ");
        string description = Console.ReadLine();

        // Validation : vérifier que la description n'est pas vide
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("\n Erreur : La description ne peut pas être vide !");
            return;
        }

        // Créer et ajouter la nouvelle tâche
        Tache nouvelleTache = new Tache(description);
        listeTaches.Add(nouvelleTache);

        Console.WriteLine($"\n Tâche ajoutée avec succès !");
        Console.WriteLine($" Nombre total de tâches : {listeTaches.Count}");
    }


    static void AfficherListeTaches()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║        LISTE DES TÂCHES             ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        if (listeTaches.Count == 0)
        {
            Console.WriteLine(" Aucune tâche dans la liste.");
            Console.WriteLine(" Astuce : Utilisez l'option 1 pour ajouter une tâche !\n");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        // Afficher toutes les tâches avec leur numéro
        for (int i = 0; i < listeTaches.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {listeTaches[i]}");
        }

        // Statistiques
        int tachesTerminees = CompterTachesTerminees();
        int tachesEnCours = listeTaches.Count - tachesTerminees;

        Console.WriteLine("\n" + new string('─', 40));
        Console.WriteLine($" Total : {listeTaches.Count} tâche(s)");
        Console.WriteLine($"  Terminées : {tachesTerminees}");
        Console.WriteLine($" En cours : {tachesEnCours}");
        Console.WriteLine(new string('─', 40) + "\n");

        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
        Console.Clear();
    }


    static void MarquerTacheTerminee()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║    MARQUER UNE TÂCHE COMME TERMINÉE  ");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        if (listeTaches.Count == 0)
        {
            Console.WriteLine(" Aucune tâche disponible !");
            return;
        }

        // Afficher les tâches non terminées
        Console.WriteLine("Tâches disponibles :\n");
        bool aTachesNonTerminees = false;

        for (int i = 0; i < listeTaches.Count; i++)
        {
            if (!listeTaches[i].EstTerminee)
            {
                Console.WriteLine($"{i + 1}. {listeTaches[i]}");
                aTachesNonTerminees = true;
            }
        }

        if (!aTachesNonTerminees)
        {
            Console.WriteLine(" Toutes les tâches sont déjà terminées ! Bravo ! 🎉");
            return;
        }

        Console.Write("\n Entrez le numéro de la tâche à marquer comme terminée : ");
        string saisie = Console.ReadLine();

        // Validation de la saisie
        if (!int.TryParse(saisie, out int numero) || numero < 1 || numero > listeTaches.Count)
        {
            Console.WriteLine("\n Numéro de tâche invalide !");
            return;
        }

        // Vérifier si la tâche est déjà terminée
        if (listeTaches[numero - 1].EstTerminee)
        {
            Console.WriteLine("\n Cette tâche est déjà marquée comme terminée !");
            return;
        }

        //  la tâche  terminée
        listeTaches[numero - 1].EstTerminee = true;
        Console.WriteLine($"\n Tâche #{numero} marquée comme terminée !");
        Console.WriteLine($" {listeTaches[numero - 1].Description}");
    }


    static void SupprimerTache()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║        SUPPRIMER UNE TÂCHE          ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        if (listeTaches.Count == 0)
        {
            Console.WriteLine(" Aucune tâche à supprimer !");
            return;
        }

        // Afficher toutes les tâches
        Console.WriteLine("Liste des tâches :\n");
        for (int i = 0; i < listeTaches.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {listeTaches[i]}");
        }

        Console.Write("\n Entrez le numéro de la tâche à supprimer : ");
        string saisie = Console.ReadLine();

        // Validation de la saisie
        if (!int.TryParse(saisie, out int numero) || numero < 1 || numero > listeTaches.Count)
        {
            Console.WriteLine("\n Numéro de tâche invalide !");
            return;
        }

 
        string tacheASupprimer = listeTaches[numero - 1].Description;
        Console.Write($"\n⚠️  Êtes-vous sûr de vouloir supprimer cette tâche ?\n   '{tacheASupprimer}'\n   (O/N) : ");
        string confirmation = Console.ReadLine().ToUpper();

        if (confirmation == "O" || confirmation == "OUI")
        {
            listeTaches.RemoveAt(numero - 1);
            Console.WriteLine("\n Tâche supprimée avec succès !");
            Console.WriteLine($" Nombre de tâches restantes : {listeTaches.Count}");
        }
        else
        {
            Console.WriteLine("\n Suppression annulée.");
        }
    }


    static int CompterTachesTerminees()
    {
        int compteur = 0;
        foreach (Tache tache in listeTaches)
        {
            if (tache.EstTerminee)
            {
                compteur++;
            }
        }
        return compteur;
    }
}