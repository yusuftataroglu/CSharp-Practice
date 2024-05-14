namespace Fibonacci
{
    public partial class Form1 : Form
    {
        private List<string> _colors = Enum.GetNames(typeof(KnownColor)).Where(x => x.Contains("Light")).ToList();
        private Random _rnd = new();
        private List<double> _numberList = [0, 1];
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            while (true)
            {
                _numberList.Add(_numberList[^1] + _numberList[^2]);
                if (_numberList[^1] is double.PositiveInfinity)
                {
                    _numberList.Clear();
                    _numberList = [0, 1];
                    break;
                }

                int selectedIndex = _rnd.Next(0, _colors.Count);
                richTextBox1.SelectionColor = Color.FromName(_colors[selectedIndex]);
                richTextBox1.AppendText($"{_numberList.Last()}  -  Ratio: {_numberList[^1] / _numberList[^2]}\n");
            }
        }
    }
}
