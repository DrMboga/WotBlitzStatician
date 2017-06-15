namespace WotBlitzStatician.WotApiClient.InternalModel
{
	using Newtonsoft.Json;

	internal class WotEncyclopediaVehiclesDefaultProfile
	{
		///<summary>
		///Прочность
		///</summary>
		[JsonProperty("hp")]
		public long? Hp { get; set; }

		///<summary>
		///Прочность корпуса
		///</summary>
		[JsonProperty("hull_hp")]
		public long? HullHp { get; set; }

		///<summary>
		///Масса корпуса (кг)
		///</summary>
		[JsonProperty("hull_weight")]
		public long? HullWeight { get; set; }

		///<summary>
		///Боекомплект
		///</summary>
		[JsonProperty("max_ammo")]
		public long? MaxAmmo { get; set; }

		///<summary>
		///Предельная масса (кг)
		///</summary>
		[JsonProperty("max_weight")]
		public long? MaxWeight { get; set; }

		///<summary>
		///Макс. скорость заднего хода (км/ч)
		///</summary>
		[JsonProperty("speed_backward")]
		public long? SpeedBackward { get; set; }

		///<summary>
		///Максимальная скорость
		///</summary>
		[JsonProperty("speed_forward")]
		public long? SpeedForward { get; set; }

		///<summary>
		///Масса (кг)
		///</summary>
		[JsonProperty("weight")]
		public long? Weight { get; set; }

		///<summary>
		///Харатеристики снарядов орудия
		///</summary>
		[JsonProperty("ammo")]
		public WotEncyclopediaVehiclesDefaultProfileAmmo Ammo { get; set; }

		///<summary>
		///Бронирование
		///</summary>
		[JsonProperty("armor")]
		public WotEncyclopediaVehiclesDefaultProfileArmor Armor { get; set; }

		///<summary>
		///Характеристики двигателя
		///</summary>
		[JsonProperty("engine")]
		public WotEncyclopediaVehiclesDefaultProfileEngine Engine { get; set; }

		///<summary>
		///Характеристики орудия
		///</summary>
		[JsonProperty("gun")]
		public WotEncyclopediaVehiclesDefaultProfileGun Gun { get; set; }

		///<summary>
		///Установленные модули
		///</summary>
		[JsonProperty("modules")]
		public WotEncyclopediaVehiclesDefaultProfileModules Modules { get; set; }

		///<summary>
		///Характеристики радиостанции
		///</summary>
		[JsonProperty("radio")]
		public WotEncyclopediaVehiclesDefaultProfileRadio Radio { get; set; }

		///<summary>
		///Характеристики ходовой
		///</summary>
		[JsonProperty("suspension")]
		public WotEncyclopediaVehiclesDefaultProfileSuspension Suspension { get; set; }

		///<summary>
		///Характеристики башни
		///</summary>
		[JsonProperty("turret")]
		public WotEncyclopediaVehiclesDefaultProfileTurret Turret { get; set; }
	}

	internal class WotEncyclopediaVehiclesDefaultProfileAmmo
	{

		///<summary>
		///Урон (hp), в виде списка значений: min, avg, max
		///</summary>
		[JsonProperty("damage")]
		public int[] Damage { get; set; }

		///<summary>
		///Пробитие (мм), в виде списка значений: min, avg, max
		///</summary>
		[JsonProperty("penetration")]
		public int[] Penetration { get; set; }

		///<summary>
		///Тип снаряда
		///</summary>
		[JsonProperty("type")]
		public string Type { get; set; }
	}

	internal class WotEncyclopediaVehiclesDefaultProfileArmor
	{

		///<summary>
		///Бронирование корпуса
		///</summary>
		[JsonProperty("hull")]
		public WotEncyclopediaVehiclesDefaultProfileArmorHull Hull { get; set; }

		///<summary>
		///Бронирование башни
		///</summary>
		[JsonProperty("turret")]
		public WotEncyclopediaVehiclesDefaultProfileArmorTurret Turret { get; set; }
	}
	internal class WotEncyclopediaVehiclesDefaultProfileArmorHull
	{

		///<summary>
		///Лоб (мм)
		///</summary>
		[JsonProperty("front")]
		public long? Front { get; set; }

		///<summary>
		///Корма (мм)
		///</summary>
		[JsonProperty("rear")]
		public long? Rear { get; set; }

		///<summary>
		///Борт (мм)
		///</summary>
		[JsonProperty("sides")]
		public long? Sides { get; set; }
	}

	internal class WotEncyclopediaVehiclesDefaultProfileArmorTurret
	{

		///<summary>
		///Лоб (мм)
		///</summary>
		[JsonProperty("front")]
		public long? Front { get; set; }

		///<summary>
		///Корма (мм)
		///</summary>
		[JsonProperty("rear")]
		public long? Rear { get; set; }

		///<summary>
		///Борт (мм)
		///</summary>
		[JsonProperty("sides")]
		public long? Sides { get; set; }
	}


	internal class WotEncyclopediaVehiclesDefaultProfileEngine
	{

		///<summary>
		///Вероятность возгорания
		///</summary>
		[JsonProperty("fire_chance")]
		public double FireChance { get; set; }

		///<summary>
		///Название модуля
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Мощность двигателя (л.с.)
		///</summary>
		[JsonProperty("power")]
		public long? Power { get; set; }

		///<summary>
		///Тег модуля
		///</summary>
		[JsonProperty("tag")]
		public string Tag { get; set; }

		///<summary>
		///Уровень
		///</summary>
		[JsonProperty("tier")]
		public long? Tier { get; set; }

		///<summary>
		///Масса (кг)
		///</summary>
		[JsonProperty("weight")]
		public long? Weight { get; set; }
	}

	internal class WotEncyclopediaVehiclesDefaultProfileGun
	{

		///<summary>
		///Время сведения (с)
		///</summary>
		[JsonProperty("aim_time")]
		public double AimTime { get; set; }

		///<summary>
		///Калибр (мм)
		///</summary>
		[JsonProperty("caliber")]
		public long? Caliber { get; set; }

		///<summary>
		///Разброс на 100 м (м)
		///</summary>
		[JsonProperty("dispersion")]
		public double Dispersion { get; set; }

		///<summary>
		///Скорострельность (выстр/мин)
		///</summary>
		[JsonProperty("fire_rate")]
		public double FireRate { get; set; }

		///<summary>
		///Угол ВН вниз (град)
		///</summary>
		[JsonProperty("move_down_arc")]
		public long? MoveDownArc { get; set; }

		///<summary>
		///Угол ВН вверх (град)
		///</summary>
		[JsonProperty("move_up_arc")]
		public long? MoveUpArc { get; set; }

		///<summary>
		///Название модуля
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Время перезарядки (с)
		///</summary>
		[JsonProperty("reload_time")]
		public double ReloadTime { get; set; }

		///<summary>
		///Тег модуля
		///</summary>
		[JsonProperty("tag")]
		public string Tag { get; set; }

		///<summary>
		///Уровень
		///</summary>
		[JsonProperty("tier")]
		public long? Tier { get; set; }

		///<summary>
		///Скорость поворота (град/с)
		///</summary>
		[JsonProperty("traverse_speed")]
		public long? TraverseSpeed { get; set; }

		///<summary>
		///Масса (кг)
		///</summary>
		[JsonProperty("weight")]
		public long? Weight { get; set; }
	}

	internal class WotEncyclopediaVehiclesDefaultProfileModules
	{

		///<summary>
		///Идентификатор двигателя
		///</summary>
		[JsonProperty("engine_id")]
		public long? EngineId { get; set; }

		///<summary>
		///Идентификатор орудия
		///</summary>
		[JsonProperty("gun_id")]
		public long? GunId { get; set; }

		///<summary>
		///Идентификатор радиостанции
		///</summary>
		[JsonProperty("radio_id")]
		public long? RadioId { get; set; }

		///<summary>
		///Идентификатор ходовой
		///</summary>
		[JsonProperty("suspension_id")]
		public long? SuspensionId { get; set; }

		///<summary>
		///Идентификатор башни
		///</summary>
		[JsonProperty("turret_id")]
		public long? TurretId { get; set; }
	}

	internal class WotEncyclopediaVehiclesDefaultProfileRadio
	{

		///<summary>
		///Название модуля
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Дальность связи
		///</summary>
		[JsonProperty("signal_range")]
		public long? SignalRange { get; set; }

		///<summary>
		///Тег модуля
		///</summary>
		[JsonProperty("tag")]
		public string Tag { get; set; }

		///<summary>
		///Уровень
		///</summary>
		[JsonProperty("tier")]
		public long? Tier { get; set; }

		///<summary>
		///Масса (кг)
		///</summary>
		[JsonProperty("weight")]
		public long? Weight { get; set; }
	}

	internal class WotEncyclopediaVehiclesDefaultProfileSuspension
	{

		///<summary>
		///Максимальная нагрузка (кг)
		///</summary>
		[JsonProperty("load_limit")]
		public long? LoadLimit { get; set; }

		///<summary>
		///Название модуля
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Тег модуля
		///</summary>
		[JsonProperty("tag")]
		public string Tag { get; set; }

		///<summary>
		///Уровень
		///</summary>
		[JsonProperty("tier")]
		public long? Tier { get; set; }

		///<summary>
		///Скорость поворота (град/с)
		///</summary>
		[JsonProperty("traverse_speed")]
		public long? TraverseSpeed { get; set; }

		///<summary>
		///Масса (кг)
		///</summary>
		[JsonProperty("weight")]
		public long? Weight { get; set; }
	}

	internal class WotEncyclopediaVehiclesDefaultProfileTurret
	{

		///<summary>
		///Прочность
		///</summary>
		[JsonProperty("hp")]
		public long? Hp { get; set; }

		///<summary>
		///Название модуля
		///</summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		///<summary>
		///Тег модуля
		///</summary>
		[JsonProperty("tag")]
		public string Tag { get; set; }

		///<summary>
		///Уровень
		///</summary>
		[JsonProperty("tier")]
		public long? Tier { get; set; }

		///<summary>
		///Угол вращения влево (град)
		///</summary>
		[JsonProperty("traverse_left_arc")]
		public long? TraverseLeftArc { get; set; }

		///<summary>
		///Угол вращения вправо (град)
		///</summary>
		[JsonProperty("traverse_right_arc")]
		public long? TraverseRightArc { get; set; }

		///<summary>
		///Скорость поворота (град/с)
		///</summary>
		[JsonProperty("traverse_speed")]
		public long? TraverseSpeed { get; set; }

		///<summary>
		///Обзор (м)
		///</summary>
		[JsonProperty("view_range")]
		public long? ViewRange { get; set; }

		///<summary>
		///Масса (кг)
		///</summary>
		[JsonProperty("weight")]
		public long? Weight { get; set; }
	}
}