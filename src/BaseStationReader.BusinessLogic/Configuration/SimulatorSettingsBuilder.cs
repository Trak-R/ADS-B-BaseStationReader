using System.Globalization;
using BaseStationReader.Entities.Config;
using BaseStationReader.Entities.Logging;
using BaseStationReader.Interfaces.Config;

namespace BaseStationReader.BusinessLogic.Configuration
{
    public class SimulatorSettingsBuilder : ConfigReader<SimulatorApplicationSettings>, ISimulatorSettingsBuilder
    {
        /// <summary>
        /// Construct the application settings from the configuration file and any command line arguments
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="configPath"></param>
        /// <returns></returns>
        public SimulatorApplicationSettings BuildSettings(ICommandLineParser parser, string configPath)
        {
            // Read the config file to provide default settings
            var settings = base.Read(configPath);

            // Environment variables provide container-friendly overrides while command-line values
            // below retain the highest precedence.
            var environmentPort = Environment.GetEnvironmentVariable("ApplicationSettings__Port");
            if (int.TryParse(environmentPort, out var port))
            {
                settings.Port = port;
            }

            var environmentLogFile = Environment.GetEnvironmentVariable("ApplicationSettings__LogFile");
            if (!string.IsNullOrWhiteSpace(environmentLogFile))
            {
                settings.LogFile = environmentLogFile;
            }

            var environmentSendInterval = Environment.GetEnvironmentVariable("ApplicationSettings__SendInterval");
            if (int.TryParse(environmentSendInterval, out var sendInterval))
            {
                settings.SendInterval = sendInterval;
            }

            var environmentNumberOfAircraft = Environment.GetEnvironmentVariable("ApplicationSettings__NumberOfAircraft");
            if (int.TryParse(environmentNumberOfAircraft, out var numberOfAircraft))
            {
                settings.NumberOfAircraft = numberOfAircraft;
            }

            var environmentLatitude = Environment.GetEnvironmentVariable("ApplicationSettings__ReceiverLatitude");
            if (double.TryParse(environmentLatitude, NumberStyles.Float, CultureInfo.InvariantCulture, out var receiverLatitude))
            {
                settings.ReceiverLatitude = receiverLatitude;
            }

            var environmentLongitude = Environment.GetEnvironmentVariable("ApplicationSettings__ReceiverLongitude");
            if (double.TryParse(environmentLongitude, NumberStyles.Float, CultureInfo.InvariantCulture, out var receiverLongitude))
            {
                settings.ReceiverLongitude = receiverLongitude;
            }

            // Apply the command line values over the defaults
            var values = parser.GetValues(CommandLineOptionType.Port);
            if (values != null)
            {
                settings.Port = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.SendInterval);
            if (values != null)
            {
                settings.SendInterval = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.NumberOfAircraft);
            if (values != null)
            {
                settings.NumberOfAircraft = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MinimumAircraftLifespan);
            if (values != null)
            {
                settings.MinimumAircraftLifespan = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MaximumAircraftLifespan);
            if (values != null)
            {
                settings.MaximumAircraftLifespan = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.LogFile);
            if (values != null)
            {
                settings.LogFile = values[0];
            }

            values = parser.GetValues(CommandLineOptionType.MinimumLogLevel);
            if (values != null && Enum.TryParse<Severity>(values[0], out Severity minimumLogLevel))
            {
                settings.MinimumLogLevel = minimumLogLevel;
            }

            values = parser.GetValues(CommandLineOptionType.MinimumAltitude);
            if (values != null)
            {
                settings.MinimumAltitude = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MaximumAltitude);
            if (values != null)
            {
                settings.MaximumAltitude = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MinimumTakeOffSpeed);
            if (values != null)
            {
                settings.MinimumTakeOffSpeed = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MaximumTakeOffSpeed);
            if (values != null)
            {
                settings.MaximumTakeOffSpeed = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MinimumApproachSpeed);
            if (values != null)
            {
                settings.MinimumApproachSpeed = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MaximumApproachSpeed);
            if (values != null)
            {
                settings.MaximumApproachSpeed = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MinimumCruisingSpeed);
            if (values != null)
            {
                settings.MinimumCruisingSpeed = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MaximumCruisingSpeed);
            if (values != null)
            {
                settings.MaximumCruisingSpeed = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MinimumClimbRate);
            if (values != null)
            {
                settings.MinimumClimbRate = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MaximumClimbRate);
            if (values != null)
            {
                settings.MaximumClimbRate = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MinimumDescentRate);
            if (values != null)
            {
                settings.MinimumDescentRate = decimal.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MaximumDescentRate);
            if (values != null)
            {
                settings.MaximumDescentRate = decimal.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.MaximumInitialRange);
            if (values != null)
            {
                settings.MaximumInitialRange = int.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.ReceiverLatitude);
            if (values != null)
            {
                settings.ReceiverLatitude = double.Parse(values[0]);
            }

            values = parser.GetValues(CommandLineOptionType.ReceiverLongitude);
            if (values != null)
            {
                settings.ReceiverLongitude = double.Parse(values[0]);
            }

            return settings;
        }
    }
}