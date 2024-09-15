import { Note } from "@/types/note";
import NoteGridItem from "@/components/notes/note-grid-item";
import styles from "@/components/notes/notes.module.scss";

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
