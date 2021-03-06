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
    /// The status schedule of an EVSE.
    /// </summary>
    public class EVSEStatusSchedule : ACustomData
    {

        #region Properties

        /// <summary>
        /// The unique identification of the EVSE.
        /// </summary>
        public EVSE_Id                                    Id               { get; }

        /// <summary>
        /// The timestamped status of the EVSE.
        /// </summary>
        public IEnumerable<Timestamped<EVSEStatusTypes>>  StatusSchedule   { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new EVSE admin status.
        /// </summary>
        /// <param name="Id">The unique identification of the EVSE.</param>
        /// <param name="StatusSchedule">The timestamped admin status of the EVSE.</param>
        /// <param name="CustomData">An optional dictionary of customer-specific data.</param>
        public EVSEStatusSchedule(EVSE_Id                                    Id,
                                  IEnumerable<Timestamped<EVSEStatusTypes>>  StatusSchedule,
                                  IReadOnlyDictionary<String, Object>        CustomData  = null)

            : base(CustomData)

        {

            this.Id              = Id;
            this.StatusSchedule  = StatusSchedule;

        }

        #endregion

    }

}
