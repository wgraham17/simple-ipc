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
using TheCodeKing.Utils.Contract;

namespace SimpleIPC
{
    public class TypedDataGram<T> where T : class
    {
        #region Constants and Fields

        private readonly DataGram dataGram;
        
        #endregion

        #region Constructors and Destructors

        private TypedDataGram(DataGram dataGram)
        {
            Validate.That(dataGram).IsNotNull();
            
            this.dataGram = dataGram;
        }

        #endregion

        #region Properties
        
        public string Channel
        {
            get { return dataGram.Channel; }
        }

        public bool IsValid
        {
            get { return Message != null; }
        }

        public T Message
        {
            get { return JsonConvert.DeserializeObject<T>(dataGram.Message); }
        }

        #endregion

        #region Operators

        public static implicit operator DataGram(TypedDataGram<T> dataGram)
        {
            if (dataGram == null)
            {
                return null;
            }
            return dataGram.dataGram;
        }

        public static implicit operator TypedDataGram<T>(DataGram dataGram)
        {
            if (dataGram == null)
            {
                return null;
            }

            return new TypedDataGram<T>(dataGram);
        }

        #endregion
    }
}