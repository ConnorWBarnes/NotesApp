import { Note } from "@/app/lib/note";
import NoteGridItem from "@/app/ui/notes/note-grid-item";

export default function NoteGrid({ notes }: { notes: Note[] }) {
  return (
    <div className={'container-fluid gx-0 results'}>
      <div className={'row'}>
        {notes.map((note) => (
          <NoteGridItem key={note.id} note={note} />
        ))}
      </div>
    </div>
  );
}
