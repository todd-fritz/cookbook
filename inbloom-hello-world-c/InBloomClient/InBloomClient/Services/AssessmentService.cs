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
using InBloomClient.Entities;
using InBloomClient.Entities.Associations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InBloomClient.Services
{
    public class AssessmentService
    {
        private InBloomHttpClient _client;

        public AssessmentService(string token)
        {
            _client = new InBloomHttpClient(token);
        }

        public async Task<IEnumerable<Assessment>> GetAll()
        {
            var response = await _client.GetData(Constants.Assessment.ALL);
            var content = await response.Content.ReadAsStringAsync();
            var Assessments = JsonConvert.DeserializeObject<IEnumerable<Assessment>>(content);
            return Assessments;
        }

        public async Task<Assessment> GetById(string id)
        {
            var response = await _client.GetData(Constants.Assessment.ALL + id);
            var content = await response.Content.ReadAsStringAsync();
            var Assessment = JsonConvert.DeserializeObject<Assessment>(content);
            return Assessment;
        }

        public async Task<HttpResponseMessage> Create(Assessment obj)
        {
            var response = await _client.PostData(Constants.Assessment.ALL, obj);
            return response;
        }

        public async Task<HttpResponseMessage> Update(Assessment obj)
        {
            var response = await _client.PutData(Constants.Assessment.ALL + obj.id, obj);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteById(string id)
        {
            var response = await _client.DeleteData(Constants.Assessment.ALL, id);
            return response;
        }
    }
}
