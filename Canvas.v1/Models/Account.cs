using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Canvas.v1.Models
{
    /// <summary>
    /// A Canvas account
    /// </summary>
    public class Account
    {
        /// <summary>
        /// The ID of the account
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        /// <summary>
        /// The name of the account
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// The default time zone of the account, in IANA format
        /// </summary>
        [JsonProperty(PropertyName = "default_time_zone")]
        public string DefaultTimeZone { get; private set; }

        /// <summary>
        /// The default course storage quota to be used, in MB, if not otherwise specified.
        /// </summary>
        [JsonProperty(PropertyName = "default_storage_quota_mb")]
        public string DefaultStorageQuotaMB { get; private set; }

        /// <summary>
        /// The default user storage quota to be used, in MB, if not otherwise specified.
        /// </summary>
        [JsonProperty(PropertyName = "default_user_storage_quota_mb")]
        public string DefaultUserStorageQuotaMB { get; private set; }

        /// <summary>
        /// The default group storage quota to be used, in MB, if not otherwise specified.
        /// </summary>
        [JsonProperty(PropertyName = "default_group_storage_quota_mb")]
        public string DefaultGroupStorageQuotaMB { get; private set; }

        /// <summary>
        /// The ID of the parent account
        /// </summary>
        [JsonProperty(PropertyName = "parent_account_id")]
        public string ParentAccountId { get; private set; }

        /// <summary>
        /// The ID of the root account
        /// </summary>
        [JsonProperty(PropertyName = "root_account_id")]
        public string RootAccountId { get; private set; }

        /// <summary>
        /// The workflow state of the account
        /// </summary>
        [JsonProperty(PropertyName = "workflow_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountWorkflowState WorkflowState { get; private set; }

    }

    public enum AccountWorkflowState
    {
        Active,
        Completed,
        All
    }
}