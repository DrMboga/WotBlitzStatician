﻿namespace WotBlitzStatician.WotApiClient.InternalModel
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class WotClanInfoResponse
    {
		///<summary>
		///Клан может приглашать игроков
		///</summary>
		[JsonProperty("accepts_join_requests")]
		public bool AcceptsJoinRequests { get; set; }

		///<summary>
		///Идентификатор клана
		///</summary>
		[JsonProperty("clan_id")]
		public long? ClanId { get; set; }

		///<summary>
		///Цвет клана в формате HEX #RRGGBB
		///</summary>
		[JsonProperty("color")]
		public string Color { get; set; }

        [JsonProperty("created_at")]
        private int? _createdAt { get; set; }
		///<summary>
		///Дата создания клана
		///</summary>
		public DateTime? CreatedAt => _createdAt.ToDateTime();

		///<summary>
		///Идентификатор игрока, создавшего клан
		///</summary>
		[JsonProperty("creator_id")]
		public long? CreatorId { get; set; }

		///<summary>
		///Имя игрока, создавшего клан
		///</summary>
		[JsonProperty("creator_name")]
		public string CreatorName { get; set; }

		///<summary>
		///Описание клана
		///</summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		///<summary>
		///Описание клана в HTML
		///</summary>
		[JsonProperty("description_html")]
		public string DescriptionHtml { get; set; }

		///<summary>
		///Клан удалён. Информация об удалённом клане содержит актуальные значения только для следующих полей: clan_id, is_clan_disbanded, updated_at.
		///</summary>
		[JsonProperty("is_clan_disbanded")]
		public bool IsClanDisbanded { get; set; }

		///<summary>
		///Идентификатор Командующего клана
		///</summary>
		[JsonProperty("leader_id")]
		public long? LeaderId { get; set; }

		///<summary>
		///Имя Командующего
		///</summary>
		[JsonProperty("leader_name")]
		public string LeaderName { get; set; }

		///<summary>
		///Количество игроков клана
		///</summary>
		[JsonProperty("members_count")]
		public long? MembersCount { get; set; }

		///<summary>
		///Девиз клана
		///</summary>
		[JsonProperty("motto")]
		public string Motto { get; set; }

		///<summary>
		///Название клана
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Старое название клана
		///</summary>
		[JsonProperty("old_name")]
		public string OldName { get; set; }

		///<summary>
		///Старый тег клана
		///</summary>
		[JsonProperty("old_tag")]
		public string OldTag { get; set; }

		///<summary>
		///Время последнего переименования клана в UTC
		///</summary>
		[JsonProperty("renamed_at")]
		public int? RenamedAt { get; set; }

		///<summary>
		///Тег клана
		///</summary>
		[JsonProperty("tag")]
		public string Tag { get; set; }

		///<summary>
		///Время обновления информации о клане
		///</summary>
		[JsonProperty("updated_at")]
		public int? UpdatedAt { get; set; }

		///<summary>
		///Информация об эмблемах клана в играх и на клановом портале
		///</summary>
		[JsonProperty("emblems")]
		public WgnClansInfoEmblems Emblems { get; set; }

		///<summary>
		///Информация об игроках клана. Формат поля зависит от входящего параметра members_key.
		///</summary>
		[JsonProperty("members")]
		public WgnClansInfoMembers Members { get; set; }

		///<summary>
		///Cекретная информация клана
		///</summary>
		[JsonProperty("private")]
		public WgnClansInfoPrivate Private { get; set; }
	}
    internal class WgnClansInfoEmblems
	{

		///<summary>
		///Перечень ссылок на эмблемы 195x195 px
		///</summary>
		[JsonProperty("x195")]
		public Dictionary<string, string> X195 { get; set; }

		///<summary>
		///Перечень ссылок на эмблемы 24x24 px
		///</summary>
		[JsonProperty("x24")]
		public Dictionary<string, string> X24 { get; set; }

		///<summary>
		///Перечень ссылок на эмблемы 256x256 px
		///</summary>
		[JsonProperty("x256")]
		public Dictionary<string, string> X256 { get; set; }

		///<summary>
		///Перечень ссылок на эмблемы 32x32 px
		///</summary>
		[JsonProperty("x32")]
		public Dictionary<string, string> X32 { get; set; }

		///<summary>
		///Перечень ссылок на эмблемы 64x64 px
		///</summary>
		[JsonProperty("x64")]
		public Dictionary<string, string> X64 { get; set; }
	}

    internal class WgnClansInfoMembers
	{

		///<summary>
		///Идентификатор аккаунта игрока
		///</summary>
		[JsonProperty("account_id")]
		public long? AccountId { get; set; }

		///<summary>
		///Имя игрока
		///</summary>
		[JsonProperty("account_name")]
		public string AccountName { get; set; }

		///<summary>
		///Дата вступления в клан
		///</summary>
		[JsonProperty("joined_at")]
		public int? JoinedAt { get; set; }

		///<summary>
		///Техническое название должности
		///</summary>
		[JsonProperty("role")]
		public string Role { get; set; }

		///<summary>
		///Позиция
		///</summary>
		[JsonProperty("role_i18n")]
		public string RoleI18n { get; set; }
	}

    internal class WgnClansInfoPrivate
	{

		///<summary>
		///Список игроков клана c активной игровой сессией в World of Tanks.
		///Дополнительное поле.
		///</summary>
		[JsonProperty("online_members")]
		public int[] OnlineMembers { get; set; }

		///<summary>
		///Золото в казне клана
		///</summary>
		[JsonProperty("treasury")]
		public long? Treasury { get; set; }
	}
}
