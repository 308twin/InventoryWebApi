﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models
{
    public class StorageListDto
    {
        //StorageList输出用DTO
        public Guid Id { get; set; }
        public string StorageNumber { get; set; }
        public DateTime StorageDate { get; set; }
        public String Note { get; set; }
    }
}
