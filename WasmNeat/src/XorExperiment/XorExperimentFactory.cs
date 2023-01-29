using System.Text.Json;
using SharpNeat.Experiments;
using SharpNeat.Experiments.ConfigModels;
using SharpNeat.IO;
using SharpNeat.NeuralNets;

public sealed class XorExperimentFactory
{
    /// <inheritdoc/>
    public string Id => "xor";

    /// <inheritdoc/>
    public INeatExperiment<double>? CreateExperiment(string jsonString)
    {
        ExperimentConfig? experimentConfig = JsonUtils.Deserialize<ExperimentConfig>(jsonString);

        // Create an evaluation scheme object for the XOR task.
        var evalScheme = new XorEvaluationScheme();

        // Create a NeatExperiment object with the evaluation scheme,
        // and assign some default settings (these can be overridden by config).
        var experiment = new NeatExperiment<double>(evalScheme, Id)
        {
            IsAcyclic = true,
            ActivationFnName = ActivationFunctionId.LeakyReLU.ToString()
        };

        // Apply configuration to the experiment instance.
        experiment.Configure(experimentConfig);
        return experiment;
    }
}