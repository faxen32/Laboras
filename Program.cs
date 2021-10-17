using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reklamalab
{
    class Program
    {
        // Sukuriama klasė Reklama vienos reklamos duomenims saugoti           
        class Reklama
        {
            private string užsakovas; // užsakovo pavadinimas
            private double minKaina; // minutės kaina
            private int parodymųSk; // parodymų skaičius paroje

            // Vienos reklamos duomenys
            public Reklama(string užsakovas, double minKaina, int parodymųSk)
            {
                this.užsakovas = užsakovas;
                this.minKaina = minKaina;
                this.parodymųSk = parodymųSk;
            }

            /** Grąžina užsakovą */
            public string ImtiUžsakovą() { return užsakovas; }
            /** Grąžina minutės kainą */
            public double ImtiKainą() { return minKaina; }
            /** Grąžina parodymų skaičių paroje */
            public int ImtiParSkaičių() { return parodymųSk; }


            public static void PerParą(string fv3, Reklama[] R, int n)
            {
                double perpara = 0;

                using (var fr = File.AppendText(fv3))
                {
                    for (int i = 0; i < n; i++)
                        perpara += R[i].ImtiKainą() * R[i].ImtiParSkaičių();
                    fr.Write("\nPer parą užsakovai išleidžia: {0}€\n", perpara);
                }
            }

            public static int IlgiausiaReklama(Reklama[] R, int n)
            {
                int k = 0;
                for (int i = 0; i < n; i++)
                    if (R[i].ImtiParSkaičių() > R[k].ImtiParSkaičių())
                        k = i;
                return k;
            }

            //public static void IlgiausiosKaina(string fv4, Reklama[] R, int n)
            //{
            //    double ilgKaina = 0;

            //    using (var fr = File.AppendText(fv4))
            //    {
            //        for (int i = 0; i < n; i++)
            //            ilgKaina = IlgiausiaReklama(R, n) * R[i].ImtiParSkaičių() * R[i].ImtiKainą();
            //        fr.Write("\nIlgiausios trukmės reklamos kaina: {0}€\n", ilgKaina);
            //    }
            //}


            /** fv - failo vardas, kuris nurodomas konstanta CFd 
                R - objektų rinkinys reklamos duomenims saugoti
                n - reklamų skaičius
                TVPav - televizijos pavadinimas */
            public static void Skaityti(string fv, Reklama[] R, out int n, out string TVPav)
            {
                string užsakovas;
                double minKaina;
                int parodymųSk;
                using (StreamReader reader = new StreamReader(fv))
                {
                    string line;
                    line = reader.ReadLine();
                    string[] parts;
                    TVPav = line;
                    line = reader.ReadLine();
                    n = int.Parse(line);
                    for (int i = 0; i < n; i++)
                    {
                        line = reader.ReadLine();
                        parts = line.Split(';');
                        užsakovas = parts[0];
                        minKaina = double.Parse(parts[1]);
                        parodymųSk = int.Parse(parts[2]);
                        R[i] = new Reklama(užsakovas, minKaina, parodymųSk);
                    }
                }
            }

            public static void Spausdinti(string fv2, Reklama[] R, int n, string TVPav)
            {
                const string lentele =
                     "|-----------------|--------------|----------------------|\r\n"
                   + "|    Užsakovas    | Parodymų sk. |   Minutės kaina (€)  | \r\n"
                   + "|-----------------|--------------|----------------------|";

                using (var fr = File.AppendText(fv2))
                {
                    fr.WriteLine("TV pavadinimas: {0}\n", TVPav);
                    fr.WriteLine(lentele);
                    Reklama tv;

                    for (int i = 0; i < n; i++)
                    {
                        tv = R[i];
                        fr.WriteLine("| {0,-15} | {1, 12} | {2, 13}        |",
                         tv.ImtiUžsakovą(), tv.ImtiParSkaičių(),
                         tv.ImtiKainą());
                    }

                    fr.WriteLine("|-------------------------------------------------------|");

                }

            }
        }


        static void Main(string[] args)
        {
            const int Cn = 100;
            /** duomenų failo sakinys */
            const string CFd = @"C:\Users\count\Desktop\2labprogr\Reklamalab\Reklamalab\Duom.txt";
            /** rezultatų failo sakinys */
            const string CFr = @"C:\Users\count\Desktop\2labprogr\Reklamalab\Reklamalab\Rez.txt";

            Reklama[] R = new Reklama[Cn];     // reklamos duomenys - objektai
            int n;                             // reklamų skaičius
            string TVPav;                      // TV pavadinimas
            Reklama.Skaityti(CFd, R, out n, out TVPav);
            Reklama.Spausdinti(CFr, R, n, TVPav);
            Reklama.PerParą(CFr, R, n);
            //Reklama.IlgiausiosKaina(CFr, R, n);
            Console.WriteLine(Reklama.IlgiausiaReklama(R, n));
            Console.WriteLine();


        }
    }
}
