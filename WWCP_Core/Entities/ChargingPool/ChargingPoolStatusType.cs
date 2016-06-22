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

namespace org.GraphDefined.WWCP
{

    /// <summary>
    /// The current status of a charging pool.
    /// </summary>
    public enum ChargingPoolStatusType
    {

        /// <summary>
        /// Unclear or unknown status of the charging pool.
        /// </summary>
        Unspecified         = 0,

        /// <summary>
        /// The charging pool is planned for the future.
        /// </summary>
        Planned             = 1,

        /// <summary>
        /// The charging pool is currently in deployment, but not fully operational yet.
        /// </summary>
        InDeployment        = 2,

        /// <summary>
        /// The charging pool is not ready for charging because it is under maintenance.
        /// </summary>
        OutOfService        = 3,

        /// <summary>
        /// Currently no communication with the charging pool possible, but charging in offline mode might be available.
        /// </summary>
        Offline             = 4,

        /// <summary>
        /// The charging pool is ready to charge.
        /// </summary>
        Available           = 5,

        /// <summary>
        /// Some ongoing charging sessions or reservations, but still ready to charge.
        /// </summary>
        PartialAvailable    = 6,

        /// <summary>
        /// The entire charging pool was reserved by an ev customer.
        /// </summary>
        Reserved            = 7,

        /// <summary>
        /// The entire charging pool is charging. Currently no additional charging sessions are possible.
        /// </summary>
        Charging            = 8,

        /// <summary>
        /// An error has occured.
        /// </summary>
        Faulted             = 9,

        /// <summary>
        /// Private or internal use.
        /// </summary>
        Other               = 10,

        /// <summary>
        /// The charging pool was not found!
        /// (Only valid within batch-processing)
        /// </summary>
        UnknownPool         = 11

    }

}
