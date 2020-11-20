using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsRDCalculator
/// </summary>
public class ClsRDCalculator
{
    double TotalMat;
    double TotalMat2 = 0;
	public ClsRDCalculator()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public double Calc_RD(double P, double R,double N,double T)
    {
        try
        {
            double count = 0;
            double co = T;
           
           
            R = R / 100;
            while (count < co)
            {
               // T = T / 12;
                double x=Math.Pow((1+R/N),N*(T/12));
                --T;
                TotalMat = P * x;
                count++;
                TotalMat2 = TotalMat + TotalMat2;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return TotalMat2;
    }

    public double Calc_IntRD(double FP,double T,double Per)
    {
        try
        {
            double totp = FP * Per;
            TotalMat = T - totp;
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return TotalMat;
    }

}