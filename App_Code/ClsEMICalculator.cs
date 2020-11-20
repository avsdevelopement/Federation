using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsEMICalculator
/// </summary>
public class ClsEMICalculator
{
    double Total;
	public ClsEMICalculator()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public double Calc_EMI(double N, double R, double P)
    {
        try
        {           
            double x, y;
            R = R / (12 * 100);
            //y=(Math.Pow((1+R),N)/Math.Pow((1+R),(N-1)));
            //Total = (P * R) * y;
            x = (P*R)*(Math.Pow((1 + R), N));
           // double n1=N-1;
           y = Math.Pow((1 + R),N);
           // double z = x / y;
           Total = x / (y-1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Total;
    }
}