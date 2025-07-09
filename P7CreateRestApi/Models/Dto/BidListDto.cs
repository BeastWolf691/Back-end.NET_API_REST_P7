﻿using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto
{
    public class BidListDto
    {
        public int BidListId { get; set; }

        public string Account { get; set; } = string.Empty;

        public string BidType { get; set; } = string.Empty;
        public double? BidQuantity { get; set; }
    }
}
