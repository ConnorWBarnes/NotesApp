import { Note } from "@/app/lib/note";
import NoteGridItem from "@/app/ui/notes/note-grid-item";
import styles from "@/app/ui/notes/notes.module.scss";

export default function NoteGrid({ notes }: { notes: Note[] }) {
  return (
    <div>
      <div className={`container-fluid gx-0 ${styles.note_grid}`}>
        <div className={`row`}>
          {notes.map((note) => (
            <NoteGridItem key={note.id} note={note} />
          ))}
        </div>
      </div>
    </div>
  );
}
