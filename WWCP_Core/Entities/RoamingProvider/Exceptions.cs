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
using System.Collections.Generic;
using System.Collections.Concurrent;

#endregion

namespace org.GraphDefined.WWCP
{

    #region RoamingProviderAlreadyExists

    /// <summary>
    /// An exception thrown whenever a roaming provider already exists within the given roaming network.
    /// </summary>
    public class RoamingProviderAlreadyExists : RoamingNetworkException
    {

        public RoamingProviderAlreadyExists(RoamingNetwork      RoamingNetwork,
                                            RoamingProvider_Id  RoamingProviderId)

            : base(RoamingNetwork,
                   "The given roaming provider identification '" + RoamingProviderId + "' already exists within the given '" + RoamingNetwork.Id + "' roaming network!")

        { }

    }

    #endregion

}
