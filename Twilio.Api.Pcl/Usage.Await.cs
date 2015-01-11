﻿using System;
using Simple;
using System.Threading.Tasks;

namespace Twilio
{
    public partial class TwilioRestClient
    {
        /// <summary>
        /// Returns a list of all usage resources
        /// </summary>
        /// <returns></returns>
        public virtual async Task<UsageResult> ListUsageAsync()
        {
            return await ListUsageAsync(String.Empty);
        }

        /// <summary>
        /// Returns a list of usage resources for a specific usage category
        /// </summary>
        /// <param name="category">The category used to filter the usage data</param>
        /// <returns></returns>
        public virtual async Task<UsageResult> ListUsageAsync(string category)
        {
            return await ListUsageAsync(category, String.Empty);
        }

        /// <summary>
        /// Returns a list of usage resources for a specific category, where the data grouped using a specific interval
        /// </summary>
        /// <param name="category">The category used to filter the usage data</param>
        /// <param name="interval">The time interval used to group the usage data</param>
        /// <returns></returns>
        public virtual async Task<UsageResult> ListUsageAsync(string category, string interval)
        {
            return await ListUsageAsync(category, interval, null, null);
        }

        /// <summary>
        /// Returns a list of usage resources for a specific category, within a specific date range
        /// </summary>
        /// <param name="category">The category used to filter the usage data</param>
        /// <param name="startDate">The start date of the filter range</param>
        /// <param name="endDate">The end date of the filter range</param>
        /// <returns></returns>
        public virtual async Task<UsageResult> ListUsageAsync(string category, DateTime? startDate, DateTime? endDate)
        {
            return await ListUsageAsync(category, String.Empty, startDate, endDate);
        }

        /// <summary>
        /// Returns a list of usage resources for a specific category, within a specific date range, grouped by a specific time interval
        /// </summary>
        /// <param name="category">The category used to filter the usage data</param>
        /// <param name="interval">The time interval used to group the usage data</param>
        /// <param name="startDate">The start date of the filter range</param>
        /// <param name="endDate">The end date of the filter range</param>
        /// <returns></returns>
        public virtual async Task<UsageResult> ListUsageAsync(string category, string interval, DateTime? startDate, DateTime? endDate)
        {
            return await ListUsageAsync(category, interval, startDate, endDate, null, null);
        }

        /// <summary>
        /// Returns a list of usage resources for a specific category, within a specific date range, grouped by a specific time interval
        /// </summary>
        /// <param name="category">The category used to filter the usage data</param>
        /// <param name="interval">The time interval used to group the usage data</param>
        /// <param name="startDate">The start date of the filter range</param>
        /// <param name="endDate">The end date of the filter range</param>
        /// <param name="pageNumber">(Optional) The page to start retrieving results from</param>
        /// <param name="count">(Optional) The number of results to retrieve</param>
        /// <returns></returns>
        public virtual async Task<UsageResult> ListUsageAsync(string category, string interval, DateTime? startDate, DateTime? endDate, int? pageNumber, int? count)
        {
            var request = new RestRequest();
            string resourceUrlRoot = "Accounts/{{AccountSid}}/Usage/Records{0}";

            if (!string.IsNullOrEmpty(interval))
            {
                request.Resource = string.Format(resourceUrlRoot, "/{Interval}.json");
                request.AddUrlSegment("Interval", interval);
            }
            else
            {
                request.Resource = string.Format(resourceUrlRoot, ".json");
            }

            if (!string.IsNullOrEmpty(category)) { request.AddParameter("Category", category.ToLower()); }
            if (startDate.HasValue) { request.AddParameter("StartDate", startDate.Value.ToString("yyyy-MM-dd")); }
            if (endDate.HasValue) { request.AddParameter("EndDate", endDate.Value.ToString("yyyy-MM-dd")); }
            if (pageNumber.HasValue) { request.AddParameter("Page", pageNumber.Value); }
            if (count.HasValue) { request.AddParameter("PageSize", count.Value); }

            return await Execute<UsageResult>(request);
        }

        /// <summary>
        /// Returns a list of usage triggers
        /// </summary>
        /// <returns></returns>
        public virtual async Task<UsageTriggerResult> ListUsageTriggersAsync()
        {
            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Usage/Triggers.json";

            return await Execute<UsageTriggerResult>(request);
        }

        /// <summary>
        /// Returns a list of usage triggers
        /// </summary>
        /// <param name="recurring">A string defining the recurrance interval for this trigger</param>
        /// <param name="usageCategory">The usage category this trigger watches</param>
        /// <param name="triggerBy">The value at which the trigger will fire</param>
        /// <returns></returns>
        public virtual async Task<UsageTriggerResult> ListUsageTriggersAsync(string recurring, string usageCategory, string triggerBy)
        {
            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Usage/Triggers.json";

            if (!string.IsNullOrEmpty(recurring)) { request.AddParameter("Recurring", recurring); }
            if (!string.IsNullOrEmpty(usageCategory)) { request.AddParameter("UsageCategory", usageCategory); }
            if (!string.IsNullOrEmpty(triggerBy)) { request.AddParameter("TriggerBy", triggerBy); }

            return await Execute<UsageTriggerResult>(request);
        }

        /// <summary>
        /// Locates and returns a specific Usage Trigger resource
        /// </summary>
        /// <param name="usageTriggerSid">The Sid of the Usage Trigger to locate</param>
        /// <returns></returns>
        public virtual async Task<UsageTrigger> GetUsageTriggerAsync(string usageTriggerSid)
        {
            Require.Argument("UsageTriggerSid", usageTriggerSid);

            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Usage/Triggers/{UsageTriggerSid}.json";

            request.AddUrlSegment("UsageTriggerSid", usageTriggerSid);

            return await Execute<UsageTrigger>(request);
        }

        /// <summary>
        /// Creates a new Usage Trigger resource
        /// </summary>
        /// <param name="usageCategory">The usage category to trigger on</param>
        /// <param name="triggerValue">The value to trigger on</param>
        /// <param name="callbackUrl">The URL to call once a trigger value has been met</param>
        /// <returns></returns>
        public virtual async Task<UsageTrigger> CreateUsageTriggerAsync(string usageCategory, string triggerValue, string callbackUrl)
        {
            var options = new UsageTriggerOptions();
            options.UsageCategory = usageCategory;
            options.TriggerValue = triggerValue;
            options.CallbackUrl = callbackUrl;

            return await CreateUsageTriggerAsync(options);
        }

        /// <summary>
        /// Creates a new Usage Trigger resource
        /// </summary>
        /// <param name="options">A UsageTriggerOption object that defines the different trigger options</param>
        /// <returns></returns>
        public virtual async Task<UsageTrigger> CreateUsageTriggerAsync(UsageTriggerOptions options)
        {
            Require.Argument("UsageCategory", options.UsageCategory);
            Require.Argument("TriggerValue", options.TriggerValue);
            Require.Argument("CallbackUrl", options.CallbackUrl);

            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Usage/Triggers.json";

            //request.Method = Method.POST;
            request.Method = "POST";

            request.AddParameter("UsageCategory", options.UsageCategory);
            request.AddParameter("TriggerValue", options.TriggerValue);
            request.AddParameter("CallbackUrl", options.CallbackUrl);

            if (!string.IsNullOrEmpty(options.CallbackMethod)) { request.AddParameter("CallbackMethod",options.CallbackMethod); }
            if (!string.IsNullOrEmpty(options.FriendlyName)) { request.AddParameter("FriendlyName",options.FriendlyName); }
            if (!string.IsNullOrEmpty(options.Recurring)) { request.AddParameter("Recurring",options.Recurring); }
            if (!string.IsNullOrEmpty(options.TriggerBy)) { request.AddParameter("TriggerBy",options.TriggerBy); }

            return await Execute<UsageTrigger>(request);
        }

        /// <summary>
        /// Updates a specific UsageTrigger resource
        /// </summary>
        /// <param name="usageTriggerSid">The Sid of the UsageTrigger to update</param>
        /// <param name="friendlyName">The friendly name of the trigger</param>
        /// <param name="callbackUrl">The URL to call once a trigger value has been met</param>
        /// <param name="callbackMethod">The HTTP method used when requesting the callback URL</param>
        /// <returns></returns>
        public virtual async Task<UsageTrigger> UpdateUsageTriggerAsync(string usageTriggerSid, string friendlyName, string callbackUrl, string callbackMethod)
        {
            Require.Argument("UsageTriggerSid", usageTriggerSid);

            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Usage/Triggers/{UsageTriggerSid}.json";

            //request.Method = Method.POST;
            request.Method = "POST";

            request.AddUrlSegment("UsageTriggerSid", usageTriggerSid);

            if (!string.IsNullOrEmpty(friendlyName)) { request.AddParameter("FriendlyName", friendlyName); }
            if (!string.IsNullOrEmpty(callbackUrl)) { request.AddParameter("CallbackUrl", callbackUrl); }
            if (!string.IsNullOrEmpty(callbackMethod)) { request.AddParameter("CallbackMethod", callbackMethod); }

            return await Execute<UsageTrigger>(request);
        }

        /// <summary>
        /// Deletes a UsageTrigger resource
        /// </summary>
        /// <param name="usageTriggerSid">The Sid of the UsageTrigger to delete</param>
        /// <returns></returns>
        public virtual async Task<DeleteStatus> DeleteUsageTriggerAsync(string usageTriggerSid)
        {
            Require.Argument("UsageTriggerSid", usageTriggerSid);

            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Usage/Triggers/{UsageTriggerSid}.json";

            request.Method = "DELETE"; // Method.DELETE;
            //request.Method = "POST";

            request.AddUrlSegment("UsageTriggerSid", usageTriggerSid);

            var response = await Execute(request);
            return response.StatusCode == System.Net.HttpStatusCode.NoContent ? DeleteStatus.Success : DeleteStatus.Failed;
        }
    }
}
