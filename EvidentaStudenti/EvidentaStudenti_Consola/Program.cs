using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
			string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            // setare locatie fisier in directorul corespunzator solutiei
            // astfel incat datele din fisier sa poata fi utilizate si de alte proiecte
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            AdministrareStudenti_FisierText adminStudenti = new AdministrareStudenti_FisierText(caleCompletaFisier);
            int nrStudenti = 0;
            // acest apel ajuta la obtinerea numarului de studenti inca de la inceputul executiei
            // astfel incat o eventuala adaugare sa atribuie un IdStudent corect noului student
            adminStudenti.GetStudenti();

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
                        List<Student> studenti = adminStudenti.GetStudenti();
                        AfisareStudenti(studenti);

                        break;
                    case "S":
                        int idStudent = nrStudenti + 1;
                        student.IdStudent = idStudent;
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
            string infoStudent = string.Format("Studentul cu id-ul #{0} are numele: {1} {2} si notele: {3}",
                    student.IdStudent,
                    student.Nume ?? " NECUNOSCUT ",
                    student.Prenume ?? " NECUNOSCUT ",
                    string.Join(",", student.GetNote()));

            Console.WriteLine(infoStudent);
        }

        public static void AfisareStudenti(List<Student> studenti)
        {
            Console.WriteLine("Studentii sunt:");
            for (int contor = 0; contor < studenti.Count; contor++)
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

            Console.WriteLine("Introduceti notele");
            string note = Console.ReadLine();
            student.SetNote(note);

            return student;
        }
    }
}
