using System;
using System.Collections.Generic;
using System.Linq;
using CMS.DataEngine;
using CMS.Membership;
using CMS.Tests;
using FluentAssertions;
using NUnit.Framework;
using XperienceCommunity.QueryExtensions.Objects;

namespace XperienceCommunity.QueryExtensions.Tests.Objects
{
    public class XperienceCommunityObjectJoinExtensionTests : UnitTests
    {
        [SetUp]
        public void SetUp()
        {
            Fake<DataClassInfo, DataClassInfoProvider>()
                .WithData(GetClasses().ToArray());

            static IEnumerable<DataClassInfo> GetClasses()
            {
                var userDc = DataClassInfo.New();
                userDc.ClassID = 1;
                userDc.ClassName = UserInfo.OBJECT_TYPE;
                userDc.ClassTableName = "CMS_User";

                yield return userDc;

                var userSettingsDc = DataClassInfo.New();
                userSettingsDc.ClassID = 2;
                userSettingsDc.ClassName = UserSettingsInfo.OBJECT_TYPE;
                userSettingsDc.ClassTableName = "CMS_UserSetting";

                yield return userSettingsDc;
            }
        }

        [Test]
        public void InnerJoin_Will_Include_A_Table_Alias_For_The_Joined_Table()
        {
            var query = new ObjectQuery<UserInfo>()
                .Columns(nameof(UserInfo.UserID))
                .WhereEquals(nameof(UserInfo.UserID), 1)
                .Source(s => s.InnerJoin<UserSettingsInfo>(
                    "UserID",
                    "UserSettingsUserID",
                    "XYZ",
                    new WhereCondition("XYZ.UserWaitingForApproval = 1"),
                    hints: new[] { SqlHints.NOLOCK }));

            var lines = query.GetFullQueryText()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(l => !string.IsNullOrWhiteSpace(l));

            string[] expected = new[]
            {
                "DECLARE @UserID int = 1;",
                "SELECT [UserID]",
                "FROM CMS_User INNER JOIN CMS_UserSetting AS XYZ WITH (NOLOCK) ON [CMS_User].[UserID] = [XYZ].[UserSettingsUserID] AND XYZ.UserWaitingForApproval = 1",
                "WHERE [UserID] = @UserID"
            };

            lines.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Join_Will_Include_A_Table_Alias_For_The_Joined_Table()
        {
            var query = new ObjectQuery<UserInfo>()
                .Columns(nameof(UserInfo.UserID))
                .WhereEquals(nameof(UserInfo.UserID), 1)
                .Source(s => s.Join<UserSettingsInfo>(
                    "UserID",
                    "UserSettingsUserID",
                    "XYZ",
                    JoinTypeEnum.LeftOuter,
                    new WhereCondition("XYZ.UserWaitingForApproval = 1"),
                    hints: new[] { SqlHints.NOLOCK }));

            var lines = query.GetFullQueryText()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(l => !string.IsNullOrWhiteSpace(l));

            string[] expected = new[]
            {
                "DECLARE @UserID int = 1;",
                "SELECT [UserID]",
                "FROM CMS_User LEFT OUTER JOIN CMS_UserSetting AS XYZ WITH (NOLOCK) ON [CMS_User].[UserID] = [XYZ].[UserSettingsUserID] AND XYZ.UserWaitingForApproval = 1",
                "WHERE [UserID] = @UserID"
            };

            lines.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LeftJoin_Will_Include_A_Table_Alias_For_The_Joined_Table()
        {
            var query = new ObjectQuery<UserInfo>()
                .Columns(nameof(UserInfo.UserID))
                .WhereEquals(nameof(UserInfo.UserID), 1)
                .Source(s => s.LeftJoin<UserSettingsInfo>(
                    "UserID",
                    "UserSettingsUserID",
                    "XYZ",
                    new WhereCondition("XYZ.UserWaitingForApproval = 1"),
                    hints: new[] { SqlHints.NOLOCK }));

            var lines = query.GetFullQueryText()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(l => !string.IsNullOrWhiteSpace(l));

            string[] expected = new[]
            {
                "DECLARE @UserID int = 1;",
                "SELECT [UserID]",
                "FROM CMS_User LEFT OUTER JOIN CMS_UserSetting AS XYZ WITH (NOLOCK) ON [CMS_User].[UserID] = [XYZ].[UserSettingsUserID] AND XYZ.UserWaitingForApproval = 1",
                "WHERE [UserID] = @UserID"
            };

            lines.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void RightJoin_Will_Include_A_Table_Alias_For_The_Joined_Table()
        {
            var query = new ObjectQuery<UserInfo>()
                .Columns(nameof(UserInfo.UserID))
                .WhereEquals(nameof(UserInfo.UserID), 1)
                .Source(s => s.RightJoin<UserSettingsInfo>(
                    "UserID",
                    "UserSettingsUserID",
                    "XYZ",
                    new WhereCondition("XYZ.UserWaitingForApproval = 1"),
                    hints: new[] { SqlHints.NOLOCK }));

            var lines = query.GetFullQueryText()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(l => !string.IsNullOrWhiteSpace(l));

            string[] expected = new[]
            {
                "DECLARE @UserID int = 1;",
                "SELECT [UserID]",
                "FROM CMS_User RIGHT OUTER JOIN CMS_UserSetting AS XYZ WITH (NOLOCK) ON [CMS_User].[UserID] = [XYZ].[UserSettingsUserID] AND XYZ.UserWaitingForApproval = 1",
                "WHERE [UserID] = @UserID"
            };

            lines.Should().BeEquivalentTo(expected);
        }
    }
}
