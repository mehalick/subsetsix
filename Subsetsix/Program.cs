using System.Text;
using Cocona;
using static Subsetsix.Constants;

namespace Subsetsix;

internal abstract class Program
{
    private static void Main()
    {
        var app = CoconaApp.Create();

        var root = Note.C;

        //var scale = new int[] { 0, 2, 4, 5, 7, 9, 11 };
        var notesInScale = new[]
        {
            Note.C, Note.D, Note.E, Note.F, Note.G, Note.A, Note.B
        };

        app.AddCommand("list", () =>
        {
            var strings = new List<Note>
            {
                Note.F,
                Note.C,
                Note.G,
                Note.C
            };

            foreach (var note in strings)
            {
                WriteString(root, note, notesInScale);
            }
        });

        app.Run();
    }

    private static void WriteString(Note root, Note note, Note[] notes)
    {
        var stringNotes = new List<Note>();

        for (var i = 0; i < 20; i++)
        {
            // get note for index on string, if in scale "X" else "-"

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
