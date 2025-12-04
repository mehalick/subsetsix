using System.Text;
using Cocona;
using static Subsetsix.Constants;

namespace Subsetsix;

internal abstract class Program
{
    private static void Main()
    {
        var app = CoconaApp.Create();

        app.AddCommand("show", ([Argument] string root, [Argument] string scale) =>
            {
                var rootNote = Note.FromId(root);
                var noteScale = Scale.FromId(scale);
                var notesInScale = noteScale.GetNotes(rootNote);
                var notesString = string.Join(", ", notesInScale.Select(i => i.ToString()));

                var strings = new List<Note>
                {
                    Note.F,
                    Note.C,
                    Note.G,
                    Note.C
                };

                Console.WriteLine();
                Console.WriteLine($"{rootNote} {noteScale} [{notesString}]");
                Console.WriteLine();

                foreach (var stringNote in strings)
                {
                    WriteString(rootNote, stringNote, notesInScale);
                }

                Console.WriteLine();
            })
            .WithDescription("Shows the fret board with scale notes.");

        app.Run();
    }

    private static void WriteString(Note root, Note note, List<Note> notes)
    {
        var stringNotes = new List<Note>();

        for (var i = 0; i < 20; i++)
        {
            stringNotes.Add(note.Next(i));
        }

        var sb = new StringBuilder();

        var isFirst = true;

        foreach (var n in stringNotes)
        {
            if (isFirst)
            {
                sb.Append($"{n.Name} ");
            }

            if (n == root)
            {
                sb.Append(DashThin, 3);
                sb.Append(CircleClosed);
                sb.Append(DashThin, 3);
            }
            else if (notes.Contains(n))
            {
                sb.Append(DashThin, 3);
                sb.Append(CircleOpen);
                sb.Append(DashThin, 3);
            }
            else
            {
                sb.Append(DashThin, 7);
            }

            if (isFirst)
            {
                sb.Append(FretHead);
                isFirst = false;
            }
            else
            {
                sb.Append(FretDivider);
            }
        }

        Console.WriteLine(sb.ToString());
    }
}
