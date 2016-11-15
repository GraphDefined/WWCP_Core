﻿/*
 * Copyright (c) 2014-2016 GraphDefined GmbH <achim.friedland@graphdefined.com>
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
    /// The unique identification of an Electric Mobility Account (driver contract) (eMAId).
    /// </summary>
    public struct eMobilityAccount_Id : IId,
                                        IEquatable<eMobilityAccount_Id>,
                                        IComparable<eMobilityAccount_Id>
    {

        #region Data

        /// <summary>
        /// The regular expression for parsing an e-mobility contract identification.
        /// </summary>
        public static readonly Regex eMobilityAccountId_RegEx  = new Regex(@"^([A-Za-z]{2}\*[A-Za-z0-9]{3})\*([A-Za-z0-9]{6})\*([0-9|X])$ |"  +   // Hubject DIN STAR:  DE*BMW*0010LY*3
                                                                           @"^([A-Za-z]{2}-[A-Za-z0-9]{3})-([A-Za-z0-9]{6})-([0-9|X])$ |"     +   // Hubject DIN HYPEN: DE-BMW-0010LY-3
                                                                           @"^([A-Za-z]{2}[A-Za-z0-9]{3})([A-Za-z0-9]{6})([0-9|X])$ |"        +   // Hubject DIN:       DEBMW0010LY3

                                                                           @"^([A-Za-z]{2}-[A-Za-z0-9]{3})-C([A-Za-z0-9]{8})-([0-9|X])$ |"    +   // Hubject ISO Hypen: DE-BMW-001000LY-3
                                                                           @"^([A-Za-z]{2}[A-Za-z0-9]{3})C([A-Za-z0-9]{8})([0-9|X])$ |"       +   // Hubject ISO:       DEBMW001000LY3

                                                                           @"^([A-Za-z]{2}-[A-Za-z0-9]{3})-([A-Za-z0-9]{9})-([A-Za-z0-9])$ |" +   // e-clearing:
                                                                           @"^([A-Za-z]{2}[A-Za-z0-9]{3})([A-Za-z0-9]{9})([A-Za-z0-9])$ |" +
                                                                           @"^([A-Za-z]{2}-[A-Za-z0-9]{3})-([A-Za-z0-9]{9})$ |" +
                                                                           @"^([A-Za-z]{2}[A-Za-z0-9]{3})([A-Za-z0-9]{9})$",
                                                                           RegexOptions.IgnorePatternWhitespace);

        /// <summary>
        /// The regular expression for parsing an e-mobility contract identification suffix.
        /// </summary>
        public static readonly Regex IdSuffix_RegEx  = new Regex(@"^[A-Za-z0-9]{9}-[A-Za-z0-9]$ | ^[A-Za-z0-9]{9}[A-Za-z0-9]$ | ^[A-Za-z0-9]{9}$",
                                                                 RegexOptions.IgnorePatternWhitespace);

        #endregion

        #region Properties

        /// <summary>
        /// The e-mobility provider identification.
        /// </summary>
        public eMobilityProvider_Id  ProviderId    { get; }

        /// <summary>
        /// The suffix of the identification.
        /// </summary>
        public String                Suffix        { get; }

        /// <summary>
        /// An optional check digit of the e-mobility contract identification.
        /// </summary>
        public Char?                 CheckDigit    { get; }

        /// <summary>
        /// Returns the length of the identificator.
        /// </summary>
        public UInt64 Length

            => ProviderId.Length +
               1UL +
               (UInt64) Suffix.Length +
               (CheckDigit.HasValue ? 2UL : 0UL);

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Generate a new e-mobility contract identification
        /// based on the given string.
        /// </summary>
        /// <param name="ProviderId">The unique identification of an e-mobility provider.</param>
        /// <param name="IdSuffix">The suffix of the e-mobility contract identification.</param>
        /// <param name="CheckDigit">An optional check digit of the e-mobility contract identification.</param>
        private eMobilityAccount_Id(eMobilityProvider_Id  ProviderId,
                                    String                IdSuffix,
                                    Char?                 CheckDigit = null)
        {

            #region Initial checks

            if (IdSuffix.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(IdSuffix),  "The identification suffix must not be null or empty!");

            #endregion

            this.ProviderId  = ProviderId;
            this.Suffix      = IdSuffix;
            this.CheckDigit  = CheckDigit;

        }

        #endregion


        #region Parse(Text)

        /// <summary>
        /// Parse the given string as a contract identification.
        /// </summary>
        public static eMobilityAccount_Id Parse(String Text)
        {

            #region Initial checks

            if (Text.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(Text), "The parameter must not be null or empty!");

            #endregion

            var _MatchCollection = eMobilityAccountId_RegEx.Matches(Text);

            if (_MatchCollection.Count != 1)
                throw new ArgumentException("Illegal contract identification '" + Text + "'!");

            eMobilityProvider_Id _ProviderId;

            if (eMobilityProvider_Id.TryParse(_MatchCollection[0].Groups[1].Value, out _ProviderId))
                return new eMobilityAccount_Id(_ProviderId,
                                       _MatchCollection[0].Groups[2].Value,
                                       _MatchCollection[0].Groups[3].Value[0]);

            if (eMobilityProvider_Id.TryParse(_MatchCollection[0].Groups[4].Value, out _ProviderId))
                return new eMobilityAccount_Id(_ProviderId,
                                       _MatchCollection[0].Groups[5].Value,
                                       _MatchCollection[0].Groups[6].Value[0]);

            if (eMobilityProvider_Id.TryParse(_MatchCollection[0].Groups[7].Value, out _ProviderId))
                return new eMobilityAccount_Id(_ProviderId,
                                       _MatchCollection[0].Groups[8].Value,
                                       _MatchCollection[0].Groups[9].Value[0]);

            if (eMobilityProvider_Id.TryParse(_MatchCollection[0].Groups[10].Value, out _ProviderId))
                return new eMobilityAccount_Id(_ProviderId,
                                       _MatchCollection[0].Groups[11].Value);


            throw new ArgumentException("Illegal contract identification '" + Text + "'!");

        }

        #endregion

        #region Parse(ProviderId, IdSuffix)

        /// <summary>
        /// Parse the given string as an contract identification.
        /// </summary>
        /// <param name="ProviderId">The unique identification of an e-mobility provider.</param>
        /// <param name="IdSuffix">The suffix of the e-mobility contract identification.</param>
        public static eMobilityAccount_Id Parse(eMobilityProvider_Id  ProviderId,
                                                String                IdSuffix)

            => new eMobilityAccount_Id(ProviderId,
                                       IdSuffix);

        #endregion

        #region TryParse(Text, out eMobilityAccountId)

        /// <summary>
        /// Parse the given string as an e-mobility contract identification.
        /// </summary>
        /// <param name="Text">A text representation of an e-mobility contract identification.</param>
        /// <param name="eMobilityAccountId">The parsed e-mobility contract identification.</param>
        public static Boolean TryParse(String Text, out eMobilityAccount_Id eMobilityAccountId)
        {

            #region Initial checks

            if (Text.IsNullOrEmpty())
            {
                eMobilityAccountId = default(eMobilityAccount_Id);
                return false;
            }

            #endregion

            try
            {

                eMobilityAccountId = default(eMobilityAccount_Id);

                var _MatchCollection = eMobilityAccountId_RegEx.Matches(Text.Trim().ToUpper());

                if (_MatchCollection.Count != 1)
                    return false;

                eMobilityProvider_Id _Provider;

                // ISO...
                if (eMobilityProvider_Id.TryParse(_MatchCollection[0].Groups[1].Value, out _Provider))
                {

                    eMobilityAccountId = new eMobilityAccount_Id(_Provider,
                                                                 _MatchCollection[0].Groups[2].Value,
                                                                 _MatchCollection[0].Groups[3].Value[0]);

                    return true;

                }

                // DIN...
                if (eMobilityProvider_Id.TryParse(_MatchCollection[0].Groups[4].Value, out _Provider))
                {

                    eMobilityAccountId = new eMobilityAccount_Id(_Provider,
                                                                 _MatchCollection[0].Groups[5].Value,
                                                                 _MatchCollection[0].Groups[6].Value[0]);

                    return true;

                }

                // Without check digit...
                if (eMobilityProvider_Id.TryParse(_MatchCollection[0].Groups[7].Value, out _Provider))
                {

                    eMobilityAccountId = new eMobilityAccount_Id(_Provider,
                                                 _MatchCollection[0].Groups[8].Value);

                    return true;

                }

            }
#pragma warning disable RCS1075  // Avoid empty catch clause that catches System.Exception.
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch (Exception)
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
#pragma warning restore RCS1075  // Avoid empty catch clause that catches System.Exception.
            { }

            eMobilityAccountId = default(eMobilityAccount_Id);
            return false;

        }


        /// <summary>
        /// Parse the given string as an e-mobility contract identification.
        /// </summary>
        /// <param name="Text">A text representation of an e-mobility contract identification.</param>
        /// <param name="eMobilityAccountId">The parsed e-mobility contract identification.</param>
        public static Boolean TryParse(String Text, out eMobilityAccount_Id? eMobilityAccountId)
        {

            eMobilityAccount_Id eMAId;

            if (TryParse(Text, out eMAId))
            {
                eMobilityAccountId = eMAId;
                return true;
            }

            eMobilityAccountId = new eMobilityAccount_Id?();

            return false;

        }

        #endregion

        #region Clone

        /// <summary>
        /// Clone this EVSE identification.
        /// </summary>
        public eMobilityAccount_Id Clone

            => new eMobilityAccount_Id(ProviderId.Clone,
                                       Suffix);

        #endregion


        #region Operator overloading

        #region Operator == (eMobilityAccountId1, eMobilityAccountId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMobilityAccountId1">A contract identification.</param>
        /// <param name="eMobilityAccountId2">Another contract identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (eMobilityAccount_Id eMobilityAccountId1, eMobilityAccount_Id eMobilityAccountId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(eMobilityAccountId1, eMobilityAccountId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) eMobilityAccountId1 == null) || ((Object) eMobilityAccountId2 == null))
                return false;

            if ((Object) eMobilityAccountId1 == null)
                throw new ArgumentNullException(nameof(eMobilityAccountId1),  "The given contract identification must not be null!");

            return eMobilityAccountId1.Equals(eMobilityAccountId2);

        }

        #endregion

        #region Operator != (eMobilityAccountId1, eMobilityAccountId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMobilityAccountId1">A contract identification.</param>
        /// <param name="eMobilityAccountId2">Another contract identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (eMobilityAccount_Id eMobilityAccountId1, eMobilityAccount_Id eMobilityAccountId2)
            => !(eMobilityAccountId1 == eMobilityAccountId2);

        #endregion

        #region Operator <  (eMobilityAccountId1, eMobilityAccountId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMobilityAccountId1">A contract identification.</param>
        /// <param name="eMobilityAccountId2">Another contract identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (eMobilityAccount_Id eMobilityAccountId1, eMobilityAccount_Id eMobilityAccountId2)
        {

            if ((Object) eMobilityAccountId1 == null)
                throw new ArgumentNullException(nameof(eMobilityAccountId1),  "The given contract identification must not be null!");

            return eMobilityAccountId1.CompareTo(eMobilityAccountId2) < 0;

        }

        #endregion

        #region Operator <= (eMobilityAccountId1, eMobilityAccountId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMobilityAccountId1">A contract identification.</param>
        /// <param name="eMobilityAccountId2">Another contract identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (eMobilityAccount_Id eMobilityAccountId1, eMobilityAccount_Id eMobilityAccountId2)
            => !(eMobilityAccountId1 > eMobilityAccountId2);

        #endregion

        #region Operator >  (eMobilityAccountId1, eMobilityAccountId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMobilityAccountId1">A contract identification.</param>
        /// <param name="eMobilityAccountId2">Another contract identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (eMobilityAccount_Id eMobilityAccountId1, eMobilityAccount_Id eMobilityAccountId2)
        {

            if ((Object) eMobilityAccountId1 == null)
                throw new ArgumentNullException(nameof(eMobilityAccountId1),  "The given contract identification must not be null!");

            return eMobilityAccountId1.CompareTo(eMobilityAccountId2) > 0;

        }

        #endregion

        #region Operator >= (eMobilityAccountId1, eMobilityAccountId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMobilityAccountId1">A contract identification.</param>
        /// <param name="eMobilityAccountId2">Another contract identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (eMobilityAccount_Id eMobilityAccountId1, eMobilityAccount_Id eMobilityAccountId2)
            => !(eMobilityAccountId1 < eMobilityAccountId2);

        #endregion

        #endregion

        #region IComparable<eMobilityAccountId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException(nameof(Object),  "The given object must not be null!");

            // Check if the given object is a contract identification.
            if (!(Object is eMobilityAccount_Id))
                throw new ArgumentException("The given object is not a eMobilityAccountId!", nameof(Object));

            return CompareTo((eMobilityAccount_Id) Object);

        }

        #endregion

        #region CompareTo(eMobilityAccountId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="eMobilityAccountId">An object to compare with.</param>
        public Int32 CompareTo(eMobilityAccount_Id eMobilityAccountId)
        {

            if ((Object) eMobilityAccountId == null)
                throw new ArgumentNullException(nameof(eMobilityAccountId),  "The given contract identification must not be null!");

            // Compare the length of the contract identifications
            var _Result = this.Length.CompareTo(eMobilityAccountId.Length);

            // If equal: Compare OperatorIds
            if (_Result == 0)
                _Result = ProviderId.CompareTo(eMobilityAccountId.ProviderId);

            // If equal: Compare contract identification suffix
            if (_Result == 0)
                _Result = String.Compare(Suffix, eMobilityAccountId.Suffix, StringComparison.Ordinal);

            // If equal: Compare contract check digit
            if (_Result == 0)
            {

                if (!CheckDigit.HasValue && !eMobilityAccountId.CheckDigit.HasValue)
                    _Result = 0;

                if ( CheckDigit.HasValue &&  eMobilityAccountId.CheckDigit.HasValue)
                    _Result = CheckDigit.Value.CompareTo(eMobilityAccountId.CheckDigit.Value);

            }

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<eMobilityAccountId> Members

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

            // Check if the given object is a contract identification.
            if (!(Object is eMobilityAccount_Id))
                return false;

            return this.Equals((eMobilityAccount_Id) Object);

        }

        #endregion

        #region Equals(eMobilityAccountId)

        /// <summary>
        /// Compares two contract identifications for equality.
        /// </summary>
        /// <param name="eMobilityAccountId">A contract identification to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(eMobilityAccount_Id eMobilityAccountId)
        {

            if ((Object) eMobilityAccountId == null)
                return false;

            return ProviderId.Equals(eMobilityAccountId.ProviderId) &&
                   Suffix.    Equals(eMobilityAccountId.Suffix)     &&

                   ((!CheckDigit.HasValue && !eMobilityAccountId.CheckDigit.HasValue) ||
                     (CheckDigit.HasValue &&  eMobilityAccountId.CheckDigit.HasValue && CheckDigit.Value.Equals(eMobilityAccountId.CheckDigit.Value)));

        }

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {

                return ProviderId.GetHashCode() * 7 ^
                       Suffix.    GetHashCode() * 5 ^

                       (CheckDigit.HasValue
                            ? CheckDigit.GetHashCode()
                            : 0);

            }
        }

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {

            switch (ProviderId.Format)
            {

                case ProviderIdFormats.DIN_STAR:
                    return String.Concat(ProviderId.CountryCode.Alpha2Code, "*",
                                         ProviderId.ProviderId, "*",
                                         Suffix,
                                         CheckDigit.HasValue
                                             ? "*" + CheckDigit
                                             : "");

                default: // ISO_HYPHEN
                    return String.Concat(ProviderId.CountryCode.Alpha2Code, "-",
                                         ProviderId.ProviderId, "-",
                                         "C", Suffix,
                                         CheckDigit.HasValue
                                             ? "-" + CheckDigit
                                             : "");

            }

        }

        #endregion

    }

}