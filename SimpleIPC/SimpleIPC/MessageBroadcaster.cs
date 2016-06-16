/*=============================================================================
*
*	(C) Copyright 2013, Michael Carlisle (mike.carlisle@thecodeking.co.uk)
*
*   http://www.TheCodeKing.co.uk
*  
*	All rights reserved.
*	The code and information is provided "as-is" without waranty of any kind,
*	either expressed or implied.
*
*=============================================================================
*/
using Newtonsoft.Json;
using System;
using TheCodeKing.Utils.Contract;

namespace SimpleIPC
{
    /// <summary>
    /// 	The implementation used to broadcast messages acorss appDomain and process boundaries
    /// 	using the Windows Messaging implementation. Non-form based application are not supported.
    /// </summary>
    public sealed class MessageBroadcaster
    {
        public void SendToChannel<T>(string channelName, T message)
        {
            Validate.That(channelName).IsNotNullOrEmpty();

            SendToChannel(channelName, JsonConvert.SerializeObject(message));
        }
        
        private void SendToChannel(string channelName, string message)
        {
            Validate.That(channelName).IsNotNullOrEmpty();
            Validate.That(message).IsNotNullOrEmpty();

            // create a DataGram instance, and ensure memory is freed
            using (var dataGram = new WinMsgDataGram(channelName, message))
            {
                // Allocate the DataGram to a memory address contained in COPYDATASTRUCT
                Native.COPYDATASTRUCT dataStruct = dataGram.ToStruct();

                // Use a filter with the EnumWindows class to get a list of windows containing
                // a property name that matches the destination channel. These are the listening
                // applications.
                var filter = new WindowEnumFilter(MessageListener.GetChannelKey(channelName));

                var winEnum = new WindowsEnum(filter.WindowFilterHandler);

                foreach (var hWnd in winEnum.Enumerate())
                {
                    IntPtr outPtr;
                    // For each listening window, send the message data. Return if hang or unresponsive within 1 sec.
                    Native.SendMessageTimeout(hWnd, Native.WM_COPYDATA, IntPtr.Zero, ref dataStruct,
                                              Native.SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out outPtr);
                }
            }
        }
    }
}