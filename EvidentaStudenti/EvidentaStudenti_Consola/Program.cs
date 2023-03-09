using LibrarieModele;
using System;

namespace EvidentaStudenti_Consola
{
    class Program
    {
        static void Main()
        {
            Student student = new Student();
            int nrStudenti = 0;

            string optiune;
            do
            {
                Console.WriteLine("I. Introducere informatii student");
                Console.WriteLine("A. Afisare studenti");
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
                    case "X":

                        return;
                    default:
                        Console.WriteLine("Optiune inexistenta");

                        break;
                }
            } while (optiune.ToUpper() != "X");

            Console.ReadKey();
        }
    }
}
