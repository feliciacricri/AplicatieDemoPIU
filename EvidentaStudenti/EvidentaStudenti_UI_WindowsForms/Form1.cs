using LibrarieModele;
using LibrarieModele.Enumerari;
using NivelStocareDate;
using System;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EvidentaStudenti_UI_WindowsForms
{
    public partial class Form1 : Form
    {
        AdministrareStudenti_FisierText adminStudenti;

        private Label lblHeaderNume;
        private Label lblHeaderPrenume;
        private Label lblHeaderNote;

        private Label[] lblsNume;
        private Label[] lblsPrenume;
        private Label[] lblsNote;

        private const int LATIME_CONTROL = 100;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 120;
	    private const int OFFSET_X = 600;

        ArrayList disciplineSelectate = new ArrayList();

        public Form1()
        {
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;
            adminStudenti = new AdministrareStudenti_FisierText(caleCompletaFisier);

            InitializeComponent();
            
            //setare proprietati
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.LimeGreen;
            this.Text = "Informatii studenti";
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            AfiseazaStudenti();
        }

        private void AfiseazaStudenti()
        {
            //adaugare control de tip Label pentru 'Nume';
            lblHeaderNume = new Label();
            lblHeaderNume.Width = LATIME_CONTROL;
            lblHeaderNume.Text = "Nume";
            lblHeaderNume.Left = OFFSET_X + 0;
            lblHeaderNume.ForeColor = Color.DarkGreen;
            this.Controls.Add(lblHeaderNume);

            //adaugare control de tip Label pentru 'Prenume';
            lblHeaderPrenume = new Label();
            lblHeaderPrenume.Width = LATIME_CONTROL;
            lblHeaderPrenume.Text = "Prenume";
            lblHeaderPrenume.Left = OFFSET_X + DIMENSIUNE_PAS_X;
            lblHeaderPrenume.ForeColor = Color.DarkGreen;
            this.Controls.Add(lblHeaderPrenume);

            //adaugare control de tip Label pentru 'Note';
            lblHeaderNote = new Label();
            lblHeaderNote.Width = LATIME_CONTROL;
            lblHeaderNote.Text = "Note";
            lblHeaderNote.Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
            lblHeaderNote.ForeColor = Color.DarkGreen;
            this.Controls.Add(lblHeaderNote);

            ArrayList studenti = adminStudenti.GetStudenti();

            int nrStudenti = studenti.Count;
            lblsNume = new Label[nrStudenti];
            lblsPrenume = new Label[nrStudenti];
            lblsNote = new Label[nrStudenti];

            int i = 0;
            foreach (Student student in studenti)
            {
                //adaugare control de tip Label pentru numele studentilor;
                lblsNume[i] = new Label();
                lblsNume[i].Width = LATIME_CONTROL;
                lblsNume[i].Text = student.Nume;
                lblsNume[i].Left = OFFSET_X + 0;
                lblsNume[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsNume[i]);

                //adaugare control de tip Label pentru prenumele studentilor
                lblsPrenume[i] = new Label();
                lblsPrenume[i].Width = LATIME_CONTROL;
                lblsPrenume[i].Text = student.Prenume;
                lblsPrenume[i].Left = OFFSET_X + DIMENSIUNE_PAS_X;
                lblsPrenume[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsPrenume[i]);

                //adaugare control de tip Label pentru notele studentilor
                lblsNote[i] = new Label();
                lblsNote[i].Width = LATIME_CONTROL;
                lblsNote[i].Text = string.Join(" ", student.GetNote());
                lblsNote[i].Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
                lblsNote[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsNote[i]);
                i++;
            }
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            if (!DateIntrareValide())
            {
                lblDiscipline.ForeColor = Color.Red;
                lblNote.ForeColor = Color.Red;

                return;
            }

            Student s = new Student(0, txtNume.Text, txtPrenume.Text);
            s.SetNote(txtNote.Text);

            //set program studiu
            //verificare radioButton selectat
            ProgramStudiu specializareSelectata = GetProgramStudiuSelectat();
            s.Specializare = specializareSelectata;
            
            //set Discipline
            s.Discipline = new ArrayList();
            s.Discipline.AddRange(disciplineSelectate);

            adminStudenti.AddStudent(s);
            lblMesaj.Text = "Studentul a fost adaugat";

            //resetarea controalelor pentru a introduce datele unui student nou
            ResetareControale();
        }

        private bool DateIntrareValide()
        {
            string[] note = txtNote.Text.Split(' ');
            if (disciplineSelectate.Count != note.Length)
            {
                return false;
            }

            return true;
        }

        private void CkbDiscipline_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBoxControl = sender as CheckBox; //operator 'as'
            //sau
            //CheckBox checkBoxControl = (CheckBox)sender;  //operator cast

            string disciplinaSelectata = checkBoxControl.Text;

            //verificare daca checkbox-ul asupra caruia s-a actionat este selectat
            if (checkBoxControl.Checked == true)
                disciplineSelectate.Add(disciplinaSelectata);
            else
                disciplineSelectate.Remove(disciplinaSelectata);
        }

        private void ResetareControale()
        {
            txtNume.Text = txtPrenume.Text = txtNote.Text = string.Empty;
            
            rdbCalculatoare.Checked = false;
            rdbAutomatica.Checked = false;
            rdbElectronica.Checked = false;
            
            ckbPCLP.Checked = false;
            ckbPOO.Checked = false;
            ckbPIU.Checked = false;
           
            disciplineSelectate.Clear();
            lblMesaj.Text = string.Empty;
        }

        private ProgramStudiu GetProgramStudiuSelectat()
        {
            if (rdbCalculatoare.Checked)
                return ProgramStudiu.Calculatoare;
            if (rdbAutomatica.Checked)
                return ProgramStudiu.Automatica;
            if (rdbElectronica.Checked)
                return ProgramStudiu.Electronica;

            return ProgramStudiu.Calculatoare;
        }

        private void BtnAfiseaza_Click(object sender, EventArgs e)
        {
            AfiseazaStudenti();
            this.Width = 1000;
        }
    }
}
