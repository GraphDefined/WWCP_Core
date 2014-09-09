﻿/*
 * Copyright (c) 2014 Achim Friedland <achim.friedland@graphdefined.com>
 * This file is part of eMI3 Core <http://www.github.com/GraphDefined/eMI3>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;

#endregion

namespace com.graphdefined.eMI3
{

    /// <summary>
    /// The unique identification of an Electric Mobility Account (driver contract) (eMA_Id).
    /// </summary>
    public class eMA_Id : IEquatable<eMA_Id>, IComparable<eMA_Id>, IComparable
    {

        #region Data

        /// <summary>
        /// The internal identification.
        /// </summary>
        protected readonly String _Id;

        #endregion

        #region Properties

        #region Length

        /// <summary>
        /// Returns the length of the identificator.
        /// </summary>
        public UInt64 Length
        {
            get
            {
                return (UInt64) _Id.Length;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region eMA_Id()

        /// <summary>
        /// Generate a new Electric Vehicle Mobility Account (driver contract) identification (eMA_Id).
        /// </summary>
        public eMA_Id()
        {
            _Id = Guid.NewGuid().ToString();
        }

        #endregion

        #region eMA_Id(String)

        /// <summary>
        /// Generate a new Electric Vehicle Mobility Account (driver contract) identification (eMA_Id)
        /// based on the given string.
        /// </summary>
        public eMA_Id(String String)
        {
            _Id = String.Trim();
        }

        #endregion

        #endregion


        #region New

        /// <summary>
        /// Generate a new eMA_Id.
        /// </summary>
        public static eMA_Id New
        {
            get
            {
                return new eMA_Id(Guid.NewGuid().ToString());
            }
        }

        #endregion

        #region Parse(eMAId)

        /// <summary>
        /// Parse the given string as an eMA identification.
        /// </summary>
        public static eMA_Id Parse(String eMAId)
        {
            return new eMA_Id(eMAId);
        }

        #endregion

        #region TryParse(Text, out eMAId)

        /// <summary>
        /// Parse the given string as an eMA identification.
        /// </summary>
        public static Boolean TryParse(String Text, out eMA_Id eMAId)
        {
            try
            {
                eMAId = new eMA_Id(Text);
                return true;
            }
            catch (Exception e)
            {
                eMAId = null;
                return false;
            }
        }

        #endregion

        #region Clone

        /// <summary>
        /// Clone an eMA_Id.
        /// </summary>
        public eMA_Id Clone
        {
            get
            {
                return new eMA_Id(_Id);
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (eMA_Id1, eMA_Id2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMA_Id1">A eMA_Id.</param>
        /// <param name="eMA_Id2">Another eMA_Id.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (eMA_Id eMA_Id1, eMA_Id eMA_Id2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(eMA_Id1, eMA_Id2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) eMA_Id1 == null) || ((Object) eMA_Id2 == null))
                return false;

            return eMA_Id1.Equals(eMA_Id2);

        }

        #endregion

        #region Operator != (eMA_Id1, eMA_Id2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMA_Id1">A eMA_Id.</param>
        /// <param name="eMA_Id2">Another eMA_Id.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (eMA_Id eMA_Id1, eMA_Id eMA_Id2)
        {
            return !(eMA_Id1 == eMA_Id2);
        }

        #endregion

        #region Operator <  (eMA_Id1, eMA_Id2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMA_Id1">A eMA_Id.</param>
        /// <param name="eMA_Id2">Another eMA_Id.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (eMA_Id eMA_Id1, eMA_Id eMA_Id2)
        {

            if ((Object) eMA_Id1 == null)
                throw new ArgumentNullException("The given eMA_Id1 must not be null!");

            return eMA_Id1.CompareTo(eMA_Id2) < 0;

        }

        #endregion

        #region Operator <= (eMA_Id1, eMA_Id2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMA_Id1">A eMA_Id.</param>
        /// <param name="eMA_Id2">Another eMA_Id.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (eMA_Id eMA_Id1, eMA_Id eMA_Id2)
        {
            return !(eMA_Id1 > eMA_Id2);
        }

        #endregion

        #region Operator >  (eMA_Id1, eMA_Id2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMA_Id1">A eMA_Id.</param>
        /// <param name="eMA_Id2">Another eMA_Id.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (eMA_Id eMA_Id1, eMA_Id eMA_Id2)
        {

            if ((Object) eMA_Id1 == null)
                throw new ArgumentNullException("The given eMA_Id1 must not be null!");

            return eMA_Id1.CompareTo(eMA_Id2) > 0;

        }

        #endregion

        #region Operator >= (eMA_Id1, eMA_Id2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMA_Id1">A eMA_Id.</param>
        /// <param name="eMA_Id2">Another eMA_Id.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (eMA_Id eMA_Id1, eMA_Id eMA_Id2)
        {
            return !(eMA_Id1 < eMA_Id2);
        }

        #endregion

        #endregion

        #region IComparable<eMA_Id> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an eMA_Id.
            var eMA_Id = Object as eMA_Id;
            if ((Object) eMA_Id == null)
                throw new ArgumentException("The given object is not a eMA_Id!");

            return CompareTo(eMA_Id);

        }

        #endregion

        #region CompareTo(eMA_Id)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMA_Id">An object to compare with.</param>
        public Int32 CompareTo(eMA_Id eMA_Id)
        {

            if ((Object) eMA_Id == null)
                throw new ArgumentNullException("The given eMA_Id must not be null!");

            // Compare the length of the eMA_Ids
            var _Result = this.Length.CompareTo(eMA_Id.Length);

            // If equal: Compare Ids
            if (_Result == 0)
                _Result = _Id.CompareTo(eMA_Id._Id);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<eMA_Id> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object is an eMA_Id.
            var eMA_Id = Object as eMA_Id;
            if ((Object) eMA_Id == null)
                return false;

            return this.Equals(eMA_Id);

        }

        #endregion

        #region Equals(eMA_Id)

        /// <summary>
        /// Compares two eMA_Ids for equality.
        /// </summary>
        /// <param name="eMA_Id">A eMA_Id to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(eMA_Id eMA_Id)
        {

            if ((Object) eMA_Id == null)
                return false;

            return _Id.Equals(eMA_Id._Id);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return _Id.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return _Id.ToString();
        }

        #endregion

    }

}
