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
using System.Linq;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace org.GraphDefined.WWCP
{

    /// <summary>
    /// A charging station operator admin status update.
    /// </summary>
    public struct ChargingStationOperatorAdminStatusUpdate : IEquatable <ChargingStationOperatorAdminStatusUpdate>,
                                                             IComparable<ChargingStationOperatorAdminStatusUpdate>
    {

        #region Properties

        /// <summary>
        /// The unique identification of the charging station operator.
        /// </summary>
        public ChargingStationOperator_Id                           Id          { get; }

        /// <summary>
        /// The old timestamped status of the charging station operator.
        /// </summary>
        public Timestamped<ChargingStationOperatorAdminStatusTypes>  OldStatus   { get; }

        /// <summary>
        /// The new timestamped status of the charging station operator.
        /// </summary>
        public Timestamped<ChargingStationOperatorAdminStatusTypes>  NewStatus   { get; }

        #endregion

        #region Constructor(s)

        #region ChargingStationOperatorAdminStatusUpdate(Id, OldStatus, NewStatus)

        /// <summary>
        /// Create a new charging station operator admin status update.
        /// </summary>
        /// <param name="Id">The unique identification of the charging station operator.</param>
        /// <param name="OldStatus">The old timestamped admin status of the charging station operator.</param>
        /// <param name="NewStatus">The new timestamped admin status of the charging station operator.</param>
        public ChargingStationOperatorAdminStatusUpdate(ChargingStationOperator_Id                           Id,
                                                        Timestamped<ChargingStationOperatorAdminStatusTypes>  OldStatus,
                                                        Timestamped<ChargingStationOperatorAdminStatusTypes>  NewStatus)

        {

            this.Id         = Id;
            this.OldStatus  = OldStatus;
            this.NewStatus  = NewStatus;

        }

        #endregion

        #region ChargingStationOperatorAdminStatusUpdate(Id, OldStatus, NewStatus)

        /// <summary>
        /// Create a new charging station operator admin status update.
        /// </summary>
        /// <param name="Id">The unique identification of the charging station operator.</param>
        /// <param name="OldStatus">The old timestamped admin status of the charging station operator.</param>
        /// <param name="NewStatus">The new timestamped admin status of the charging station operator.</param>
        public ChargingStationOperatorAdminStatusUpdate(ChargingStationOperator_Id          Id,
                                                        ChargingStationOperatorAdminStatus  OldStatus,
                                                        ChargingStationOperatorAdminStatus  NewStatus)

        {

            this.Id         = Id;
            this.OldStatus  = OldStatus.Combined;
            this.NewStatus  = NewStatus.Combined;

        }

        #endregion

        #endregion


        #region (static) Snapshot(ChargingStationOperator)

        /// <summary>
        /// Take a snapshot of the current charging station operator status.
        /// </summary>
        /// <param name="ChargingStationOperator">A charging station operator.</param>
        public static ChargingStationOperatorAdminStatusUpdate Snapshot(ChargingStationOperator ChargingStationOperator)

            => new ChargingStationOperatorAdminStatusUpdate(ChargingStationOperator.Id,
                                                            ChargingStationOperator.AdminStatus,
                                                            ChargingStationOperator.AdminStatusSchedule().Skip(1).FirstOrDefault());

        #endregion


        #region Operator overloading

        #region Operator == (ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingStationOperatorAdminStatusUpdate1">A charging station operator status update.</param>
        /// <param name="ChargingStationOperatorAdminStatusUpdate2">Another charging station operator status update.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) ChargingStationOperatorAdminStatusUpdate1 == null) || ((Object) ChargingStationOperatorAdminStatusUpdate2 == null))
                return false;

            return ChargingStationOperatorAdminStatusUpdate1.Equals(ChargingStationOperatorAdminStatusUpdate2);

        }

        #endregion

        #region Operator != (ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingStationOperatorAdminStatusUpdate1">A charging station operator status update.</param>
        /// <param name="ChargingStationOperatorAdminStatusUpdate2">Another charging station operator status update.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate2)
            => !(ChargingStationOperatorAdminStatusUpdate1 == ChargingStationOperatorAdminStatusUpdate2);

        #endregion

        #region Operator <  (ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingStationOperatorAdminStatusUpdate1">A charging station operator status update.</param>
        /// <param name="ChargingStationOperatorAdminStatusUpdate2">Another charging station operator status update.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate2)
        {

            if ((Object) ChargingStationOperatorAdminStatusUpdate1 == null)
                throw new ArgumentNullException(nameof(ChargingStationOperatorAdminStatusUpdate1), "The given ChargingStationOperatorAdminStatusUpdate1 must not be null!");

            return ChargingStationOperatorAdminStatusUpdate1.CompareTo(ChargingStationOperatorAdminStatusUpdate2) < 0;

        }

        #endregion

        #region Operator <= (ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingStationOperatorAdminStatusUpdate1">A charging station operator status update.</param>
        /// <param name="ChargingStationOperatorAdminStatusUpdate2">Another charging station operator status update.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate2)
            => !(ChargingStationOperatorAdminStatusUpdate1 > ChargingStationOperatorAdminStatusUpdate2);

        #endregion

        #region Operator >  (ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingStationOperatorAdminStatusUpdate1">A charging station operator status update.</param>
        /// <param name="ChargingStationOperatorAdminStatusUpdate2">Another charging station operator status update.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate2)
        {

            if ((Object) ChargingStationOperatorAdminStatusUpdate1 == null)
                throw new ArgumentNullException(nameof(ChargingStationOperatorAdminStatusUpdate1), "The given ChargingStationOperatorAdminStatusUpdate1 must not be null!");

            return ChargingStationOperatorAdminStatusUpdate1.CompareTo(ChargingStationOperatorAdminStatusUpdate2) > 0;

        }

        #endregion

        #region Operator >= (ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingStationOperatorAdminStatusUpdate1">A charging station operator status update.</param>
        /// <param name="ChargingStationOperatorAdminStatusUpdate2">Another charging station operator status update.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate1, ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate2)
            => !(ChargingStationOperatorAdminStatusUpdate1 < ChargingStationOperatorAdminStatusUpdate2);

        #endregion

        #endregion

        #region IComparable<ChargingStationOperatorAdminStatusUpdate> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException(nameof(Object), "The given object must not be null!");

            if (!(Object is ChargingStationOperatorAdminStatusUpdate))
                throw new ArgumentException("The given object is not a ChargingStationOperatorStatus!",
                                            nameof(Object));

            return CompareTo((ChargingStationOperatorAdminStatusUpdate) Object);

        }

        #endregion

        #region CompareTo(ChargingStationOperatorAdminStatusUpdate)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingStationOperatorAdminStatusUpdate">An object to compare with.</param>
        public Int32 CompareTo(ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate)
        {

            if ((Object) ChargingStationOperatorAdminStatusUpdate == null)
                throw new ArgumentNullException(nameof(ChargingStationOperatorAdminStatusUpdate), "The given ChargingStationOperator status update must not be null!");

            // Compare ChargingStationOperator Ids
            var _Result = Id.CompareTo(ChargingStationOperatorAdminStatusUpdate.Id);

            // If equal: Compare the new charging station operator status
            if (_Result == 0)
                _Result = NewStatus.CompareTo(ChargingStationOperatorAdminStatusUpdate.NewStatus);

            // If equal: Compare the old ChargingStationOperator status
            if (_Result == 0)
                _Result = OldStatus.CompareTo(ChargingStationOperatorAdminStatusUpdate.OldStatus);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<ChargingStationOperatorAdminStatusUpdate> Members

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

            if (!(Object is ChargingStationOperatorAdminStatusUpdate))
                return false;

            return this.Equals((ChargingStationOperatorAdminStatusUpdate) Object);

        }

        #endregion

        #region Equals(ChargingStationOperatorAdminStatusUpdate)

        /// <summary>
        /// Compares two ChargingStationOperator status updates for equality.
        /// </summary>
        /// <param name="ChargingStationOperatorAdminStatusUpdate">A charging station operator status update to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(ChargingStationOperatorAdminStatusUpdate ChargingStationOperatorAdminStatusUpdate)
        {

            if ((Object) ChargingStationOperatorAdminStatusUpdate == null)
                return false;

            return Id.       Equals(ChargingStationOperatorAdminStatusUpdate.Id)        &&
                   OldStatus.Equals(ChargingStationOperatorAdminStatusUpdate.OldStatus) &&
                   NewStatus.Equals(ChargingStationOperatorAdminStatusUpdate.NewStatus);

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

                return Id.       GetHashCode() * 7 ^
                       OldStatus.GetHashCode() * 5 ^
                       NewStatus.GetHashCode();

            }
        }

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => String.Concat(Id, ": ",
                             OldStatus,
                             " -> ",
                             NewStatus);

        #endregion

    }

}
