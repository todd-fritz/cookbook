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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using InBloomClient.Enum;

namespace InBloomClient.Entities.Associations
{
    public class StudentParentAssociation
    {
        public string id { get; set; }

        /// <summary>
        /// The numeric order of the preferred sequence or priority of contact.
        /// </summary>
        public int contactPriority { get; set; }

        /// <summary>
        /// Restrictions for student and/or teacher contact with the individual (e.g., the student may not be picked up by the individual)
        /// </summary>
        [Range(1, 250)]
        [StringLength(250)]
        public string contactRestrictions { get; set; }

        /// <summary>
        /// Indicator of whether the person is a designated emergency contact for the student.
        /// </summary>
        public Boolean emergencyContactStatus { get; set; }

        /// <summary>
        /// Indicator of whether the student lives with the associated parent.
        /// </summary>
        public Boolean livesWith { get; set; }

        /// <summary>
        /// Indicator of whether the person is a primary parental contact for the student.
        /// </summary>
        public Boolean primaryContactStatus { get; set; }

        /// <summary>
        /// Indicator of whether the person is a primary parental contact for the student.
        /// </summary>
        //public RelationType relation { get; set; }

        /// <summary>
        /// The parent or guardian of the student.
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// The student
        /// </summary>
        public string studentId { get; set; }
    }
}
