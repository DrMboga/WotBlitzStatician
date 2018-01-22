﻿namespace WotBlitzStatician.ViewModel
{
    using WotBlitzStatician.Model;

    public class TankInfoVewModel
    {
        public long TankId { get; set; }
        public long AccountId { get; set; }
        public string Name { get; set; }
        public long Tier { get; set; }
        public string RomanTier { get; set; }
        public string Nation { get; set; }
        public string Type { get; set; }
        public bool IsPremium { get; set; }
        public string PreviewImageUrl { get; set; }
        public string NormalImageUrl { get; set; }
        public MarkOfMastery MarkOfMastery { get; set; }
        public string MarkOfMasteryImageUrl { get; set; }

    }
}