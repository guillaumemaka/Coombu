using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Coombu.Models;
using Coombu.Filters;
using Coombu.Models.Repositories;
using Coombu.Models.ViewModels;

namespace Coombu.Controllers.Apis
{
    [TokenValidation]
    public class GroupsController : ApiController
    {
        private readonly IGroupRepository _repository;

        public GroupsController(IGroupRepository repo)
        {
            _repository = repo;
        }

        // GET api/groups/?username=<>
        [UserNotFoundException]
        public HttpResponseMessage Get(string username = null, int page = 1, int size = 25)
        {
            var groups = _repository.GetAll(username, page, size);
            ApiResponse<List<Group>> response;

            if (groups == null)
            {
                response = new ApiResponse<List<Group>>(new List<Group>());
                response.AddMetatData("totalPage", 0);
                response.AddMetatData("currentPage", 0);
                response.AddMetatData("size", size);

                return Request.CreateResponse<ApiResponse<List<Group>>>(HttpStatusCode.OK, response);
            }
            else
            {
                response = new ApiResponse<List<Group>>(groups.ToList());
                response.AddMetatData("totalPage", groups.PageCount);
                response.AddMetatData("currentPage", groups.PageNumber);
                response.AddMetatData("size", groups.PageSize);

                return Request.CreateResponse<ApiResponse<List<Group>>>(HttpStatusCode.OK, response);
            }               
        }

        // GET api/groups/5
        [GroupNotFoundException]
        public HttpResponseMessage Get(int id)
        {
            ApiResponse<Group> response = new ApiResponse<Group>(_repository.Get(id));
            response.AddMetatData("status", HttpStatusCode.OK);

            return Request.CreateResponse<ApiResponse<Group>>(HttpStatusCode.OK,response);
        }

        // PUT api/groups
        public HttpResponseMessage Put(Group group)
        {            
            ApiResponse<bool> response = new ApiResponse<bool>(_repository.Update(group));
            response.AddMetatData("status",HttpStatusCode.OK);

            return Request.CreateResponse<ApiResponse<bool>>(HttpStatusCode.OK,response);            
        }

        // POST api/groups
        public HttpResponseMessage Post(Group group)
        {
            ApiResponse<Group> response = new ApiResponse<Group>(_repository.Add(group));
            response.AddMetatData("status",HttpStatusCode.OK);

            return Request.CreateResponse<ApiResponse<Group>>(HttpStatusCode.OK,response);
        }

        // DELETE api/groups/5
        [GroupNotFoundException]
        public HttpResponseMessage DeleteGroup(int id)
        {                                   
            _repository.Remove(id);            
            return Request.CreateResponse<ApiResponse<bool>>(HttpStatusCode.OK, new ApiResponse<bool>(true));
        }

        protected override void Dispose(bool disposing)
        {            
            base.Dispose(disposing);
        }
    }
}