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
using System.Collections.Generic;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace org.GraphDefined.WWCP
{

    /// <summary>
    /// An EVSE status diff.
    /// </summary>
    public class EVSEStatusDiff : StatusDiff<EVSE_Id, EVSEStatusTypes>
    {

        #region EVSEStatusDiff(Timestamp, EVSEOperatorId, EVSEOperatorName = null)

        /// <summary>
        /// Create a new EVSE status diff.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the status diff.</param>
        /// <param name="EVSEOperatorId">The unique identification of the Charging Station Operator.</param>
        /// <param name="EVSEOperatorName">The optional internationalized name of the Charging Station Operator.</param>
        public EVSEStatusDiff(DateTime         Timestamp,
                              ChargingStationOperator_Id  EVSEOperatorId,
                              I18NString       EVSEOperatorName = null)

            : base(Timestamp, EVSEOperatorId, EVSEOperatorName)

        { }

        #endregion

        #region EVSEStatusDiff(Timestamp, EVSEOperatorId, NewStatus, ChangedStatus, RemovedIds, EVSEOperatorName = null)

        /// <summary>
        /// Create a new EVSE status diff.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the status diff.</param>
        /// <param name="EVSEOperatorId">The unique identification of the Charging Station Operator.</param>
        /// <param name="NewStatus">All new status.</param>
        /// <param name="ChangedStatus">All changed status.</param>
        /// <param name="RemovedIds">All removed status.</param>
        /// <param name="EVSEOperatorName">The optional internationalized name of the Charging Station Operator.</param>
        public EVSEStatusDiff(DateTime                                            Timestamp,
                              ChargingStationOperator_Id                                     EVSEOperatorId,
                              IEnumerable<KeyValuePair<EVSE_Id, EVSEStatusTypes>>  NewStatus,
                              IEnumerable<KeyValuePair<EVSE_Id, EVSEStatusTypes>>  ChangedStatus,
                              IEnumerable<EVSE_Id>                                RemovedIds,
                              I18NString                                          EVSEOperatorName = null)

            : base(Timestamp, EVSEOperatorId, NewStatus, ChangedStatus, RemovedIds, EVSEOperatorName)

        { }

        #endregion

    }
}
