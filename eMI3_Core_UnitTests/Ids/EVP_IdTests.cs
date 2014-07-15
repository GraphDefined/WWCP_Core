﻿/*
 * Copyright (c) 2013 Achim Friedland <achim.friedland@graphdefined.com>
 * This file is part of eMI3 Mockup <http://www.github.com/eMI3/Mockup>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
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

using NUnit.Framework;

using org.emi3group;

#endregion

namespace org.emi3group.UnitTests
{

    /// <summary>
    /// Unit tests for the EVP_Id class.
    /// </summary>
    [TestFixture]
    public class EVP_IdTests
    {

        #region EVP_IdEmptyConstructorTest()

        /// <summary>
        /// A test for an empty EVP_Id constructor.
        /// </summary>
        [Test]
        public void EVP_IdEmptyConstructorTest()
        {
            var _EVP_Id1 = new ChargingPool_Id();
            var _EVP_Id2 = new ChargingPool_Id();
            Assert.IsTrue(_EVP_Id1.Length > 0);
            Assert.IsTrue(_EVP_Id2.Length > 0);
            Assert.AreNotEqual(_EVP_Id1, _EVP_Id2);
        }

        #endregion

        #region EVP_IdStringConstructorTest()

        /// <summary>
        /// A test for the EVP_Id string constructor.
        /// </summary>
        [Test]
        public void EVP_IdStringConstructorTest()
        {
            var _EVP_Id = new ChargingPool_Id("123");
            Assert.AreEqual("123", _EVP_Id.ToString());
            Assert.AreEqual(3,     _EVP_Id.Length);
        }

        #endregion

        #region EVP_IdEVP_IdConstructorTest()

        /// <summary>
        /// A test for the EVP_Id EVP_Id constructor.
        /// </summary>
        [Test]
        public void EVP_IdEVP_IdConstructorTest()
        {
            var _EVP_Id1 = ChargingPool_Id.New;
            var _EVP_Id2 = _EVP_Id1.Clone;
            Assert.AreEqual(_EVP_Id1.ToString(), _EVP_Id2.ToString());
            Assert.AreEqual(_EVP_Id1.Length,     _EVP_Id2.Length);
            Assert.AreEqual(_EVP_Id1,            _EVP_Id2);
        }

        #endregion


        #region NewEVP_IdMethodTest()

        /// <summary>
        /// A test for the static newEVP_Id method.
        /// </summary>
        [Test]
        public void NewEVP_IdMethodTest()
        {
            var _EVP_Id1 = ChargingPool_Id.New;
            var _EVP_Id2 = ChargingPool_Id.New;
            Assert.AreNotEqual(_EVP_Id1, _EVP_Id2);
        }

        #endregion


        #region op_Equality_Null_Test1()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test1()
        {
            var      _EVP_Id1 = new ChargingPool_Id();
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsFalse(_EVP_Id1 == _EVP_Id2);
        }

        #endregion

        #region op_Equality_Null_Test2()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test2()
        {
            ChargingPool_Id _EVP_Id1 = null;
            var      _EVP_Id2 = new ChargingPool_Id();
            Assert.IsFalse(_EVP_Id1 == _EVP_Id2);
        }

        #endregion

        #region op_Equality_BothNull_Test()

        /// <summary>
        /// A test for the equality operator both null.
        /// </summary>
        [Test]
        public void op_Equality_BothNull_Test()
        {
            ChargingPool_Id _EVP_Id1 = null;
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsTrue(_EVP_Id1 == _EVP_Id2);
        }

        #endregion

        #region op_Equality_SameReference_Test()

        /// <summary>
        /// A test for the equality operator same reference.
        /// </summary>
        [Test]
        
        public void op_Equality_SameReference_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id();
            #pragma warning disable
            Assert.IsTrue(_EVP_Id1 == _EVP_Id1);
            #pragma warning restore
        }

        #endregion

        #region op_Equality_Equals_Test()

        /// <summary>
        /// A test for the equality operator equals.
        /// </summary>
        [Test]
        public void op_Equality_Equals_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsTrue(_EVP_Id1 == _EVP_Id2);
        }

        #endregion

        #region op_Equality_NotEquals_Test()

        /// <summary>
        /// A test for the equality operator not-equals.
        /// </summary>
        [Test]
        public void op_Equality_NotEquals_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("2");
            Assert.IsFalse(_EVP_Id1 == _EVP_Id2);
        }

        #endregion


        #region op_Inequality_Null_Test1()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test1()
        {
            var      _EVP_Id1 = new ChargingPool_Id();
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsTrue(_EVP_Id1 != _EVP_Id2);
        }

        #endregion

        #region op_Inequality_Null_Test2()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test2()
        {
            ChargingPool_Id _EVP_Id1 = null;
            var      _EVP_Id2 = new ChargingPool_Id();
            Assert.IsTrue(_EVP_Id1 != _EVP_Id2);
        }

        #endregion

        #region op_Inequality_BothNull_Test()

        /// <summary>
        /// A test for the inequality operator both null.
        /// </summary>
        [Test]
        public void op_Inequality_BothNull_Test()
        {
            ChargingPool_Id _EVP_Id1 = null;
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsFalse(_EVP_Id1 != _EVP_Id2);
        }

        #endregion

        #region op_Inequality_SameReference_Test()

        /// <summary>
        /// A test for the inequality operator same reference.
        /// </summary>
        [Test]
        public void op_Inequality_SameReference_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id();
            #pragma warning disable
            Assert.IsFalse(_EVP_Id1 != _EVP_Id1);
            #pragma warning restore
        }

        #endregion

        #region op_Inequality_Equals_Test()

        /// <summary>
        /// A test for the inequality operator equals.
        /// </summary>
        [Test]
        public void op_Inequality_Equals_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsFalse(_EVP_Id1 != _EVP_Id2);
        }

        #endregion

        #region op_Inequality_NotEquals1_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals1_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("2");
            Assert.IsTrue(_EVP_Id1 != _EVP_Id2);
        }

        #endregion

        #region op_Inequality_NotEquals2_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals2_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("5");
            var _EVP_Id2 = new ChargingPool_Id("23");
            Assert.IsTrue(_EVP_Id1 != _EVP_Id2);
        }

        #endregion


        #region op_Smaller_Null_Test1()

        /// <summary>
        /// A test for the smaller operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Smaller_Null_Test1()
        {
            var      _EVP_Id1 = new ChargingPool_Id();
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsTrue(_EVP_Id1 < _EVP_Id2);
        }

        #endregion

        #region op_Smaller_Null_Test2()

        /// <summary>
        /// A test for the smaller operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Smaller_Null_Test2()
        {
            ChargingPool_Id _EVP_Id1 = null;
            var      _EVP_Id2 = new ChargingPool_Id();
            Assert.IsTrue(_EVP_Id1 < _EVP_Id2);
        }

        #endregion

        #region op_Smaller_BothNull_Test()

        /// <summary>
        /// A test for the smaller operator both null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Smaller_BothNull_Test()
        {
            ChargingPool_Id _EVP_Id1 = null;
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsFalse(_EVP_Id1 < _EVP_Id2);
        }

        #endregion

        #region op_Smaller_SameReference_Test()

        /// <summary>
        /// A test for the smaller operator same reference.
        /// </summary>
        [Test]
        public void op_Smaller_SameReference_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id();
            #pragma warning disable
            Assert.IsFalse(_EVP_Id1 < _EVP_Id1);
            #pragma warning restore
        }

        #endregion

        #region op_Smaller_Equals_Test()

        /// <summary>
        /// A test for the smaller operator equals.
        /// </summary>
        [Test]
        public void op_Smaller_Equals_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsFalse(_EVP_Id1 < _EVP_Id2);
        }

        #endregion

        #region op_Smaller_Smaller1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller1_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("2");
            Assert.IsTrue(_EVP_Id1 < _EVP_Id2);
        }

        #endregion

        #region op_Smaller_Smaller2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller2_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("5");
            var _EVP_Id2 = new ChargingPool_Id("23");
            Assert.IsTrue(_EVP_Id1 < _EVP_Id2);
        }

        #endregion

        #region op_Smaller_Bigger1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger1_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("2");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsFalse(_EVP_Id1 < _EVP_Id2);
        }

        #endregion

        #region op_Smaller_Bigger2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger2_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("23");
            var _EVP_Id2 = new ChargingPool_Id("5");
            Assert.IsFalse(_EVP_Id1 < _EVP_Id2);
        }

        #endregion


        #region op_SmallerOrEqual_Null_Test1()

        /// <summary>
        /// A test for the smallerOrEqual operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_SmallerOrEqual_Null_Test1()
        {
            var      _EVP_Id1 = new ChargingPool_Id();
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsTrue(_EVP_Id1 <= _EVP_Id2);
        }

        #endregion

        #region op_SmallerOrEqual_Null_Test2()

        /// <summary>
        /// A test for the smallerOrEqual operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_SmallerOrEqual_Null_Test2()
        {
            ChargingPool_Id _EVP_Id1 = null;
            var      _EVP_Id2 = new ChargingPool_Id();
            Assert.IsTrue(_EVP_Id1 <= _EVP_Id2);
        }

        #endregion

        #region op_SmallerOrEqual_BothNull_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator both null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_SmallerOrEqual_BothNull_Test()
        {
            ChargingPool_Id _EVP_Id1 = null;
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsFalse(_EVP_Id1 <= _EVP_Id2);
        }

        #endregion

        #region op_SmallerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SameReference_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id();
            #pragma warning disable
            Assert.IsTrue(_EVP_Id1 <= _EVP_Id1);
            #pragma warning restore
        }

        #endregion

        #region op_SmallerOrEqual_Equals_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Equals_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsTrue(_EVP_Id1 <= _EVP_Id2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan1_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("2");
            Assert.IsTrue(_EVP_Id1 <= _EVP_Id2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan2_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("5");
            var _EVP_Id2 = new ChargingPool_Id("23");
            Assert.IsTrue(_EVP_Id1 <= _EVP_Id2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger1_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("2");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsFalse(_EVP_Id1 <= _EVP_Id2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger2_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("23");
            var _EVP_Id2 = new ChargingPool_Id("5");
            Assert.IsFalse(_EVP_Id1 <= _EVP_Id2);
        }

        #endregion


        #region op_Bigger_Null_Test1()

        /// <summary>
        /// A test for the bigger operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Bigger_Null_Test1()
        {
            var      _EVP_Id1 = new ChargingPool_Id();
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsTrue(_EVP_Id1 > _EVP_Id2);
        }

        #endregion

        #region op_Bigger_Null_Test2()

        /// <summary>
        /// A test for the bigger operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Bigger_Null_Test2()
        {
            ChargingPool_Id _EVP_Id1 = null;
            var      _EVP_Id2 = new ChargingPool_Id();
            Assert.IsTrue(_EVP_Id1 > _EVP_Id2);
        }

        #endregion

        #region op_Bigger_BothNull_Test()

        /// <summary>
        /// A test for the bigger operator both null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Bigger_BothNull_Test()
        {
            ChargingPool_Id _EVP_Id1 = null;
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsFalse(_EVP_Id1 > _EVP_Id2);
        }

        #endregion

        #region op_Bigger_SameReference_Test()

        /// <summary>
        /// A test for the bigger operator same reference.
        /// </summary>
        [Test]
        public void op_Bigger_SameReference_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id();
            #pragma warning disable
            Assert.IsFalse(_EVP_Id1 > _EVP_Id1);
            #pragma warning restore
        }

        #endregion

        #region op_Bigger_Equals_Test()

        /// <summary>
        /// A test for the bigger operator equals.
        /// </summary>
        [Test]
        public void op_Bigger_Equals_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsFalse(_EVP_Id1 > _EVP_Id2);
        }

        #endregion

        #region op_Bigger_Smaller1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller1_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("2");
            Assert.IsFalse(_EVP_Id1 > _EVP_Id2);
        }

        #endregion

        #region op_Bigger_Smaller2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller2_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("5");
            var _EVP_Id2 = new ChargingPool_Id("23");
            Assert.IsFalse(_EVP_Id1 > _EVP_Id2);
        }

        #endregion

        #region op_Bigger_Bigger1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger1_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("2");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsTrue(_EVP_Id1 > _EVP_Id2);
        }

        #endregion

        #region op_Bigger_Bigger2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger2_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("23");
            var _EVP_Id2 = new ChargingPool_Id("5");
            Assert.IsTrue(_EVP_Id1 > _EVP_Id2);
        }

        #endregion


        #region op_BiggerOrEqual_Null_Test1()

        /// <summary>
        /// A test for the biggerOrEqual operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_BiggerOrEqual_Null_Test1()
        {
            var      _EVP_Id1 = new ChargingPool_Id();
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsTrue(_EVP_Id1 >= _EVP_Id2);
        }

        #endregion

        #region op_BiggerOrEqual_Null_Test2()

        /// <summary>
        /// A test for the biggerOrEqual operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_BiggerOrEqual_Null_Test2()
        {
            ChargingPool_Id _EVP_Id1 = null;
            var      _EVP_Id2 = new ChargingPool_Id();
            Assert.IsTrue(_EVP_Id1 >= _EVP_Id2);
        }

        #endregion

        #region op_BiggerOrEqual_BothNull_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator both null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_BiggerOrEqual_BothNull_Test()
        {
            ChargingPool_Id _EVP_Id1 = null;
            ChargingPool_Id _EVP_Id2 = null;
            Assert.IsFalse(_EVP_Id1 >= _EVP_Id2);
        }

        #endregion

        #region op_BiggerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SameReference_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id();
            #pragma warning disable
            Assert.IsTrue(_EVP_Id1 >= _EVP_Id1);
            #pragma warning restore
        }

        #endregion

        #region op_BiggerOrEqual_Equals_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Equals_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsTrue(_EVP_Id1 >= _EVP_Id2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan1_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("2");
            Assert.IsFalse(_EVP_Id1 >= _EVP_Id2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan2_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("5");
            var _EVP_Id2 = new ChargingPool_Id("23");
            Assert.IsFalse(_EVP_Id1 >= _EVP_Id2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger1_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("2");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsTrue(_EVP_Id1 >= _EVP_Id2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger2_Test()
        {
            var _EVP_Id1 = new ChargingPool_Id("23");
            var _EVP_Id2 = new ChargingPool_Id("5");
            Assert.IsTrue(_EVP_Id1 >= _EVP_Id2);
        }

        #endregion


        #region CompareToNullTest1()

        /// <summary>
        /// A test for CompareTo null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompareToNullTest1()
        {
            var    _EVP_Id = ChargingPool_Id.New;
            Object _Object   = null;
            _EVP_Id.CompareTo(_Object);
        }

        #endregion

        #region CompareToNullTest2()

        /// <summary>
        /// A test for CompareTo null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompareToNullTest2()
        {
            var      _EVP_Id = ChargingPool_Id.New;
            ChargingPool_Id _Object   = null;
            _EVP_Id.CompareTo(_Object);
        }

        #endregion

        #region CompareToNonEVP_IdTest()

        /// <summary>
        /// A test for CompareTo a non-EVP_Id.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CompareToNonEVP_IdTest()
        {
            var _EVP_Id = ChargingPool_Id.New;
            var _Object   = "123";
            _EVP_Id.CompareTo(_Object);
        }

        #endregion

        #region CompareToSmallerTest1()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest1()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("2");
            Assert.IsTrue(_EVP_Id1.CompareTo(_EVP_Id2) < 0);
        }

        #endregion

        #region CompareToSmallerTest2()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest2()
        {
            var _EVP_Id1 = new ChargingPool_Id("5");
            var _EVP_Id2 = new ChargingPool_Id("23");
            Assert.IsTrue(_EVP_Id1.CompareTo(_EVP_Id2) < 0);
        }

        #endregion

        #region CompareToEqualsTest()

        /// <summary>
        /// A test for CompareTo equals.
        /// </summary>
        [Test]
        public void CompareToEqualsTest()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsTrue(_EVP_Id1.CompareTo(_EVP_Id2) == 0);
        }

        #endregion

        #region CompareToBiggerTest()

        /// <summary>
        /// A test for CompareTo bigger.
        /// </summary>
        [Test]
        public void CompareToBiggerTest()
        {
            var _EVP_Id1 = new ChargingPool_Id("2");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsTrue(_EVP_Id1.CompareTo(_EVP_Id2) > 0);
        }

        #endregion


        #region EqualsNullTest1()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest1()
        {
            var    _EVP_Id = ChargingPool_Id.New;
            Object _Object   = null;
            Assert.IsFalse(_EVP_Id.Equals(_Object));
        }

        #endregion

        #region EqualsNullTest2()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest2()
        {
            var      _EVP_Id = ChargingPool_Id.New;
            ChargingPool_Id _Object   = null;
            Assert.IsFalse(_EVP_Id.Equals(_Object));
        }

        #endregion

        #region EqualsNonEVP_IdTest()

        /// <summary>
        /// A test for equals a non-EVP_Id.
        /// </summary>
        [Test]
        public void EqualsNonEVP_IdTest()
        {
            var _EVP_Id = ChargingPool_Id.New;
            var _Object   = "123";
            Assert.IsFalse(_EVP_Id.Equals(_Object));
        }

        #endregion

        #region EqualsEqualsTest()

        /// <summary>
        /// A test for equals.
        /// </summary>
        [Test]
        public void EqualsEqualsTest()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("1");
            Assert.IsTrue(_EVP_Id1.Equals(_EVP_Id2));
        }

        #endregion

        #region EqualsNotEqualsTest()

        /// <summary>
        /// A test for not-equals.
        /// </summary>
        [Test]
        public void EqualsNotEqualsTest()
        {
            var _EVP_Id1 = new ChargingPool_Id("1");
            var _EVP_Id2 = new ChargingPool_Id("2");
            Assert.IsFalse(_EVP_Id1.Equals(_EVP_Id2));
        }

        #endregion


        #region GetHashCodeEqualTest()

        /// <summary>
        /// A test for GetHashCode
        /// </summary>
        [Test]
        public void GetHashCodeEqualTest()
        {
            var _SensorHashCode1 = new ChargingPool_Id("5").GetHashCode();
            var _SensorHashCode2 = new ChargingPool_Id("5").GetHashCode();
            Assert.AreEqual(_SensorHashCode1, _SensorHashCode2);
        }

        #endregion

        #region GetHashCodeNotEqualTest()

        /// <summary>
        /// A test for GetHashCode
        /// </summary>
        [Test]
        public void GetHashCodeNotEqualTest()
        {
            var _SensorHashCode1 = new ChargingPool_Id("1").GetHashCode();
            var _SensorHashCode2 = new ChargingPool_Id("2").GetHashCode();
            Assert.AreNotEqual(_SensorHashCode1, _SensorHashCode2);
        }

        #endregion


        #region EVP_IdsAndNUnitTest()

        /// <summary>
        /// Tests EVP_Ids in combination with NUnit.
        /// </summary>
        [Test]
        public void EVP_IdsAndNUnitTest()
        {

            var a = new ChargingPool_Id("1");
            var b = new ChargingPool_Id("2");
            var c = new ChargingPool_Id("1");

            Assert.AreEqual(a, a);
            Assert.AreEqual(b, b);
            Assert.AreEqual(c, c);

            Assert.AreEqual(a, c);
            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(b, c);

        }

        #endregion

        #region EVP_IdsInHashSetTest()

        /// <summary>
        /// Test EVP_Ids within a HashSet.
        /// </summary>
        [Test]
        public void EVP_IdsInHashSetTest()
        {

            var a = new ChargingPool_Id("1");
            var b = new ChargingPool_Id("2");
            var c = new ChargingPool_Id("1");

            var _HashSet = new HashSet<ChargingPool_Id>();
            Assert.AreEqual(0, _HashSet.Count);

            _HashSet.Add(a);
            Assert.AreEqual(1, _HashSet.Count);

            _HashSet.Add(b);
            Assert.AreEqual(2, _HashSet.Count);

            _HashSet.Add(c);
            Assert.AreEqual(2, _HashSet.Count);

        }

        #endregion

    }

}