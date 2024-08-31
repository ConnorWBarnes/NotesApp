'use client';

import { useActionState } from "react";
import { Note } from "@/app/lib/note";
import { createNoteActionAsync, navigateToNotesAsync } from "@/app/lib/actions";

export default function NoteCreateForm() {
  const [saveState, saveAction] = useActionState(createNoteActionAsync, ({} as Note));

  return (
    <div className="note-compose align-items-center">
      <form className="note note-padding show-border">
        <input id="title" className="note-compose-text" placeholder="Title"/>
        <textarea id="body" className="note-compose-text" placeholder="Take a note..."></textarea>
        <div className="right">
          <button className="btn btn-dark" formAction={saveAction}>Save Changes</button>
          <button className="btn btn-dark" formAction={navigateToNotesAsync}>Cancel</button>
        </div>
      </form>
    </div>
  );
}