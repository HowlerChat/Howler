using System.Collections.Generic;
using System.Linq;
using Howler.Database.Core.Models;

namespace Howler.Database.Core.Tests
{
    public class MockCoreDatabaseContext : ICoreDatabaseContext
    {
        public MockCoreDatabaseContext()
        {
            this.Spaces = new List<Space>().AsQueryable();
            this.Channels = new List<Channel>().AsQueryable();
            this.ChannelMembers = new List<ChannelMember>().AsQueryable();
            this.ChannelMemberStates = new List<ChannelMemberState>().AsQueryable();
            this.Messages = new List<Message>().AsQueryable();
            this.MessageIndex = new List<MessageIndex>().AsQueryable();
            this.SpaceBans = new List<SpaceBan>().AsQueryable();
            this.SpaceHistory = new List<SpaceHistory>().AsQueryable();
            this.SpaceMembers = new List<SpaceMember>().AsQueryable();
            this.Attachments = new List<Attachment>().AsQueryable();
        }

        public IQueryable<Space> Spaces { get; private set; }

        public IQueryable<Channel> Channels { get; private set; }

        public IQueryable<ChannelMember> ChannelMembers { get; private set; }

        public IQueryable<ChannelMemberState> ChannelMemberStates { get; private set; }

        public IQueryable<Message> Messages { get; private set; }

        public IQueryable<MessageIndex> MessageIndex { get; private set; }

        public IQueryable<SpaceBan> SpaceBans { get; private set; }

        public IQueryable<SpaceHistory> SpaceHistory { get; private set; }

        public IQueryable<SpaceMember> SpaceMembers { get; private set; }

        public IQueryable<Attachment> Attachments { get; private set; }
    }
}
