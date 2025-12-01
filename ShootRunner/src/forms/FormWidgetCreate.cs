#pragma warning disable IDE0079
#pragma warning disable IDE0130

namespace ShootRunner
{
    public partial class FormWidgetCreate : Form
    {
        readonly List<Item> itemList = [];

        public FormWidgetCreate()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            Item? item = comboBox1.SelectedItem as Item;
            WidgetType? type = item?.Type;
            Program.widgetManager.CreateWidget(name, type);
            this.Close();
        }

        private void FormWidgetCreate_Load(object sender, EventArgs e)
        {

            itemList.Clear();

            int pos = 0;
            foreach (WidgetType type in Program.widgetManager.widgeTypes)
            {
                Item item = new()
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

            ComboBox1_SelectedIndexChanged(null, null);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to permanently delete this item?\n" +
                "This action cannot be undone.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning 
            );

            if (result == DialogResult.Yes)
            {
                int indexToSelect = comboBox1.SelectedIndex;
                if (comboBox1.SelectedItem is Item item)
                {
                    WidgetType? type = item.Type;
                    if (File.Exists(type?.source))
                    {
                        File.Move(type.source, type.source + ".bak");
                    }

                    itemList.Remove(item);
                    comboBox1.DataSource = null;
                    comboBox1.DataSource = itemList;


                    if (itemList.Count == 0)
                    {
                        comboBox1.SelectedIndex = -1;
                    }
                    else if (indexToSelect >= itemList.Count)
                    {
                        comboBox1.SelectedIndex = itemList.Count - 1;
                    }
                    else
                    {
                        comboBox1.SelectedIndex = indexToSelect;
                    }

                    ComboBox1_SelectedIndexChanged(null, null);
                }

            }
        }

        private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs? e)
        {
            try
            {
                if (comboBox1.SelectedItem is Item item)
                {
                    WidgetType? type = item.Type;

                    textBox2.Text = type?.html.Replace("\n", "\r\n").Replace("\r\r\n", "\r\n");
                }
                else
                {
                    textBox2.Text = "";
                }
            }
            catch (Exception)
            {

            }
            
        }
    }

    public class Item
    {
        public string? Name { get; set; }
        public int Id { get; set; }

        public WidgetType? Type { get; set; }

        public override string? ToString()
        {
            return Name;
        }
    }
}
