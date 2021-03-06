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
using System.Collections.Concurrent;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Illias.Votes;
using org.GraphDefined.Vanaheimr.Styx.Arrows;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;
using org.GraphDefined.Vanaheimr.Hermod;

#endregion

namespace org.GraphDefined.WWCP
{

    /// <summary>
    /// Extention methods for brands.
    /// </summary>
    public static class BrandExtentions
    {

        #region ToJSON(this Brands, Skip = null, Take = null, Embedded = false, ...)

        /// <summary>
        /// Return a JSON representation for the given enumeration of brands.
        /// </summary>
        /// <param name="Brands">An enumeration of brands.</param>
        /// <param name="Skip">The optional number of charging station operators to skip.</param>
        /// <param name="Take">The optional number of charging station operators to return.</param>
        /// <param name="Embedded">Whether this data is embedded into another data structure.</param>
        public static JArray ToJSON(this IEnumerable<Brand>   Brands,
                                    UInt64?                   Skip                       = null,
                                    UInt64?                   Take                       = null,
                                    Boolean                   Embedded                   = false,
                                    InfoStatus                ExpandChargingPoolIds      = InfoStatus.Hidden,
                                    InfoStatus                ExpandChargingStationIds   = InfoStatus.Hidden,
                                    InfoStatus                ExpandEVSEIds              = InfoStatus.Hidden,
                                    InfoStatus                ExpandDataLicenses         = InfoStatus.ShowIdOnly)


            => Brands == null || !Brands.Any()

                   ? new JArray()

                   : new JArray(Brands.
                                    Where         (brand => brand != null).
                                    OrderBy       (brand => brand.Id).
                                    SkipTakeFilter(Skip, Take).
                                    SafeSelect    (brand => brand.ToJSON(Embedded,
                                                                         ExpandChargingPoolIds,
                                                                         ExpandChargingStationIds,
                                                                         ExpandEVSEIds,
                                                                         ExpandDataLicenses)));

        #endregion

    }


    /// <summary>
    /// An Electric Vehicle Supply Equipment (Brand) to charge an electric vehicle (EV).
    /// This is meant to be one electrical circuit which can charge a electric vehicle
    /// independently. Thus there could be multiple interdependent power sockets.
    /// </summary>
    public class Brand : IHasId<Brand_Id>,
                         IEntity<Brand_Id>,
                         IEquatable<Brand>,
                         IComparable<Brand>,
                         IComparable
    {

        #region Properties

        /// <summary>
        /// The unique identification of the brand.
        /// </summary>
        public Brand_Id    Id          { get; }

        /// <summary>
        /// The multi-language brand name.
        /// </summary>
        public I18NString  Name        { get; }

        /// <summary>
        /// The optional URI of the logo of this brand.
        /// </summary>
        [Optional]
        public String      LogoURI     { get; }

        /// <summary>
        /// The homepage of this brand.
        /// </summary>
        [Optional]
        public String      Homepage    { get; }

        public ulong Length => Id.Length;

        public bool IsNullOrEmpty => Id.IsNullOrEmpty;

        public DateTime LastChange => throw new NotImplementedException();

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new brand having the given brand identification.
        /// </summary>
        /// <param name="Id">The unique identification of this brand.</param>
        /// <param name="Name">The multi-language brand name.</param>
        /// <param name="Logo">An optional logo of this brand.</param>
        /// <param name="Homepage">An optional homepage of this brand.</param>
        public Brand(Brand_Id    Id,
                     I18NString  Name,
                     String      Logo     = null,
                     String      Homepage = null)
        {

            #region Initial checks

            if (Name.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(Name), "The given brand name must not be null or empty!");

            #endregion

            this.Id        = Id;
            this.Name      = Name;
            this.LogoURI   = Logo;
            this.Homepage  = Homepage;

        }

        public event OnPropertyChangedDelegate OnPropertyChanged;

        #endregion


        #region ToJSON()

        public JObject ToJSON(Boolean     Embedded                   = false,
                              InfoStatus  ExpandChargingPoolIds      = InfoStatus.Hidden,
                              InfoStatus  ExpandChargingStationIds   = InfoStatus.Hidden,
                              InfoStatus  ExpandEVSEIds              = InfoStatus.Hidden,
                              InfoStatus  ExpandDataLicenses         = InfoStatus.ShowIdOnly)

            => JSONObject.Create(

                   new JProperty("Id",    Id.  ToString()),
                   new JProperty("Name",  Name.ToJSON()),

                   LogoURI.IsNotNullOrEmpty()
                       ? new JProperty("LogoURI",   LogoURI)
                       : null,

                   Homepage.IsNotNullOrEmpty()
                       ? new JProperty("Homepage",  Homepage)
                       : null

               );

        #endregion


        #region IComparable<Brand> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is a brand.
            var Brand = Object as Brand;
            if ((Object) Brand == null)
                throw new ArgumentException("The given object is not a brand!");

            return CompareTo(Brand);

        }

        #endregion

        #region CompareTo(BrandId)

        /// <summary>
        /// Compares this brand to another brand identification.
        /// </summary>
        /// <param name="BrandId">Another brand identification to compare with.</param>
        public Int32 CompareTo(Brand_Id BrandId)
        {

            if ((Object) BrandId == null)
                throw new ArgumentNullException(nameof(BrandId),  "The given brand identification must not be null!");

            return Id.CompareTo(BrandId);

        }

        #endregion

        #region CompareTo(Brand)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Brand">Another brand to compare with.</param>
        public Int32 CompareTo(Brand Brand)
        {

            if ((Object) Brand == null)
                throw new ArgumentNullException(nameof(Brand),  "The given brand must not be null!");

            return Id.CompareTo(Brand.Id);

        }

        #endregion

        #endregion

        #region IEquatable<Brand> Members

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

            // Check if the given object is a brand.
            var Brand = Object as Brand;
            if ((Object) Brand == null)
                return false;

            return this.Equals(Brand);

        }

        #endregion

        #region Equals(BrandId)

        /// <summary>
        /// Compares two this brand to another brand identification for equality.
        /// </summary>
        /// <param name="BrandId">An brand identification to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(Brand_Id BrandId)
        {

            if ((Object) BrandId == null)
                return false;

            return Id.Equals(BrandId);

        }

        #endregion

        #region Equals(Brand)

        /// <summary>
        /// Compares two Brands for equality.
        /// </summary>
        /// <param name="Brand">An Brand to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(Brand Brand)
        {

            if ((Object) Brand == null)
                return false;

            return Id.Equals(Brand.Id);

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
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => Id.ToString();

        #endregion

    }

}
