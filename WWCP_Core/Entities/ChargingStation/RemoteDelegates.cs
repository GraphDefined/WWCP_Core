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

using org.GraphDefined.Vanaheimr.Illias;
using System;

#endregion

namespace org.GraphDefined.WWCP
{

    public delegate void CSConnectedDelegate(IRemoteChargingStation ChargingStation);

    public delegate void CSEVSEOperatorTimeoutReachedDelegate(IRemoteChargingStation ChargingStation, String EVSEOperatorDNS);

    public delegate void CSDisconnectedDelegate(IRemoteChargingStation ChargingStation);

    public delegate void CSStateChangedDelegate(IRemoteChargingStation ChargingStation,
                                            ChargingStationStatusType OldState,
                                            ChargingStationStatusType NewState);


    /// <summary>
    /// A delegate called whenever the static data of any subordinated EVSE changed.
    /// </summary>
    /// <param name="Timestamp">The timestamp when this change was detected.</param>
    /// <param name="EVSE">The updated EVSE.</param>
    /// <param name="PropertyName">The name of the changed property.</param>
    /// <param name="OldValue">The old value of the changed property.</param>
    /// <param name="NewValue">The new value of the changed property.</param>
    public delegate void OnEVSEDataChangedDelegate(DateTime Timestamp, IRemoteEVSE EVSE, String PropertyName, Object OldValue, Object NewValue);

    /// <summary>
    /// A delegate called whenever the dynamic status of any subordinated EVSE changed.
    /// </summary>
    /// <param name="Timestamp">The timestamp when this change was detected.</param>
    /// <param name="EVSE">The updated EVSE.</param>
    /// <param name="OldStatus">The old timestamped status of the EVSE.</param>
    /// <param name="NewStatus">The new timestamped status of the EVSE.</param>
    public delegate void OnEVSEStatusChangedDelegate(DateTime Timestamp, IRemoteEVSE EVSE, Timestamped<EVSEStatusType> OldStatus, Timestamped<EVSEStatusType> NewStatus);


    /// <summary>
    /// A delegate called whenever the admin status of any subordinated EVSE changed.
    /// </summary>
    /// <param name="Timestamp">The timestamp when this change was detected.</param>
    /// <param name="EVSE">The updated EVSE.</param>
    /// <param name="OldStatus">The old timestamped admin status of the EVSE.</param>
    /// <param name="NewStatus">The new timestamped admin status of the EVSE.</param>
    public delegate void OnEVSEAdminStatusChangedDelegate(DateTime Timestamp, IRemoteEVSE EVSE, Timestamped<EVSEAdminStatusType> OldStatus, Timestamped<EVSEAdminStatusType> NewStatus);




}