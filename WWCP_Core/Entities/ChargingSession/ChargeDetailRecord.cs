﻿/*
 * Copyright (c) 2014-2016 GraphDefined GmbH <achim.friedland@graphdefined.com>
 * This file is part of WWCP Core <https://github.com/GraphDefined/WWCP_Core>
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
using System.Collections.Generic;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace org.GraphDefined.WWCP
{

    /// <summary>
    /// A charge detail record for a charging session.
    /// </summary>
    public class ChargeDetailRecord : IEquatable <ChargeDetailRecord>,
                                      IComparable<ChargeDetailRecord>,
                                      IComparable
    {

        #region Data

        #endregion

        #region Properties

        #region SessionId

        private readonly ChargingSession_Id _SessionId;

        /// <summary>
        /// The unique charging session identification.
        /// </summary>
        [Mandatory]
        public ChargingSession_Id SessionId
        {
            get
            {
                return _SessionId;
            }
        }

        #endregion

        #region ChargingReservation

        private readonly ChargingReservation _ChargingReservation;

        /// <summary>
        /// An optional charging reservation used for charging.
        /// </summary>
        [Mandatory]
        public ChargingReservation ChargingReservation
        {
            get
            {
                return _ChargingReservation;
            }
        }

        #endregion


        #region EVSE

        private readonly EVSE _EVSE;

        [Optional]
        public EVSE EVSE
        {
            get
            {
                return _EVSE;
            }
        }

        #endregion

        #region ChargingStation

        private readonly ChargingStation _ChargingStation;

        [Optional]
        public ChargingStation ChargingStation
        {
            get
            {
                return _ChargingStation;
            }

        }

        #endregion

        #region ChargingPool

        private readonly ChargingPool _ChargingPool;

        [Optional]
        public ChargingPool ChargingPool
        {
            get
            {
                return _ChargingPool;
            }
        }

        #endregion

        #region EVSEOperator

        private readonly EVSEOperator _EVSEOperator;

        [Optional]
        public EVSEOperator EVSEOperator
        {
            get
            {
                return _EVSEOperator;
            }
        }

        #endregion

        #region ChargingProductId

        private readonly ChargingProduct_Id _ChargingProductId;

        /// <summary>
        /// The charging product selected for this charge detail record.
        /// </summary>
        [Optional]
        public ChargingProduct_Id ChargingProductId
        {
            get
            {
                return _ChargingProductId;
            }
        }

        #endregion


        #region ReservationTime

        private readonly StartEndDateTime? _ReservationTime;

        [Mandatory]
        public StartEndDateTime? ReservationTime
        {
            get
            {
                return _ReservationTime;
            }
        }

        #endregion

        #region SessionTime

        private readonly StartEndDateTime? _SessionTime;

        [Mandatory]
        public StartEndDateTime? SessionTime
        {
            get
            {
                return _SessionTime;
            }
        }

        #endregion

        #region ParkingTime

        private readonly StartEndDateTime? _ParkingTime;

        [Optional]
        public StartEndDateTime? ParkingTime
        {
            get
            {
                return _ParkingTime;
            }
        }

        #endregion

        #region ChargingTime

        private readonly StartEndDateTime? _ChargingTime;

        [Mandatory]
        public StartEndDateTime? ChargingTime
        {
            get
            {
                return _ChargingTime;
            }
        }

        #endregion


        #region EnergyMeterId

        private readonly EnergyMeter_Id _EnergyMeterId;

        /// <summary>
        /// An optional unique identification of the energy meter.
        /// </summary>
        [Optional]
        public EnergyMeter_Id EnergyMeterId
        {
            get
            {
                return _EnergyMeterId;
            }
        }

        #endregion

        #region EnergyMeterValues

        private readonly IEnumerable<Timestamped<Double>> _EnergyMeterValues;

        /// <summary>
        /// An optional enumeration of intermediate energy meter values.
        /// </summary>
        [Optional]
        public IEnumerable<Timestamped<Double>> EnergyMeterValues
        {
            get
            {
                return _EnergyMeterValues;
            }
        }

        #endregion

        #region ConsumedEnergy

        /// <summary>
        /// The current amount of energy consumed while charging in [kWh].
        /// </summary>
        [Mandatory]
        public Double ConsumedEnergy
        {
            get
            {

                return _EnergyMeterValues.
                           Select(metervalue => metervalue.Value).
                           Sum() / 1000;

            }
        }

        #endregion

        #region MeteringSignature

        private readonly String _MeteringSignature;

        /// <summary>
        /// An optional signature for the metering values.
        /// </summary>
        [Optional]
        public String MeteringSignature
        {
            get
            {
                return _MeteringSignature;
            }
        }

        #endregion


        #region Identification

        private readonly AuthInfo _Identification;

        [Optional]
        public AuthInfo Identification
        {
            get
            {
                return _Identification;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a charge detail record for the given charging session (identification).
        /// </summary>
        /// <param name="SessionId">The unique charging session identification.</param>
        /// <param name="ChargingReservation">An optional charging reservation used for charging.</param>
        /// 
        /// <param name="EVSE">The EVSE of the EVSE used for charging.</param>
        /// <param name="ChargingStation">The charging station of the charging station used for charging.</param>
        /// <param name="ChargingPool">The charging pool of the charging pool used for charging.</param>
        /// <param name="EVSEOperator">The EVSE operator used for charging.</param>
        /// <param name="ChargingProductId">An unqiue identification for the consumed charging product.</param>
        /// 
        /// <param name="ReservationTime">Optional timestamps when the reservation started and ended.</param>
        /// <param name="ParkingTime">Optional timestamps when the parking started and ended.</param>
        /// <param name="SessionTime">Optional timestamps when the charging session started and ended.</param>
        /// <param name="ChargingTime">Optional timestamps when the charging started and ended.</param>
        /// 
        /// <param name="EnergyMeterId">An optional unique identification of the energy meter.</param>
        /// <param name="EnergyMeterValues">An optional enumeration of intermediate energy meter values.</param>
        /// <param name="MeteringSignature">An optional signature for the metering values.</param>
        /// 
        /// <param name="Identification">An identification.</param>
        public ChargeDetailRecord(ChargingSession_Id                SessionId,
                                  ChargingReservation               ChargingReservation  = null,

                                  EVSE                              EVSE                 = null,
                                  ChargingStation                   ChargingStation      = null,
                                  ChargingPool                      ChargingPool         = null,
                                  EVSEOperator                      EVSEOperator         = null,
                                  ChargingProduct_Id                ChargingProductId    = null,

                                  StartEndDateTime?                 ReservationTime      = null,
                                  StartEndDateTime?                 ParkingTime          = null,
                                  StartEndDateTime?                 SessionTime          = null,
                                  StartEndDateTime?                 ChargingTime         = null,

                                  EnergyMeter_Id                    EnergyMeterId        = null,
                                  IEnumerable<Timestamped<Double>>  EnergyMeterValues    = null,
                                  String                            MeteringSignature    = null,

                                  AuthInfo                          Identification       = null)

        {

            #region Initial checks

            if (SessionId == null)
                throw new ArgumentNullException("Id", "The charging session identification must not be null!");

            #endregion

            this._SessionId            = SessionId;
            this._ChargingReservation  = ChargingReservation;

            this._EVSE                 = EVSE;
            this._ChargingStation      = ChargingStation;
            this._ChargingPool         = ChargingPool;
            this._EVSEOperator         = EVSEOperator;
            this._ChargingProductId    = ChargingProductId;

            this._ReservationTime      = ReservationTime;
            this._ParkingTime          = ParkingTime;
            this._SessionTime          = SessionTime;
            this._ChargingTime         = ChargingTime;

            this._EnergyMeterId        = EnergyMeterId;
            this._EnergyMeterValues    = EnergyMeterValues;

        }

        #endregion


        #region IComparable<ChargeDetailRecord> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is a charge detail record.
            var ChargeDetailRecord = Object as ChargeDetailRecord;
            if ((Object) ChargeDetailRecord == null)
                throw new ArgumentException("The given object is not a charge detail record!");

            return CompareTo(ChargeDetailRecord);

        }

        #endregion

        #region CompareTo(ChargeDetailRecord)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargeDetailRecord">A charge detail record object to compare with.</param>
        public Int32 CompareTo(ChargeDetailRecord ChargeDetailRecord)
        {

            if ((Object) ChargeDetailRecord == null)
                throw new ArgumentNullException("The given charge detail record must not be null!");

            return _SessionId.CompareTo(ChargeDetailRecord._SessionId);

        }

        #endregion

        #endregion

        #region IEquatable<ChargeDetailRecord> Members

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

            // Check if the given object is a charge detail record.
            var ChargeDetailRecord = Object as ChargeDetailRecord;
            if ((Object) ChargeDetailRecord == null)
                return false;

            return this.Equals(ChargeDetailRecord);

        }

        #endregion

        #region Equals(ChargeDetailRecord)

        /// <summary>
        /// Compares two charge detail records for equality.
        /// </summary>
        /// <param name="ChargeDetailRecord">A charge detail record to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(ChargeDetailRecord ChargeDetailRecord)
        {

            if ((Object) ChargeDetailRecord == null)
                return false;

            return _SessionId.Equals(ChargeDetailRecord._SessionId);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Get the hashcode of this object.
        /// </summary>
        public override Int32 GetHashCode()
        {
            return _SessionId.GetHashCode();
        }

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return _SessionId.ToString();
        }

        #endregion

    }

}