using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDocWebRepository.Data;

[Table("physiciannotification")]
public partial class Physiciannotification
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("pysicianid")]
    public int Pysicianid { get; set; }

    [Column("isnotificationstopped", TypeName = "bit(1)")]
    public BitArray Isnotificationstopped { get; set; } = null!;
}
