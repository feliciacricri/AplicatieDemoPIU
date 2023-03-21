﻿using System;
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
            // acest apel ajuta la obtinerea numarului de studenti inca de la inceputul executiei
            // astfel incat o eventuala adaugare sa atribuie un IdStudent corect noului student
            adminStudenti.GetStudenti(out nrStudenti);

            string optiune;
            do
            {
                Console.WriteLine("I. Introducere informatii student");
                Console.WriteLine("A. Afisarea ultimului student introdus");
                Console.WriteLine("F. Afisare studenti din fisier");
                Console.WriteLine("S. Salvare student in fisier");
                Console.WriteLine("X. Inchidere program");
                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine();
                switch (optiune.ToUpper())
                {
                    case "I":
                        student = CitireStudentTastatura();

                        break;
                    case "A":
                        AfisareStudent(student);

                        break;
                    case "F":
                        Student[] studenti = adminStudenti.GetStudenti(out nrStudenti);
                        AfisareStudenti(studenti, nrStudenti);

                        break;
                    case "S":
                        int idStudent = nrStudenti + 1;
                        student.SetIdStudent(idStudent);
                        //adaugare student in fisier
                        adminStudenti.AddStudent(student);

                        nrStudenti = nrStudenti + 1;

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

        public static void AfisareStudent(Student student)
        {
            string infoStudent = string.Format("Studentul cu id-ul #{0} are numele: {1} {2}",
                   student.GetIdStudent(),
                   student.GetNume() ?? " NECUNOSCUT ",
                   student.GetPrenume() ?? " NECUNOSCUT ");

            Console.WriteLine(infoStudent);
        }

        public static void AfisareStudenti(Student[] studenti, int nrStudenti)
        {
            Console.WriteLine("Studentii sunt:");
            for (int contor = 0; contor < nrStudenti; contor++)
            {
                AfisareStudent(studenti[contor]);
            }
        }

        public static Student CitireStudentTastatura()
        {
            Console.WriteLine("Introduceti numele");
            string nume = Console.ReadLine();

            Console.WriteLine("Introduceti prenumele");
            string prenume = Console.ReadLine();

            Student student = new Student(0, nume, prenume);

            return student;
        }
    }
}
