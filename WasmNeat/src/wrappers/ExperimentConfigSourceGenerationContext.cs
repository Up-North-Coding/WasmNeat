using System.Text.Json.Serialization;
using SharpNeat.Experiments.ConfigModels;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ExperimentConfig))]
internal partial class ExperimentConfigSourceGenerationContext : JsonSerializerContext
{
}
