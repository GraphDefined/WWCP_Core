﻿/*
 * Copyright (c) 2014-2020 GraphDefined GmbH <achim.friedland@graphdefined.com>
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
    /// The unique identification of an Electric Vehicle Supply Equipment (EVSE).
    /// </summary>
    public readonly struct EVSE_Id : IId,
                                     IEquatable<EVSE_Id>,
                                     IComparable<EVSE_Id>

    {

        #region Data

        //ToDo: Replace with better randomness!
        private static readonly Random _Random = new Random(DateTime.UtcNow.Millisecond);

        /// <summary>
        /// The regular expression for parsing an EVSE identification.
        /// </summary>                                            // new format:
        public static readonly Regex  EVSEId_RegEx      = new Regex(@"^([A-Za-z]{2}\*?[A-Za-z0-9]{3})\*?E([A-Za-z0-9\*]{1,30})$ | " +

                                                                    // old format:
                                                                    @"^(\+?[0-9]{1,5}\*[0-9]{3,6})\*?([A-Za-z0-9\*]{1,32})$",
                                                                    // Hubject ([A-Za-z]{2}\*?[A-Za-z0-9]{3}\*?E[A-Za-z0-9\*]{1,30})  |  (\+?[0-9]{1,3}\*[0-9]{3,6}\*[0-9\*]{1,32})
                                                                    // OCHP.eu                                                           /^\+[0-9]{1,3}\*?[A-Z0-9]{3}\*?[A-Z0-9\*]{0,40}(?=$)/i;
                                                                    // var valid_evse_warning= /^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9\*]*/; // look ahead: at least one upper and one lower case letter

                                                                    RegexOptions.IgnorePatternWhitespace);

        /// <summary>
        /// The regular expression for parsing an ISO EVSE identification suffix.
        /// </summary>
        public static readonly Regex IdSuffixISO_RegEx  = new Regex(@"^([A-Za-z0-9\*]{1,30})$",
                                                                    RegexOptions.IgnorePatternWhitespace);

        /// <summary>
        /// The regular expression for parsing a DIN EVSE identification suffix.
        /// </summary>
        public static readonly Regex IdSuffixDIN_RegEx  = new Regex(@"^([0-9\*]{1,32})$",
                                                                    RegexOptions.IgnorePatternWhitespace);

        #endregion

        #region Properties

        /// <summary>
        /// The charging station operator identification.
        /// </summary>
        public ChargingStationOperator_Id  OperatorId   { get; }

        /// <summary>
        /// The suffix of the identification.
        /// </summary>
        public String                      Suffix       { get; }

        /// <summary>
        /// The detected format of the EVSE identification.
        /// </summary>
        public OperatorIdFormats           Format
            => OperatorId.Format;

        /// <summary>
        /// Indicates whether this identification is null or empty.
        /// </summary>
        public Boolean IsNullOrEmpty
            => Suffix.IsNullOrEmpty();

        /// <summary>
        /// Returns the length of the identification.
        /// </summary>
        public UInt64 Length
        {
            get
            {

                switch (Format)
                {

                    case OperatorIdFormats.DIN:
                        return OperatorId.Length + 1 + (UInt64) Suffix.Length;

                    case OperatorIdFormats.ISO_STAR:
                        return OperatorId.Length + 2 + (UInt64) Suffix.Length;

                    default:  // ISO
                        return OperatorId.Length + 1 + (UInt64) Suffix.Length;

                }

            }
        }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Generate a new Electric Vehicle Supply Equipment (EVSE) identification
        /// based on the given charging station operator and identification suffix.
        /// </summary>
        /// <param name="OperatorId">The unique identification of a charging station operator.</param>
        /// <param name="Suffix">The suffix of the EVSE identification.</param>
        private EVSE_Id(ChargingStationOperator_Id  OperatorId,
                        String                      Suffix)
        {

            #region Initial checks

            if (Suffix.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(Suffix),  "The EVSE identification suffix must not be null or empty!");

            #endregion

            this.OperatorId  = OperatorId;
            this.Suffix      = Suffix;

        }

        #endregion


        #region Random(OperatorId, Mapper = null)

        /// <summary>
        /// Generate a new unique identification of an EVSE.
        /// </summary>
        /// <param name="OperatorId">The unique identification of a charging station operator.</param>
        /// <param name="Mapper">A delegate to modify the newly generated EVSE identification.</param>
        public static EVSE_Id Random(ChargingStationOperator_Id  OperatorId,
                                     Func<String, String>        Mapper  = null)


            => new EVSE_Id(OperatorId,
                           Mapper != null ? Mapper(_Random.RandomString(12)) : _Random.RandomString(12));

        #endregion

        #region Parse(Text)

        /// <summary>
        /// Parse the given string as an EVSE identification.
        /// </summary>
        /// <param name="Text">A text representation of an EVSE identification.</param>
        public static EVSE_Id Parse(String Text)
        {

            #region Initial checks

            if (Text.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(Text), "The given text representation of an EVSE identification must not be null or empty!");

            #endregion

            var MatchCollection = EVSEId_RegEx.Matches(Text);

            if (MatchCollection.Count != 1)
                throw new ArgumentException("Illegal text representation of an EVSE identification: '" + Text + "'!",
                                            nameof(Text));

            ChargingStationOperator_Id _OperatorId;

            if (ChargingStationOperator_Id.TryParse(MatchCollection[0].Groups[1].Value, out _OperatorId))
                return new EVSE_Id(_OperatorId,
                                   MatchCollection[0].Groups[2].Value);

            if (ChargingStationOperator_Id.TryParse(MatchCollection[0].Groups[3].Value, out _OperatorId))
                return new EVSE_Id(_OperatorId,
                                   MatchCollection[0].Groups[4].Value);


            throw new ArgumentException("Illegal EVSE identification '" + Text + "'!",
                                        nameof(Text));

        }

        #endregion

        #region Parse(OperatorId, Suffix)

        /// <summary>
        /// Parse the given string as an EVSE identification.
        /// </summary>
        /// <param name="OperatorId">The unique identification of a charging station operator.</param>
        /// <param name="Suffix">The suffix of the EVSE identification.</param>
        public static EVSE_Id Parse(ChargingStationOperator_Id  OperatorId,
                                    String                      Suffix)
        {

            switch (OperatorId.Format)
            {

                case OperatorIdFormats.ISO:
                case OperatorIdFormats.ISO_STAR:
                    if (!IdSuffixISO_RegEx.IsMatch(Suffix))
                        throw new ArgumentException("Illegal EVSE identification suffix '" + Suffix + "'!",
                                                    nameof(Suffix));
                    return new EVSE_Id(OperatorId,
                                       Suffix);

                case OperatorIdFormats.DIN:
                    if (!IdSuffixDIN_RegEx.IsMatch(Suffix))
                        throw new ArgumentException("Illegal EVSE identification suffix '" + Suffix + "'!",
                                                    nameof(Suffix));
                    return new EVSE_Id(OperatorId,
                                       Suffix);

            }

            throw new ArgumentException("Illegal EVSE identification suffix '" + Suffix + "'!",
                                        nameof(Suffix));

        }

        #endregion

        #region TryParse(Text)

        /// <summary>
        /// Parse the given string as an EVSE identification.
        /// </summary>
        public static EVSE_Id? TryParse(String Text)
        {

            EVSE_Id EVSEId;

            if (TryParse(Text, out EVSEId))
                return EVSEId;

            return null;

        }

        #endregion

        #region TryParse(Text, out EVSE_Id)

        /// <summary>
        /// Parse the given string as an EVSE identification.
        /// </summary>
        public static Boolean TryParse(String Text, out EVSE_Id EVSEId)
        {

            #region Initial checks

            if (Text.IsNullOrEmpty())
            {
                EVSEId = default(EVSE_Id);
                return false;
            }

            #endregion

            try
            {

                EVSEId = default(EVSE_Id);

                var _MatchCollection = EVSEId_RegEx.Matches(Text);

                if (_MatchCollection.Count != 1)
                    return false;

                ChargingStationOperator_Id _OperatorId;

                // New format...
                if (ChargingStationOperator_Id.TryParse(_MatchCollection[0].Groups[1].Value, out _OperatorId))
                {

                    EVSEId = new EVSE_Id(_OperatorId,
                                         _MatchCollection[0].Groups[2].Value);

                    return true;

                }

                if (ChargingStationOperator_Id.TryParse(_MatchCollection[0].Groups[3].Value, out _OperatorId))
                {

                    EVSEId = new EVSE_Id(_OperatorId,
                                         _MatchCollection[0].Groups[4].Value);

                    return true;

                }

            }
#pragma warning disable RCS1075  // Avoid empty catch clause that catches System.Exception.
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch (Exception e)
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
#pragma warning restore RCS1075  // Avoid empty catch clause that catches System.Exception.
            { }

            EVSEId = default(EVSE_Id);
            return false;

        }

        #endregion

        #region ChangeFormat(NewFormat)

        /// <summary>
        /// Return a new EVSE identification in the given format.
        /// </summary>
        /// <param name="NewFormat">An EVSE identification format.</param>
        public EVSE_Id ChangeFormat(OperatorIdFormats NewFormat)

            => new EVSE_Id(OperatorId.ChangeFormat(NewFormat),
                           Suffix);

        #endregion

        #region Clone

        /// <summary>
        /// Clone this EVSE identification.
        /// </summary>
        public EVSE_Id Clone

            => new EVSE_Id(OperatorId.Clone,
                           new String(Suffix.ToCharArray()));

        #endregion


        #region Replace(Old, New)

        /// <summary>
        /// Returns a new EVSE Id in which all occurrences of the specified
        /// old string value are replaced with the new value.
        /// </summary>
        /// <param name="OldValue">The string to be replaced.</param>
        /// <param name="NewValue">The new string value.</param>
        public EVSE_Id Replace(String  OldValue,
                               String  NewValue)

            => Parse(ToString().Replace(OldValue, NewValue));

        #endregion


        #region ToFormat(IdFormat)

        /// <summary>
        /// Return the identification in the given format.
        /// </summary>
        /// <param name="IdFormat">The format.</param>
        public String ToFormat(OperatorIdFormats IdFormat)

            => IdFormat == OperatorIdFormats.ISO
                   ? String.Concat(OperatorId.ToString(IdFormat), "*E", Suffix)
                   : String.Concat(OperatorId.ToString(IdFormat),  "*", Suffix);

        #endregion


        #region Operator overloading

        #region Operator == (EVSEId1, EVSEId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EVSEId1">A EVSE identification.</param>
        /// <param name="EVSEId2">Another EVSE identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (EVSE_Id EVSEId1, EVSE_Id EVSEId2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(EVSEId1, EVSEId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) EVSEId1 == null) || ((Object) EVSEId2 == null))
                return false;

            return EVSEId1.Equals(EVSEId2);

        }

        #endregion

        #region Operator != (EVSEId1, EVSEId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EVSEId1">A EVSE identification.</param>
        /// <param name="EVSEId2">Another EVSE identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (EVSE_Id EVSEId1, EVSE_Id EVSEId2)
            => !(EVSEId1 == EVSEId2);

        #endregion

        #region Operator <  (EVSEId1, EVSEId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EVSEId1">A EVSE identification.</param>
        /// <param name="EVSEId2">Another EVSE identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (EVSE_Id EVSEId1, EVSE_Id EVSEId2)
        {

            if ((Object) EVSEId1 == null)
                throw new ArgumentNullException(nameof(EVSEId1), "The given EVSEId1 must not be null!");

            return EVSEId1.CompareTo(EVSEId2) < 0;

        }

        #endregion

        #region Operator <= (EVSEId1, EVSEId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EVSEId1">A EVSE identification.</param>
        /// <param name="EVSEId2">Another EVSE identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (EVSE_Id EVSEId1, EVSE_Id EVSEId2)
            => !(EVSEId1 > EVSEId2);

        #endregion

        #region Operator >  (EVSEId1, EVSEId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EVSEId1">A EVSE identification.</param>
        /// <param name="EVSEId2">Another EVSE identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (EVSE_Id EVSEId1, EVSE_Id EVSEId2)
        {

            if ((Object) EVSEId1 == null)
                throw new ArgumentNullException(nameof(EVSEId1), "The given EVSEId1 must not be null!");

            return EVSEId1.CompareTo(EVSEId2) > 0;

        }

        #endregion

        #region Operator >= (EVSEId1, EVSEId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EVSEId1">A EVSE identification.</param>
        /// <param name="EVSEId2">Another EVSE identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (EVSE_Id EVSEId1, EVSE_Id EVSEId2)
            => !(EVSEId1 < EVSEId2);

        #endregion

        #endregion

        #region IComparable<EVSEId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException(nameof(Object), "The given object must not be null!");

            if (!(Object is EVSE_Id))
                throw new ArgumentException("The given object is not an EVSE identification!");

            return CompareTo((EVSE_Id) Object);

        }

        #endregion

        #region CompareTo(EVSEId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EVSEId">An object to compare with.</param>
        public Int32 CompareTo(EVSE_Id EVSEId)
        {

            if ((Object) EVSEId == null)
                throw new ArgumentNullException(nameof(EVSEId),  "The given EVSE identification must not be null!");

            // Compare the length of the identifications
            var _Result = this.Length.CompareTo(EVSEId.Length);

            // If equal: Compare charging operator identifications
            if (_Result == 0)
                _Result = OperatorId.CompareTo(EVSEId.OperatorId);

            // If equal: Compare suffix
            if (_Result == 0)
                _Result = String.Compare(Suffix, EVSEId.Suffix, StringComparison.Ordinal);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<EVSEId> Members

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

            if (!(Object is EVSE_Id))
                return false;

            return Equals((EVSE_Id) Object);

        }

        #endregion

        #region Equals(EVSEId)

        /// <summary>
        /// Compares two EVSE identifications for equality.
        /// </summary>
        /// <param name="EVSEId">An EVSE identification to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(EVSE_Id EVSEId)
        {

            if ((Object) EVSEId == null)
                return false;

            return OperatorId.Equals(EVSEId.OperatorId) &&
                   Suffix.    Equals(EVSEId.Suffix);

        }

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()

            => OperatorId.GetHashCode() ^
               Suffix.    GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// ISO-IEC-15118 – Annex H "Specification of Identifiers"
        /// </summary>
        public override String ToString()
        {

            switch (Format)
            {

                case OperatorIdFormats.DIN:
                    return String.Concat(OperatorId,  "*", Suffix);

                case OperatorIdFormats.ISO_STAR:
                    return String.Concat(OperatorId, "*E", Suffix);

                default:  // ISO
                    return String.Concat(OperatorId,  "E", Suffix);

            }

        }

        #endregion

    }

}
