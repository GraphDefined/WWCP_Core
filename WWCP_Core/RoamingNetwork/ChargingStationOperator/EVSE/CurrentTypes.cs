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
using System.Collections.Generic;

#endregion

namespace org.GraphDefined.WWCP
{

    /// <summary>
    /// Extention methods for current types.
    /// </summary>
    public static class CurrentTypesExtentions
    {

        public static CurrentTypes Reduce(this IEnumerable<CurrentTypes> EnumerationOfCurrentTypes)
        {

            var _CurrentTypes = CurrentTypes.Unspecified;

            foreach (var CurrentType in EnumerationOfCurrentTypes)
                _CurrentTypes |= CurrentType;

            return _CurrentTypes;

        }

        public static IEnumerable<CurrentTypes> ToEnumeration(this CurrentTypes CurrentTypesEnum)

            => Enum.GetValues(typeof(CurrentTypes)).
                    Cast<CurrentTypes>().
                    Where(flag => CurrentTypesEnum.HasFlag(flag) && flag != CurrentTypes.Unspecified);

        public static IEnumerable<String> ToText(this CurrentTypes CurrentTypesEnum)

            => CurrentTypesEnum.ToEnumeration().Select(item => item.ToString());

    }

    /// <summary>
    /// The type of the current at an EVSE.
    /// </summary>
    [Flags]
    public enum CurrentTypes
    {

        /// <summary>
        /// Unknown current type.
        /// </summary>
        Unspecified         = 0,

        /// <summary>
        /// AC with 1 phase.
        /// </summary>
        AC_OnePhase         = 1,

        /// <summary>
        /// AC with 3 phases.
        /// </summary>
        AC_ThreePhases      = 2,

        /// <summary>
        /// Direct current.
        /// </summary>
        DC                  = 4

    }

}
