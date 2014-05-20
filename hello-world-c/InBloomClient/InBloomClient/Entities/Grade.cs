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

namespace InBloomClient.Entities
{
    public class Grade
    {
        public string id { get; set; }

        /// <summary>
        /// A statement provided by the teacher that provides information in addition to the grade or assessment score.
        /// </summary>
        [Range(1, 1024)]
        [StringLength(1024)]
        public string diagnosticStatement { get; set; }

        /// <summary>
        /// The type of grade reported (e.g., Exam, Final, Grading Period).
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GradeType gradeType { get; set; }

        /// <summary>
        /// A final or interim (grading period) indicator of student performance in a class as submitted by the instructor.
        /// </summary>
        [Range(1, 20)]
        [StringLength(20)]
        public string letterGradeEarned { get; set; }

        /// <summary>
        /// A final or interim (grading period) indicator of student performance in a class as submitted by the instructor.
        /// </summary>
        public int numericGradeEarned { get; set; }

        /// <summary>
        /// A conversion of the level to a standard set of performance levels.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PerformanceBaseType performanceBaseConversion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SchoolYearType schoolYear { get; set; }

        /// <summary>
        /// Identifies the grading period for the grade.
        /// </summary>
        public string gradingPeriodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public List<ReportCards> reportCards { get; set; }

        /// <summary>
        /// Section id associated to the student-section association with which the grade is awarded.
        /// </summary>
        public string sectionId { get; set; }

        /// <summary>
        /// Student id associated to the student-section association with which the grade is awarded
        /// </summary>
        public string studentId { get; set; }

        /// <summary>
        /// Association to the student-section association with which the grade is awarded.
        /// </summary>
        public string studentSectionAssociationId { get; set; }
    }
}
