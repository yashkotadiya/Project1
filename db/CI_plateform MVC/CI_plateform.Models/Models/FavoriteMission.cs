﻿using System;
using System.Collections.Generic;

namespace CI_plateform.Models.Models;

public partial class FavoriteMission
{
    public long FavoriteMissionId { get; set; }

    public long UserId { get; set; }

    public long MissionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}