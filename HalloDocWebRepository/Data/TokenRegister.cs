using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDocWebRepository.Data;

[Table("TokenRegister")]
public partial class TokenRegister
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("requestid")]
    public int? Requestid { get; set; }

    [Column("isVerified", TypeName = "bit(1)")]
    public BitArray? IsVerified { get; set; }

    [Column("isDeleted", TypeName = "bit(1)")]
    public BitArray? IsDeleted { get; set; }

    [Column("tokenValue")]
    [StringLength(8)]
    public string TokenValue { get; set; } = null!;

    [Column("email")]
    public string? Email { get; set; }
}
