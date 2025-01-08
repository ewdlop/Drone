using System;
using System.Collections.Generic;

namespace GlidingSystems
{
    // Base interface for all gliding objects
    public interface IGlider
    {
        double CalculateGlideRatio();
        double CalculateDescentRate(double altitude, double airSpeed);
        bool IsAirworthy(WeatherCondition conditions);
    }

    // Weather conditions class
    public class WeatherCondition
    {
        public double WindSpeed { get; set; }        // m/s
        public double Temperature { get; set; }      // Celsius
        public double Humidity { get; set; }         // Percentage
        public bool HasThermals { get; set; }
        public double Visibility { get; set; }       // meters
    }

    // Aircraft Glider class
    public class AircraftGlider : IGlider
    {
        public double Wingspan { get; set; }         // meters
        public double WingArea { get; set; }         // square meters
        public double EmptyWeight { get; set; }      // kg
        public double MaxTakeoffWeight { get; set; } // kg
        public double BestGlideSpeed { get; set; }   // km/h

        public double CalculateGlideRatio()
        {
            // Typical glider L/D ratios range from 20:1 to 60:1
            return WingArea * 3.0 / EmptyWeight * 20;
        }

        public double CalculateDescentRate(double altitude, double airSpeed)
        {
            // Basic descent rate calculation (m/s)
            return airSpeed / (CalculateGlideRatio() * 3.6);
        }

        public bool IsAirworthy(WeatherCondition conditions)
        {
            return conditions.WindSpeed < 15 &&      // Max wind speed 15 m/s
                   conditions.Visibility > 5000 &&    // Minimum visibility 5km
                   conditions.Temperature > -10 &&    // Minimum temperature
                   conditions.Temperature < 35;       // Maximum temperature
        }
    }

    // Sugar Glider class (Petaurus breviceps)
    public class SugarGlider : IGlider
    {
        public double BodyLength { get; set; }       // cm
        public double MembraneArea { get; set; }     // square cm
        public double Weight { get; set; }           // grams
        public double TailLength { get; set; }       // cm

        public double CalculateGlideRatio()
        {
            // Sugar gliders typically achieve glide ratios of 1.5:1 to 2.5:1
            return (MembraneArea / (Weight / 100)) * 0.15;
        }

        public double CalculateDescentRate(double altitude, double airSpeed)
        {
            // Simplified descent rate for sugar glider (m/s)
            return airSpeed / (CalculateGlideRatio() * 2.5);
        }

        public bool IsAirworthy(WeatherCondition conditions)
        {
            return conditions.WindSpeed < 5 &&       // Max wind speed 5 m/s
                   conditions.Temperature > 15 &&     // Minimum temperature
                   conditions.Temperature < 30 &&     // Maximum temperature
                   !conditions.HasThermals;          // Avoid strong thermals
        }
    }

    // Parachute class
    public class Parachute : IGlider
    {
        public double CanopyDiameter { get; set; }   // meters
        public double CanopyArea { get; set; }       // square meters
        public double TotalWeight { get; set; }      // kg
        public string Type { get; set; }             // Round, RAM-air, etc.
        public double DragCoefficient { get; set; }

        public double CalculateGlideRatio()
        {
            // RAM-air parachutes can achieve 3:1 glide ratios
            if (Type.ToLower() == "ram-air")
                return 3.0;
            // Round parachutes have minimal glide ratio
            return 0.5;
        }

        public double CalculateDescentRate(double altitude, double airSpeed)
        {
            // Terminal velocity calculation for parachute
            double g = 9.81;  // gravitational acceleration
            double rho = 1.225 * Math.Exp(-altitude/10000);  // air density at altitude
            double terminalVelocity = Math.Sqrt((2 * TotalWeight * g) / 
                                              (rho * CanopyArea * DragCoefficient));
            return terminalVelocity;
        }

        public bool IsAirworthy(WeatherCondition conditions)
        {
            return conditions.WindSpeed < 10 &&      // Max wind speed 10 m/s
                   conditions.Visibility > 3000 &&    // Minimum visibility 3km
                   !conditions.HasThermals;          // Avoid strong thermals
        }
    }

    // Gliding performance analyzer
    public class GlidingAnalyzer
    {
        public static void AnalyzeGlider(IGlider glider, WeatherCondition conditions)
        {
            Console.WriteLine($"Glide Ratio: {glider.CalculateGlideRatio():F2}:1");
            Console.WriteLine($"Descent Rate at 1000m, 100km/h: {glider.CalculateDescentRate(1000, 100):F2} m/s");
            Console.WriteLine($"Airworthy in current conditions: {glider.IsAirworthy(conditions)}");
        }

        public static void CompareGliders(IGlider glider1, IGlider glider2, WeatherCondition conditions)
        {
            Console.WriteLine("Comparative Analysis:");
            Console.WriteLine($"Glider 1 vs Glider 2 Glide Ratio: " +
                            $"{glider1.CalculateGlideRatio():F2}:1 vs {glider2.CalculateGlideRatio():F2}:1");
            Console.WriteLine($"Descent Rate Difference: " +
                            $"{Math.Abs(glider1.CalculateDescentRate(1000, 100) - glider2.CalculateDescentRate(1000, 100)):F2} m/s");
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            var weatherConditions = new WeatherCondition
            {
                WindSpeed = 5,
                Temperature = 20,
                Humidity = 60,
                HasThermals = false,
                Visibility = 10000
            };

            var aircraftGlider = new AircraftGlider
            {
                Wingspan = 15,
                WingArea = 11,
                EmptyWeight = 300,
                MaxTakeoffWeight = 450,
                BestGlideSpeed = 90
            };

            var sugarGlider = new SugarGlider
            {
                BodyLength = 20,
                MembraneArea = 300,
                Weight = 150,
                TailLength = 15
            };

            var parachute = new Parachute
            {
                CanopyDiameter = 8,
                CanopyArea = 50,
                TotalWeight = 100,
                Type = "RAM-air",
                DragCoefficient = 1.2
            };

            Console.WriteLine("Aircraft Glider Analysis:");
            GlidingAnalyzer.AnalyzeGlider(aircraftGlider, weatherConditions);

            Console.WriteLine("\nSugar Glider Analysis:");
            GlidingAnalyzer.AnalyzeGlider(sugarGlider, weatherConditions);

            Console.WriteLine("\nParachute Analysis:");
            GlidingAnalyzer.AnalyzeGlider(parachute, weatherConditions);

            Console.WriteLine("\nComparative Analysis:");
            GlidingAnalyzer.CompareGliders(aircraftGlider, parachute, weatherConditions);
        }
    }
}
