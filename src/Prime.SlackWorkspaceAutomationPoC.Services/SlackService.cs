using Newtonsoft.Json;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.EmailSender;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Helpers;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Repositories;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Services;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.ChannelDtos;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.PendingInviteDtos;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.RegisteredUserDtos;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.TechnologyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Services
{
    public class SlackService : ISlackService
    {
        private readonly ISlackConfigurationHelper _slackConfigurationHelper;
        private readonly IHeaderHelper _headerHelper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEmailSender _emailSender;
        private readonly IChannelRepository _channelRepository;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private readonly IPendingInviteRepository _pendingInviteRepository;

        public SlackService(IHttpClientFactory httpClient, IEmailSender emailSender, 
            ISlackConfigurationHelper slackAccessTokenHelper, IChannelRepository channelRepository,
            ITechnologyRepository technologyRepository, IRegisteredUserRepository registeredUserRepository,
            IPendingInviteRepository pendingInviteRepository, IHeaderHelper headerHelper)
        {
            _httpClientFactory = httpClient;
            _emailSender = emailSender;
            _slackConfigurationHelper = slackAccessTokenHelper;
            _channelRepository = channelRepository;
            _technologyRepository = technologyRepository;
            _registeredUserRepository = registeredUserRepository;
            _pendingInviteRepository = pendingInviteRepository;
            _headerHelper = headerHelper;
        }

        public async Task<string> CreatePrivateChannelAsync(string channelName)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_slackConfigurationHelper.AccessToken}");
                var createChannelResponse = await client.PostAsync("https://slack.com/api/conversations.create", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("name", channelName),
                    new KeyValuePair<string, string>("is_private", "true")
                }));

                createChannelResponse.EnsureSuccessStatusCode();

                string createdChannelInfo = await createChannelResponse.Content.ReadAsStringAsync();

                string channelId = createdChannelInfo.Substring(createdChannelInfo.IndexOf("id") + 5, 11);

                await CreateChannelAsync(channelId, channelName, false);


                return channelName + ":" + channelId;
            }
        }

        public async Task<string> CreatePublicChannelAsync(string channelName)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_slackConfigurationHelper.AccessToken}");
                var createChannelResponse = await client.PostAsync("https://slack.com/api/conversations.create", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("name", channelName),
                    new KeyValuePair<string, string>("is_private", "false")
                }));

                createChannelResponse.EnsureSuccessStatusCode();

                string createdChannelInfo = await createChannelResponse.Content.ReadAsStringAsync();

                string channelId = createdChannelInfo.Substring(createdChannelInfo.IndexOf("id") + 5, 11);

                await CreateChannelAsync(channelId, channelName, true);


                return channelName + ":" + channelId;
            }
        }

        public async Task<string> CreateUserGroupAsync(string userGroupName, string mentionHandle)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_slackConfigurationHelper.AccessToken}");
                var createChannelResponse = await client.PostAsync("https://slack.com/api/usergroups.create", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("name", userGroupName),
                    new KeyValuePair<string, string>("handle", mentionHandle)
                }));

                createChannelResponse.EnsureSuccessStatusCode();

                string createdChannelInfo = await createChannelResponse.Content.ReadAsStringAsync();

                return createdChannelInfo;
            }
        }

        public async Task<PaginatedResult<ChannelResponseDto>> GetAllChannelsAsync()
        {
            //using(var client = _httpClientFactory.CreateClient())
            //{
            //    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_slackAccessTokenHelper.SlackAccessToken}");
            //    var allChannelsResponse = await client
            //        .GetAsync("https://slack.com/api/conversations.list?types=public_channel%2Cprivate_channel&pretty=1");

            //    allChannelsResponse.EnsureSuccessStatusCode();

            //    return await allChannelsResponse.Content.ReadAsStringAsync();
            //}
            return await _channelRepository.GetAllAsync<ChannelResponseDto>(1, 20);
        }

        public async Task<string> GetAllUserGroupsAsync()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_slackConfigurationHelper.AccessToken}");
                var allChannelsResponse = await client.GetAsync("https://slack.com/api/usergroups.list?pretty=1");

                allChannelsResponse.EnsureSuccessStatusCode();

                return await allChannelsResponse.Content.ReadAsStringAsync();
            }
        }

        public async Task<PaginatedResult<RegisteredUserResponseDto>> GetAllUsersAsync()
        {
            //using (var client = _httpClientFactory.CreateClient())
            //{
            //    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_slackAccessTokenHelper.SlackAccessToken}");
            //    var requestResponse = await client.GetAsync("https://slack.com/api/users.list?pretty=1");

            //    requestResponse.EnsureSuccessStatusCode();

            //    return await requestResponse.Content.ReadAsStringAsync();
            //}
            return await _registeredUserRepository.GetAllAsync<RegisteredUserResponseDto>(1, 20);
        }

        public async Task<PaginatedResult<PendingInviteResponseDto>> GetAllPendingInvitesAsync()
        {
            return await _pendingInviteRepository.GetAllAsync<PendingInviteResponseDto>(1, 20);
        }

        public async Task<string> InviteUsersToChannelAsync(string channelId, List<string> userIds)
        {
            using (var client = _httpClientFactory.CreateClient())
            { 
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_slackConfigurationHelper.AccessToken}");
                var inviteResponse = await client.PostAsync("https://slack.com/api/conversations.invite", new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("channel", channelId),
                        new KeyValuePair<string, string>("users", string.Join(',',userIds))
                }));

                inviteResponse.EnsureSuccessStatusCode();

                return await inviteResponse.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> InviteUsersToGroupAsync(string groupId, List<string> userIds)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_slackConfigurationHelper.AccessToken}");
                var inviteResponse = await client.PostAsync("https://slack.com/api/usergroups.users.update", new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("usergroup", groupId),
                        new KeyValuePair<string, string>("users", string.Join(',',userIds))
                }));

                inviteResponse.EnsureSuccessStatusCode();
               
                return await inviteResponse.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> InviteUserToWorkspaceAsync()
        {
            string userEmail = _headerHelper.UserEmail;
            string technology = _headerHelper.UserTechnology;

            string inviteLink = _slackConfigurationHelper.InviteLink;

            await _emailSender.SendEmailAsync(userEmail, "Invitation to Slack Workspace", inviteLink);

            string channelId;
            if(!await _technologyRepository.HasAnyByNameAsync(technology))
            {
                var channelDetails = await CreatePrivateChannelAsync(technology);
                channelId = channelDetails.Split(":")[1];
            }
            else
            {
                channelId = await _channelRepository.GetChannelIdByNameAsync(technology);
            }

            await _pendingInviteRepository.CreateAsync(new PendingInviteRequestDto() { UserEmail = userEmail, ChannelIds = channelId});

            return "Invitation sent!";
        }

        public async Task ExecutePendingInvitesAsync(string userEmail, string userId)
        {
            var invitation = await _pendingInviteRepository.GetInvitationByUserEmailAsync(userEmail);
            if (invitation is not null)
            {
                await InviteUsersToChannelAsync(invitation.ChannelIds, new() { userId });
                await _pendingInviteRepository.DeleteAsync(invitation.Id);
            }

            if (!await _registeredUserRepository.HasAnyByEmailAsync(userEmail))
            {
                await _registeredUserRepository.CreateAsync(new RegisteredUserRequestDto() { Email = userEmail, SlackId = userId });
            }
        }

        public async Task<List<string>> CreatePrivateChannelsFromTechnologiesAsync()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("logged-user-role", "ROLE_ADMIN");
                client.DefaultRequestHeaders.Add("logged-user-id", "1");
                client.DefaultRequestHeaders.Add("X-API-KEY", "85dbe15d75ef9308c7ae0f33c7a324cc6f4bf519a2ed2f3027bd33c140a4f9aa");

                var technologiesResponse = await client.GetAsync("https://int-team.protal.biz/progreso/dev/dotnet-api/api/technologies");

                technologiesResponse.EnsureSuccessStatusCode();

                List<string> channelIds = new();

                var technologyNames = await ExtractTechnologyNamesFromResponseAsync(technologiesResponse);

                foreach (var item in technologyNames)
                {
                    var createdChannel = await CreatePrivateChannelAsync(item);

                    var channelInfo = createdChannel.Split(":");

                    channelIds.Add(item + ", id: " + channelInfo[1]);

                    await CreateTechnologyAsync(item);

                    await CreateChannelAsync(channelInfo[1], channelInfo[0], false);
                }

                return channelIds;
            }
        }

        private async Task<List<string>> ExtractTechnologyNamesFromResponseAsync(HttpResponseMessage httpResponseMessage)
        {
            string json = await httpResponseMessage.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<PaginatedResult<TechnologyResponseDto>>(json);

            return data.Content.Select(t => t.Name).Distinct().ToList();
        }

        private async Task CreateChannelAsync(string channelId, string channelName, bool isPublic)
        {
            if (channelId is null || channelName is null) 
                throw new ArgumentNullException();
            await _channelRepository.CreateAsync(new ChannelRequestDto() { ChannelId = channelId, Name = channelName, IsPublic = isPublic });
        }

        private async Task CreateTechnologyAsync(string name)
        {
            if(name is null) throw new ArgumentNullException();

            if(!await _technologyRepository.HasAnyByNameAsync(name))
            {
                await _technologyRepository.CreateAsync(new TechnologyRequestDto() { Name = name });
            }
        }
    }
}
