using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using SharpNeat;
using SharpNeat.Experiments;
using SharpNeat.IO;
using SharpNeat.IO.Models;
using SharpNeat.Neat;
using SharpNeat.Neat.EvolutionAlgorithm;
using SharpNeat.Neat.Genome.IO;

namespace WasmNeat
{
    public partial class MainJS
    {
        public static async Task Main()
        {
            if (!OperatingSystem.IsBrowser())
            {
                throw new PlatformNotSupportedException("This demo is expected to run on browser platform");
            }

            Console.WriteLine("Ready!");
        }

        [JSExport]
        public static void DoTheThing()
        {
            Console.WriteLine("I did the thing!");
        }

        public class User
        {
            public User() {
            }
            public int? ID { get; set; }
            public string? Name { get; set; }
            public string? Address { get; set; }
        }

        [JSExport]
        public static string RunExperiment(string json_config)
        {
            Console.WriteLine("Running Experiment!");

            XorExperimentFactory factory = new XorExperimentFactory();
            INeatExperiment<double>? experiment = factory.CreateExperiment(json_config);
            NeatEvolutionAlgorithm<double> ea = NeatUtils.CreateNeatEvolutionAlgorithm(experiment);
            ea.Initialise();
            NeatPopulation<double> neatPop = ea.Population;

            for (int i = 0; i < 10_000; i++)
            {
                ea.PerformOneGeneration();
                if (neatPop.Stats.BestFitness.PrimaryFitness > 13.9)
                {
                    break;
                }
                Console.WriteLine($"{ea.Stats.Generation} {neatPop.Stats.BestFitness.PrimaryFitness} {neatPop.Stats.MeanComplexity} {ea.ComplexityRegulationMode} {neatPop.Stats.MeanFitness}");
            }
            Console.WriteLine("FINISHED");

            NetFileModel model = NeatGenomeConverter.ToNetFileModel(ea.Population.BestGenome);
            MemoryStream net_file = new MemoryStream();
            NetFile.Save(model, net_file);
            net_file.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(net_file);
            string text = reader.ReadToEnd();

            return text;
        }

        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.Unicode.GetBytes(value ?? ""));
        }
    }
}