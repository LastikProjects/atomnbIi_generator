using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atomnbIi_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[] prime_numbers = new int[1000000];
        int[] prime_numbers2 = new int[78498];
        double[] R = new double[1000000];
        int L = 0;
        double[] Rtest = new double[10000];
        double[] xikvadrat_tabl = { 0, 3.8, 6, 7.8, 9.5, 11.1, 12.6, 14.1, 15.5, 16.9, 18.3, 19.7, 21, 22.4, 23.7, 25, 26.3, 27.6, 28.9, 30.1, 31.4, 32.7, 33.9, 35.2, 36.4, 37.7, 38.9, 40.1, 41.3, 42.6, 43.8 };

        private void button1_Click(object sender, EventArgs e)
        {
            initialization_first_sensor();
        }

        private void initialization_first_sensor()
        {
            int U = 1789;
            int M = 0;
            for (int count2 = 6000; count2 < 6010; count2++)
            {
                int p = prime_numbers2[count2];
                M = 0;
                search_M(ref M, p);
                search_L(R, p, U, M, ref L);
            }

        }

        private void search_M(ref int M, int p)
        {
            for (int i = 0; i < 30; i++)
                if (p < Math.Pow(3, i))
                {
                    M = Convert.ToInt32(p - Math.Pow(3, i - 1));
                    textBox3.Text = i.ToString();
                    break;
                }
            textBox2.Text = M.ToString();
        }

        private void search_L(double[] R, int p, int U, int M, ref int L)
        {
            for (int i = 0; i < R.Length; i++)
            {
                R[i] = (double)U / (double)p;
                U = Convert.ToInt32(((double)U * (double)M)) % p;
                for (int j = 0; j < i; j++)
                    if (R[i] == R[j])
                    {
                        if (i - 1 > L)
                        {
                            L = i - 1;
                            textBox1.Text = p.ToString();
                            textBox5.Text = (L - j).ToString();
                            //massivL[countmassivL] = L;
                            //countmassivL++;
                            textBox4.Text = L.ToString();// + "|" + i.ToString() + "|" + j.ToString();
                            U = Convert.ToInt32((double)U * (double)M) % p;
                        }
                        return;
                    }
            }
        }

        private void button2_Click(object sender, EventArgs e)//prostble chisla
        {
            for (int i = 0; i < prime_numbers.Length; i++)
                prime_numbers[i] = i;
            prime_numbers[1] = 0;
            for (int i = 2; i < 1001; i++)
            {
                if (prime_numbers[i] != 0)
                    for (int j = i + 1; j < prime_numbers.Length; j++)
                        if (prime_numbers[j] % i == 0)
                            prime_numbers[j] = 0;
            }
            int count = 0;
            for (int i = 0; i < prime_numbers.Length; i++)
                if (prime_numbers[i] != 0)
                {
                    prime_numbers2[count] = prime_numbers[i];
                    count++;
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            second_first_sensor();
        }

        private void second_first_sensor()
        {
            int q = Convert.ToInt32(textBox6.Text);
            int[] m = new int[q];
            double xikvadrat = 0;

            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            for (int i = 0; i < q; i++)
                for (int j = 0; j < R.Length - 1; j++)
                {
                    if (R[j] != 0)
                        if (R[j] <= ((i + 1) / (double)q) & R[j] > (i / (double)q))
                            m[i]++;
                    if (R[j] == 0 & R[j + 1] != 0)
                        m[0]++;
                }

            for (int i = 0; i < q; i++)
                xikvadrat += Math.Pow(m[i] - countR / (double)q, 2);
            xikvadrat /= countR / (double)q;
            textBox7.Text = xikvadrat.ToString();
            textBox8.Text = xikvadrat_tabl[q - 2].ToString();
            if (xikvadrat < xikvadrat_tabl[q - 2])
                textBox9.Text = "Верно";
            else
                textBox9.Text = "Неверно";

            chart1.Series[0].Points.Clear();
            for (int i = 0; i < m.Length; i++)
                chart1.Series[0].Points.AddXY((i / (double)q + 1 / (double)2 / (double)q), m[i]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            second_second_sensor();
        }

        private void second_second_sensor()
        {
            int q = Convert.ToInt32(textBox13.Text);
            int[,] m = new int[q, q];
            double xikvadrat = 0;

            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            for (int i = 0; i < countR / 2 * 2; i += 2)//чтобы не было выхода из массива при нечетном countR
                for (int j = 0; j < q; j++)
                    for (int k = 0; k < q; k++)
                        if (R[i] <= ((j + 1) / (double)q) & R[i] > (j / (double)q) & R[i + 1] <= ((k + 1) / (double)q) & R[i + 1] > (k / (double)q))
                            m[j, k]++;

            for (int i = 0; i < q; i++)
                for (int j = 0; j < q; j++)
                    xikvadrat += Math.Pow(m[i, j] - countR / 2 / (double)(q * q), 2);
            xikvadrat /= countR / 2 / (double)(q * q);
            textBox12.Text = xikvadrat.ToString();
            textBox11.Text = xikvadrat_tabl[q * q - 2].ToString();
            if (xikvadrat < xikvadrat_tabl[q * q - 2])
                textBox10.Text = "Верно";
            else
                textBox10.Text = "Неверно";

            Graphics g = pictureBox1.CreateGraphics();
            for (int i = 0; i < countR; i += 2)
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(Convert.ToInt32(R[i] * 700), 700 - Convert.ToInt32(R[i + 1] * 700), 1, 1));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            second_third_sensor();
        }

        private void second_third_sensor()
        {
            int q = Convert.ToInt32(textBox14.Text);
            int[,,] m = new int[q, q, q];
            double xikvadrat = 0;

            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            for (int i = 0; i < countR / 3 * 3; i += 3)//чтобы не было выхода из массива при нечетном countR
                for (int j = 0; j < q; j++)
                    for (int k = 0; k < q; k++)
                        for (int l = 0; l < q; l++)
                            if (R[i] <= ((j + 1) / (double)q) & R[i] > (j / (double)q) & R[i + 1] <= ((k + 1) / (double)q) & R[i + 1] > (k / (double)q) & R[i + 2] <= ((l + 1) / (double)q) & R[i + 2] > (l / (double)q))
                                m[j, k, l]++;

            for (int i = 0; i < q; i++)
                for (int j = 0; j < q; j++)
                    for (int k = 0; k < q; k++)
                        xikvadrat += Math.Pow(m[i, j, k] - countR / 3 / (double)(q * q * q), 2);
            xikvadrat /= countR / 3 / (double)(q * q * q);
            textBox17.Text = xikvadrat.ToString();
            textBox16.Text = xikvadrat_tabl[q * q * q - 2].ToString();
            if (xikvadrat < xikvadrat_tabl[q * q * q - 2])
                textBox15.Text = "Верно";
            else
                textBox15.Text = "Неверно";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            second_forth_sensor();
        }

        private void second_forth_sensor()
        {
            int q = Convert.ToInt32(textBox18.Text);
            int[,,,] m = new int[q, q, q, q];
            double xikvadrat = 0;

            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            for (int i = 0; i < countR / 4 * 4; i += 4)//чтобы не было выхода из массива при нечетном countR
                for (int j = 0; j < q; j++)
                    for (int k = 0; k < q; k++)
                        for (int l = 0; l < q; l++)
                            for (int n = 0; n < q; n++)
                                if (R[i] <= ((j + 1) / (double)q) & R[i] > (j / (double)q) & R[i + 1] <= ((k + 1) / (double)q) & R[i + 1] > (k / (double)q) & R[i + 2] <= ((l + 1) / (double)q) & R[i + 2] > (l / (double)q) & R[i + 3] <= ((n + 1) / (double)q) & R[i + 3] > (n / (double)q))
                                    m[j, k, l, n]++;

            for (int i = 0; i < q; i++)
                for (int j = 0; j < q; j++)
                    for (int k = 0; k < q; k++)
                        for (int l = 0; l < q; l++)
                            xikvadrat += Math.Pow(m[i, j, k, l] - countR / 4 / (double)(q * q * q * q), 2);
            xikvadrat /= countR / 4 / (double)(q * q * q * q);
            textBox21.Text = xikvadrat.ToString();
            textBox20.Text = xikvadrat_tabl[q * q * q * q - 2].ToString();
            if (xikvadrat < xikvadrat_tabl[q * q * q * q - 2])
                textBox19.Text = "Верно";
            else
                textBox19.Text = "Неверно";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            third_sensor();
        }

        private void third_sensor()
        {
            int q = Convert.ToInt32(textBox23.Text);
            int[] m = new int[q];
            double Kn = 0;

            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            for (int i = 0; i < q; i++)
                for (int j = 0; j < R.Length - 1; j++)
                {
                    if (R[j] != 0)
                        if (R[j] <= ((i + 1) / (double)q) & R[j] > (i / (double)q))
                            m[i]++;
                    if (R[j] == 0 & R[j + 1] != 0)
                        m[0]++;
                }
            for (int i = 0; i < q; i++)
                Kn += Math.Abs(m[i] - countR / (double)q);
            Kn = Math.Sqrt(Kn / (double)q) * q / (double)countR * 100;

            textBox22.Text = Kn.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int stepenb = Convert.ToInt32(textBox24.Text);
            four_first_sensor(stepenb);
        }

        private void four_first_sensor(int stepenb)
        {
            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            int[] m = new int[10];
            for (int i = 0; i < m.Length; i++)
                for (int j = 0; j < countR; j++)
                    if (Math.Truncate(R[j] * Math.Pow(10, stepenb)) % 10 == i)
                        m[i]++;

            chart1.Series[0].Points.Clear();
            for (int i = 0; i < m.Length; i++)
                chart1.Series[0].Points.AddXY(i, m[i]);

            double xikvadrat = 0;

            for (int i = 0; i < m.Length; i++)
                xikvadrat += Math.Pow(m[i] - countR / 10, 2);
            xikvadrat /= countR / 10;
            textBox27.Text = xikvadrat.ToString();
            textBox26.Text = xikvadrat_tabl[8].ToString();
            if (xikvadrat < xikvadrat_tabl[8])
                textBox25.Text = "Верно";
            else
                textBox25.Text = "Неверно";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int stepenb = Convert.ToInt32(textBox31.Text);
            four_second_sensor(stepenb);
        }

        private void four_second_sensor(int stepenb)
        {
            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            int[] m = new int[100];
            for (int i = 0; i < m.Length; i++)
                for (int j = 0; j < countR; j++)
                    if (Math.Truncate(R[j] * Math.Pow(10, stepenb * 2)) % 100 == i)
                        m[i]++;

            int[] m2 = new int[10];
            for (int j = 0; j < m2.Length; j++)
                for (int i = 0; i < m.Length; i++)
                    if (i >= j * 10 & i < (j + 1) * 10)
                        m2[j] += m[i];

            chart1.Series[0].Points.Clear();
            for (int i = 0; i < m2.Length; i++)
                chart1.Series[0].Points.AddXY(i * 10 + 5, m2[i]);

            double xikvadrat = 0;
            for (int i = 0; i < m2.Length; i++)
                xikvadrat += Math.Pow(m2[i] - countR / 10, 2);
            xikvadrat /= countR / 10;
            textBox30.Text = xikvadrat.ToString();
            textBox29.Text = xikvadrat_tabl[8].ToString();
            if (xikvadrat < xikvadrat_tabl[8])
                textBox28.Text = "Верно";
            else
                textBox28.Text = "Неверно";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int stepenb = Convert.ToInt32(textBox35.Text);
            four_third_sensor(stepenb);
        }

        private void four_third_sensor(int stepenb)
        {
            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            int[] m = new int[1000];
            for (int i = 0; i < m.Length; i++)
                for (int j = 0; j < countR; j++)
                    if (Math.Truncate(R[j] * Math.Pow(10, stepenb * 3)) % 1000 == i)
                        m[i]++;

            int[] m2 = new int[10];
            for (int j = 0; j < m2.Length; j++)
                for (int i = 0; i < m.Length; i++)
                    if (i >= j * 100 & i < (j + 1) * 100)
                        m2[j] += m[i];

            chart1.Series[0].Points.Clear();
            for (int i = 0; i < m2.Length; i++)
                chart1.Series[0].Points.AddXY(i * 100 + 50, m2[i]);

            double xikvadrat = 0;
            for (int i = 0; i < m2.Length; i++)
                xikvadrat += Math.Pow(m2[i] - countR / 10, 2);
            xikvadrat /= countR / 10;
            textBox34.Text = xikvadrat.ToString();
            textBox33.Text = xikvadrat_tabl[8].ToString();
            if (xikvadrat < xikvadrat_tabl[8])
                textBox32.Text = "Верно";
            else
                textBox32.Text = "Неверно";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int stepenb = Convert.ToInt32(textBox36.Text);
            four_forth_sensor(stepenb);
        }

        private void four_forth_sensor(int stepenb)
        {
            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            int[] m = new int[10000];
            for (int i = 0; i < m.Length; i++)
                for (int j = 0; j < countR; j++)
                    if (Math.Truncate(R[j] * Math.Pow(10, stepenb * 4)) % 10000 == i)
                        m[i]++;

            int[] m2 = new int[10];
            for (int j = 0; j < m2.Length; j++)
                for (int i = 0; i < m.Length; i++)
                    if (i >= j * 1000 & i < (j + 1) * 1000)
                        m2[j] += m[i];

            chart1.Series[0].Points.Clear();
            for (int i = 0; i < m2.Length; i++)
                chart1.Series[0].Points.AddXY(i * 1000 + 500, m2[i]);

            double xikvadrat = 0;
            for (int i = 0; i < m2.Length; i++)
                xikvadrat += Math.Pow(m2[i] - countR / 10, 2);
            xikvadrat /= countR / 10;
            textBox38.Text = xikvadrat.ToString();
            textBox39.Text = xikvadrat_tabl[8].ToString();
            if (xikvadrat < xikvadrat_tabl[8])
                textBox37.Text = "Верно";
            else
                textBox37.Text = "Неверно";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            five_sensor();
        }

        private void five_sensor()
        {
            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;

            int k = Convert.ToInt32(textBox40.Text);
            //double numerator1 = 0;
            //double numerator2 = 0;
            //double numerator3 = 0;
            //double denominator1 = 0;
            //double denominator2 = 0;
            //double denominator3 = 0;
            //double denominator4 = 0;

            //for (int i = 0; i < countR - k; i++)
            //{
            //    numerator1 += R[i] * R[i + k];
            //    numerator2 += R[i];
            //    numerator3 += R[i + k];
            //    denominator1 += R[i] * R[i];
            //    denominator4 += R[i + k] * R[i + k];
            //}
            //numerator1 = numerator1 * countR - numerator2 * numerator3;

            //denominator2 = numerator2 * numerator2;
            //denominator3 = numerator3 * numerator3;
            //denominator1 *= countR;
            //denominator4 *= countR;
            //denominator1 -= denominator2;
            //denominator4 -= denominator3;
            //denominator1 *= denominator4;
            //denominator1 = Math.Sqrt(denominator1);
            //numerator1 /= denominator1;
            //textBox41.Text = numerator1.ToString();


            //numerator1 = 0;
            //numerator2 = 0;
            //numerator3 = 0;
            //denominator1 = 0;
            //denominator2 = 0;
            //denominator3 = 0;
            //denominator4 = 0;
            //double mozhidaniei = 0;
            //double mozhidaniek = 0;
            //double mozhidanieik = 0;
            //double dispersiai = 0;
            //double dispersiak = 0;

            //for (int i = 0; i < countR-k; i++)
            //{
            //    numerator1 += R[i] * R[i + k];
            //    numerator3 += R[i + k];
            //}
            //for (int i = 0; i < countR; i++)
            //{
            //    numerator2 += R[i];
            //}
            //mozhidaniei = numerator2 / countR;
            //mozhidaniek = numerator3 / (countR-k);
            //mozhidanieik = numerator1 / (countR-k);
            //for (int i = 0; i < countR; i++)
            //{
            //    dispersiai += Math.Pow(R[i] - mozhidaniei, 2);
            //}
            //dispersiai /= countR - 1;
            //dispersiai = Math.Sqrt(dispersiai);
            //for (int i = 0; i < countR-k; i++)
            //{
            //    dispersiak += Math.Pow(R[i] - mozhidaniek, 2);
            //}
            //dispersiak /= countR - k - 1;
            //dispersiak = Math.Sqrt(dispersiak);
            //double otvet = (mozhidanieik - mozhidaniei * mozhidaniek)/(dispersiai*dispersiak);
            //textBox41.Text = otvet.ToString();
            double numerator1 = 0;
            double numerator2 = 0;
            double numerator3 = 0;
            double denominator1 = 0;
            double denominator2 = 0;
            for (int i = 0; i < countR - k; i++)
            {
                numerator1 += R[i] * R[i + k];
                numerator2 += R[i + k];
            }
            numerator1 /= countR - k;
            numerator2 /= countR - k;
            for (int i = 0; i < countR; i++)
            {
                numerator3 += R[i];
                denominator1 += R[i] * R[i];
                denominator2 += R[i];
            }
            numerator3 /= countR;
            denominator1 /= countR;
            denominator2 /= countR;
            denominator2 *= denominator2;

            double otvet = (numerator1 - numerator2 * numerator3) / (denominator1 - denominator2);
            textBox41.Text = otvet.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int countR = 0;//для подсчета количества ненулевых элементов в массиве R
            for (int i = 0; i < R.Length - 1; i++)
                if (R[i] != 0 | (R[i] == 0 & R[i + 1] != 0))
                    countR++;
            if (countR % 2 != 0)
                countR--;

            double counter = 0;
            for (int i = 0; i < (countR - 1) / 2; i++)
                if (Math.Sqrt(R[2 * i] * R[2 * i] + R[2 * i + 1] * R[2 * i + 1]) < 1)
                    counter++;
            double Pi_exp = 8 * (double)counter / countR;
            textBox43.Text = Pi_exp.ToString();
        }
    }
}
