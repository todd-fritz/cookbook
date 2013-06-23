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
    public class ReportCards
    {
        public string id { get; set; }

        /// <summary>
        /// A measure of cumulative average performance in all courses taken by an individual from the beginning of the school year through the current grading period.
        /// </summary
        public double gpaCumulative { get; set; }

        /// <summary>
        /// A measure of average performance in all courses taken by an individual for the current grading period.
        /// </summary
        public double gpaGivenGradingPeriod { get; set; }

        /// <summary>
        /// The number of days an individual is absent when school is in session during a given reporting period.
        /// </summary
        public double numberOfDaysAbsent { get; set; }

        /// <summary>
        /// The number of days an individual is present when school is in session during a given reporting period.
        /// </summary
        public double numberOfDaysInAttendance { get; set; }

        /// <summary>
        /// The number of days an individual is tardy during a given reporting period.
        /// </summary
        public double numberOfDaysTardy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SchoolYearType schoolYear { get; set; }

        /// <summary>
        /// Grades for the classes attended by the student for this grading period.
        /// </summary>
        public List<Grade> grades { get; set; }

        /// <summary>
        /// Identifies the grading period for this report card
        /// </summary>
        public string gradingPeriodId { get; set; }

        /// <summary>
        /// Identifies the student that is associated with the Report Card
        /// </summary>
        public string studentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<StudentAcademicRecord> studentAcademicRecords { get; set; }

        /// <summary>
        /// The student competency evaluations associated for this grading period.
        /// </summary>
        public List<StudentCompetency> studentCompetencies { get; set; }
    }
}
