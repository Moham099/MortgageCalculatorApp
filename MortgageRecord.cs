using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MortgageCalculatorApp
{
    public class MortgageRecord
    {

        public static void LogMortgage(Dictionary<string, double> MyDict)
        {
            try
            {
                if (!File.Exists("MortgageLog.txt"))
                {
                    StreamWriter writer = new StreamWriter("MortgageLog.txt");

                    writer.Write("Loan Approved: " + MyDict["Requested Amount+ 2500 fee"] + "House Market Value: " + MyDict["Home Market Value"] + "Monthly Payments: " + MyDict["Amount Owed After Adjustments"]);

                    writer.Close();

                }
                else
                {
                    StreamWriter writer = new StreamWriter("MortgageLog.txt", true);
                    writer.Write("Loan Approved: " + MyDict["Requested Amount+ 2500 fee"] + "House Market Value: " + MyDict["Home Market Value"] + "Monthly Payments: " + MyDict["Amount Owed After Adjustments"]);
                    writer.Close();
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}
