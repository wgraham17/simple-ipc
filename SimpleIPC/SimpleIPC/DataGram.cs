namespace SimpleIPC
{
    using Newtonsoft.Json;
    using TheCodeKing.Utils.Contract;

    /// <summary>
    /// The data class that is passed between AppDomain boundaries. This is
    /// sent as a delimited string containing the channel and message.
    /// </summary>
    public class DataGram
    {
        private static readonly string ModelVersion = "1.0";

        /// <summary>
        /// Constructor which creates the data gram from a message and channel name.
        /// </summary>
        /// <param name = "channel">The channel through which the message will be sent.</param>
        /// <param name = "assemblyQualifiedName">The data type used for deserialization hints.</param>
        /// <param name = "message">The string message to send.</param>
        public DataGram(string channel, string message)
        {
            Validate.That(channel, "channel").IsNotNullOrEmpty();
            Validate.That(message, "message").IsNotNullOrEmpty();

            this.Channel = channel;
            this.Message = message;
            this.Version = ModelVersion;
        }

        internal DataGram()
        {
            Version = ModelVersion;
        }
        
        /// <summary>
        /// Gets the DataGram version.
        /// </summary>
        [JsonProperty("v")]
        public string Version { get; protected set; }

        /// <summary>
        /// Gets the channel name.
        /// </summary>
        [JsonProperty("c")]
        public string Channel { get; protected set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        [JsonProperty("m")]
        public string Message { get; protected set; }

        /// <summary>
        /// Indicates whether the DataGram contains valid data.
        /// </summary>
        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(Channel) && !string.IsNullOrEmpty(Message); }
        }

        /// <summary>
        /// Converts the instance to the string delimited format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(Channel, ":", Message);
        }
    }
}