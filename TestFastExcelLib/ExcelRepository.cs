namespace TestFastExcelLib
{
    public class ExcelRepository
    {
        public ExcelRepository()
        {



        }


        public void readFile(string filename)
        {
            var inputFile = new FileInfo(filename);

            // Create an instance of Fast Excel
            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(inputFile, true))
            {
                foreach (var worksheet in fastExcel.Worksheets)
                {
                    Console.WriteLine(string.Format("Worksheet Name:{0}, Index:{1}", worksheet.Name, worksheet.Index));

                    //To read the rows call read
                    worksheet.Read();
                    var rows = worksheet.Rows.ToArray();
                    //Do something with rows
                    Console.WriteLine(string.Format("Worksheet Rows:{0}", rows.Count()));
                }
            }
        }
    }


}
