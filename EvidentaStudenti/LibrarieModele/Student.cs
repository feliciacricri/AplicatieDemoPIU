namespace LibrarieModele
{
    public class Student
    {
        private int idStudent; //identificator unic student
        private string nume;
        private string prenume;

        //contructor implicit
        public Student()
        {
            nume = prenume = string.Empty;
        }

        //constructor cu parametri
        public Student(int idStudent, string nume, string prenume)
        {
            this.idStudent = idStudent;
            this.nume = nume;
            this.prenume = prenume;
        }

        public string Info()
        {
            string info = string.Format("Id:{0} Nume:{1} Prenume: {2}",
                idStudent.ToString(),
                (nume ?? " NECUNOSCUT "),
                (prenume ?? " NECUNOSCUT "));

            return info;
        }
    }
}
