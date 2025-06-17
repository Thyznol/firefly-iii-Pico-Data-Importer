using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class Note
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint NoteableId { get; set; }

    public string NoteableType { get; set; } = null!;

    public string? Title { get; set; }

    public string? Text { get; set; }
}
