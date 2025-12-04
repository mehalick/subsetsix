namespace Subsetsix;

public class Scale
{
    private readonly string _name;
    private readonly int[] _positions;
    private static readonly Dictionary<string, Scale> ScalesById = new Dictionary<string, Scale>();

    public static readonly Scale Major = new Scale("Major", [0, 2, 4, 5, 7, 9, 11]);
    public static readonly Scale Minor = new Scale("Minor", [0, 2, 3, 5, 7, 8, 10]);
    public static readonly Scale Dorian = new Scale("Dorian", [0, 2, 3, 5, 7, 9, 10]);

    // https://github.com/ruiiiijiiiiang/daily_scale/blob/main/src/scales.rs
    // Scale::Major => &[0, 2, 4, 5, 7, 9, 11],
    // Scale::HarmonicMinor => &[0, 2, 3, 5, 7, 8, 11],
    // Scale::MelodicMinor => &[0, 2, 3, 5, 7, 9, 11],
    // Scale::NaturalMinor => &[0, 2, 3, 5, 7, 8, 10],
    // Scale::PentatonicMajor => &[0, 2, 4, 7, 9],
    // Scale::PentatonicMinor => &[0, 3, 5, 7, 10],
    // Scale::PentatonicBlues => &[0, 3, 5, 6, 7, 10],
    // Scale::PentatonicNeutral => &[0, 2, 5, 7, 10],
    // Scale::WholeDiminished => &[0, 2, 3, 5, 6, 8, 9, 11],
    // Scale::HalfDiminished => &[0, 1, 3, 4, 6, 7, 9, 10],
    // Scale::Ionian => &[0, 2, 4, 5, 7, 9, 11],
    // Scale::Dorian => &[0, 2, 3, 5, 7, 9, 10],
    // Scale::Phrygian => &[0, 1, 3, 5, 7, 8, 10],
    // Scale::Lydian => &[0, 2, 4, 6, 7, 9, 11],
    // Scale::Mixolydian => &[0, 2, 4, 5, 7, 9, 10],
    // Scale::Aeolian => &[0, 2, 3, 5, 7, 8, 10],
    // Scale::Locrian => &[0, 1, 3, 5, 6, 8, 10],

    public List<Note> GetNotes(Note root)
    {
        return _positions.Select(root.Next).ToList();
    }

    private Scale(string name, int[] positions)
    {
        var id = name.Replace(" ", "-").ToLowerInvariant();
        _name = name;
        _positions = positions;

        ScalesById.Add(id, this);
    }

    public static Scale FromId(string id)
    {
        return ScalesById.GetValueOrDefault(id.ToLowerInvariant(), Major);
    }

    public override string ToString()
    {
        return _name;
    }
}
