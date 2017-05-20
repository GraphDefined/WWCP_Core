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
using System.Collections.Generic;

#endregion

namespace org.GraphDefined.WWCP
{

    public abstract class ACustomData
    {

        #region Data

        private Dictionary<String, Object> _CustomData   { get; }

        public IReadOnlyDictionary<String, Object> Values
            => _CustomData;

        #endregion

        #region Constructor(s)

        protected ACustomData(IReadOnlyDictionary<String, Object> CustomData)
        {

            if (CustomData != null)
            {

                this._CustomData = new Dictionary<String, Object>();

                foreach (var item in CustomData)
                    _CustomData.Add(item.Key, item.Value);

            }

        }

        #endregion


        public Boolean IsDefined(String Key)
        {

            if (_CustomData == null)
                return false;

            Object _Value;

            return Values.TryGetValue(Key, out _Value);

        }

        public Object GetCustomData(String Key)
        {

            if (_CustomData == null)
                return null;

            Object _Value;

            if (Values.TryGetValue(Key, out _Value))
                return _Value;

            return null;

        }

        public T GetCustomDataAs<T>(String Key)
        {

            if (_CustomData == null)
                return default(T);

            Object _Value;

            if (Values.TryGetValue(Key, out _Value))
                return (T) _Value;

            return default(T);

        }


        public void IfDefined(String          Key,
                              Action<Object>  ValueDelegate)
        {

            if (_CustomData   == null ||
                ValueDelegate == null)
                return;

            Object _Value;

            if (Values.TryGetValue(Key, out _Value))
                ValueDelegate(_Value);

        }

        public void IfDefinedAs<T>(String     Key,
                                   Action<T>  ValueDelegate)
        {

            if (_CustomData   == null ||
                ValueDelegate == null)
                return;

            Object _Value;

            if (Values.TryGetValue(Key, out _Value))
                ValueDelegate((T) _Value);

        }

    }


    public abstract class ACustomDataBuilder : ICustomDataBuilder
    {

        #region Data

        private Dictionary<String, Object> _CustomData;

        #endregion

        #region Constructor(s)

        protected ACustomDataBuilder(Dictionary<String, Object> CustomData = null)
        {

            this._CustomData = CustomData;

        }

        #endregion


        public void AddCustomData(String Key,
                                  Object Value)
        {

            if (_CustomData == null)
                _CustomData = new Dictionary<String, Object>();

            _CustomData.Add(Key, Value);

        }

        public Boolean IsDefined(String Key)
        {

            Object _Value;

            return _CustomData.TryGetValue(Key, out _Value);

        }

        public Object GetCustomData(String Key)
        {

            Object _Value;

            if (_CustomData.TryGetValue(Key, out _Value))
                return _Value;

            return null;

        }

        public T GetCustomDataAs<T>(String Key)
        {

            Object _Value;

            if (_CustomData.TryGetValue(Key, out _Value))
                return (T)_Value;

            return default(T);

        }


        public void IfDefined(String Key,
                              Action<Object> ValueDelegate)
        {

            if (ValueDelegate == null)
                return;

            Object _Value;

            if (_CustomData.TryGetValue(Key, out _Value))
                ValueDelegate(_Value);

        }

        public void IfDefinedAs<T>(String Key,
                                   Action<T> ValueDelegate)
        {

            if (ValueDelegate == null)
                return;

            Object _Value;

            if (_CustomData.TryGetValue(Key, out _Value))
                ValueDelegate((T)_Value);

        }

    }

}