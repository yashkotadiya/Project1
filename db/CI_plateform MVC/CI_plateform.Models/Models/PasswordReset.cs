﻿using System;
using System.Collections.Generic;

namespace CI_plateform.Models.Models;

public partial class PasswordReset
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
