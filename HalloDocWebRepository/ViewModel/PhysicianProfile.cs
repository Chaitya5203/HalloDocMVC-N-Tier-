﻿using HalloDocWebRepository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocWebRepository.ViewModel
{
    public class PhysicianProfile
    {
        public List<Physicianregion> physicianregion { get; set; }
        public List<Region> regions { get; set; }
        public List<int>? SelectedReg { get; set; }
        public List<Physician>? physician { get; set; }
        public string? Firstname { get; set; } = null!;
        public string? Lastname { get; set; }
        public string? Email { get; set; } = null!;
        public string? Mobile { get; set; }
        public string? Medicallicense { get; set; }
        public string? Photo { get; set; }
        public string? Adminnotes { get; set; }
        public BitArray? Isagreementdoc { get; set; }
        public BitArray? Isbackgrounddoc { get; set; }
        public BitArray? Istrainingdoc { get; set; }
        public BitArray? Isnondisclosuredoc { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public int? Regionid { get; set; }
        public int PhysicianId { get; set; }
        public string? Zip { get; set; }
        public string? Altphone { get; set; }
        public string? Createdby { get; set; } = null!;
        public DateTime Createddate { get; set; }
        public string? Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }
        public short? Status { get; set; }
        public string Businessname { get; set; } = null!;
        public string Businesswebsite { get; set; } = null!;
        public BitArray? Isdeleted { get; set; }
        public string? Npinumber { get; set; }
        public string? Signature { get; set; }
        public string? Syncemailaddress { get; set; }
    }
}