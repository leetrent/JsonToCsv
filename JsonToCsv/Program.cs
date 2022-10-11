using ChoETL;

public static class Program
{
    public static readonly string _jsonFilePath = "C:\\Users\\Lee\\Dev\\GSA\\Vicki\\JsonFiles\\";
    public static readonly string _csvFilePath = "C:\\Users\\Lee\\Dev\\GSA\\Vicki\\CsvFiles\\";
    
    public static void Main(string[] args)
    {
        try
        {
            string fileName = args[0];
            string jsonFileName = $"{_jsonFilePath}{fileName}.json";
            string csvFileName = $"{_csvFilePath}{fileName}.csv";

            Console.WriteLine($"(fileName)....: '{fileName}'");
            Console.WriteLine($"(jsonFileName): '{jsonFileName}'");
            Console.WriteLine($"(csvFileName).: '{csvFileName}'");

            using (var csv = new ChoCSVWriter(csvFileName).WithFirstLineHeader())
            {
                using (var json = new ChoJSONReader(jsonFileName))
                {
                    csv.Write(json);
                }
            }
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

//using (var csv = new ChoCSVWriter(csvDestinationFile).WithFirstLineHeader())
//{
//    using (var json = new ChoJSONReader(jsonSourceFile)
//        //.WithField("FirstName")
//        //.WithField("LastName")
//        //.WithField("Age", fieldType: typeof(int))
//        //.WithField("StreetAddress", jsonPath: "$.address.streetAddress", isArray: false)
//        //.WithField("City", jsonPath: "$.address.city", isArray: false)
//        //.WithField("State", jsonPath: "$.address.state", isArray: false)
//        //.WithField("PostalCode", jsonPath: "$.address.postalCode", isArray: false)
//        //.WithField("Phone", jsonPath: "$.phoneNumber[?(@.type=='home')].number", isArray: false)
//        //.WithField("Fax", jsonPath: "$.phoneNumber[?(@.type=='fax')].number", isArray: false)
//    )
//    {
//        csv.Write(json);
//    }
//}