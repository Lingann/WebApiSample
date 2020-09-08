using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
namespace Entities.Models
{
    /// <summary>
    /// 身份验证请求模型
    /// </summary>
    public class AuthenticateRequest
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }
        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
