using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Services;
using Prime.SlackWorkspaceAutomationPoC.Domain.RequestModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Prime.SlackWorkspaceAutomationPoC.Api.Controllers
{
    [Route("api/slack")]
    [ApiController]
    public class SlackController : ControllerBase
    {
        private readonly ISlackService _slackService;

        public SlackController(ISlackService slackService)
        { 
            _slackService = slackService;
        }

        [HttpGet]
        public async Task<IActionResult> TestEndpoint()
        {
            return Ok("Here we are.");
        }

        [HttpPost("event")]
        [Produces("text/plain")]
        public async Task<IActionResult> SubscribeToEventAsync()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();

                //uncomment only when verifying endpoint
                //var userInfo = JsonConvert.DeserializeObject<EventSubscribtionRequestModel>(body);

                //if (userInfo != null && userInfo.Challenge != null)
                //{
                //    return Ok(userInfo.Challenge);
                //}

                string userId = body.Substring(body.IndexOf("user") + 13, 11);

                string userEmail = body.Substring(body.IndexOf("email"), 70);

                string pattern = @"(\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b)";

                Regex regex = new Regex(pattern);

                MatchCollection matches = regex.Matches(userEmail);

                userEmail = matches[0].Value;

                await _slackService.ExecutePendingInvitesAsync(userEmail, userId);
                return Ok();
            }
        }

        [HttpPost("create-channels-from-api")]
        public async Task<IActionResult> CreatePrivateChannelsFromTechnologiesAsync()
        {
            var channelIds = await _slackService.CreatePrivateChannelsFromTechnologiesAsync();
            return Ok(channelIds);
        }

        [HttpPost("invite-user-to-workspace")]
        public async Task<IActionResult> InviteUserToWorkspaceAsync()
        {
            var result = await _slackService.InviteUserToWorkspaceAsync();
            return Ok(result);
        }

        [HttpPost("create-public-channel")]
        public async Task<IActionResult> CreatePublicChannel(string channelName)
        {
            var result = await _slackService.CreatePublicChannelAsync(channelName);
            return Ok(result);
        }

        [HttpPost("create-private-channel")]
        public async Task<IActionResult> CreatePrivateChannel(string channelName)
        {
            var result = await _slackService.CreatePrivateChannelAsync(channelName);
            return Ok(result);
        }

        [HttpPost("create-usergroup")]
        public async Task<IActionResult> CreateUserGroup(string userGroupName, string mentionHandle)
        {
            var result = await _slackService.CreateUserGroupAsync(userGroupName, mentionHandle);
            return Ok(result);
        }

        [HttpGet("list-all-channels")]
        public async Task<IActionResult> GetAllChannelsAsync()
        {
            var result = await _slackService.GetAllChannelsAsync();
            return Ok(result);
        }

        [HttpGet("list-all-pending-invites")]
        public async Task<IActionResult> GetAllPendingInvitesAsync()
        {
            var result = await _slackService.GetAllPendingInvitesAsync();
            return Ok(result);
        }

        [HttpGet("list-all-groups")]
        public async Task<IActionResult> GetAllUserGroupsAsync()
        {
            var result = await _slackService.GetAllUserGroupsAsync();
            return Ok(result);
        }

        [HttpGet("list-all-users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _slackService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpPost("invite-users-to-channel")]
        public async Task<IActionResult> InviteUsersToChannelAsync(string channelId, List<string> userIds)
        {
            var result = await _slackService.InviteUsersToChannelAsync(channelId,userIds);
            return Ok(result);
        }

        [HttpPost("invite-users-to-group")]
        public async Task<IActionResult> InviteUsersToGroupAsync(string groupId, List<string> userIds)
        {
            var result = await _slackService.InviteUsersToGroupAsync(groupId, userIds);
            return Ok(result);
        }
    }
}
