//Kaylee Nicasio
//knicasio1@student.cnm.edu
//C# Spring 2026
//P1: Ideal Gas Calc
//Program.cs

namespace KnicasioP1
{
    internal class Program
    {
        public const double KELVIN = 273.15;
        public const double R = 8.3145;
        public const double PSI_PER_PA = 0.0001450377;

        static void Main(string[] args)
        {
            
            //Call DisplayHeader:
            DisplayHeader();

            //CallGetMolecularWeights
            string[] gasNames = [];
            double[] molecularWeights = [];
            int count = 0;
            getMolecularWeights(ref gasNames, ref molecularWeights, out count);

            //Call DisplayGasNames: 
            DisplayGasNames(gasNames, count);
            //display gas names in three columns

            //*All within a do while loop
            double gasWeight = 0;
            string doAnother = "y";
            do
            {
                Console.Write("\nPlease enter a gas name: ");
                string inputGas = Console.ReadLine();

                gasWeight = GetMolecularWeightFromName(inputGas,
                    gasNames, molecularWeights, count);

                if (gasWeight == 0)
                {
                    continue;
                }

                Console.WriteLine("Enter the gas's volume in cubic meters: ");
                try
                {
                    double volume = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Enter the gas's mass in grams: ");

                    double mass = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Enter the gas's temperature in celcius: ");

                    double temp = Convert.ToDouble(Console.ReadLine());

                    double pressure = Pressure(mass, volume, temp, gasWeight);

                    DisplayPressure(pressure);
                }
                catch
                {
                    continue;
                }

                Console.WriteLine("Do you want to do another? (y/n) ");
                doAnother = Console.ReadLine();  
            
            } while (doAnother == "y");
            Console.WriteLine("Thank you for using the Ideal Gas Calculater! c:");

            
        }

        //Diplay header method:
        public static void DisplayHeader()
        {
            Console.WriteLine("Author: Kaylee Nicasio");
            Console.WriteLine("Program: Ideal Gas Calculator\n");
            Console.WriteLine("Gas Names:");

        }

        //GetMolecularWeights method:
        static void getMolecularWeights(ref string[] gasNames, ref double[] molecularWeights,
                    out int count)
        {
            string? line;
            count = 0;

            try
            {
                //Pass the file path & name to StreamReader:
                StreamReader sr = new StreamReader("MolecularWeightsGasesAndVapors.csv");
                //Read the first line, but ommit header:
                line = sr.ReadLine();
                line = sr.ReadLine();
                //Continue to read until end of file:
                while (line != null)
                {
                    string[] elements = line.Split(',');
                    //Split & show weights
                    //Console.WriteLine($"Gas Name: {elements[0]} | \t\tMolecular Weights: {elements[1]}");

                    Array.Resize(ref gasNames, gasNames.Length + 1);
                    Array.Resize(ref molecularWeights, molecularWeights.Length + 1);
                    gasNames[count] = elements[0];
                    molecularWeights[count] = Convert.ToDouble(elements[1]);
                    count++;
                
                    line = sr.ReadLine();

                }

                //Close the file:
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }


        }

        //DisplayGasNames:
        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            for (int idx=0; idx < countGases / 3; idx++)
            {
                for (int col=0; col<3; col++)
                {
                    // If the next column will exceed the total number 
                    // of gasNames, break the loop, we're done.
                    if (idx * 3 + col + 1 < countGases)
                    {
                        Console.Write(gasNames[idx * 3 + col]);
                    }
                    if (col < 2)
                    {
                        Console.Write(" | ");
                    }
                }
                Console.Write('\n');
            }
        }

        private static double GetMolecularWeightFromName(string gasName,
            string[] gasNames, double[] molecularWeights, int countGases)
        {
            
            // Iterate through the arrays and compare string names
            for (int i = 0; i < countGases; i++)
            {
                string curGas = gasNames[i];
                
                // If this is our gas, display the weight and break 
                if (curGas.ToUpper() == gasName.ToUpper())
                {
                    //Console.WriteLine(molecularWeights[i]);
                    return molecularWeights[i];
                }
            }
            
            return 0; 
        }

        //Pressure:
        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {
            double P = 0;

            P = (NumberOfMoles(mass, molecularWeight) * R * celsiusToKelvin(KELVIN, temp)) / vol;
            return P;
        }

        //Number of moles:
        static double NumberOfMoles(double mass, double molecularWeight)
        {
            return mass / molecularWeight;
        }

        //CelciusToKelvin:
        public static double celsiusToKelvin(double tempKelvin, double tempCelius)
        {
            tempKelvin = tempCelius + KELVIN;
            return tempKelvin;
        }


        //DisplayPressure:
        private static void DisplayPressure(double pressure)
        {
            double psi = PaToPSI(pressure);
            Console.WriteLine("\nYour results are: ");
            Console.WriteLine($"Pascals: {pressure}");
            Console.WriteLine($"PSI: {psi}");
        }

        //PaToPSI:
        static double PaToPSI(double pascals)
        {
            return pascals * PSI_PER_PA;
        }

    }
}
