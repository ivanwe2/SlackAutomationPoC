using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.ChannelDtos;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.RegisteredUserDtos;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.PendingInviteDtos;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Services
{
    public interface ISlackService
    {
        Task<List<string>> CreatePrivateChannelsFromTechnologiesAsync();
        Task<string> CreatePublicChannelAsync(string channelName);
        Task<string> CreatePrivateChannelAsync(string channelName);
        Task<string> CreateUserGroupAsync(string userGroupName, string mentionHandle);
        Task<string> InviteUsersToChannelAsync(string channelId, List<string> userIds);
        Task<string> InviteUsersToGroupAsync(string groupId, List<string> userIds);
        Task<string> InviteUserToWorkspaceAsync();
        Task ExecutePendingInvitesAsync(string userEmail, string userId);
        Task<PaginatedResult<ChannelResponseDto>> GetAllChannelsAsync();
        Task<string> GetAllUserGroupsAsync();
        Task<PaginatedResult<RegisteredUserResponseDto>> GetAllUsersAsync();
        Task<PaginatedResult<PendingInviteResponseDto>> GetAllPendingInvitesAsync();
    }
}
