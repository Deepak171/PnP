using Newtonsoft.Json;
using OfficeDevPnP.Core.WebAPI;
using Provisioning.Common.Data.SiteRequests;
using Provisioning.Common.Utilities;
using Provisioning.UX.AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Provisioning.UX.AppWeb.Controllers
{
    public class SiteRequestController : ApiController
    {
        [HttpPut]
        public void Register(WebAPIContext sharePointServiceContext)
        {
            WebAPIHelper.AddToCache(sharePointServiceContext);
        }

        /// <summary>
        /// Saves a site request to the Data Repository
        /// POST api/<controller>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/provisioning/siteRequests/saveSiteRequest")]
        [WebAPIContextFilter]
        [HttpPost]
        public SiteRequest SaveSiteRequest([FromBody]string value)
        {
            var _request = new SiteRequest();
            _request.Success = false;

            try
            {
                _request = JsonConvert.DeserializeObject<SiteRequest>(value);
                this.SaveSiteRequestToRepository(_request);
                _request.Success = true;
            }
            catch (Exception ex)
            {
                Log.Error("Provisioning.UX.AppWeb.Controllers.ProvisioningController",
                    "There was an error saving the Site Request. Error Message {0} Error Stack {1}",
                    ex.Message,
                    ex);
                _request.ErrorMessage = ex.Message;
            }
            return _request;

        }

        #region Private Members
        /// <summary>
        /// Save the Site Request to the Data Repository
        /// </summary>
        /// <param name="siteRequest"></param>
        private void SaveSiteRequestToRepository(SiteRequest siteRequest)
        {
            try
            {
                var _newRequest = ObjectMapper.ToSiteRequestInformation(siteRequest);
                ///Save the Site Request
                ISiteRequestFactory _srf = SiteRequestFactory.GetInstance();
                var _manager = _srf.GetSiteRequestManager();
                _manager.CreateNewSiteRequest(_newRequest);
            }
            catch (Exception _ex)
            {
                throw;
            }

        }
        #endregion
    }
}
