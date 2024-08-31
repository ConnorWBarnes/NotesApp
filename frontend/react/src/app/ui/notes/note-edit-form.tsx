import { useActionState } from "react";
import { archiveNoteActionAsync, updateNoteActionAsync } from "@/app/lib/actions";
import { Note } from "@/app/lib/note";

export default function NoteEditForm({ note }: { note: Note }) {
  const [, saveAction] = useActionState(updateNoteActionAsync.bind(note), note);
  const [, archiveAction] = useActionState(archiveNoteActionAsync.bind(note), note);

  function getArchiveIconClass(): string {
    return `bi bi-file-earmark-arrow-${note.isArchived ? 'up' : 'down'}`;
  }

  return (
    <div className="note-compose align-items-center">
      <form className="note note-padding show-border">
        <input id="title" className="note-compose-text" placeholder="Title"/>
        <textarea id="body" className="note-compose-text" placeholder="Take a note..."></textarea>
        {/* TODO: Add last modified date (and show created date as tooltip) above bottom bar */}
        <div className="container-fluid">
          <div className="row flex-grow-1">
            <div className="col p-0">
              {/* TODO: Add tooltip to archive button */}
              <button className="btn btn-dark" formAction={archiveAction}>
                <i className={getArchiveIconClass()} style={{fontSize: "1rem"}}></i>
              </button>
            </div>
            <div className="col-md-auto p-0 right">
              <button className="btn btn-dark" formAction={saveAction}>Save Changes</button>
              <button className="btn btn-dark">Cancel</button>
            </div>
          </div>
        </div>
      </form>
    </div>
  );
}
