using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootRunner.src.forms
{
    public partial class FormWidgetCreate : Form
    {
        public FormWidgetCreate()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string type = ((Item)comboBox1.SelectedValue).Name;
            Program.widgetManager.CreateWidget(name, type);
            this.Close();
        }

        private void FormWidgetCreate_Load(object sender, EventArgs e)
        {

            List<Item> itemList = new List<Item>();

            int pos = 0;
            foreach (WidgetType type in Program.widgetManager.widgeTypes)
            {
                Item item = new Item
                {
                    Name = type.name,
                    Id = pos++,
                    Type = type,
                };

                itemList.Add(item);
            }

            comboBox1.DataSource = itemList;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public WidgetType Type { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
