﻿/*
 * Copyright (c) 2013-2014 Achim Friedland <achim.friedland@graphdefined.com>
 * This file is part of eMI3 Core <http://www.github.com/eMI3/Core>
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
using System.Collections.Concurrent;

#endregion

namespace org.emi3group
{

    /// <summary>
    /// A EVS pool exception.
    /// </summary>
    public class ChargingStationException : eMI3Exception
    {

        public ChargingStationException(String Message)
            : base(Message)
        { }

        public ChargingStationException(String Message, Exception InnerException)
            : base(Message, InnerException)
        { }

    }


    #region EVSEAlreadyExists

    /// <summary>
    /// An exception thrown whenever an EVSE already exists within the given charging station.
    /// </summary>
    public class EVSEAlreadyExists : ChargingStationException
    {

        public EVSEAlreadyExists(EVSE_Id             EVSE_Id,
                                 ChargingStation_Id  ChargingStation_Id)
            : base("The given EVSE identification '" + EVSE_Id + "' already exists within the given '" + ChargingStation_Id + "' charging station!")
        { }

    }

    #endregion

}