
using System.ComponentModel.DataAnnotations;

namespace Mean_Mode_Median;

public partial class Form1 : Form
{
    private TextBox? inputTextBox;
    private Button? meanButton;
    private Button? medianButton;
    private Button? modeButton;
    private Button? sdButton;
  
    private Label? resultLabel;
   

    public Form1()
    {
        InitializeComponent();
        InitializeCalculator();
       
       
    }

    private void InitializeCalculator()
    {
       this.BackgroundImage = Image.FromFile("sss.png");
       this.BackgroundImageLayout = ImageLayout.Stretch;
        inputTextBox = new TextBox()
        {
            Width = 200,
            Location = new System.Drawing.Point(297, 250),
            PlaceholderText = "Enter Number separated by commas",
        };
          inputTextBox.Anchor = AnchorStyles.None;
        this.Controls.Add(inputTextBox);

        meanButton = new Button()
        {
            Text = "Mean",
            Location  = new System.Drawing.Point(240,290)
        };
        meanButton.Anchor = AnchorStyles.None;
        meanButton.Click += (s, e) => CalculatorStatistic("mean");
        this.Controls.Add(meanButton);

        medianButton = new Button()
        {
            Text = "Median",
            Location = new System.Drawing.Point(320,290)
        };
        medianButton.Anchor = AnchorStyles.None;
        medianButton.Click += (s, e) => CalculatorStatistic("median");
        this.Controls.Add(medianButton);

        modeButton = new Button()
        {
            Text = "Mode",
            Location = new System.Drawing.Point(400,290)
        };
        modeButton.Anchor = AnchorStyles.None;
        modeButton.Click += (s, e) => CalculatorStatistic("mode");
        this.Controls.Add(modeButton);
        sdButton = new Button()
        {
            Text = "Standard Deviation",
            Location = new System.Drawing.Point(480,290)
        };
        sdButton.Anchor = AnchorStyles.None;
        sdButton.Click += (s, e) => CalculatorStatistic("sd");
        this.Controls.Add(sdButton);

        resultLabel =new Label()
        {
            AutoSize = true,
            Location = new System.Drawing.Point(297,324),
            Text = "Result: "
        };
         resultLabel.Anchor = AnchorStyles.None;
        this.Controls.Add(resultLabel);

    }
    private void CalculatorStatistic(string type)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(inputTextBox?.Text))
            {
                MessageBox.Show("Please enter numbers separated by commas.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<double> values = inputTextBox.Text.Split(',').Select(v => double.Parse(v.Trim())).ToList();

                 double result = 0;
                 List<double> modeResult = new List<double>();
                switch (type)
                {
                     case "mean":
                     result = CalculateMaen(values);
#pragma warning disable CS8602 
                    resultLabel.Text = $"Mean: {result}";
#pragma warning restore CS8602 
                    break;

                     case "median":
                     result = CalculateMedian(values);
#pragma warning disable CS8602 
                    resultLabel.Text = $"Median: {result}";
#pragma warning restore CS8602 
                    break;

                     case "mode":
                     modeResult = CalculateMode(values);
#pragma warning disable CS8602 
                    resultLabel.Text = $"Mode: {string.Join(", ", modeResult)}";
#pragma warning restore CS8602 
                    break;
                    case "sd":
                              result = CalculateStandardDeviation(values);
#pragma warning disable CS8602                              
                              resultLabel.Text = $"Standard Deviation: {result}";
#pragma warning restore CS8602                              
                               break;
                }


        } catch (Exception)
        {
            MessageBox.Show("Invalid input. Please enter numbers separated by commas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
            
            
        
       
    }

 

    private double CalculateMaen(List<double> values)
    {
        double sum = values.Sum();
        return sum / values.Count;
    }
     private double CalculateMedian(List<double> values)
        {
            values.Sort();
            int count = values.Count;
            if (count % 2 == 1)
                return values[count / 2];
            else
                return (values[(count / 2) - 1] + values[count / 2]) / 2.0;
        }

     private List<double> CalculateMode(List<double> values)
{
    // Group values by their frequency
    var frequencyDict = values.GroupBy(v => v)
                              .ToDictionary(g => g.Key, g => g.Count());

    // Find the maximum frequency
    int maxFrequency = frequencyDict.Values.Max();

    // Check if all values have the same frequency
    if (frequencyDict.Values.All(freq => freq == maxFrequency))
    {
        MessageBox.Show("No mode: All numbers occur equally often.", "Mode Calculation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return new List<double>(); // No mode
    }

    // Find the mode(s)
    return frequencyDict.Where(pair => pair.Value == maxFrequency)
                        .Select(pair => pair.Key)
                        .ToList();
}
     private double CalculateStandardDeviation(List<double> values)
        {
            double mean = CalculateMaen(values);
            double numstore = values.Select(value => Math.Pow(value - mean, 2)).Sum() / values.Count;
            return Math.Sqrt(numstore);
        }    
    
}
