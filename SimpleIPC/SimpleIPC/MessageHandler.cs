using System;

namespace SimpleIPC
{
    /// <summary>
    /// 	The delegate used for handling cross AppDomain communications.
    /// </summary>
    /// <param name = "sender">The event sender.</param>
    /// <param name = "e">The event args containing the DataGram data.</param>
    public delegate void MessageHandler(object sender, MessageEventArgs e);

    /// <summary>
    ///   The event args used by the message handler. This enables DataGram data 
    ///   to be passed to the handler.
    /// </summary>
    public sealed class MessageEventArgs : EventArgs
    {
        #region Constants and Fields

        /// <summary>
        ///   Stores the DataGram containing message and channel data.
        /// </summary>
        private readonly DataGram dataGram;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Constructor used to create a new instance from a DataGram struct.
        /// </summary>
        /// <param name = "dataGram">The DataGram instance.</param>
        public MessageEventArgs(DataGram dataGram)
        {
            this.dataGram = dataGram;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets the DataGram associated with this instance.
        /// </summary>
        public DataGram DataGram
        {
            get { return dataGram; }
        }

        #endregion
    }
}
