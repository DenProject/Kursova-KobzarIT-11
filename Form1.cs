using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova
{

    public partial class Form1 : Form
    {
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }
        static bool check_parsed(double xn, TextBox textBox)//перевірка введення
        {
            bool right = true;
            try
            {
                xn = double.Parse(textBox.Text);
            }
            catch (FormatException)
            {
                right = false;
            }
            return right;
        }

        static void process_oper(double q, Random rnd, ListBox listBox1, ListBox listBox2, double x, ref int first_count, ref int second_count)
        {
            const double a = 1;
            q = rnd.NextDouble();
            if (q <= 0.45)
            {
                double result = Math.Log10(a - q * x) / x;
                if (double.IsNaN(result) || double.IsInfinity(result))
                {
                    listBox1.Items.Add($"The value of the function cannot be calculated for x = {x} ||| q = {q.ToString("0.00")}");
                }
                else
                {
                    listBox1.Items.Add($"Value of the function with q = {q.ToString("0.00")} ||| x = {x} ||| y={result}");
                    first_count += 1;
                }
            }
            else if (q > 0.45 && q < 1.0)
            {
                double result = Math.Pow(x, 1.0 / 3);
                if (double.IsNaN(result) || double.IsInfinity(result))
                {
                    listBox2.Items.Add($"The value of the function cannot be calculated for x = {x} ||| q = {q.ToString("0.00")}");
                }
                else
                {
                    listBox2.Items.Add($"Value of the function with q = {q.ToString("0.00")} ||| x = {x} ||| y={result}");
                    second_count += 1;
                }
            }
        }
        double xn = 0;
        double dx = 0;
        double xk = 0;
        double q;
        bool count = true;
        int first_count = 0;
        int second_count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            count = true;
            first_count = 0;
            second_count = 0;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            if (check_parsed(xn, textBox1) && check_parsed(dx, textBox2) && check_parsed(xk, textBox3))
            {
                xn = double.Parse(textBox1.Text);
                dx = double.Parse(textBox2.Text);
                xk = double.Parse(textBox3.Text);
                if (xn > xk && dx>0)
                {
                    count = false;
                    listBox1.Items.Add("xn > xk");
                    listBox2.Items.Add("xn > xk");
                }
                else if (xn==xk) 
                {
                    count = false;
                    listBox1.Items.Add("xn == xk");
                    listBox2.Items.Add("xn == xk");
                }
                else if(dx == 0)
                {
                    count = false;
                    listBox1.Items.Add("dx == 0");
                    listBox2.Items.Add("dx == 0");
                }
                else if(xn < xk && dx < 0)
                {
                    count = false;
                    listBox1.Items.Add("xn < xk and dx < 0");
                    listBox2.Items.Add("xn < xk and dx < 0");
                }
                if(count == true) {
                    if (dx > 0)
                    {
                        for (double x = xn; x <= xk; x += dx)
                        {
                            process_oper(q, rnd, listBox1, listBox2, x, ref first_count, ref second_count);
                        }
                    }
                    else
                    {
                        for (double x = xn; x >= xk; x += dx)
                        {
                            process_oper(q, rnd, listBox1, listBox2, x, ref first_count, ref second_count);
                        }
                    }
                    label10.Text = $"Amount for f1(x): {first_count}";
                    label11.Text = $"Amount for f2(x): {second_count}";
                }
                else
                {
                    listBox1.Items.Add("Can't calculate");
                    listBox2.Items.Add("Can't calculate");
                }
            }
            else
            {
                listBox1.Items.Add("Can't read parameters");
                listBox2.Items.Add("Can't read parameters");
            }
        }
    }
}
