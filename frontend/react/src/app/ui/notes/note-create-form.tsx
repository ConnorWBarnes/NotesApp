'use client';

import { useActionState } from "react";
import { Note } from "@/app/lib/note";
import { createNoteActionAsync, navigateToNotesAsync } from "@/app/lib/actions";
import styles from "./notes.module.scss";

export default function NoteCreateForm() {
  const [saveState, saveAction] = useActionState(createNoteActionAsync, ({} as Note));

  return (
    <div className={`${styles.note_compose} align-items-center`}>
      <form className={`${styles.note} ${styles.note_padding} show-border`}>
        <input id="title" className={styles.note_compose_text} placeholder="Title"/>
        <textarea id="body" className={styles.note_compose_text} placeholder="Take a note..."></textarea>
        <div className="right">
          <button className="btn btn-dark" formAction={saveAction}>Save Changes</button>
          <button className="btn btn-dark" formAction={navigateToNotesAsync}>Cancel</button>
        </div>
      </form>
    </div>
  );
}