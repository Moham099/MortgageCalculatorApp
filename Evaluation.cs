using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculatorApp
{
    public class Evaluation
    {
        public static string EvaluationF(double grossIncome, double totalPayment)
        {

            Console.WriteLine("Customer's total monthly payment is: {0}", totalPayment);
            Console.WriteLine("Customer's gross monthly income is: {0}", grossIncome);
            Console.WriteLine("PRESS ANY BUTTON TO CONTINUE\n");

            Console.ReadLine();

            if (totalPayment <= .25 * grossIncome)
            {
                Console.Write("Approve or Deny?\n1-APPROVE\n2-DENY\n>");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        return "approve";

                    case 2:
                        return "deny";
                    default:
                        return "";
                }
            }
            else
            {
                return "deny";
            }

        }
    
    }
}
