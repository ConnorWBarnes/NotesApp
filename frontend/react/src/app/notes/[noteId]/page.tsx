import { getNoteAsync } from "@/app/lib/note-service";
import NoteEditForm from "@/app/ui/notes/note-edit-form";

export default async function Page({ params }: { params: { noteId: string } }) {
  let note = await getNoteAsync(params.noteId);
  return <NoteEditForm note={note}/>;
}
