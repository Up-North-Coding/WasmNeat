import { dotnet } from './dotnet.js'
const { getAssemblyExports, getConfig } = await dotnet.create();
const exports = await getAssemblyExports(getConfig().mainAssemblyName);
await dotnet.run();

const neat_settings = {
    "name": "Logical XOR",
    "isAcyclic": true,
    "activationFnName": "LeakyReLU",
    "evolutionAlgorithm": {
        "speciesCount": 10,
        "elitismProportion": 0.2,
        "selectionProportion": 0.2,
        "offspringAsexualProportion": 0.5,
        "offspringSexualProportion": 0.5,
        "interspeciesMatingProportion": 0.01
    },
    "reproductionAsexual": {
        "connectionWeightMutationProbability": 0.94,
        "addNodeMutationProbability": 0.01,
        "addConnectionMutationProbability": 0.025,
        "deleteConnectionMutationProbability": 0.025
    },
    "reproductionSexual": {
        "secondaryParentGeneProbability": 0.1
    },
    "populationSize": 150,
    "initialInterconnectionsProportion": 0.05,
    "connectionWeightScale": 5.0,
    "complexityRegulationStrategy": {
        "strategyName": "absolute",
        "complexityCeiling": 10,
        "minSimplifcationGenerations": 10
    },
    "degreeOfParallelism": 4,
    "enableHardwareAcceleratedNeuralNets": false,
    "enableHardwareAcceleratedActivationFunctions": false
};

setTimeout(async () => {
    const net_file = await exports.WasmNeat.MainJS.RunExperiment(JSON.stringify(neat_settings));
    console.log("Got the net file");
    console.log(net_file);
}, 2000);