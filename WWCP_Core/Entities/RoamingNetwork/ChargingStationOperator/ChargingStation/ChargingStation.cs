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
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Illias.Votes;
using org.GraphDefined.Vanaheimr.Styx.Arrows;
using org.GraphDefined.Vanaheimr.Aegir;

#endregion

namespace org.GraphDefined.WWCP
{

    /// <summary>
    /// A charging station to charge an electric vehicle.
    /// </summary>
    public class ChargingStation : AEMobilityEntity<ChargingStation_Id>,
                                   IEquatable<ChargingStation>, IComparable<ChargingStation>, IComparable,
                                   IEnumerable<EVSE>,
                                   IStatus<ChargingStationStatusTypes>
    {

        #region Data

        /// <summary>
        /// The default max size of the charging station (aggregated EVSE) status list.
        /// </summary>
        public const UInt16 DefaultMaxStatusListSize         = 15;

        /// <summary>
        /// The default max size of the charging station admin status list.
        /// </summary>
        public const UInt16 DefaultMaxAdminStatusListSize    = 15;

        #endregion

        #region Properties

        #region ServiceIdentification

        private String _ServiceIdentification;

        /// <summary>
        /// The internal service identification of the charging station maintained by the Charging Station Operator.
        /// </summary>
        [Optional]
        public String ServiceIdentification
        {

            get
            {
                return _ServiceIdentification;
            }

            set
            {

                if (ServiceIdentification != value)
                    SetProperty<String>(ref _ServiceIdentification, value);

            }

        }

        #endregion

        #region ModelCode

        private String _ModelCode;

        /// <summary>
        /// The internal model code of the charging station maintained by the Charging Station Operator.
        /// </summary>
        [Optional]
        public String ModelCode
        {

            get
            {
                return _ModelCode;
            }

            set
            {

                if (ModelCode != value)
                    SetProperty<String>(ref _ModelCode, value);

            }

        }

        #endregion

        #region HubjectStationId

        private String _HubjectStationId;

        [Optional]
        public String HubjectStationId
        {

            get
            {
                return _HubjectStationId;
            }

            set
            {

                if (HubjectStationId != value)
                    SetProperty<String>(ref _HubjectStationId, value);

            }

        }

        #endregion

        #region Name

        private I18NString _Name;

        /// <summary>
        /// The offical (multi-language) name of this charging station.
        /// </summary>
        [Mandatory]
        public I18NString Name
        {

            get
            {

                if (_Name == null || !_Name.Any())
                    return ChargingPool.Name;

                return _Name;

            }

            set
            {

                if (_Name != value && _Name != ChargingPool.Name)
                    SetProperty<I18NString>(ref _Name, value);

            }

        }

        #endregion

        #region Description

        internal I18NString _Description;

        /// <summary>
        /// An optional (multi-language) description of this charging station.
        /// </summary>
        [Optional]
        public I18NString Description
        {

            get
            {
                return _Description ?? ChargingPool.Description;
            }

            set
            {

                if (value == ChargingPool.Description)
                    return;

                if (Description != value)
                    SetProperty<I18NString>(ref _Description, value);

            }

        }

        #endregion

        #region Brand

        private Brand _Brand;

        /// <summary>
        /// A brand for this charging station
        /// is this is different from the Charging Station Operator.
        /// </summary>
        [Optional]
        public Brand Brand
        {

            get
            {
                return _Brand;
            }

            set
            {

                if (_Brand != value)
                    SetProperty(ref _Brand, value);

            }

        }

        #endregion

        #region Address

        internal Address _Address;

        /// <summary>
        /// The address of this charging station.
        /// </summary>
        [Optional]
        public Address Address
        {

            get
            {
                return _Address ?? ChargingPool.Address;
            }

            set
            {

                if (value == ChargingPool.Address)
                    return;

                if (Address != value)
                {

                    if (value == null)
                        DeleteProperty(ref _Address);

                    else
                        SetProperty(ref _Address, value);

                }

            }

        }

        #endregion

        #region OSM_NodeId

        private String _OSM_NodeId;

        /// <summary>
        /// OSM Node Id.
        /// </summary>
        [Optional]
        public String OSM_NodeId
        {

            get
            {
                return _OSM_NodeId;
            }

            set
            {
                SetProperty<String>(ref _OSM_NodeId, value);
            }

        }

        #endregion

        #region GeoLocation

        internal GeoCoordinate? _GeoLocation;

        /// <summary>
        /// The geographical location of this charging station.
        /// </summary>
        [Optional]
        public GeoCoordinate? GeoLocation
        {

            get
            {

                return _GeoLocation.HasValue
                    ? _GeoLocation
                    : ChargingPool.GeoLocation;

            }

            set
            {

                if (value == ChargingPool.GeoLocation)
                    return;

                if (GeoLocation != value)
                {

                    if (value == null)
                        DeleteProperty(ref _GeoLocation);

                    else
                        SetProperty(ref _GeoLocation, value);

                }

            }

        }

        #endregion

        #region EntranceAddress

        internal Address _EntranceAddress;

        /// <summary>
        /// The address of the entrance to this charging station.
        /// (If different from 'Address').
        /// </summary>
        [Optional]
        public Address EntranceAddress
        {

            get
            {
                return _EntranceAddress ?? ChargingPool.EntranceAddress;
            }

            set
            {

                if (value == ChargingPool.EntranceAddress)
                    return;

                if (EntranceAddress != value)
                {

                    if (value == null)
                        DeleteProperty(ref _EntranceAddress);

                    else
                        SetProperty(ref _EntranceAddress, value);

                }

            }

        }

        #endregion

        #region EntranceLocation

        internal GeoCoordinate? _EntranceLocation;

        /// <summary>
        /// The geographical location of the entrance to this charging station.
        /// (If different from 'GeoLocation').
        /// </summary>
        [Optional]
        public GeoCoordinate? EntranceLocation
        {

            get
            {

                return _EntranceLocation.HasValue
                    ? _EntranceLocation
                    : ChargingPool.EntranceLocation;

            }

            set
            {

                if (value == ChargingPool.EntranceLocation)
                    return;

                if (EntranceLocation != value)
                {

                    if (value == null)
                        DeleteProperty(ref _EntranceLocation);

                    else
                        SetProperty(ref _EntranceLocation, value);

                }

            }

        }

        #endregion

        #region ExitAddress

        internal Address _ExitAddress;

        /// <summary>
        /// The address of the exit of this charging station.
        /// (If different from 'Address').
        /// </summary>
        [Optional]
        public Address ExitAddress
        {

            get
            {
                return _ExitAddress ?? ChargingPool.ExitAddress;
            }

            set
            {

                if (value == ChargingPool.ExitAddress)
                    return;

                if (ExitAddress != value)
                {

                    if (value == null)
                        DeleteProperty(ref _ExitAddress);

                    else
                        SetProperty(ref _ExitAddress, value);

                }

            }

        }

        #endregion

        #region ExitLocation

        internal GeoCoordinate? _ExitLocation;

        /// <summary>
        /// The geographical location of the exit of this charging station.
        /// (If different from 'GeoLocation').
        /// </summary>
        [Optional]
        public GeoCoordinate? ExitLocation
        {

            get
            {

                return _ExitLocation.HasValue
                    ? _ExitLocation
                    : ChargingPool.ExitLocation;

            }

            set
            {

                if (value == ChargingPool.ExitLocation)
                    return;

                if (ExitLocation != value)
                {

                    if (value == null)
                        DeleteProperty(ref _ExitLocation);

                    else
                        SetProperty(ref _ExitLocation, value);

                }

            }

        }

        #endregion

        #region ParkingSpaces

        private List<ParkingSpace> _ParkingSpaces;

        /// <summary>
        /// parking spaces reachable from this charging station.
        /// </summary>
        [Optional]
        public IEnumerable<ParkingSpace> ParkingSpaces

            => _ParkingSpaces;

        #endregion

        #region OpeningTimes

        internal OpeningTimes _OpeningTimes;

        /// <summary>
        /// The opening times of this charging station.
        /// </summary>
        public OpeningTimes OpeningTimes
        {

            get
            {
                return _OpeningTimes ?? ChargingPool.OpeningTimes;
            }

            set
            {

                if (value == ChargingPool.OpeningTimes)
                    return;

                if (OpeningTimes != value)
                {

                    if (value == null)
                        DeleteProperty(ref _OpeningTimes);

                    else
                        SetProperty(ref _OpeningTimes, value);

                }

            }

        }

        #endregion

        #region AuthenticationModes

        internal ReactiveSet<AuthenticationModes> _AuthenticationModes;

        public ReactiveSet<AuthenticationModes> AuthenticationModes
        {

            get
            {
                return _AuthenticationModes ?? ChargingPool.AuthenticationModes;
            }

            set
            {

                if (value == ChargingPool.AuthenticationModes)
                    return;

                if (AuthenticationModes != value)
                {

                    if (_AuthenticationModes == null)
                        _AuthenticationModes = new ReactiveSet<AuthenticationModes>();

                    if (value == null)
                        DeleteProperty(ref _AuthenticationModes);

                    else
                        SetProperty(ref _AuthenticationModes, value);

                }

            }

        }

        #endregion

        #region PaymentOptions

        internal ReactiveSet<PaymentOptions> _PaymentOptions;

        [Mandatory]
        public ReactiveSet<PaymentOptions> PaymentOptions
        {

            get
            {
                return _PaymentOptions ?? ChargingPool.PaymentOptions;
            }

            set
            {

                if (value == ChargingPool.PaymentOptions)
                    return;

                if (PaymentOptions != value)
                {

                    if (_PaymentOptions == null)
                        _PaymentOptions = new ReactiveSet<PaymentOptions>();

                    if (value == null)
                        DeleteProperty(ref _PaymentOptions);

                    else
                        SetProperty(ref _PaymentOptions, value);

                }

            }

        }

        #endregion

        #region Accessibility

        internal AccessibilityTypes _Accessibility;

        [Optional]
        public AccessibilityTypes Accessibility
        {

            get
            {

                return _Accessibility != AccessibilityTypes.Unspecified
                    ? _Accessibility
                    : ChargingPool.Accessibility;

            }

            set
            {

                if (value == ChargingPool.Accessibility)
                    return;

                if (Accessibility != value)
                {

                    SetProperty(ref _Accessibility, value);

                }

            }

        }

        #endregion

        #region HotlinePhoneNumber

        internal String _HotlinePhoneNumber;

        /// <summary>
        /// The telephone number of the Charging Station Operator hotline.
        /// </summary>
        [Optional]
        public String HotlinePhoneNumber
        {

            get
            {
                return _HotlinePhoneNumber ?? ChargingPool.HotlinePhoneNumber;
            }

            set
            {

                if (value == ChargingPool.HotlinePhoneNumber)
                    return;

                if (HotlinePhoneNumber != value)
                {

                    if (value == null)
                        DeleteProperty(ref _HotlinePhoneNumber);

                    else
                        SetProperty(ref _HotlinePhoneNumber, value);

                }

            }

        }

        #endregion

        #region IsHubjectCompatible

        private Boolean _IsHubjectCompatible;

        [Optional]
        public Boolean IsHubjectCompatible
        {

            get
            {
                return _IsHubjectCompatible;
            }

            set
            {

                if (_IsHubjectCompatible != value)
                    SetProperty(ref _IsHubjectCompatible, value);

            }

        }

        #endregion

        #region DynamicInfoAvailable

        private Boolean _DynamicInfoAvailable;

        [Optional]
        public Boolean DynamicInfoAvailable
        {

            get
            {
                return _DynamicInfoAvailable;
            }

            set
            {

                if (_DynamicInfoAvailable != value)
                    SetProperty(ref _DynamicInfoAvailable, value);

            }

        }

        #endregion


        #region UserComment

        private I18NString _UserComment;

        /// <summary>
        /// A comment from the users.
        /// </summary>
        [Optional]
        public I18NString UserComment
        {

            get
            {
                return _UserComment;
            }

            set
            {
                SetProperty<I18NString>(ref _UserComment, value);
            }

        }

        #endregion

        #region ServiceProviderComment

        private I18NString _ServiceProviderComment;

        /// <summary>
        /// A comment from the service provider.
        /// </summary>
        [Optional]
        public I18NString ServiceProviderComment
        {

            get
            {
                return _ServiceProviderComment;
            }

            set
            {
                SetProperty<I18NString>(ref _ServiceProviderComment, value);
            }

        }

        #endregion

        #region GridConnection

        private GridConnection _GridConnection;

        /// <summary>
        /// The grid connection of the charging station.
        /// </summary>
        [Optional]
        public GridConnection GridConnection
        {

            get
            {
                return _GridConnection;
            }

            set
            {
                SetProperty<GridConnection>(ref _GridConnection, value);
            }

        }

        #endregion

        #region UIFeatures

        private ChargingStationUIFeatures _UIFeatures;

        /// <summary>
        /// The features of the charging station.
        /// </summary>
        [Optional]
        public ChargingStationUIFeatures UIFeatures
        {

            get
            {
                return _UIFeatures;
            }

            set
            {
                SetProperty<ChargingStationUIFeatures>(ref _UIFeatures, value);
            }

        }

        #endregion

        #region PhotoURIs

        private ReactiveSet<String> _PhotoURIs;

        /// <summary>
        /// URIs of photos of this charging station.
        /// </summary>
        [Optional]
        public ReactiveSet<String> PhotoURIs
        {

            get
            {
                return _PhotoURIs;
            }

            set
            {
                SetProperty(ref _PhotoURIs, value);
            }

        }

        #endregion


        #region Status

        /// <summary>
        /// The current charging station status.
        /// </summary>
        [Optional]
        public Timestamped<ChargingStationStatusTypes> Status
        {

            get
            {

                if (AdminStatus.Value == ChargingStationAdminStatusTypes.Operational ||
                    AdminStatus.Value == ChargingStationAdminStatusTypes.InternalUse)
                {

                    return _StatusSchedule.CurrentStatus;

                }

                else
                {

                    switch (AdminStatus.Value)
                    {

                        default:
                            return new Timestamped<ChargingStationStatusTypes>(AdminStatus.Timestamp, ChargingStationStatusTypes.OutOfService);

                    }

                }

            }

            set
            {

                if (value == null)
                    return;

                if (_StatusSchedule.CurrentValue != value.Value)
                    SetStatus(value);

            }

        }

        #endregion

        #region StatusSchedule

        private StatusSchedule<ChargingStationStatusTypes> _StatusSchedule;

        /// <summary>
        /// The charging station status schedule.
        /// </summary>
        public IEnumerable<Timestamped<ChargingStationStatusTypes>> StatusSchedule(UInt64? HistorySize = null)
        {

            if (AdminStatus.Value == ChargingStationAdminStatusTypes.Operational ||
                AdminStatus.Value == ChargingStationAdminStatusTypes.InternalUse)
            {

                if (HistorySize.HasValue)
                    return _StatusSchedule.Take(HistorySize);

                return _StatusSchedule;

            }

            else
            {

                switch (AdminStatus.Value)
                {

                    default:
                        return new Timestamped<ChargingStationStatusTypes>[] {
                                   new Timestamped<ChargingStationStatusTypes>(AdminStatus.Timestamp, ChargingStationStatusTypes.OutOfService)
                               };

                }

            }

        }

        #endregion

        #region StatusAggregationDelegate

        /// <summary>
        /// A delegate called to aggregate the dynamic status of all subordinated EVSEs.
        /// </summary>
        public Func<EVSEStatusReport, ChargingStationStatusTypes> StatusAggregationDelegate { get; set; }

        #endregion


        #region AdminStatus

        /// <summary>
        /// The current charging station admin status.
        /// </summary>
        [Optional]
        public Timestamped<ChargingStationAdminStatusTypes> AdminStatus
        {

            get
            {
                return _AdminStatusSchedule.CurrentStatus;
            }

            set
            {
                SetAdminStatus(value);
            }

        }

        #endregion

        #region AdminStatusSchedule

        private StatusSchedule<ChargingStationAdminStatusTypes> _AdminStatusSchedule;

        /// <summary>
        /// The charging station admin status schedule.
        /// </summary>
        public IEnumerable<Timestamped<ChargingStationAdminStatusTypes>> AdminStatusSchedule(UInt64? HistorySize = null)
        {

            if (HistorySize.HasValue)
                return _AdminStatusSchedule.Take(HistorySize);

            return _AdminStatusSchedule;

        }

        #endregion

        #endregion

        #region Links

        /// <summary>
        /// An optional remote charging station.
        /// </summary>
        public IRemoteChargingStation  RemoteChargingStation    { get; }


        /// <summary>
        /// The charging pool.
        /// </summary>
        [InternalUseOnly]
        public ChargingPool            ChargingPool             { get; }


        /// <summary>
        /// The Charging Station Operator of this EVSE.
        /// </summary>
        [InternalUseOnly]
        public ChargingStationOperator  Operator

            => ChargingPool?.Operator;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new charging station having the given identification.
        /// </summary>
        /// <param name="Id">The unique identification of the charging station pool.</param>
        /// <param name="ChargingPool">The parent charging pool.</param>
        /// <param name="MaxAdminStatusListSize">The default size of the admin status list.</param>
        /// <param name="MaxStatusListSize">The default size of the status list.</param>
        internal ChargingStation(ChargingStation_Id                    Id,
                                 ChargingPool                          ChargingPool,
                                 Action<ChargingStation>               Configurator                  = null,
                                 RemoteChargingStationCreatorDelegate  RemoteChargingStationCreator  = null,
                                 ChargingStationAdminStatusTypes       AdminStatus                   = ChargingStationAdminStatusTypes.Operational,
                                 ChargingStationStatusTypes            Status                        = ChargingStationStatusTypes.Available,
                                 UInt16                                MaxAdminStatusListSize        = DefaultMaxAdminStatusListSize,
                                 UInt16                                MaxStatusListSize             = DefaultMaxStatusListSize)

            : base(Id)

        {

            #region Initial checks

            if (ChargingPool == null)
                throw new ArgumentNullException(nameof(ChargingPool),  "The charging pool must not be null!");

            #endregion

            #region Init data and properties

            this.ChargingPool               = ChargingPool;

            this._EVSEs                      = new EntityHashSet<ChargingStation, EVSE_Id, EVSE>(this);

            this.Name                        = new I18NString();
            this.Description                 = new I18NString();

            this._UserComment                = new I18NString();
            this._ServiceProviderComment     = new I18NString();
            //this.GeoLocation                 = new GeoCoordinate();

            this._ParkingSpaces               = new List<ParkingSpace>();

            this._PaymentOptions             = new ReactiveSet<PaymentOptions>();

            this._AdminStatusSchedule        = new StatusSchedule<ChargingStationAdminStatusTypes>(MaxAdminStatusListSize);
            this._AdminStatusSchedule.Insert(AdminStatus);

            this._StatusSchedule             = new StatusSchedule<ChargingStationStatusTypes>(MaxStatusListSize);
            this._StatusSchedule.Insert(Status);

            #endregion

            #region Init events

            // ChargingStation events
            this.EVSEAddition             = new VotingNotificator<DateTime, ChargingStation, EVSE, Boolean>(() => new VetoVote(), true);
            this.EVSERemoval              = new VotingNotificator<DateTime, ChargingStation, EVSE, Boolean>(() => new VetoVote(), true);

            // EVSE events
            this.SocketOutletAddition     = new VotingNotificator<DateTime, EVSE, SocketOutlet, Boolean>(() => new VetoVote(), true);
            this.SocketOutletRemoval      = new VotingNotificator<DateTime, EVSE, SocketOutlet, Boolean>(() => new VetoVote(), true);

            #endregion

            #region Link events

            this._StatusSchedule.     OnStatusChanged += (Timestamp, EventTrackingId, StatusSchedule, OldStatus, NewStatus)
                                                          => UpdateStatus(Timestamp, OldStatus, NewStatus);

            this._AdminStatusSchedule.OnStatusChanged += (Timestamp, EventTrackingId, StatusSchedule, OldStatus, NewStatus)
                                                          => UpdateAdminStatus(Timestamp, OldStatus, NewStatus);

            // ChargingStation events
            this.OnEVSEAddition.           OnVoting       += (timestamp, station, evse, vote)      => ChargingPool.EVSEAddition.           SendVoting      (timestamp, station, evse, vote);
            this.OnEVSEAddition.           OnNotification += (timestamp, station, evse)            => ChargingPool.EVSEAddition.           SendNotification(timestamp, station, evse);

            this.OnEVSERemoval.            OnVoting       += (timestamp, station, evse, vote)      => ChargingPool.EVSERemoval .           SendVoting      (timestamp, station, evse, vote);
            this.OnEVSERemoval.            OnNotification += (timestamp, station, evse)            => ChargingPool.EVSERemoval .           SendNotification(timestamp, station, evse);

            // EVSE events
            this.SocketOutletAddition.     OnVoting       += (timestamp, evse, outlet, vote)       => ChargingPool.SocketOutletAddition.   SendVoting      (timestamp, evse, outlet, vote);
            this.SocketOutletAddition.     OnNotification += (timestamp, evse, outlet)             => ChargingPool.SocketOutletAddition.   SendNotification(timestamp, evse, outlet);

            this.SocketOutletRemoval.      OnVoting       += (timestamp, evse, outlet, vote)       => ChargingPool.SocketOutletRemoval.    SendVoting      (timestamp, evse, outlet, vote);
            this.SocketOutletRemoval.      OnNotification += (timestamp, evse, outlet)             => ChargingPool.SocketOutletRemoval.    SendNotification(timestamp, evse, outlet);

            #endregion

            this.OnPropertyChanged += UpdateData;

            Configurator?.Invoke(this);

            this.RemoteChargingStation = RemoteChargingStationCreator?.Invoke(this);

        }

        #endregion


        #region Data/(Admin-)Status management

        #region OnData/(Admin)StatusChanged

        /// <summary>
        /// An event fired whenever the static data changed.
        /// </summary>
        public event OnChargingStationDataChangedDelegate         OnDataChanged;

        /// <summary>
        /// An event fired whenever the dynamic status changed.
        /// </summary>
        public event OnChargingStationStatusChangedDelegate       OnStatusChanged;

        /// <summary>
        /// An event fired whenever the admin status changed.
        /// </summary>
        public event OnChargingStationAdminStatusChangedDelegate  OnAdminStatusChanged;

        #endregion


        #region SetStatus(NewStatus)

        /// <summary>
        /// Set the status.
        /// </summary>
        /// <param name="NewStatus">A new timestamped status.</param>
        public void SetStatus(Timestamped<ChargingStationStatusTypes>  NewStatus)
        {

            _StatusSchedule.Insert(NewStatus);

        }

        #endregion


        #region SetAdminStatus(NewAdminStatus)

        /// <summary>
        /// Set the admin status.
        /// </summary>
        /// <param name="NewAdminStatus">A new timestamped admin status.</param>
        public void SetAdminStatus(ChargingStationAdminStatusTypes  NewAdminStatus)
        {

            _AdminStatusSchedule.Insert(NewAdminStatus);

        }

        #endregion

        #region SetAdminStatus(NewTimestampedAdminStatus)

        /// <summary>
        /// Set the admin status.
        /// </summary>
        /// <param name="NewTimestampedAdminStatus">A new timestamped admin status.</param>
        public void SetAdminStatus(Timestamped<ChargingStationAdminStatusTypes> NewTimestampedAdminStatus)
        {

            _AdminStatusSchedule.Insert(NewTimestampedAdminStatus);

        }

        #endregion

        #region SetAdminStatus(NewAdminStatus, Timestamp)

        /// <summary>
        /// Set the admin status.
        /// </summary>
        /// <param name="NewAdminStatus">A new admin status.</param>
        /// <param name="Timestamp">The timestamp when this change was detected.</param>
        public void SetAdminStatus(ChargingStationAdminStatusTypes  NewAdminStatus,
                                   DateTime                        Timestamp)
        {

            _AdminStatusSchedule.Insert(NewAdminStatus, Timestamp);

        }

        #endregion

        #region SetAdminStatus(NewAdminStatusList, ChangeMethod = ChangeMethods.Replace)

        /// <summary>
        /// Set the timestamped admin status.
        /// </summary>
        /// <param name="NewAdminStatusList">A list of new timestamped admin status.</param>
        /// <param name="ChangeMethod">The change mode.</param>
        public void SetAdminStatus(IEnumerable<Timestamped<ChargingStationAdminStatusTypes>>  NewAdminStatusList,
                                   ChangeMethods                                             ChangeMethod = ChangeMethods.Replace)
        {

            _AdminStatusSchedule.Insert(NewAdminStatusList, ChangeMethod);

        }

        #endregion


        #region (internal) UpdateData(Timestamp, Sender, PropertyName, OldValue, NewValue)

        /// <summary>
        /// Update the static data.
        /// </summary>
        /// <param name="Timestamp">The timestamp when this change was detected.</param>
        /// <param name="Sender">The changed charging station.</param>
        /// <param name="PropertyName">The name of the changed property.</param>
        /// <param name="OldValue">The old value of the changed property.</param>
        /// <param name="NewValue">The new value of the changed property.</param>
        internal async Task UpdateData(DateTime  Timestamp,
                                       Object    Sender,
                                       String    PropertyName,
                                       Object    OldValue,
                                       Object    NewValue)
        {

            var OnDataChangedLocal = OnDataChanged;
            if (OnDataChangedLocal != null)
                await OnDataChangedLocal(Timestamp, Sender as ChargingStation, PropertyName, OldValue, NewValue);

        }

        #endregion

        #region (internal) UpdateStatus(Timestamp, OldStatus, NewStatus)

        /// <summary>
        /// Update the current status.
        /// </summary>
        /// <param name="Timestamp">The timestamp when this change was detected.</param>
        /// <param name="OldStatus">The old EVSE status.</param>
        /// <param name="NewStatus">The new EVSE status.</param>
        internal void UpdateStatus(DateTime                                Timestamp,
                                   Timestamped<ChargingStationStatusTypes>  OldStatus,
                                   Timestamped<ChargingStationStatusTypes>  NewStatus)
        {

            var OnAggregatedStatusChangedLocal = OnStatusChanged;
            if (OnAggregatedStatusChangedLocal != null)
                OnAggregatedStatusChangedLocal(Timestamp, this, OldStatus, NewStatus);

        }

        #endregion

        #region (internal) UpdateAdminStatus(Timestamp, OldStatus, NewStatus)

        /// <summary>
        /// Update the current admin status.
        /// </summary>
        /// <param name="Timestamp">The timestamp when this change was detected.</param>
        /// <param name="OldStatus">The old charging station admin status.</param>
        /// <param name="NewStatus">The new charging station admin status.</param>
        internal void UpdateAdminStatus(DateTime                                    Timestamp,
                                        Timestamped<ChargingStationAdminStatusTypes>  OldStatus,
                                        Timestamped<ChargingStationAdminStatusTypes>  NewStatus)
        {

            var OnAdminStatusChangedLocal = OnAdminStatusChanged;
            if (OnAdminStatusChangedLocal != null)
                OnAdminStatusChangedLocal(Timestamp, this, OldStatus, NewStatus);

        }

        #endregion

        #endregion

        #region EVSEs

        #region EVSEs

        private readonly EntityHashSet<ChargingStation, EVSE_Id, EVSE> _EVSEs;

        /// <summary>
        /// All Electric Vehicle Supply Equipments (EVSE) present
        /// within this charging station.
        /// </summary>
        public IEnumerable<EVSE> EVSEs

            => _EVSEs;

        #endregion

        #region EVSEIds

        /// <summary>
        /// The unique identifications of all Electric Vehicle Supply Equipment (EVSEs)
        /// present within this charging station.
        /// </summary>
        public IEnumerable<EVSE_Id> EVSEIds

            => _EVSEs.Ids;

        #endregion

        #region EVSEAdminStatus

        /// <summary>
        /// Return the admin status of all EVSEs registered within this charging station.
        /// </summary>

        public IEnumerable<KeyValuePair<EVSE_Id, IEnumerable<Timestamped<EVSEAdminStatusType>>>> EVSEAdminStatus(UInt64 HistorySize)

            => _EVSEs.Select(evse => new KeyValuePair<EVSE_Id, IEnumerable<Timestamped<EVSEAdminStatusType>>>(evse.Id,
                                                                                                              evse.AdminStatusSchedule(HistorySize)));

        #endregion

        #region EVSEStatus

        /// <summary>
        /// Return the status of all EVSEs registered within this charging station.
        /// </summary>
        public IEnumerable<KeyValuePair<EVSE_Id, IEnumerable<Timestamped<EVSEStatusTypes>>>> EVSEStatus(UInt64 HistorySize)

            => _EVSEs.Select(evse => new KeyValuePair<EVSE_Id, IEnumerable<Timestamped<EVSEStatusTypes>>>(evse.Id,
                                                                                                         evse.StatusSchedule(HistorySize)));

        #endregion

        #region EVSEAddition

        internal readonly IVotingNotificator<DateTime, ChargingStation, EVSE, Boolean> EVSEAddition;

        /// <summary>
        /// Called whenever an EVSE will be or was added.
        /// </summary>
        public IVotingSender<DateTime, ChargingStation, EVSE, Boolean> OnEVSEAddition

            => EVSEAddition;

        #endregion

        #region EVSERemoval

        internal readonly IVotingNotificator<DateTime, ChargingStation, EVSE, Boolean> EVSERemoval;

        /// <summary>
        /// Called whenever an EVSE will be or was removed.
        /// </summary>
        public IVotingSender<DateTime, ChargingStation, EVSE, Boolean> OnEVSERemoval

            => EVSERemoval;

        #endregion

        #region CreateEVSE(EVSEId, Configurator = null, RemoteConfigurator = null, OnSuccess = null, OnError = null)

        /// <summary>
        /// Create and register a new EVSE having the given
        /// unique EVSE identification.
        /// </summary>
        /// <param name="EVSEId">The unique identification of the new EVSE.</param>
        /// <param name="Configurator">An optional delegate to configure the new EVSE after its creation.</param>
        /// <param name="RemoteConfigurator">An optional delegate to configure a new remote EVSE after its creation.</param>
        /// <param name="OnSuccess">An optional delegate called after successful creation of the EVSE.</param>
        /// <param name="OnError">An optional delegate for signaling errors.</param>
        public EVSE CreateEVSE(EVSE_Id                           EVSEId,
                               Action<EVSE>                      Configurator        = null,
                               Action<IRemoteEVSE>               RemoteConfigurator  = null,
                               Action<EVSE>                      OnSuccess           = null,
                               Action<ChargingStation, EVSE_Id>  OnError             = null)
        {

            #region Initial checks

            if (EVSEId == null)
                throw new ArgumentNullException(nameof(EVSEId),  "The given EVSE identification must not be null!");

            if (_EVSEs.Any(evse => evse.Id == EVSEId))
            {
                if (OnError == null)
                    throw new EVSEAlreadyExistsInStation(this, EVSEId);
                else
                    OnError?.Invoke(this, EVSEId);
            }

            if (!this.Operator.Ids.Contains(EVSEId.OperatorId))
                throw new InvalidEVSEOperatorId(this,
                                                EVSEId.OperatorId,
                                                this.Operator.Ids);

            #endregion

            var Now   = DateTime.Now;
            var _EVSE = new EVSE(EVSEId, this);

            if (EVSEAddition.SendVoting(Now, this, _EVSE) &&
                _EVSEs.TryAdd(_EVSE))
            {

                _EVSE.OnDataChanged           += UpdateEVSEData;
                _EVSE.OnStatusChanged         += UpdateEVSEStatus;
                _EVSE.OnAdminStatusChanged    += UpdateEVSEAdminStatus;

                _EVSE.OnNewReservation        += SendNewReservation;
                _EVSE.OnReservationCancelled  += SendOnReservationCancelled;
                _EVSE.OnNewChargingSession    += SendNewChargingSession;
                _EVSE.OnNewChargeDetailRecord += SendNewChargeDetailRecord;

                Configurator?.Invoke(_EVSE);
                EVSEAddition.SendNotification(Now, this, _EVSE);
                UpdateEVSEStatus(Now,
                                 EventTracking_Id.New,
                                 _EVSE,
                                 new Timestamped<EVSEStatusTypes>(Now, EVSEStatusTypes.Unspecified),
                                 _EVSE.Status).Wait();

                if (RemoteChargingStation != null)
                {

                    _EVSE.RemoteEVSE = RemoteChargingStation.CreateNewEVSE(EVSEId);

                    _EVSE.OnStatusChanged                    += async (Timestamp, EventTrackingId, EVSE,   OldStatus, NewStatus) => _EVSE.RemoteEVSE.Status           = NewStatus;
                    _EVSE.OnAdminStatusChanged               += async (Timestamp, EventTrackingId, EVSE,   OldStatus, NewStatus) => _EVSE.RemoteEVSE.AdminStatus      = NewStatus;
                    _EVSE.OnNewReservation                   += (Timestamp, Sender, Reservation)                => _EVSE.RemoteEVSE.Reservation      = Reservation;
                    //_EVSE.OnReservationCancelled             += (Timestamp, Sender, Reservation, ReservationCancellation) => _EVSE.RemoteEVSE.Send
                    _EVSE.OnNewChargingSession               += (Timestamp, Sender, ChargingSession)            => _EVSE.RemoteEVSE.ChargingSession  = ChargingSession;

                    _EVSE.RemoteEVSE.OnStatusChanged         += async (Timestamp, EventTrackingId, RemoteEVSE, OldStatus, NewStatus)   => _EVSE.Status                      = NewStatus;
                    _EVSE.RemoteEVSE.OnAdminStatusChanged    += async (Timestamp, EventTrackingId, RemoteEVSE, OldStatus, NewStatus)   => _EVSE.AdminStatus                 = NewStatus;
                    _EVSE.RemoteEVSE.OnNewReservation        += (Timestamp, RemoteEVSE, Reservation)            => _EVSE.Reservation                 = Reservation;
                    _EVSE.RemoteEVSE.OnReservationCancelled  += _EVSE.SendOnReservationCancelled;
                    _EVSE.RemoteEVSE.OnNewChargingSession    += (Timestamp, RemoteEVSE, ChargingSession)        => _EVSE.ChargingSession             = ChargingSession;
                    _EVSE.RemoteEVSE.OnNewChargeDetailRecord += (Timestamp, RemoteEVSE, ChargeDetailRecord)     => _EVSE.SendNewChargeDetailRecord(Timestamp, RemoteEVSE, ChargeDetailRecord);

                    RemoteConfigurator?.Invoke(_EVSE.RemoteEVSE);

                }

                OnSuccess?.Invoke(_EVSE);

                return _EVSE;

            }

            Debug.WriteLine("EVSE '" + EVSEId + "' was not created!");
            return null;

        }

        #endregion


        #region ContainsEVSE(EVSE)

        /// <summary>
        /// Check if the given EVSE is already present within the charging station.
        /// </summary>
        /// <param name="EVSE">An EVSE.</param>
        public Boolean ContainsEVSE(EVSE EVSE)

            => _EVSEs.Contains(EVSE);

        #endregion

        #region ContainsEVSE(EVSEId)

        /// <summary>
        /// Check if the given EVSE identification is already present within the charging station.
        /// </summary>
        /// <param name="EVSEId">The unique identification of an EVSE.</param>
        public Boolean ContainsEVSE(EVSE_Id EVSEId)

            => _EVSEs.ContainsId(EVSEId);

        #endregion

        #region GetEVSEbyId(EVSEId)

        public EVSE GetEVSEbyId(EVSE_Id EVSEId)

            => _EVSEs.GetById(EVSEId);

        #endregion

        #region TryGetEVSEbyId(EVSEId, out EVSE)

        public Boolean TryGetEVSEbyId(EVSE_Id EVSEId, out EVSE EVSE)

            => _EVSEs.TryGet(EVSEId, out EVSE);

        #endregion

        #region RemoveEVSE(EVSEId)

        public EVSE RemoveEVSE(EVSE_Id EVSEId)

            => _EVSEs.Remove(EVSEId);

        #endregion

        #region TryRemoveEVSE(EVSEId, out EVSE)

        public Boolean TryRemoveEVSE(EVSE_Id EVSEId, out EVSE EVSE)

            => _EVSEs.TryRemove(EVSEId, out EVSE);

        #endregion


        #region OnEVSEData/(Admin)StatusChanged

        /// <summary>
        /// An event fired whenever the static data of any subordinated EVSE changed.
        /// </summary>
        public event OnEVSEDataChangedDelegate         OnEVSEDataChanged;

        /// <summary>
        /// An event fired whenever the dynamic status of any subordinated EVSE changed.
        /// </summary>
        public event OnEVSEStatusChangedDelegate       OnEVSEStatusChanged;

        /// <summary>
        /// An event fired whenever the admin status of any subordinated EVSE changed.
        /// </summary>
        public event OnEVSEAdminStatusChangedDelegate  OnEVSEAdminStatusChanged;

        #endregion

        #region SocketOutletAddition

        internal readonly IVotingNotificator<DateTime, EVSE, SocketOutlet, Boolean> SocketOutletAddition;

        /// <summary>
        /// Called whenever a socket outlet will be or was added.
        /// </summary>
        public IVotingSender<DateTime, EVSE, SocketOutlet, Boolean> OnSocketOutletAddition

            => SocketOutletAddition;

        #endregion

        #region SocketOutletRemoval

        internal readonly IVotingNotificator<DateTime, EVSE, SocketOutlet, Boolean> SocketOutletRemoval;

        /// <summary>
        /// Called whenever a socket outlet will be or was removed.
        /// </summary>
        public IVotingSender<DateTime, EVSE, SocketOutlet, Boolean> OnSocketOutletRemoval

            => SocketOutletRemoval;

        #endregion


        #region (internal) UpdateEVSEData(Timestamp, EVSE, OldStatus, NewStatus)

        /// <summary>
        /// Update the data of an EVSE.
        /// </summary>
        /// <param name="Timestamp">The timestamp when this change was detected.</param>
        /// <param name="EVSE">The changed EVSE.</param>
        /// <param name="PropertyName">The name of the changed property.</param>
        /// <param name="OldValue">The old value of the changed property.</param>
        /// <param name="NewValue">The new value of the changed property.</param>
        internal async Task UpdateEVSEData(DateTime  Timestamp,
                                           EVSE      EVSE,
                                           String    PropertyName,
                                           Object    OldValue,
                                           Object    NewValue)
        {

            var OnEVSEDataChangedLocal = OnEVSEDataChanged;
            if (OnEVSEDataChangedLocal != null)
                await OnEVSEDataChangedLocal(Timestamp, EVSE, PropertyName, OldValue, NewValue);

        }

        #endregion

        #region (internal) UpdateEVSEAdminStatus(Timestamp, EventTrackingId, EVSE, OldStatus, NewStatus)

        /// <summary>
        /// Update the current charging station status.
        /// </summary>
        /// <param name="Timestamp">The timestamp when this change was detected.</param>
        /// <param name="EventTrackingId">An event tracking identification for correlating this request with other events.</param>
        /// <param name="EVSE">The updated EVSE.</param>
        /// <param name="OldStatus">The old EVSE status.</param>
        /// <param name="NewStatus">The new EVSE status.</param>
        internal async Task UpdateEVSEAdminStatus(DateTime                          Timestamp,
                                                  EventTracking_Id                  EventTrackingId,
                                                  EVSE                              EVSE,
                                                  Timestamped<EVSEAdminStatusType>  OldStatus,
                                                  Timestamped<EVSEAdminStatusType>  NewStatus)
        {

            var OnEVSEAdminStatusChangedLocal = OnEVSEAdminStatusChanged;
            if (OnEVSEAdminStatusChangedLocal != null)
                await OnEVSEAdminStatusChangedLocal(Timestamp,
                                                    EventTrackingId,
                                                    EVSE,
                                                    OldStatus,
                                                    NewStatus);

        }

        #endregion

        #region (internal) UpdateEVSEStatus     (Timestamp, EventTrackingId, EVSE, OldStatus, NewStatus)

        /// <summary>
        /// Update the current charging station status.
        /// </summary>
        /// <param name="Timestamp">The timestamp when this change was detected.</param>
        /// <param name="EventTrackingId">An event tracking identification for correlating this request with other events.</param>
        /// <param name="EVSE">The updated EVSE.</param>
        /// <param name="OldStatus">The old EVSE status.</param>
        /// <param name="NewStatus">The new EVSE status.</param>
        internal async Task UpdateEVSEStatus(DateTime                     Timestamp,
                                             EventTracking_Id             EventTrackingId,
                                             EVSE                         EVSE,
                                             Timestamped<EVSEStatusTypes>  OldStatus,
                                             Timestamped<EVSEStatusTypes>  NewStatus)
        {

            var OnEVSEStatusChangedLocal = OnEVSEStatusChanged;
            if (OnEVSEStatusChangedLocal != null)
                await OnEVSEStatusChangedLocal(Timestamp,
                                               EventTrackingId,
                                               EVSE,
                                               OldStatus,
                                               NewStatus);

            if (StatusAggregationDelegate != null)
            {
                _StatusSchedule.Insert(StatusAggregationDelegate(new EVSEStatusReport(_EVSEs)),
                                       Timestamp);
            }

        }

        #endregion


        #region IEnumerable<EVSE> Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => _EVSEs.GetEnumerator();

        public IEnumerator<EVSE> GetEnumerator()
            => _EVSEs.GetEnumerator();

        #endregion

        #endregion


        #region Reservations

        /// <summary>
        /// The maximum time span for a reservation.
        /// </summary>
        public static readonly TimeSpan MaxReservationDuration = TimeSpan.FromMinutes(30);

        #region ChargingReservations

        /// <summary>
        /// Return all current charging reservations.
        /// </summary>

        public IEnumerable<ChargingReservation> ChargingReservations

            => _EVSEs.
                   Select(evse        => evse.Reservation).
                   Where (reservation => reservation != null);

        #endregion

        #region OnReserve... / OnReserved... / OnNewReservation

        /// <summary>
        /// An event fired whenever an EVSE is being reserved.
        /// </summary>
        public event OnReserveEVSERequestDelegate              OnReserveEVSERequest;

        /// <summary>
        /// An event fired whenever an EVSE was reserved.
        /// </summary>
        public event OnReserveEVSEResponseDelegate             OnReserveEVSEResponse;

        /// <summary>
        /// An event fired whenever a charging station is being reserved.
        /// </summary>
        public event OnReserveChargingStationRequestDelegate   OnReserveChargingStationRequest;

        /// <summary>
        /// An event fired whenever a charging station was reserved.
        /// </summary>
        public event OnReserveChargingStationResponseDelegate  OnReserveChargingStationResponse;

        /// <summary>
        /// An event fired whenever a new charging reservation was created.
        /// </summary>
        public event OnNewReservationDelegate                  OnNewReservation;

        #endregion

        #region Reserve(...EVSEId, StartTime, Duration, ReservationId = null, ProviderId = null, ...)

        /// <summary>
        /// Reserve the possibility to charge at the given EVSE.
        /// </summary>
        /// <param name="EVSEId">The unique identification of the EVSE to be reserved.</param>
        /// <param name="StartTime">The starting time of the reservation.</param>
        /// <param name="Duration">The duration of the reservation.</param>
        /// <param name="ReservationId">An optional unique identification of the reservation. Mandatory for updates.</param>
        /// <param name="ProviderId">An optional unique identification of e-Mobility service provider.</param>
        /// <param name="eMAId">An optional unique identification of e-Mobility account/customer requesting this reservation.</param>
        /// <param name="ChargingProduct">The charging product to be reserved.</param>
        /// <param name="AuthTokens">A list of authentication tokens, who can use this reservation.</param>
        /// <param name="eMAIds">A list of eMobility account identifications, who can use this reservation.</param>
        /// <param name="PINs">A list of PINs, who can be entered into a pinpad to use this reservation.</param>
        /// 
        /// <param name="Timestamp">The optional timestamp of the request.</param>
        /// <param name="CancellationToken">An optional token to cancel this request.</param>
        /// <param name="EventTrackingId">An optional event tracking identification for correlating this request with other events.</param>
        /// <param name="RequestTimeout">An optional timeout for this request.</param>
        public async Task<ReservationResult>

            Reserve(EVSE_Id                           EVSEId,
                    DateTime?                         StartTime           = null,
                    TimeSpan?                         Duration            = null,
                    ChargingReservation_Id?           ReservationId       = null,
                    eMobilityProvider_Id?             ProviderId          = null,
                    eMobilityAccount_Id?              eMAId               = null,
                    ChargingProduct                   ChargingProduct     = null,
                    IEnumerable<Auth_Token>           AuthTokens          = null,
                    IEnumerable<eMobilityAccount_Id>  eMAIds              = null,
                    IEnumerable<UInt32>               PINs                = null,

                    DateTime?                         Timestamp           = null,
                    CancellationToken?                CancellationToken   = null,
                    EventTracking_Id                  EventTrackingId     = null,
                    TimeSpan?                         RequestTimeout      = null)

        {

            #region Initial checks

            if (EVSEId == null)
                throw new ArgumentNullException(nameof(EVSEId),  "The given EVSE identification must not be null!");

            ReservationResult result = null;

            if (!Timestamp.HasValue)
                Timestamp = DateTime.Now;

            if (EventTrackingId == null)
                EventTrackingId = EventTracking_Id.New;

            #endregion

            #region Send OnReserveEVSERequest event

            var Runtime = Stopwatch.StartNew();

            try
            {

                OnReserveEVSERequest?.Invoke(DateTime.Now,
                                             Timestamp.Value,
                                             this,
                                             EventTrackingId,
                                             ChargingPool.Operator.RoamingNetwork.Id,
                                             ReservationId,
                                             EVSEId,
                                             StartTime,
                                             Duration,
                                             ProviderId,
                                             eMAId,
                                             ChargingProduct,
                                             AuthTokens,
                                             eMAIds,
                                             PINs,
                                             RequestTimeout);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnReserveEVSERequest));
            }

            #endregion


            if (AdminStatus.Value == ChargingStationAdminStatusTypes.Operational ||
                AdminStatus.Value == ChargingStationAdminStatusTypes.InternalUse)
            {

                #region Try the remote charging station...

                if (RemoteChargingStation != null)
                {

                    result = await RemoteChargingStation.
                                       Reserve(EVSEId,
                                               StartTime,
                                               Duration,
                                               ReservationId,
                                               ProviderId,
                                               eMAId,
                                               ChargingProduct,
                                               AuthTokens,
                                               eMAIds,
                                               PINs,

                                               Timestamp,
                                               CancellationToken,
                                               EventTrackingId,
                                               RequestTimeout);

                }

                #endregion

                #region ...else/or try local

                if (RemoteChargingStation == null ||
                    (result != null &&
                    (result.Result == ReservationResultType.UnknownEVSE ||
                     result.Result == ReservationResultType.Error)))
                {


                    var _EVSE = GetEVSEbyId(EVSEId);

                    if (_EVSE != null)
                    {

                        result = await _EVSE.Reserve(StartTime,
                                                     Duration,
                                                     ReservationId,
                                                     ProviderId,
                                                     eMAId,
                                                     ChargingProduct,
                                                     AuthTokens,
                                                     eMAIds,
                                                     PINs,

                                                     Timestamp,
                                                     CancellationToken,
                                                     EventTrackingId,
                                                     RequestTimeout);

                    }

                    else
                        result = ReservationResult.UnknownEVSE;

                }

                #endregion

                #region In case of success...

                if (result != null &&
                    result.Result == ReservationResultType.Success)
                {

                //    // The reservation can be delivered within the response
                //    // or via an explicit message afterwards!
                //    if (result.Session != null)
                //    {

                //        if (result.Session.ChargingStation == null)
                //            result.Session.ChargingStation = this;

                //    }

                }

                #endregion

            }
            else
            {

                switch (AdminStatus.Value)
                {

                    case ChargingStationAdminStatusTypes.OutOfService:
                        result = ReservationResult.OutOfService;
                        break;

                    default:
                        result = ReservationResult.NoEVSEsAvailable;
                        break;

                }

            }

            #region Send OnReserveEVSEResponse event

            Runtime.Stop();

            try
            {

                OnReserveEVSEResponse?.Invoke(DateTime.Now,
                                              Timestamp.Value,
                                              this,
                                              EventTrackingId,
                                              ChargingPool.Operator.RoamingNetwork.Id,
                                              ReservationId,
                                              EVSEId,
                                              StartTime,
                                              Duration,
                                              ProviderId,
                                              eMAId,
                                              ChargingProduct,
                                              AuthTokens,
                                              eMAIds,
                                              PINs,
                                              result,
                                              Runtime.Elapsed,
                                              RequestTimeout);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnReserveEVSEResponse));
            }

            #endregion

            return result;

        }

        #endregion

        #region Reserve(...StartTime, Duration, ReservationId = null, ProviderId = null, ...)

        /// <summary>
        /// Reserve the possibility to charge.
        /// </summary>
        /// <param name="StartTime">The starting time of the reservation.</param>
        /// <param name="Duration">The duration of the reservation.</param>
        /// <param name="ReservationId">An optional unique identification of the reservation. Mandatory for updates.</param>
        /// <param name="ProviderId">An optional unique identification of e-Mobility service provider.</param>
        /// <param name="eMAId">An optional unique identification of e-Mobility account/customer requesting this reservation.</param>
        /// <param name="ChargingProduct">The charging product to be reserved.</param>
        /// <param name="AuthTokens">A list of authentication tokens, who can use this reservation.</param>
        /// <param name="eMAIds">A list of eMobility account identifications, who can use this reservation.</param>
        /// <param name="PINs">A list of PINs, who can be entered into a pinpad to use this reservation.</param>
        /// 
        /// <param name="Timestamp">The optional timestamp of the request.</param>
        /// <param name="CancellationToken">An optional token to cancel this request.</param>
        /// <param name="EventTrackingId">An optional event tracking identification for correlating this request with other events.</param>
        /// <param name="RequestTimeout">An optional timeout for this request.</param>
        public async Task<ReservationResult>

            Reserve(DateTime?                         StartTime           = null,
                    TimeSpan?                         Duration            = null,
                    ChargingReservation_Id?           ReservationId       = null,
                    eMobilityProvider_Id?             ProviderId          = null,
                    eMobilityAccount_Id?              eMAId               = null,
                    ChargingProduct                   ChargingProduct     = null,
                    IEnumerable<Auth_Token>           AuthTokens          = null,
                    IEnumerable<eMobilityAccount_Id>  eMAIds              = null,
                    IEnumerable<UInt32>               PINs                = null,

                    DateTime?                         Timestamp           = null,
                    CancellationToken?                CancellationToken   = null,
                    EventTracking_Id                  EventTrackingId     = null,
                    TimeSpan?                         RequestTimeout      = null)

        {

            #region Initial checks

            ReservationResult result = null;

            if (!Timestamp.HasValue)
                Timestamp = DateTime.Now;

            if (EventTrackingId == null)
                EventTrackingId = EventTracking_Id.New;

            #endregion

            #region Send OnReserveChargingStationRequest event

            var Runtime = Stopwatch.StartNew();

            try
            {

                OnReserveChargingStationRequest?.Invoke(DateTime.Now,
                                                        Timestamp.Value,
                                                        this,
                                                        EventTrackingId,
                                                        ChargingPool.Operator.RoamingNetwork.Id,
                                                        Id,
                                                        StartTime,
                                                        Duration,
                                                        ReservationId,
                                                        ProviderId,
                                                        eMAId,
                                                        ChargingProduct,
                                                        AuthTokens,
                                                        eMAIds,
                                                        PINs,
                                                        RequestTimeout);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnReserveChargingStationRequest));
            }

            #endregion


            if (AdminStatus.Value == ChargingStationAdminStatusTypes.Operational ||
                AdminStatus.Value == ChargingStationAdminStatusTypes.InternalUse)
            {

                if (RemoteChargingStation != null)
                {

                    result = await RemoteChargingStation.
                                       Reserve(StartTime,
                                               Duration,
                                               ReservationId,
                                               ProviderId,
                                               eMAId,
                                               ChargingProduct,
                                               AuthTokens,
                                               eMAIds,
                                               PINs,

                                               Timestamp,
                                               CancellationToken,
                                               EventTrackingId,
                                               RequestTimeout);

                }

                else
                    result = ReservationResult.Offline;

            }
            else
            {

                switch (AdminStatus.Value)
                {

                    case ChargingStationAdminStatusTypes.OutOfService:
                        result = ReservationResult.OutOfService;
                        break;

                    default:
                        result = ReservationResult.NoEVSEsAvailable;
                        break;

                }

            }


            #region Send OnReserveChargingStationResponse event

            Runtime.Stop();

            try
            {

                OnReserveChargingStationResponse?.Invoke(DateTime.Now,
                                                         Timestamp.Value,
                                                         this,
                                                         EventTrackingId,
                                                         ChargingPool.Operator.RoamingNetwork.Id,
                                                         Id,
                                                         StartTime,
                                                         Duration,
                                                         ReservationId,
                                                         ProviderId,
                                                         eMAId,
                                                         ChargingProduct,
                                                         AuthTokens,
                                                         eMAIds,
                                                         PINs,
                                                         result,
                                                         Runtime.Elapsed,
                                                         RequestTimeout);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnReserveChargingStationResponse));
            }

            #endregion

            return result;

        }

        #endregion

        #region (internal) SendNewReservation(Timestamp, Sender, Reservation)

        internal void SendNewReservation(DateTime             Timestamp,
                                         Object               Sender,
                                         ChargingReservation  Reservation)
        {

            var OnNewReservationLocal = OnNewReservation;
            if (OnNewReservationLocal != null)
                OnNewReservationLocal(Timestamp, Sender, Reservation);

        }

        #endregion


        #region TryGetReservationById(ReservationId, out Reservation)

        /// <summary>
        /// Return the charging reservation specified by its unique identification.
        /// </summary>
        /// <param name="ReservationId">The charging reservation identification.</param>
        /// <param name="Reservation">The charging reservation identification.</param>
        /// <returns>True when successful, false otherwise.</returns>
        public Boolean TryGetReservationById(ChargingReservation_Id ReservationId, out ChargingReservation Reservation)
        {

            Reservation = _EVSEs.Where (evse => evse.Reservation    != null &&
                                                evse.Reservation.Id == ReservationId).
                                 Select(evse => evse.Reservation).
                                 FirstOrDefault();

            return Reservation != null;

        }

        #endregion


        #region CancelReservation(...ReservationId, Reason, ProviderId = null, EVSEId = null, ...)

        /// <summary>
        /// Try to remove the given charging reservation.
        /// </summary>
        /// <param name="ReservationId">The unique charging reservation identification.</param>
        /// <param name="Reason">A reason for this cancellation.</param>
        /// <param name="ProviderId">An optional unique identification of e-Mobility service provider.</param>
        /// <param name="EVSEId">An optional identification of the EVSE.</param>
        /// 
        /// <param name="Timestamp">The optional timestamp of the request.</param>
        /// <param name="CancellationToken">An optional token to cancel this request.</param>
        /// <param name="EventTrackingId">An optional event tracking identification for correlating this request with other events.</param>
        /// <param name="RequestTimeout">An optional timeout for this request.</param>
        public async Task<CancelReservationResult>

            CancelReservation(ChargingReservation_Id                 ReservationId,
                              ChargingReservationCancellationReason  Reason,
                              eMobilityProvider_Id?                  ProviderId          = null,
                              EVSE_Id?                               EVSEId              = null,

                              DateTime?                              Timestamp           = null,
                              CancellationToken?                     CancellationToken   = null,
                              EventTracking_Id                       EventTrackingId     = null,
                              TimeSpan?                              RequestTimeout      = null)

        {

            #region Initial checks

            if (ReservationId == null)
                throw new ArgumentNullException(nameof(ReservationId), "The given charging reservation identification must not be null!");

            CancelReservationResult result = null;

            if (!Timestamp.HasValue)
                Timestamp = DateTime.Now;

            if (EventTrackingId == null)
                EventTrackingId = EventTracking_Id.New;

            #endregion


            if (AdminStatus.Value == ChargingStationAdminStatusTypes.Operational ||
                AdminStatus.Value == ChargingStationAdminStatusTypes.InternalUse)
            {

                #region Try the remote charging station...

                if (RemoteChargingStation != null)
                {

                    result = await RemoteChargingStation.
                                       CancelReservation(ReservationId,
                                                         Reason,
                                                         ProviderId,
                                                         EVSEId,

                                                         Timestamp,
                                                         CancellationToken,
                                                         EventTrackingId,
                                                         RequestTimeout);

                }

                #endregion

                #region Cancel locally...

                var _EVSE = _EVSEs.FirstOrDefault(evse => evse.Reservation    != null &&
                                                  evse.Reservation.Id == ReservationId);

                if (_EVSE != null)
                {

                    await _EVSE.CancelReservation(ReservationId,
                                                  Reason,
                                                  ProviderId,

                                                  Timestamp,
                                                  CancellationToken,
                                                  EventTrackingId,
                                                  RequestTimeout);

                }

                #endregion

            }
            else
            {

                switch (AdminStatus.Value)
                {

                    case ChargingStationAdminStatusTypes.OutOfService:
                        result = CancelReservationResult.OutOfService(ReservationId,
                                                                      Reason);
                        break;

                    default:
                        result = CancelReservationResult.Error(ReservationId,
                                                               Reason);
                        break;

                }

            }

            return result;

        }

        #endregion

        #region OnReservationCancelled

        /// <summary>
        /// An event fired whenever a charging reservation was deleted.
        /// </summary>
        public event OnCancelReservationResponseDelegate OnReservationCancelled;

        #endregion

        #region (internal) SendOnReservationCancelled(...)

        internal Task SendOnReservationCancelled(DateTime                               LogTimestamp,
                                                 DateTime                               RequestTimestamp,
                                                 Object                                 Sender,
                                                 EventTracking_Id                       EventTrackingId,

                                                 RoamingNetwork_Id?                     RoamingNetworkId,
                                                 eMobilityProvider_Id?                  ProviderId,
                                                 ChargingReservation_Id                 ReservationId,
                                                 ChargingReservation                    Reservation,
                                                 ChargingReservationCancellationReason  Reason,
                                                 CancelReservationResult                Result,
                                                 TimeSpan                               Runtime,
                                                 TimeSpan?                              RequestTimeout)
        {

            return OnReservationCancelled?.Invoke(LogTimestamp,
                                                  RequestTimestamp,
                                                  Sender,
                                                  EventTrackingId,
                                                  RoamingNetworkId,
                                                  ProviderId,
                                                  ReservationId,
                                                  Reservation,
                                                  Reason,
                                                  Result,
                                                  Runtime,
                                                  RequestTimeout);

        }

        #endregion

        #endregion

        #region RemoteStart/-Stop and Sessions

        #region ChargingSessions

        /// <summary>
        /// Return all current charging sessions.
        /// </summary>

        public IEnumerable<ChargingSession> ChargingSessions

            => _EVSEs.
                   Select(evse    => evse.ChargingSession).
                   Where (session => session != null);


        #region GetChargingSessionById(ChargingSessionId)

        public ChargingSession GetChargingSessionById(ChargingSession_Id ChargingSessionId)

            => _EVSEs.Where (evse => evse.ChargingSession != null &&
                                     evse.ChargingSession.Id == ChargingSessionId).
                      Select(evse => evse.ChargingSession).
                      FirstOrDefault();

        #endregion

        #endregion


        #region OnRemote...Start / OnRemote...Started / OnNewChargingSession

        /// <summary>
        /// An event fired whenever a remote start EVSE command was received.
        /// </summary>
        public event OnRemoteStartEVSERequestDelegate              OnRemoteEVSEStartRequest;

        /// <summary>
        /// An event fired whenever a remote start EVSE command completed.
        /// </summary>
        public event OnRemoteStartEVSEResponseDelegate             OnRemoteEVSEStartResponse;

        /// <summary>
        /// An event fired whenever a remote start charging station command was received.
        /// </summary>
        public event OnRemoteChargingStationStartRequestDelegate   OnRemoteChargingStationStartRequest;

        /// <summary>
        /// An event fired whenever a remote start charging station command completed.
        /// </summary>
        public event OnRemoteChargingStationStartResponseDelegate  OnRemoteChargingStationStartResponse;

        /// <summary>
        /// An event fired whenever a new charging session was created.
        /// </summary>
        public event OnNewChargingSessionDelegate                  OnNewChargingSession;

        #endregion

        #region RemoteStart(...EVSEId, ChargingProduct = null, ReservationId = null, SessionId = null, ProviderId = null, eMAId = null, ...)

        /// <summary>
        /// Start a charging session at the given EVSE.
        /// </summary>
        /// <param name="EVSEId">The unique identification of the EVSE to be started.</param>
        /// <param name="ChargingProduct">The choosen charging product.</param>
        /// <param name="ReservationId">The unique identification for a charging reservation.</param>
        /// <param name="SessionId">The unique identification for this charging session.</param>
        /// <param name="ProviderId">The unique identification of the e-mobility service provider for the case it is different from the current message sender.</param>
        /// <param name="eMAId">The unique identification of the e-mobility account.</param>
        /// 
        /// <param name="Timestamp">The optional timestamp of the request.</param>
        /// <param name="CancellationToken">An optional token to cancel this request.</param>
        /// <param name="EventTrackingId">An optional event tracking identification for correlating this request with other events.</param>
        /// <param name="RequestTimeout">An optional timeout for this request.</param>
        public async Task<RemoteStartEVSEResult>

            RemoteStart(EVSE_Id                  EVSEId,
                        ChargingProduct          ChargingProduct     = null,
                        ChargingReservation_Id?  ReservationId       = null,
                        ChargingSession_Id?      SessionId           = null,
                        eMobilityProvider_Id?    ProviderId          = null,
                        eMobilityAccount_Id?     eMAId               = null,

                        DateTime?                Timestamp           = null,
                        CancellationToken?       CancellationToken   = null,
                        EventTracking_Id         EventTrackingId     = null,
                        TimeSpan?                RequestTimeout      = null)

        {

            #region Initial checks

            if (!Timestamp.HasValue)
                Timestamp = DateTime.Now;

            if (!CancellationToken.HasValue)
                CancellationToken = new CancellationTokenSource().Token;

            if (EventTrackingId == null)
                EventTrackingId = EventTracking_Id.New;


            RemoteStartEVSEResult result = null;

            #endregion

            #region Send OnRemoteEVSEStart event

            var Runtime = Stopwatch.StartNew();

            try
            {

                OnRemoteEVSEStartRequest?.Invoke(DateTime.Now,
                                          Timestamp.Value,
                                          this,
                                          EventTrackingId,
                                          ChargingPool.Operator.RoamingNetwork.Id,
                                          EVSEId,
                                          ChargingProduct,
                                          ReservationId,
                                          SessionId,
                                          ProviderId,
                                          eMAId,
                                          RequestTimeout);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnRemoteEVSEStartRequest));
            }

            #endregion


            if (AdminStatus.Value == ChargingStationAdminStatusTypes.Operational ||
                AdminStatus.Value == ChargingStationAdminStatusTypes.InternalUse)
            {

                #region Try the remote charging station...

                if (RemoteChargingStation != null)
                {

                    result = await RemoteChargingStation.
                                       RemoteStart(EVSEId,
                                                   ChargingProduct,
                                                   ReservationId,
                                                   SessionId,
                                                   ProviderId,
                                                   eMAId,

                                                   Timestamp,
                                                   CancellationToken,
                                                   EventTrackingId,
                                                   RequestTimeout);

                }

                #endregion

                #region ...else/or try local

                if (RemoteChargingStation == null ||
                    (result                != null &&
                    (result.Result         == RemoteStartEVSEResultType.UnknownEVSE ||
                     result.Result         == RemoteStartEVSEResultType.Error)))
                {


                    var _EVSE = GetEVSEbyId(EVSEId);

                    if (_EVSE != null)
                    {

                        result = await _EVSE.RemoteStart(ChargingProduct,
                                                         ReservationId,
                                                         SessionId,
                                                         ProviderId,
                                                         eMAId,

                                                         Timestamp,
                                                         CancellationToken,
                                                         EventTrackingId,
                                                         RequestTimeout);

                    }

                    else
                        result = RemoteStartEVSEResult.UnknownEVSE;

                }

                #endregion

                #region In case of success...

                if (result != null &&
                    result.Result == RemoteStartEVSEResultType.Success)
                {

                    // The session can be delivered within the response
                    // or via an explicit message afterwards!
                    if (result.Session != null)
                    {

                        if (result.Session.ChargingStation == null)
                            result.Session.ChargingStation = this;

                    }

                }

                #endregion

            }
            else
            {

                switch (AdminStatus.Value)
                {

                    default:
                        result = RemoteStartEVSEResult.OutOfService;
                        break;

                }

            }



            #region Send OnRemoteEVSEStarted event

            Runtime.Stop();

            try
            {

                OnRemoteEVSEStartResponse?.Invoke(DateTime.Now,
                                            Timestamp.Value,
                                            this,
                                            EventTrackingId,
                                            ChargingPool.Operator.RoamingNetwork.Id,
                                            EVSEId,
                                            ChargingProduct,
                                            ReservationId,
                                            SessionId,
                                            ProviderId,
                                            eMAId,
                                            RequestTimeout,
                                            result,
                                            Runtime.Elapsed);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnRemoteEVSEStartResponse));
            }

            #endregion

            return result;

        }

        #endregion

        #region RemoteStart(...ChargingProduct = null, ReservationId = null, SessionId = null, ProviderId = null, eMAId = null, ...)

        /// <summary>
        /// Start a charging session at the given charging station.
        /// </summary>
        /// <param name="ChargingProduct">The choosen charging product.</param>
        /// <param name="ReservationId">The unique identification for a charging reservation.</param>
        /// <param name="SessionId">The unique identification for this charging session.</param>
        /// <param name="ProviderId">The unique identification of the e-mobility service provider for the case it is different from the current message sender.</param>
        /// <param name="eMAId">The unique identification of the e-mobility account.</param>
        /// 
        /// <param name="Timestamp">The optional timestamp of the request.</param>
        /// <param name="CancellationToken">An optional token to cancel this request.</param>
        /// <param name="EventTrackingId">An optional event tracking identification for correlating this request with other events.</param>
        /// <param name="RequestTimeout">An optional timeout for this request.</param>
        public async Task<RemoteStartChargingStationResult>

            RemoteStart(ChargingProduct          ChargingProduct     = null,
                        ChargingReservation_Id?  ReservationId       = null,
                        ChargingSession_Id?      SessionId           = null,
                        eMobilityProvider_Id?    ProviderId          = null,
                        eMobilityAccount_Id?     eMAId               = null,

                        DateTime?                Timestamp           = null,
                        CancellationToken?       CancellationToken   = null,
                        EventTracking_Id         EventTrackingId     = null,
                        TimeSpan?                RequestTimeout      = null)

        {

            #region Initial checks

            if (!Timestamp.HasValue)
                Timestamp = DateTime.Now;

            if (!CancellationToken.HasValue)
                CancellationToken = new CancellationTokenSource().Token;

            if (EventTrackingId == null)
                EventTrackingId = EventTracking_Id.New;


            RemoteStartChargingStationResult result = null;

            #endregion

            #region Send OnRemoteChargingStationStartRequest event

            var Runtime = Stopwatch.StartNew();

            try
            {

                OnRemoteChargingStationStartRequest?.Invoke(DateTime.Now,
                                                            Timestamp.Value,
                                                            this,
                                                            EventTrackingId,
                                                            ChargingPool.Operator.RoamingNetwork.Id,
                                                            Id,
                                                            ChargingProduct,
                                                            ReservationId,
                                                            SessionId,
                                                            ProviderId,
                                                            eMAId,
                                                            RequestTimeout);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnRemoteChargingStationStartRequest));
            }

            #endregion


            if (AdminStatus.Value == ChargingStationAdminStatusTypes.Operational ||
                AdminStatus.Value == ChargingStationAdminStatusTypes.InternalUse)
            {

                if (RemoteChargingStation != null)
                {
                    result = await RemoteChargingStation.
                                      RemoteStart(ChargingProduct,
                                                  ReservationId,
                                                  SessionId,
                                                  ProviderId,
                                                  eMAId,

                                                  Timestamp,
                                                  CancellationToken,
                                                  EventTrackingId,
                                                  RequestTimeout);
                }

                //if (result == null)
                //{

                //    var OnRemoteStartChargingStationLocal = OnRemoteStartChargingStation;
                //    if (OnRemoteStartChargingStationLocal == null)
                //        return RemoteStartChargingStationResult.Error("");

                //    var results = await Task.WhenAll(OnRemoteStartChargingStationLocal.
                //                                         GetInvocationList().
                //                                         Select(subscriber => (subscriber as OnRemoteStartChargingStationDelegate)
                //                                             (Timestamp,
                //                                              this,
                //                                              CancellationToken,
                //                                              EventTrackingId,
                //                                              Id,
                //                                              ChargingProductId,
                //                                              ReservationId,
                //                                              SessionId,
                //                                              ProviderId,
                //                                              eMAId,
                //                                              RequestTimeout)));

                //    result = results.
                //                 Where(result2 => result2.Result != RemoteStartChargingStationResultType.Unspecified).
                //                 First();

                //}

                //if (result == null)
                //{

                //    var _AvailableEVSE = _EVSEs.Where(evse => evse.Status.Value == EVSEStatusType.Available).
                //                                FirstOrDefault();

                //    if (_AvailableEVSE != null)
                //    {

                //        RemoteStartEVSEResult EVSEStartResult = null;

                //        EVSEStartResult = await RemoteStart(Timestamp,
                //                                            CancellationToken,
                //                                            EventTrackingId,
                //                                            _AvailableEVSE.Id,
                //                                            ChargingProductId,
                //                                            ReservationId,
                //                                            SessionId,
                //                                            ProviderId,
                //                                            eMAId);

                //        switch (EVSEStartResult.Result)
                //        {

                //            case RemoteStartEVSEResultType.Error:
                //                result = RemoteStartChargingStationResult.Error(EVSEStartResult.ErrorMessage);
                //                break;



                //        }

                //    }

                //}

            }
            else
            {

                switch (AdminStatus.Value)
                {

                    default:
                        result = RemoteStartChargingStationResult.OutOfService;
                        break;

                }

            }


            #region Send OnRemoteChargingStationStartRequest event

            Runtime.Stop();

            try
            {

                OnRemoteChargingStationStartResponse?.Invoke(DateTime.Now,
                                                             Timestamp.Value,
                                                             this,
                                                             EventTrackingId,
                                                             ChargingPool.Operator.RoamingNetwork.Id,
                                                             Id,
                                                             ChargingProduct,
                                                             ReservationId,
                                                             SessionId,
                                                             ProviderId,
                                                             eMAId,
                                                             RequestTimeout,
                                                             result,
                                                             Runtime.Elapsed);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnRemoteChargingStationStartResponse));
            }

            #endregion

            return result;

        }

        #endregion

        #region (internal) SendNewChargingSession(Timestamp, Sender, Session)

        internal void SendNewChargingSession(DateTime         Timestamp,
                                             Object           Sender,
                                             ChargingSession  Session)
        {

            if (Session != null)
            {

                if (Session.ChargingStation == null)
                    Session.ChargingStation = this;

            }

            OnNewChargingSession?.Invoke(Timestamp, Sender, Session);

        }

        #endregion


        #region OnRemote...Stop / OnRemote...Stopped / OnNewChargeDetailRecord

        /// <summary>
        /// An event fired whenever a remote stop command was received.
        /// </summary>
        public event OnRemoteStopRequestDelegate                  OnRemoteStopRequest;

        /// <summary>
        /// An event fired whenever a remote stop command completed.
        /// </summary>
        public event OnRemoteStopResponseDelegate                 OnRemoteStopResponse;

        /// <summary>
        /// An event fired whenever a remote stop EVSE command was received.
        /// </summary>
        public event OnRemoteStopEVSERequestDelegate              OnRemoteEVSEStopRequest;

        /// <summary>
        /// An event fired whenever a remote stop EVSE command completed.
        /// </summary>
        public event OnRemoteStopEVSEResponseDelegate             OnRemoteEVSEStopResponse;

        /// <summary>
        /// An event fired whenever a new charge detail record was created.
        /// </summary>
        public event OnNewChargeDetailRecordDelegate              OnNewChargeDetailRecord;

        #endregion

        #region RemoteStop(...SessionId, ReservationHandling, ProviderId = null, eMAId = null, ...)

        /// <summary>
        /// Stop the given charging session.
        /// </summary>
        /// <param name="SessionId">The unique identification for this charging session.</param>
        /// <param name="ReservationHandling">Whether to remove the reservation after session end, or to keep it open for some more time.</param>
        /// <param name="ProviderId">The unique identification of the e-mobility service provider.</param>
        /// <param name="eMAId">The unique identification of the e-mobility account.</param>
        /// 
        /// <param name="Timestamp">The optional timestamp of the request.</param>
        /// <param name="CancellationToken">An optional token to cancel this request.</param>
        /// <param name="EventTrackingId">An optional event tracking identification for correlating this request with other events.</param>
        /// <param name="RequestTimeout">An optional timeout for this request.</param>
        public async Task<RemoteStopResult>

            RemoteStop(ChargingSession_Id     SessionId,
                       ReservationHandling    ReservationHandling,
                       eMobilityProvider_Id?  ProviderId          = null,
                       eMobilityAccount_Id?   eMAId               = null,

                       DateTime?              Timestamp           = null,
                       CancellationToken?     CancellationToken   = null,
                       EventTracking_Id       EventTrackingId     = null,
                       TimeSpan?              RequestTimeout      = null)

        {

            #region Initial checks

            if (!Timestamp.HasValue)
                Timestamp = DateTime.Now;

            if (!CancellationToken.HasValue)
                CancellationToken = new CancellationTokenSource().Token;

            if (EventTrackingId == null)
                EventTrackingId = EventTracking_Id.New;


            RemoteStopResult result = null;

            #endregion

            #region Send OnRemoteStopRequest event

            var Runtime = Stopwatch.StartNew();

            try
            {

                OnRemoteStopRequest?.Invoke(DateTime.Now,
                                            Timestamp.Value,
                                            this,
                                            EventTrackingId,
                                            ChargingPool.Operator.RoamingNetwork.Id,
                                            SessionId,
                                            ReservationHandling,
                                            ProviderId,
                                            eMAId,
                                            RequestTimeout);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnRemoteStopRequest));
            }

            #endregion


            if (AdminStatus.Value == ChargingStationAdminStatusTypes.Operational ||
                AdminStatus.Value == ChargingStationAdminStatusTypes.InternalUse)
            {

                if (RemoteChargingStation != null)
                {
                    result = await RemoteChargingStation.
                                      RemoteStop(SessionId,
                                                 ReservationHandling,
                                                 ProviderId,
                                                 eMAId,

                                                 Timestamp,
                                                 CancellationToken,
                                                 EventTrackingId,
                                                 RequestTimeout);
                }

                if (RemoteChargingStation == null ||
                    (result        != null &&
                     result.Result == RemoteStopResultType.Error))
                {

                    var ChargingSession = ChargingSessions.
                                          FirstOrDefault(session => session.Id == SessionId);

                    if (ChargingSession == null)
                        result = RemoteStopResult.InvalidSessionId(SessionId);

                    if (result == null && ChargingSession.EVSE == null)
                        result = RemoteStopResult.Error(SessionId, "Unkown EVSE for the given charging session identification!");

                    if (result == null)
                    {

                        var result2 = await ChargingSession.
                                                EVSE.
                                                RemoteStop(SessionId,
                                                           ReservationHandling,
                                                           ProviderId,
                                                           eMAId,

                                                           Timestamp,
                                                           CancellationToken,
                                                           EventTrackingId,
                                                           RequestTimeout);

                        switch (result2.Result)
                        {

                            case RemoteStopEVSEResultType.Error:
                                result = RemoteStopResult.Error(SessionId, result2.Message);
                                break;

                        }

                    }

                    if (result == null)
                        result = RemoteStopResult.Error(SessionId);

                }

            }
            else
            {

                switch (AdminStatus.Value)
                {

                    default:
                        result = RemoteStopResult.OutOfService(SessionId);
                        break;

                }

            }


            #region Send OnRemoteStopResponse event

            Runtime.Stop();

            try
            {

                OnRemoteStopResponse?.Invoke(DateTime.Now,
                                        Timestamp.Value,
                                        this,
                                        EventTrackingId,
                                        ChargingPool.Operator.RoamingNetwork.Id,
                                        SessionId,
                                        ReservationHandling,
                                        ProviderId,
                                        eMAId,
                                        RequestTimeout,
                                        result,
                                        Runtime.Elapsed);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnRemoteStopResponse));
            }

            #endregion

            return result;

        }

        #endregion

        #region RemoteStop(...EVSEId, SessionId, ReservationHandling, ProviderId = null, eMAId = null, ...)

        /// <summary>
        /// Stop the given charging session at the given EVSE.
        /// </summary>
        /// <param name="EVSEId">The unique identification of the EVSE to be stopped.</param>
        /// <param name="SessionId">The unique identification for this charging session.</param>
        /// <param name="ReservationHandling">Whether to remove the reservation after session end, or to keep it open for some more time.</param>
        /// <param name="ProviderId">The unique identification of the e-mobility service provider.</param>
        /// <param name="eMAId">The unique identification of the e-mobility account.</param>
        /// 
        /// <param name="Timestamp">The optional timestamp of the request.</param>
        /// <param name="CancellationToken">An optional token to cancel this request.</param>
        /// <param name="EventTrackingId">An optional event tracking identification for correlating this request with other events.</param>
        /// <param name="RequestTimeout">An optional timeout for this request.</param>
        public async Task<RemoteStopEVSEResult>

            RemoteStop(EVSE_Id                EVSEId,
                       ChargingSession_Id     SessionId,
                       ReservationHandling    ReservationHandling,
                       eMobilityProvider_Id?  ProviderId          = null,
                       eMobilityAccount_Id?   eMAId               = null,

                       DateTime?              Timestamp           = null,
                       CancellationToken?     CancellationToken   = null,
                       EventTracking_Id       EventTrackingId     = null,
                       TimeSpan?              RequestTimeout      = null)

        {

            #region Initial checks

            if (!Timestamp.HasValue)
                Timestamp = DateTime.Now;

            if (!CancellationToken.HasValue)
                CancellationToken = new CancellationTokenSource().Token;

            if (EventTrackingId == null)
                EventTrackingId = EventTracking_Id.New;


            RemoteStopEVSEResult result = null;

            #endregion

            #region Send OnRemoteEVSEStopRequest event

            var Runtime = Stopwatch.StartNew();

            try
            {

                OnRemoteEVSEStopRequest?.Invoke(DateTime.Now,
                                                Timestamp.Value,
                                                this,
                                                EventTrackingId,
                                                ChargingPool.Operator.RoamingNetwork.Id,
                                                EVSEId,
                                                SessionId,
                                                ReservationHandling,
                                                ProviderId,
                                                eMAId,
                                                RequestTimeout);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnRemoteEVSEStopRequest));
            }

            #endregion


            if (AdminStatus.Value == ChargingStationAdminStatusTypes.Operational ||
                AdminStatus.Value == ChargingStationAdminStatusTypes.InternalUse)
            {

                #region Try the remote charging station...

                if (RemoteChargingStation != null)
                {

                    result = await RemoteChargingStation.
                                       RemoteStop(EVSEId,
                                                  SessionId,
                                                  ReservationHandling,
                                                  ProviderId,
                                                  eMAId,

                                                  Timestamp,
                                                  CancellationToken,
                                                  EventTrackingId,
                                                  RequestTimeout);

                }

                #endregion

                #region ...else/or try local

                if (RemoteChargingStation == null ||
                    (result                != null &&
                    (result.Result         == RemoteStopEVSEResultType.UnknownEVSE ||
                     result.Result         == RemoteStopEVSEResultType.Error)))
                {


                    var _EVSE = GetEVSEbyId(EVSEId);

                    if (_EVSE != null)
                    {

                        result = await _EVSE.RemoteStop(SessionId,
                                                        ReservationHandling,
                                                        ProviderId,
                                                        eMAId,

                                                        Timestamp,
                                                        CancellationToken,
                                                        EventTrackingId,
                                                        RequestTimeout);

                    }

                    else
                        result = RemoteStopEVSEResult.UnknownEVSE(SessionId);

                }

                #endregion

                #region In case of success...

                if (result        != null &&
                    result.Result == RemoteStopEVSEResultType.Success)
                {

                    // The charge detail record can be delivered within the response
                    // or via an explicit message afterwards!
                    if (result.ChargeDetailRecord != null)
                    {

                        //if (result.ChargeDetailRecord.ChargingStation == null)
                        //    result.ChargeDetailRecord.ChargingStation = this;

                    }

                }

                #endregion

            }
            else
            {

                switch (AdminStatus.Value)
                {

                    default:
                        result = RemoteStopEVSEResult.OutOfService(SessionId);
                        break;

                }

            }


            #region Send OnRemoteEVSEStopResponse event

            Runtime.Stop();

            try
            {

                OnRemoteEVSEStopResponse?.Invoke(DateTime.Now,
                                                 Timestamp.Value,
                                                 this,
                                                 EventTrackingId,
                                                 ChargingPool.Operator.RoamingNetwork.Id,
                                                 EVSEId,
                                                 SessionId,
                                                 ReservationHandling,
                                                 ProviderId,
                                                 eMAId,
                                                 RequestTimeout,
                                                 result,
                                                 Runtime.Elapsed);

            }
            catch (Exception e)
            {
                e.Log(nameof(ChargingStation) + "." + nameof(OnRemoteStopResponse));
            }

            #endregion

            return result;

        }

        #endregion

        #region (internal) SendNewChargeDetailRecord(Timestamp, Sender, ChargeDetailRecord)

        internal void SendNewChargeDetailRecord(DateTime            Timestamp,
                                                Object              Sender,
                                                ChargeDetailRecord  ChargeDetailRecord)
        {

            OnNewChargeDetailRecord?.Invoke(Timestamp, Sender, ChargeDetailRecord);

        }

        #endregion

        #endregion


        public void AddParkingSpaces(params ParkingSpace[] ParkingSpaces)
        {

            if (ParkingSpaces != null)
                _ParkingSpaces.AddRange(ParkingSpaces);

        }



        #region UpdateWith(OtherChargingStation)

        /// <summary>
        /// Update this charging station with the data of the other charging station.
        /// </summary>
        /// <param name="OtherChargingStation">Another charging station.</param>
        public ChargingStation UpdateWith(ChargingStation OtherChargingStation)
        {

            Name                 = OtherChargingStation.Name;
            Description          = OtherChargingStation.Description;
            Brand                = OtherChargingStation.Brand;
            HotlinePhoneNumber   = OtherChargingStation.HotlinePhoneNumber;

            Address              = OtherChargingStation.Address;
            GeoLocation          = OtherChargingStation.GeoLocation;
            EntranceAddress      = OtherChargingStation.EntranceAddress;
            EntranceLocation     = OtherChargingStation.EntranceLocation;
//            ArrivalInstructions  = OtherChargingStation.ArrivalInstructions;
            OpeningTimes         = OtherChargingStation.OpeningTimes;
            ExitAddress          = OtherChargingStation.ExitAddress;
            ExitLocation         = OtherChargingStation.ExitLocation;

            AuthenticationModes  = OtherChargingStation.AuthenticationModes;
            PaymentOptions       = OtherChargingStation.PaymentOptions;
            Accessibility        = OtherChargingStation.Accessibility;

//            PoolOwner            = OtherChargingStation.PoolOwner;
//            LocationOwner        = OtherChargingStation.LocationOwner;
            PhotoURIs            = OtherChargingStation.PhotoURIs;

            return this;

        }

        #endregion



        #region IComparable<ChargingStation> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is a charging station.
            var ChargingStation = Object as ChargingStation;
            if ((Object) ChargingStation == null)
                throw new ArgumentException("The given object is not a charging station!");

            return CompareTo(ChargingStation);

        }

        #endregion

        #region CompareTo(ChargingStation)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingStation">A charging station object to compare with.</param>
        public Int32 CompareTo(ChargingStation ChargingStation)
        {

            if ((Object) ChargingStation == null)
                throw new ArgumentNullException("The given charging station must not be null!");

            return Id.CompareTo(ChargingStation.Id);

        }

        #endregion

        #endregion

        #region IEquatable<ChargingStation> Members

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

            // Check if the given object is a charging station.
            var ChargingStation = Object as ChargingStation;
            if ((Object) ChargingStation == null)
                return false;

            return this.Equals(ChargingStation);

        }

        #endregion

        #region Equals(ChargingStation)

        /// <summary>
        /// Compares two charging stations for equality.
        /// </summary>
        /// <param name="ChargingStation">A charging station to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(ChargingStation ChargingStation)
        {

            if ((Object) ChargingStation == null)
                return false;

            return Id.Equals(ChargingStation.Id);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Get the hashcode of this object.
        /// </summary>
        public override Int32 GetHashCode()
            => Id.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
            => Id.ToString();

        #endregion


        public class Builder
        {

            #region Properties

            /// <summary>
            /// The internal service identification of the charging station maintained by the Charging Station Operator.
            /// </summary>
            [Optional]
            public String ServiceIdentification { get; set; }

            [Optional]
            public String HubjectStationId { get; set; }

            /// <summary>
            /// The offical (multi-language) name of this charging station.
            /// </summary>
            [Mandatory]
            public I18NString Name { get; set; }

            /// <summary>
            /// An optional (multi-language) description of this charging station.
            /// </summary>
            [Optional]
            public I18NString Description { get; set; }

            /// <summary>
            /// A brand for this charging station
            /// is this is different from the Charging Station Operator.
            /// </summary>
            [Optional]
            public Brand Brand { get; set; }

            /// <summary>
            /// The address of this charging station.
            /// </summary>
            [Optional]
            public Address Address { get; set; }

            /// <summary>
            /// OSM Node Id.
            /// </summary>
            [Optional]
            public String OSM_NodeId { get; set; }

            /// <summary>
            /// The geographical location of this charging station.
            /// </summary>
            [Optional]
            public GeoCoordinate GeoLocation { get; set; }

            /// <summary>
            /// The address of the entrance to this charging station.
            /// (If different from 'Address').
            /// </summary>
            [Optional]
            public Address EntranceAddress { get; set; }

            /// <summary>
            /// The geographical location of the entrance to this charging station.
            /// (If different from 'GeoLocation').
            /// </summary>
            [Optional]
            public GeoCoordinate EntranceLocation { get; set; }

            /// <summary>
            /// The address of the exit of this charging station.
            /// (If different from 'Address').
            /// </summary>
            [Optional]
            public Address ExitAddress { get; set; }

            /// <summary>
            /// The geographical location of the exit of this charging station.
            /// (If different from 'GeoLocation').
            /// </summary>
            [Optional]
            public GeoCoordinate ExitLocation { get; set; }

            /// <summary>
            /// parking spaces reachable from this charging station.
            /// </summary>
            [Optional]
            public HashSet<ParkingSpace> ParkingSpaces { get; set; }

            /// <summary>
            /// The opening times of this charging station.
            /// </summary>
            public OpeningTimes OpeningTimes { get; set; }

            public HashSet<AuthenticationModes> AuthenticationModes { get; set; }

            [Mandatory]
            public HashSet<PaymentOptions> PaymentOptions { get; set; }

            [Optional]
            public AccessibilityTypes Accessibility { get; set; }

            /// <summary>
            /// The telephone number of the Charging Station Operator hotline.
            /// </summary>
            [Optional]
            public String HotlinePhoneNumber { get; set; }

            [Optional]
            public Boolean IsHubjectCompatible { get; set; }

            [Optional]
            public Boolean DynamicInfoAvailable { get; set; }

            /// <summary>
            /// A comment from the users.
            /// </summary>
            [Optional]
            public I18NString UserComment { get; set; }

            /// <summary>
            /// A comment from the service provider.
            /// </summary>
            [Optional]
            public I18NString ServiceProviderComment { get; set; }

            /// <summary>
            /// The grid connection of the charging station.
            /// </summary>
            [Optional]
            public GridConnection GridConnection { get; set; }

            /// <summary>
            /// The features of the charging station.
            /// </summary>
            [Optional]
            public ChargingStationUIFeatures UIFeatures { get; set; }

            /// <summary>
            /// URIs of photos of this charging station.
            /// </summary>
            [Optional]
            public HashSet<String> PhotoURIs { get; set; }

            /// <summary>
            /// A delegate called to aggregate the dynamic status of all subordinated EVSEs.
            /// </summary>
            public Func<EVSEStatusReport, ChargingStationStatusTypes> StatusAggregationDelegate { get; set; }

            /// <summary>
            /// The charging station admin status schedule.
            /// </summary>
            public IEnumerable<Timestamped<ChargingStationAdminStatusTypes>> AdminStatusSchedule { get; set; }

            #endregion

            public Builder(ChargingStation_Id Id)
            { }

        }

    }

}
