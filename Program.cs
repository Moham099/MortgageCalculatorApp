using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MortgageCalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
           


            while (true)
            {
                try
                {
                    Dictionary<string, double> storedVals = new Dictionary<string, double>();
                    List<User> UserList = new List<User>();
                    double yearPayments = 12;

                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("|| Welcome to your loan application, before we can APPROVE or DENY you we will need the some information to process ||");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------\n");
                    Console.Write("What is the principal loan amount you are requesting (note:a $2500 fee will be added)?\n>$");
                    double prLoanAmt = double.Parse(Console.ReadLine());
                    storedVals.Add("Requested Amount+ 2500 fee", prLoanAmt+2500);
                    Console.WriteLine("");

                   
                    Console.Write("What is the current market value of the home?\n>$");
                    double initHomeMV = double.Parse(Console.ReadLine());
                    storedVals.Add("Home Market Value", initHomeMV);
                    Console.WriteLine("");
                    Console.Write("What is your monthly HOA?\n>$");
                    double hOA = double.Parse(Console.ReadLine());
                    storedVals.Add("HOA", hOA);
                    Console.WriteLine("");
                    Console.Write("Please select the term length (please note this will affect the APR)\n1- 15 years at 1.824% APR\n2- 30 years at 2.494% APR\n>");
                    double termSelect = int.Parse(Console.ReadLine());
                    storedVals.Add("Term Length", termSelect);
                    Console.WriteLine("");
                    double APR = 0;
                    switch (termSelect)
                    {
                        case 1:
                            APR = .01824;
                            termSelect = 15;
                            storedVals["Term Length"] = termSelect;
                            break;
                        case 2:
                            APR = .02494;
                            termSelect = 30;
                            storedVals["Term Length"] = termSelect;
                            break;
                        default:
                            break;

                    }

                    Console.Write("Downpayment amount (must be minimum 10% of current market value of house)?\n>$");
                    double downPayment = double.Parse(Console.ReadLine());
                    storedVals.Add("Downpayment", downPayment);
                    Console.WriteLine("");



                    while (downPayment < .1 * initHomeMV || downPayment > initHomeMV)
                    {
                        Console.Write("Invalid downpayment entry, please try again\n>$");
                        downPayment = double.Parse(Console.ReadLine());
                        Console.WriteLine("");

                        storedVals["Downpayment"] = downPayment;
                    }


                    Console.Write("What is your GROSS monthly income?\n>$");
                    double grossMoInc = double.Parse(Console.ReadLine());
                    Console.WriteLine("");

                    storedVals.Add("Gross Monthly Income", grossMoInc);

                    double termMonths = yearPayments * termSelect;

                    //create user object and make calcs
                    User user1 = new User();
                    //initial payment before adjustments
                    double InitPayment = user1.InitLV(prLoanAmt, initHomeMV, termSelect, APR, downPayment, grossMoInc, yearPayments);
                    storedVals.Add("Payments before Adjustments", InitPayment);
                    //calculate equity
                    double equity = user1.Equity(initHomeMV, prLoanAmt);
                    storedVals.Add("Equity", equity);
                    //escrow for term length
                    double escrow = user1.EscrowHOA(initHomeMV, hOA, termSelect, yearPayments);
                    storedVals.Add("Escrow Fee", escrow);
                    //insurance required?
                    double insurance = user1.Insurance(initHomeMV, equity, downPayment, termSelect, termMonths);
                    storedVals.Add("Insurance Fee", insurance);
                    double noInsurance = ((.1 * prLoanAmt) + equity);
                    storedVals.Add("Downpayment needed for no insurance", noInsurance);
                    //calculate TOTAL montly payment include initial payments and additional fees
                    double totalOwed = user1.TotalLoanAmt(InitPayment, escrow, insurance);
                    storedVals.Add("Amount Owed After Adjustments", totalOwed);


                    Console.Write("Would you like to verify all the values\n1-YES\n2-NO?\n>");
                    int verInput = int.Parse(Console.ReadLine());
                    Console.WriteLine("");


                    switch (verInput)
                    {
                        case 1:
                            foreach (KeyValuePair<string, double> v in storedVals)
                            {
                                Console.WriteLine("\n{0}: {1}",
                                v.Key,v.Value);
                            }
                            Console.WriteLine("PRESS ANY BUTTON TO CONTINUE TO VALUATION\n");
                            
                            Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("PRESS ANY BUTTON TO CONTINUE TO VALUATION\n");
                           
                            Console.ReadLine();
                               
                            break;
                    }






                    //call evaluation class
                    string decision = Evaluation.EvaluationF(grossMoInc, totalOwed);
                    //return from decision back into Main while loop so if its denied you can start anotehr "application"
                    if (decision == "approve")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("CONGRATULATIONS on your new house and payments!");
                        Console.ResetColor();
                        MortgageRecord.LogMortgage(storedVals);                    


                        Console.ReadLine();
                        GC.Collect();
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("We are sorry you loan did not get approved, please consider a different house or increasing your downpayment\n");
                        Console.ResetColor();
                    }



                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArithmeticException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    Console.WriteLine("");
                }



            }


        }
    }
}
