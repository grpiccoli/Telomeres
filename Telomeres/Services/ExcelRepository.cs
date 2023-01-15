using FastExcel;

namespace Telomeres.Services
{


    public class ExcelRepository
    {

        private const string TAB_DATA_SOURCE = "Tabular Data";
        private const string EXCEL_DATA_SOURCE = "Libro1.xlsx";
        private string FilePath;


        public ExcelRepository(string filepath)
        {
            this.FilePath = filepath;
        }

        public string ReadFile()
        {

            /*
            *  1. open excel data source
            *  2. read "tab data"
            *  
            */


            var inputFile = new FileInfo(FilePath);
            Worksheet worksheet = new Worksheet();
            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(inputFile, true))
            {
                worksheet = fastExcel.Read(TAB_DATA_SOURCE);

                var rows = worksheet.Rows;


            }


            return "";
        }
    }
}
