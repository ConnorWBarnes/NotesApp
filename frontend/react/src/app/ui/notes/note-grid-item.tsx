import Link from "next/link";
import { Note } from "@/app/lib/note";
import "@/app/ui/notes/_note.scss";

export default function NoteGridItem({ note }: { note: Note }) {
  return (
    <div className="card note show-border">
      <Link href={`/Notes/${note.id}`}>
        {/*TODO: Display any attached images here: <img src="..." className="card-img-top" alt="...">*/}
        <div className="card-body">
          <h2 className="card-title note-title">{note.title}</h2>
          <p className="card-text note-body">{note.body}</p>
          {/*TODO: List links here*/}
        </div>
      </Link>
    </div>
  );
}
