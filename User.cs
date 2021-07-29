using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculatorApp
{
    public class User
    {
        private double _prLoan;
        private double _initHomeMV;
        private double _termLen;
        private double _downPayment;
        private double _aPR;
        private double _grossMoInc;
        private double _yearPayments;


        double PrLoan
        {
            get { return _prLoan; }
            set { _prLoan = value; }
        }

        double InitHomeMV
        {
            get { return _initHomeMV; }
            set { _initHomeMV = value; }
        }

        double TermLen
        {
            get { return _termLen; }
            set { _termLen = value; }
        }

        double APR
        {
            get { return _aPR; }
            set { _aPR = value; }
        }

        double DownPayment
        {
            get { return _downPayment; }
            set { _downPayment = value; }
        }

        double GrossMoInc
        {
            get { return _grossMoInc; }
            set { _grossMoInc = value; }
        }

        double YearPayments
        {
            get { return _yearPayments; }
            set { _yearPayments = value; }
        }



        public User()
        {
            PrLoan = 0;
            InitHomeMV = 0;
            TermLen = 0;
            APR = 0;
            DownPayment = 0;
            GrossMoInc = 0;
        }

        /*public User(double PrLoan, double InitHomeMV, double TermLen, double APR, double DownPayment, double GrossMoInc, double YearPayments)
        {
            _prLoan = PrLoan;
            _initHomeMV = InitHomeMV;
            _termLen = TermLen;
            _aPR = APR;
            _downPayment = DownPayment;
            _grossMoInc = GrossMoInc;
            _yearPayments = YearPayments;
          
        }*/

        public double InitLV(double PrLoan, double InitHomeMV, double TermLen, double APR, double DownPayment, double GrossMoInc, double YearPayments)
        {
            double P = ((PrLoan - DownPayment) * (APR / YearPayments));
            double I = Math.Pow((1 + APR / YearPayments), (YearPayments * TermLen));
            double D = I - 1;

            return P * I / D;
        }

        public double Equity(double InitHomeMV, double PrLoan)
        {

            return PrLoan - InitHomeMV;
        }

        public double EscrowHOA(double InitHomeMV, double HOA, double TermLen, double TermMonths)
        {
            //monthly
            return ((InitHomeMV * .0125) / TermMonths) + HOA;

        }

        public double Insurance(double initHomeMV, double Equity, double DownPayment, double TermLen, double TermMonths)
        {
            if (DownPayment < (.1 * initHomeMV) + Equity)
            {
                //monthly
                return ((.01 * initHomeMV) * TermLen) / TermMonths;
            }
            else
            {
                return 0;
            }
        }

        public double TotalLoanAmt(double InitPayment, double EscrowHOA, double Insurance)
        {

            return InitPayment + EscrowHOA + Insurance;

        }


    }
}
