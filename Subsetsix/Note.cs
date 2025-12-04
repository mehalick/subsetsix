namespace Subsetsix;

public class Note
{
    public string Name { get; }

    private static readonly Dictionary<string, Note> NotesById = new Dictionary<string, Note>();
    private static readonly List<Note> NoteList = [];

    public static readonly Note A = new Note("A");
    public static readonly Note ASharp = new Note("A#");
    public static readonly Note B = new Note("B");
    public static readonly Note C = new Note("C");
    public static readonly Note CSharp = new Note("C#");
    public static readonly Note D = new Note("D");
    public static readonly Note DSharp = new Note("D#");
    public static readonly Note E = new Note("E");
    public static readonly Note F = new Note("F");
    public static readonly Note FSharp = new Note("F#");
    public static readonly Note G = new Note("G");
    public static readonly Note GSharp = new Note("G#");

    private Note(string name)
    {
        var id = name.ToLowerInvariant();
        Name = name;

        NotesById.Add(id, this);
        NoteList.Add(this);
    }

    public Note Next(int i)
    {
        var index = NoteList.IndexOf(this);
        var newIndex = index + i;

        while (newIndex >= NoteList.Count)
        {
            newIndex -= NoteList.Count;
        }

        return NoteList[newIndex];
    }

    public static Note FromId(string id)
    {
        return NotesById.GetValueOrDefault(id.ToLowerInvariant(), C);
    }

    public override string ToString()
    {
        return Name;
    }
}
