using ChoETL;
using System.Text;

public static class Program
{
    public static readonly string _jsonFilePath = "C:\\Users\\Lee\\Dev\\GSA\\Vicki\\JsonFiles\\";
    public static readonly string _csvFilePath = "C:\\Users\\Lee\\Dev\\GSA\\Vicki\\CsvFiles\\";
    
    public static void Main(string[] args)
    {
        try
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please provide file name 'without' the file extension (i.e. 'senators')");
                return;
            }

            string fileName = args[0];
            string jsonFileName = $"{_jsonFilePath}{fileName}.json";
            string csvFileName = $"{_csvFilePath}{fileName}.csv";

            Console.WriteLine($"(fileName)....: '{fileName}'");
            Console.WriteLine($"(jsonFileName): '{jsonFileName}'");
            Console.WriteLine($"(csvFileName).: '{csvFileName}'");

            var sampleJson = File.ReadAllText(jsonFileName);

            StringBuilder stringBuilder = new StringBuilder();
            using (var data = ChoJSONReader.LoadText(sampleJson).WithJSONPath("$.objects"))
            {
                using (var w = new ChoCSVWriter(stringBuilder)
                    //.WithFirstLineHeader()
                    .Configure(c => c.MaxScanRows = 1)
                    .Configure(c => c.ThrowAndStopOnMissingField = false)
                    )
                {
                    w.Write(data);
                }
            }

            File.WriteAllText(csvFileName, stringBuilder.ToString());
        }
        catch(Exception exc)
        {
            Console.WriteLine();
            Console.WriteLine(exc.Message);
            Console.WriteLine();
            Console.WriteLine(exc.StackTrace);
            Console.WriteLine();
        }
    }
}