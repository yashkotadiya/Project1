﻿using System;
using System.Collections.Generic;

namespace CI_plateform.Models.Models;

public partial class MissionSkill
{
    public long MissionSkillId { get; set; }

    public int SkillId { get; set; }

    public long MissionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
