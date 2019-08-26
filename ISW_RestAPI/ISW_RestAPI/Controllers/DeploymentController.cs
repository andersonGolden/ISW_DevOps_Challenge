using ISW_RestAPI.Models;
using ISW_RestAPI.RepoPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ISW_RestAPI.Controllers
{
    public class DeploymentController : ApiController
    {
        public UnitOfWork unitOfWork = new UnitOfWork(new ISW_RestApiDBContext());
        public APIResponseModel ServiceResponse;

        // List : api/deployments (This function returns a list of all available deploymemnts)
        [HttpGet]
        [Route("~/api/deployments")]
        public HttpResponseMessage List()
        {
            try
            {
                var deployments = unitOfWork.GetInstance<Deployment>().GetAll();
                if (deployments != null)
                {
                    //if deployments exist, return the available deployments
                    ServiceResponse = new APIResponseModel("Deployments available", deployments, false);
                }
                else
                {
                    //if no deploments exist, notify user that no deployments are available
                    ServiceResponse = new APIResponseModel("No deployments available at the moment", deployments, false);
                }
                
            }
            catch (Exception ex)
            {
                //if an error occurs, return error message with code
                ServiceResponse = new APIResponseModel("Failed to get deployments", null, true);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
            }
            //return successful response
            return Request.CreateResponse(HttpStatusCode.OK, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
        }

        // Create: api/deployments (This function creates a new deployment)
        [HttpPost]
        [Route("~/api/deployments")]
        public HttpResponseMessage Create(Deployment model)
        {
            try
            {
                var deploymentExists = unitOfWork.GetInstance<Deployment>().Find(d => d.Name == model.Name).Any();
                if (deploymentExists)
                {
                    //if a deployment already exists with the specified name, notify user to use a different name
                    ServiceResponse = new APIResponseModel("A deployment with this name already exists! Please enter a different name", null, true);
                }
                else
                {
                    //if no deployments exists with the specified name, create a new deployment
                    var newDeploy = new Deployment()
                    {
                        Name = model.Name,
                        ScheduleTime = model.ScheduleTime,
                        Status = model.Status,
                        IssuesEncountered = model.IssuesEncountered,
                        Description = model.Description,
                        DurationOfDeployment = model.DurationOfDeployment
                    };

                    //insert new deployment into DB
                    unitOfWork.GetInstance<Deployment>().Add(newDeploy);
                    unitOfWork.Complete();

                    //notify user of successful deployment creation
                    ServiceResponse = new APIResponseModel("Deployment has been successfully created!", null, false);
                }
            }
            catch (Exception ex)
            {
                //if an error occurs, return error message with code
                ServiceResponse = new APIResponseModel("Failed to create deployment", null, true);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
            }

            //return successful response
            return Request.CreateResponse(HttpStatusCode.OK, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
        }

        // GET: api/deployments/{name} (This function returns a specific deployment using the supplied deployment name)
        [HttpGet]
        [Route("~/api/deployments/{name}")]
        public HttpResponseMessage Get(string name)
        {
            try
            {
                var deployment = unitOfWork.GetInstance<Deployment>().Find(d => d.Name == name).FirstOrDefault();
                if (deployment != null)
                {
                    //if the required deployment exists, return the deployment
                    ServiceResponse = new APIResponseModel("Deployment available", deployment, false);
                }
                else
                {
                    //if no deploment exists with the required name, notify user that no deployment with such name exists
                    ServiceResponse = new APIResponseModel("Deployment does not exist", deployment, true);
                }
                
            }
            catch (Exception ex)
            {
                //if an error occurs, return error message with code
                ServiceResponse = new APIResponseModel("Failed to get deployments", null, true);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
            }

            //return successful response
            return Request.CreateResponse(HttpStatusCode.OK, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
        }


        // Update: api/deployments/{name} (This function updates and existing deployment)
        [HttpPut]
        [Route("~/api/deployments/{name}")]
        public HttpResponseMessage Update(string name)
        {
            try
            {
                var existingDeployment = unitOfWork.GetInstance<Deployment>().Find(d => d.Name == name).FirstOrDefault();
                if (existingDeployment == null)
                {
                    //if a deployment with the specified name does not exist, notify user
                    ServiceResponse = new APIResponseModel("A deployment with this name does not exists!", null, true);
                }
                else
                {
                    //if the specified deployment is found, then substitute existing value with updated value
                    existingDeployment.Name = name;

                    //update the corresponding record in the db
                    unitOfWork.GetInstance<Deployment>().Update(existingDeployment);
                    unitOfWork.Complete();

                    //notify user of successful deployment update
                    ServiceResponse = new APIResponseModel("Deployment has been successfully updated!", null, false);
                }
            }
            catch (Exception ex)
            {
                //if an error occurs, return error message with code
                ServiceResponse = new APIResponseModel("Failed to update deployment", null, true);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
            }

            //return successful response
            return Request.CreateResponse(HttpStatusCode.OK, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
        }

        // Delete: api/deployments/{name} (This function deletes an existing deployment)
        [HttpDelete]
        [Route("~/api/deployments/{name}")]
        public HttpResponseMessage Delete(string name)
        {
            try
            {
                var existingDeployment = unitOfWork.GetInstance<Deployment>().Find(d => d.Name == name).FirstOrDefault();
                if (existingDeployment == null)
                {
                    //if a deployment with the specified name does not exist, notify user
                    ServiceResponse = new APIResponseModel("A deployment with this name does not exists!", null, true);
                }
                else
                {
                    //if a deployment with the secified name is found, proceed to delete
                    unitOfWork.GetInstance<Deployment>().Remove(existingDeployment);
                    unitOfWork.Complete();

                    //notify user of successful deployment delete
                    ServiceResponse = new APIResponseModel("Deployment has been successfully deleted!", null, false);
                }
            }
            catch (Exception ex)
            {
                //if an error occurs, return error message with code
                ServiceResponse = new APIResponseModel("Failed to delete deployment", null, true);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
            }

            //return successful response
            return Request.CreateResponse(HttpStatusCode.OK, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
        }

        // Query : api/deployments/{name} (This function returns a specific deployment using the supplied deployment name)
        [HttpGet]
        [Route("~/api/search?name={name}&status={status}")]
        public HttpResponseMessage Query(string name, string status)
        {
            try
            {
                var deployment = unitOfWork.GetInstance<Deployment>().Find(d => d.Name == name && d.Status == status).FirstOrDefault();
                if (deployment != null)
                {
                    //if the required deployment exists, return the deployment
                    ServiceResponse = new APIResponseModel("Deployment available", deployment, false);
                }
                else
                {
                    //if no deploment exists with the required search creiteria, notify user that no deployment with such creiteria exists
                    ServiceResponse = new APIResponseModel("Deployment does not exist", deployment, true);
                }

            }
            catch (Exception ex)
            {
                //if an error occurs, return error message with code
                ServiceResponse = new APIResponseModel("Failed to get deployment", null, true);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
            }

            //return successful response
            return Request.CreateResponse(HttpStatusCode.OK, ServiceResponse, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
}
