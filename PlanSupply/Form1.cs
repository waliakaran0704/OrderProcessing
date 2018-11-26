using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using PlanSupply.Pocos;

namespace PlanSupply
{
    public partial class Form1 : Form
    {
        Bitmap DrawArea;

        List<Supplier> Suppliers;
        List<Customer> Customers;

        public Form1()
        {
            InitializeComponent();
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = DrawArea;

            Suppliers = new List<Supplier>();
            Customers = new List<Customer>();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Random rnd = new Random();
         
            Suppliers.Clear();

            for (int i = 1; i <= numericUpDown1.Value; i++)
            {
                Suppliers.Add(
                    new Supplier("S" + i.ToString(), new Point(rnd.Next(1, DrawArea.Width), rnd.Next(1, DrawArea.Height))));
            }
            Plot();

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Random rnd = new Random();

            Customers.Clear();
            for (int i = 1; i <= numericUpDown2.Value; i++)
            {
                Customers.Add(
                    new Customer("C" + i.ToString(), new Point(rnd.Next(1, DrawArea.Width), rnd.Next(1, DrawArea.Height))));
            }

            Plot();
        }
        void Plot()
        {
            Graphics g = Graphics.FromImage(DrawArea);
            g.Clear(Color.White);

            foreach (Supplier s in Suppliers)
            {
                g.FillEllipse(
                    new SolidBrush(Color.Red),
                    s.Location.X - 3, s.Location.Y - 3, 6, 6
                    );
            }

            foreach (Customer c in Customers)
            {
                g.FillEllipse(
                    new SolidBrush(Color.Blue),
                    c.Location.X - 3, c.Location.Y - 3, 6, 6
                    );
            }


            if (Suppliers.Count > 0 && Customers.Count > 0)
            {
                var qDistanceAll = from c in Customers
                                   from s in Suppliers
                                   select new
                                   {
                                       Supplier = s,
                                       Customer = c,
                                       Distance = Distance(s, c)
                                   };

                var qDistanceAllSorted = from r in qDistanceAll.ToList()
                                         orderby r.Customer.Name, r.Distance
                                         select r;

                var qCustomerWithNearestSuppler = qDistanceAllSorted.ToList().Where((x, i) => i % Suppliers.Count == 0);

                foreach (var i in qCustomerWithNearestSuppler)
                {
                    g.DrawLine(new Pen(Color.Red),
                        i.Supplier.Location.X, i.Supplier.Location.Y,
                        i.Customer.Location.X, i.Customer.Location.Y);
                }
            }
            pictureBox1.Image = DrawArea;

            g.Dispose();
        }
        double Distance(Supplier s, Customer c)
        {

            return Math.Sqrt(
                Math.Pow(Math.Abs(s.Location.X - c.Location.X), 2)
                    +
                    Math.Pow(Math.Abs(s.Location.Y - c.Location.Y), 2)
                    );

        }


    }
}
