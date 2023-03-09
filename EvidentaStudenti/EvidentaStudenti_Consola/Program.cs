using System;
using System.Configuration;
using LibrarieModele;
using NivelStocareDate;

namespace EvidentaStudenti_Consola
{
    class Program
    {
        static void Main()
        {
            Student student = new Student();
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            AdministrareStudenti_FisierText adminStudenti = new AdministrareStudenti_FisierText(numeFisier);
            int nrStudenti = 0;

            string optiune;
            do
            {
                Console.WriteLine("I. Introducere informatii student");
                Console.WriteLine("A. Afisare studenti");
                Console.WriteLine("F. Afisare studenti din fisier");
                Console.WriteLine("S. Salvare student in fisier");
                Console.WriteLine("X. Inchidere program");
                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine();
                switch (optiune.ToUpper())
                {
                    case "I":
                        int idStudent = nrStudenti + 1;

                        Console.WriteLine("Introdu numele studentului {0} : ", idStudent);
                        string nume = Console.ReadLine();
                        Console.WriteLine("Introdu prenumele studentului {0} : ", idStudent);
                        string prenume = Console.ReadLine();
                        student = new Student(idStudent, nume, prenume);
                        nrStudenti++;

                        break;
                    case "A":
                        string infoStudent = student.Info();
                        Console.WriteLine("Studentul {0}", infoStudent);

                        break;
                    case "F":
                        Student[] studenti = adminStudenti.GetStudenti(out nrStudenti);
                        AfisareStudenti(studenti, nrStudenti);

                        break;
                    case "S":
                        idStudent = nrStudenti + 1;
                        nrStudenti++;
                        student = new Student(idStudent, "Ioana", "Radu");
                        //adaugare student in fisier
                        adminStudenti.AddStudent(student);

                        break;
                    case "X":

                        return;
                    default:
                        Console.WriteLine("Optiune inexistenta");

                        break;
                }
            } while (optiune.ToUpper() != "X");

            Console.ReadKey();
        }

        public static void AfisareStudenti(Student[] studenti, int nrStudenti)
        {
            Console.WriteLine("Studentii sunt:");
            for (int contor = 0; contor < nrStudenti; contor++)
            {
                string infoStudent = string.Format("Studentul cu id-ul #{0} are numele: {1} {2}",
                   studenti[contor].GetIdStudent(),
                   studenti[contor].GetNume() ?? " NECUNOSCUT ",
                   studenti[contor].GetPrenume() ?? " NECUNOSCUT ");

                Console.WriteLine(infoStudent);
            }
        }
    }
}
