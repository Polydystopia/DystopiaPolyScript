using BepInEx.Logging;
using System.Text.Json;

namespace DystopiaPolyScript;

public class ServerConfig
{
    public string ServerUrl { get; init; } = "http://localhost:5051";
}

public static class Main
{
    private const string CONFIG_FILE_NAME = "polydystopia_server_config.json";
    private const string DEFAULT_SERVER_URL = "http://localhost:5051";

    public static void Load(ManualLogSource logger)
    {
        var serverUrl = LoadServerUrlFromFile(logger);

        BuildConfigHelper.GetSelectedBuildConfig().buildServerURL = BuildServerURL.Custom;
        BuildConfigHelper.GetSelectedBuildConfig().customServerURL = serverUrl;

        logger.LogInfo($"Polydystopia> Server URL set to: {serverUrl}");
    }

    private static string LoadServerUrlFromFile(ManualLogSource logger)
    {
        try
        {
            if (File.Exists(CONFIG_FILE_NAME))
            {
                var jsonContent = File.ReadAllText(CONFIG_FILE_NAME);
                var config = JsonSerializer.Deserialize<ServerConfig>(jsonContent);

                if (config != null && !string.IsNullOrEmpty(config.ServerUrl))
                {
                    logger.LogInfo($"Loaded server URL from {CONFIG_FILE_NAME}: {config.ServerUrl}");
                    return config.ServerUrl;
                }
            }

            var defaultConfig = new ServerConfig { ServerUrl = DEFAULT_SERVER_URL };
            var defaultJson =
                JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CONFIG_FILE_NAME, defaultJson);
            logger.LogInfo($"Polydystopia> Created default config file {CONFIG_FILE_NAME} with URL: {DEFAULT_SERVER_URL}");

            return DEFAULT_SERVER_URL;
        }
        catch (Exception ex)
        {
            logger.LogError($"Polydystopia> Error reading config file: {ex.Message}. Using default URL: {DEFAULT_SERVER_URL}");

            return DEFAULT_SERVER_URL;
        }
    }
}