﻿namespace WotBlitzStatician.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Achievement
    {
        [Key]
        public string AchievementId { get; set; }

        public string Description { get; set; }
        public string Condition { get; set; }
        public string Image { get; set; }
        public string ImageBig { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public long Order { get; set; }
        [NotMapped]
        public List<AchievementOption> Options { get; set; }
    }
}