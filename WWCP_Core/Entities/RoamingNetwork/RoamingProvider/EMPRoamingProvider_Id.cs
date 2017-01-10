﻿/*
 * Copyright (c) 2014-2017 GraphDefined GmbH <achim.friedland@graphdefined.com>
 * This file is part of WWCP Core <https://github.com/OpenChargingCloud/WWCP_Core>
 *
 * Licensed under the Affero GPL license, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/agpl.html
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Text.RegularExpressions;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace org.GraphDefined.WWCP
{

    /// <summary>
    /// The unique identification of a e-mobility roaming provider.
    /// </summary>
    public class EMPRoamingProvider_Id : IId,
                                         IEquatable <EMPRoamingProvider_Id>,
                                         IComparable<EMPRoamingProvider_Id>,
                                         IComparable

    {

        #region Data

        /// <summary>
        /// The internal identification.
        /// </summary>
        private readonly String InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// The length of the e-mobility roaming provider identificator.
        /// </summary>
        public UInt64 Length
            => (UInt64) InternalId.Length;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new e-mobility roaming provider identification.
        /// based on the given string.
        /// </summary>
        private EMPRoamingProvider_Id(String Text)
        {
            InternalId = Text;
        }

        #endregion


        #region Parse(Text)

        /// <summary>
        /// Parse the given string as a e-mobility roaming provider identification.
        /// </summary>
        /// <param name="Text">A text representation of a e-mobility roaming provider identification.</param>
        public static EMPRoamingProvider_Id Parse(String Text)
        {

            #region Initial checks

            if (Text != null)
                Text = Text.Trim();

            if (Text.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(Text), "The given text representation of a e-mobility roaming provider identification must not be null or empty!");

            #endregion

            return new EMPRoamingProvider_Id(Text);

        }

        #endregion

        #region TryParse(Text, out EMPRoamingProviderId)

        /// <summary>
        /// Parse the given string as a e-mobility roaming provider identification.
        /// </summary>
        /// <param name="Text">A text representation of a e-mobility roaming provider identification.</param>
        /// <param name="EMPRoamingProviderId">The parsed e-mobility roaming provider identification.</param>
        public static Boolean TryParse(String Text, out EMPRoamingProvider_Id EMPRoamingProviderId)
        {

            #region Initial checks

            if (Text != null)
                Text = Text.Trim();

            if (Text.IsNullOrEmpty())
            {
                EMPRoamingProviderId = default(EMPRoamingProvider_Id);
                return false;
            }

            #endregion

            try
            {

                EMPRoamingProviderId = new EMPRoamingProvider_Id(Text);

                return true;

            }

#pragma warning disable RCS1075  // Avoid empty catch clause that catches System.Exception.
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch (Exception)
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
#pragma warning restore RCS1075  // Avoid empty catch clause that catches System.Exception.
            { }

            EMPRoamingProviderId = default(EMPRoamingProvider_Id);
            return false;

        }

        #endregion

        #region Clone

        /// <summary>
        /// Clone this e-mobility roaming provider identification.
        /// </summary>
        public EMPRoamingProvider_Id Clone

            => new EMPRoamingProvider_Id(
                   new String(InternalId.ToCharArray())
               );

        #endregion


        #region Operator overloading

        #region Operator == (EMPRoamingProviderId1, EMPRoamingProviderId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EMPRoamingProviderId1">A e-mobility roaming provider identification.</param>
        /// <param name="EMPRoamingProviderId2">Another e-mobility roaming provider identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (EMPRoamingProvider_Id EMPRoamingProviderId1, EMPRoamingProvider_Id EMPRoamingProviderId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(EMPRoamingProviderId1, EMPRoamingProviderId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) EMPRoamingProviderId1 == null) || ((Object) EMPRoamingProviderId2 == null))
                return false;

            return EMPRoamingProviderId1.Equals(EMPRoamingProviderId2);

        }

        #endregion

        #region Operator != (EMPRoamingProviderId1, EMPRoamingProviderId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EMPRoamingProviderId1">A e-mobility roaming provider identification.</param>
        /// <param name="EMPRoamingProviderId2">Another e-mobility roaming provider identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (EMPRoamingProvider_Id EMPRoamingProviderId1, EMPRoamingProvider_Id EMPRoamingProviderId2)
            => !(EMPRoamingProviderId1 == EMPRoamingProviderId2);

        #endregion

        #region Operator <  (EMPRoamingProviderId1, EMPRoamingProviderId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EMPRoamingProviderId1">A e-mobility roaming provider identification.</param>
        /// <param name="EMPRoamingProviderId2">Another e-mobility roaming provider identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (EMPRoamingProvider_Id EMPRoamingProviderId1, EMPRoamingProvider_Id EMPRoamingProviderId2)
        {

            if ((Object) EMPRoamingProviderId1 == null)
                throw new ArgumentNullException(nameof(EMPRoamingProviderId1), "The given EMPRoamingProviderId1 must not be null!");

            return EMPRoamingProviderId1.CompareTo(EMPRoamingProviderId2) < 0;

        }

        #endregion

        #region Operator <= (EMPRoamingProviderId1, EMPRoamingProviderId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EMPRoamingProviderId1">A e-mobility roaming provider identification.</param>
        /// <param name="EMPRoamingProviderId2">Another e-mobility roaming provider identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (EMPRoamingProvider_Id EMPRoamingProviderId1, EMPRoamingProvider_Id EMPRoamingProviderId2)
            => !(EMPRoamingProviderId1 > EMPRoamingProviderId2);

        #endregion

        #region Operator >  (EMPRoamingProviderId1, EMPRoamingProviderId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EMPRoamingProviderId1">A e-mobility roaming provider identification.</param>
        /// <param name="EMPRoamingProviderId2">Another e-mobility roaming provider identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (EMPRoamingProvider_Id EMPRoamingProviderId1, EMPRoamingProvider_Id EMPRoamingProviderId2)
        {

            if ((Object) EMPRoamingProviderId1 == null)
                throw new ArgumentNullException(nameof(EMPRoamingProviderId1), "The given EMPRoamingProviderId1 must not be null!");

            return EMPRoamingProviderId1.CompareTo(EMPRoamingProviderId2) > 0;

        }

        #endregion

        #region Operator >= (EMPRoamingProviderId1, EMPRoamingProviderId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EMPRoamingProviderId1">A e-mobility roaming provider identification.</param>
        /// <param name="EMPRoamingProviderId2">Another e-mobility roaming provider identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (EMPRoamingProvider_Id EMPRoamingProviderId1, EMPRoamingProvider_Id EMPRoamingProviderId2)
            => !(EMPRoamingProviderId1 < EMPRoamingProviderId2);

        #endregion

        #endregion

        #region IComparable<EMPRoamingProviderId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException(nameof(Object), "The given object must not be null!");

            if (!(Object is EMPRoamingProvider_Id))
                throw new ArgumentException("The given object is not a e-mobility roaming provider identification!",
                                            nameof(Object));

            return CompareTo((EMPRoamingProvider_Id) Object);

        }

        #endregion

        #region CompareTo(EMPRoamingProviderId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EMPRoamingProviderId">An object to compare with.</param>
        public Int32 CompareTo(EMPRoamingProvider_Id EMPRoamingProviderId)
        {

            if ((Object) EMPRoamingProviderId == null)
                throw new ArgumentNullException(nameof(EMPRoamingProviderId),  "The given e-mobility roaming provider identification must not be null!");

            // Compare the length of the EMPRoamingProviderIds
            var _Result = this.Length.CompareTo(EMPRoamingProviderId.Length);

            if (_Result == 0)
                _Result = String.Compare(InternalId, EMPRoamingProviderId.InternalId, StringComparison.Ordinal);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<EMPRoamingProviderId> Members

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

            if (!(Object is EMPRoamingProvider_Id))
                return false;

            return Equals((EMPRoamingProvider_Id) Object);

        }

        #endregion

        #region Equals(EMPRoamingProviderId)

        /// <summary>
        /// Compares two EMPRoamingProviderIds for equality.
        /// </summary>
        /// <param name="EMPRoamingProviderId">A e-mobility roaming provider identification to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(EMPRoamingProvider_Id EMPRoamingProviderId)
        {

            if ((Object) EMPRoamingProviderId == null)
                return false;

            return InternalId.Equals(EMPRoamingProviderId.InternalId);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
            => InternalId.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
            => InternalId;

        #endregion

    }

}