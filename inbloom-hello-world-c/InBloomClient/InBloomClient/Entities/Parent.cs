/*
 * Copyright 2012-2013 inBloom, Inc. and its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using InBloomClient.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InBloomClient.Entities
{
    public class Parent
    {
        public string id { get; set; }

        /// <summary>
        /// Parent's address, if different from student address.
        /// </summary>
        public List<Address> address { get; set; }

        /// <summary>
        /// The numbers, letters, and symbols used to identify an electronic mail (e-mail) user within the network to which the individual or organization belongs.
        /// </summary>
        public List<ElectronicMail> electronicMail { get; set; }

        /// <summary>
        /// The login ID for the user; used for security access control interface.
        /// </summary>
        [StringLength(60)]
        public string loginId { get; set; }

        /// <summary>
        /// Full legal name of the person.
        /// </summary>
        public Name name { get; set; }

        /// <summary>
        /// Other names (e.g., alias, nickname, previous legal name) associated with a person.
        /// </summary>
        public List<OtherName> otherName { get; set; }

        /// <summary>
        /// A unique number or alphanumeric code assigned to a person.
        /// </summary>
        [StringLength(30)]
        public string parentUniqueStateId { get; set; }

        /// <summary>
        /// A person's gender.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SexType sex { get; set; }

        /// <summary>
        /// The 10-digit telephone number, including the area code, for the person.
        /// </summary>
        public List<Telephone> telephone { get; set; }
    }
}
