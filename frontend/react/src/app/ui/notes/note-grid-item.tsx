import Link from "next/link";
import { Note } from "@/app/lib/note";
import styles from "@/app/ui/notes/notes.module.scss";

export default function NoteGridItem({ note }: { note: Note }) {
  return (
    <div className={`${styles.note_grid_item}`}>
      <Link href={`/notes/${note.id}`} className={`card show-border ${styles.note}`} style={{ color: "inherit", textDecoration: "none" }}>
        {/* TODO: Display any attached images here: <img src="..." className="card-img-top" alt="..."> */}
        <div className="card-body">
          <h2 className={`card-title ${styles.note_title}`}>{note.title}</h2>
          <p className={`card-text ${styles.note_body}`}>{note.body}</p>
          {/* TODO: List links here */}
        </div>
      </Link>
    </div>
  );
}
